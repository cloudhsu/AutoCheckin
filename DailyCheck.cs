using System;
using System.Threading;
using System.Threading.Tasks;

namespace AutoCheckIn
{
    internal sealed class DailyCheck : IAutoCheckScheduler
    {
        private static readonly TimeSpan PollInterval = TimeSpan.FromSeconds(20);
        private static readonly TimeSpan FailureRetryInterval = TimeSpan.FromMinutes(5);

        private readonly IUofClient m_uofClient;
        private readonly bool[] m_isWorkingDay = { false, true, true, true, true, true, false };
        private readonly Random m_random = new Random();
        private readonly object m_stateLock = new object();

        private CancellationTokenSource m_cancellation;
        private Task m_worker;
        private DateTime m_today;
        private DateTime m_nextCheckInAttempt;
        private DateTime m_nextCheckOutAttempt;
        private int m_targetCheckIn;
        private int m_targetCheckOut;
        private bool m_isCheckIn;
        private bool m_isCheckOut;
        private string m_loginId;
        private string m_password;
        private string m_statusMessage = string.Empty;
        private string m_checkInMessage = string.Empty;
        private string m_checkOutMessage = string.Empty;
        private int m_consecutiveFailures;

        public DailyCheck(IUofClient uofClient)
        {
            m_uofClient = uofClient ?? throw new ArgumentNullException(nameof(uofClient));
        }

        public string StatusMessage { get { lock (m_stateLock) return m_statusMessage; } }
        public string CheckInMessage { get { lock (m_stateLock) return m_checkInMessage; } }
        public string CheckOutMessage { get { lock (m_stateLock) return m_checkOutMessage; } }
        public int ConsecutiveFailures { get { lock (m_stateLock) return m_consecutiveFailures; } }

        public void SetWorkingDay(DayOfWeek day, bool isWorking)
        {
            lock (m_stateLock)
            {
                m_isWorkingDay[(int)day] = isWorking;
            }
        }

        public void Start(string loginId, string password)
        {
            if (string.IsNullOrWhiteSpace(loginId)) throw new ArgumentException("Login ID is required.", nameof(loginId));
            if (string.IsNullOrEmpty(password)) throw new ArgumentException("Password is required.", nameof(password));

            lock (m_stateLock)
            {
                if (m_worker != null && !m_worker.IsCompleted)
                {
                    return;
                }

                m_loginId = loginId;
                m_password = password;
                InitializeDay(DateTime.Now);
                m_statusMessage = "Auto check started.";
                m_cancellation = new CancellationTokenSource();
                m_worker = Task.Run(() => RunAsync(m_cancellation.Token));
            }
        }

        public void Stop()
        {
            CancellationTokenSource cancellation;
            Task worker;
            lock (m_stateLock)
            {
                cancellation = m_cancellation;
                worker = m_worker;
            }

            if (cancellation == null)
            {
                return;
            }

            cancellation.Cancel();
            try
            {
                worker?.GetAwaiter().GetResult();
            }
            catch (OperationCanceledException)
            {
                // Expected during shutdown.
            }

            lock (m_stateLock)
            {
                m_statusMessage = "Auto check finished.";
                m_checkInMessage = string.Empty;
                m_checkOutMessage = string.Empty;
                m_loginId = null;
                m_password = null;
                m_worker = null;
                m_cancellation.Dispose();
                m_cancellation = null;
            }
        }

        public void Dispose()
        {
            Stop();
        }

        private async Task RunAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    await CheckScheduleAsync(DateTime.Now, cancellationToken).ConfigureAwait(false);
                }
                catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
                {
                    break;
                }
                catch (Exception ex)
                {
                    // Last line of defence: a scheduler failure must never terminate the process.
                    ErrorLogger.Log("Automatic check scheduler", ex);
                    SetStatus("Auto check error: " + ex.Message);
                }

                try
                {
                    await Task.Delay(PollInterval, cancellationToken).ConfigureAwait(false);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }
        }

        private async Task CheckScheduleAsync(DateTime now, CancellationToken cancellationToken)
        {
            CheckAction? pendingAction = null;
            string loginId;
            string password;

            lock (m_stateLock)
            {
                if (now.Date > m_today.Date)
                {
                    InitializeDay(now);
                    m_statusMessage = "Schedule reset for a new day.";
                }

                if (!m_isWorkingDay[(int)now.DayOfWeek])
                {
                    m_statusMessage = "Today is a day off.";
                    return;
                }

                if (CheckWindowPolicy.ShouldAttemptCheckIn(now, m_targetCheckIn, m_isCheckIn, m_nextCheckInAttempt))
                {
                    pendingAction = CheckAction.CheckIn;
                    m_nextCheckInAttempt = now.Add(FailureRetryInterval);
                }
                else if (CheckWindowPolicy.ShouldAttemptCheckOut(now, m_targetCheckOut, m_isCheckOut, m_nextCheckOutAttempt))
                {
                    pendingAction = CheckAction.CheckOut;
                    m_nextCheckOutAttempt = now.Add(FailureRetryInterval);
                }

                loginId = m_loginId;
                password = m_password;
            }

            if (!pendingAction.HasValue)
            {
                return;
            }

            var result = await m_uofClient.CheckAsync(
                loginId, password, pendingAction.Value, cancellationToken).ConfigureAwait(false);

            lock (m_stateLock)
            {
                if (result.Succeeded)
                {
                    if (pendingAction == CheckAction.CheckIn) m_isCheckIn = true;
                    if (pendingAction == CheckAction.CheckOut) m_isCheckOut = true;
                    m_consecutiveFailures = 0;
                }
                else
                {
                    m_consecutiveFailures++;
                }

                m_statusMessage = result.Message;
            }
        }

        private void InitializeDay(DateTime now)
        {
            m_today = now.Date;
            m_isCheckIn = false;
            m_isCheckOut = false;
            m_consecutiveFailures = 0;
            m_targetCheckIn = m_random.Next(50, 59);
            m_targetCheckOut = m_random.Next(1, 10);
            m_nextCheckInAttempt = now.Date;
            m_nextCheckOutAttempt = now.Date;
            m_checkInMessage = $"Auto check in at 8:{m_targetCheckIn:00}";
            m_checkOutMessage = $"Auto check out at 18:{m_targetCheckOut:00}";
        }

        private void SetStatus(string message)
        {
            lock (m_stateLock)
            {
                m_statusMessage = message;
            }
        }
    }
}
