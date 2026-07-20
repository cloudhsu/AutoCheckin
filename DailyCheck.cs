using System;
using System.Threading;

namespace AutoCheckIn
{
    internal class DailyCheck : IDisposable
    {
        Thread m_worker;
        ManualResetEventSlim m_stop;
        int m_targetCheckIn;
        int m_targetCheckOut;
        DateTime m_today;
        bool m_isRunning = false;
        bool m_isCheckIn = false;
        bool m_isCheckOut = false;

        bool[] m_isWorkingDay = new bool[7] { false, true, true, true, true, true, false };

        static readonly TimeSpan CheckInStart = new TimeSpan(8, 50, 0);
        static readonly TimeSpan CheckInStop = new TimeSpan(8, 59, 0);

        static readonly TimeSpan CheckOutStart = new TimeSpan(18, 1, 0);
        static readonly TimeSpan CheckOutStop = new TimeSpan(18, 9, 59);

        public string LoginID { get; set; }
        public string Password { get; set; }

        public string StatusMessage { get; set; }

        public string CheckInMessage { get; set; }
        public string CheckOutMessage { get; set; }

        CrackClient m_crack = new CrackClient();

        public DailyCheck()
        {
            
        }

        public void setDayOff(int day, bool isWorking)
        {
            m_isWorkingDay[day] = isWorking;
        }

        public void Start()
        {
            m_crack.LoginID = LoginID;
            m_crack.Password = Password;

            if(m_worker == null)
            {
                m_worker = new Thread(loop) { IsBackground = true, Name = "DailyCheck" };
            }
            if (!m_isRunning)
            {
                StatusMessage = "To start auto check.";
                initial();
                m_isRunning = true;
                m_stop = new ManualResetEventSlim(false);
                m_worker.Start();
                Console.WriteLine("Auto Check Thread start.");
            }
        }
        public void Stop()
        {
            if (m_isRunning)
            {
                StatusMessage = "Auto check finished.";
                m_isRunning = false;
                m_stop.Set();
                m_worker.Join();
                m_worker = null;
                CheckInMessage = "";
                CheckOutMessage = "";
            }
        }

        public void Dispose() => Stop();

        private void loop()
        {
            while(!m_stop.IsSet)
            {
                checkNextDay();
                autoCheck();

                if (m_stop.Wait(TimeSpan.FromSeconds(20)))
                {
                    break;
                }
            }

            Console.WriteLine("Auto Check Thread finish.");
        }

        void initial()
        {
            m_today = DateTime.Now;
            m_isCheckIn = false;
            m_isCheckOut = false;

            m_targetCheckIn = (new Random()).Next() % 9 + 50;
            m_targetCheckOut = (new Random()).Next() % 9 + 1;

            Console.WriteLine($"It will check in at 8:{m_targetCheckIn}");
            Console.WriteLine($"It will check out at 18:{m_targetCheckOut}");

            CheckInMessage = $"Auto check in at 8:{m_targetCheckIn}";
            CheckOutMessage = $"Auto check out at 18:{m_targetCheckOut}";
        }

        void checkNextDay()
        {
            var now = DateTime.Now;
            if (now.Date > m_today)
            {
                StatusMessage = "Reset in new day.";
                initial();
            }
        }

        void autoCheck()
        {
            var now = DateTime.Now;

            if (!m_isWorkingDay[(int)now.DayOfWeek])
            {
                Console.WriteLine("Today is off.");
                StatusMessage = "Today is off";
                return;
            }

            if(now.Hour == 8 && now.Minute >= m_targetCheckIn)
            {
                // Check In
                if(!m_isCheckIn)
                {
                    m_isCheckIn = true;
                    Console.WriteLine($"Check In at {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                    m_crack.checkInOut(CrackClient.CheckAction.CheckIn);
                    StatusMessage = m_crack.StatusMessage;
                }
            }

            if(now.Hour == 18 && now.Minute >= m_targetCheckOut)
            {
                // Check out
                if (!m_isCheckOut)
                {
                    m_isCheckOut= true;
                    Console.WriteLine($"Check Out at {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                    m_crack.checkInOut(CrackClient.CheckAction.CheckOut);
                    StatusMessage = m_crack.StatusMessage;
                }
            }
        }
    }
}
