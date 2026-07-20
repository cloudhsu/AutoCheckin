using System;

namespace AutoCheckIn
{
    internal interface IAutoCheckScheduler : IDisposable
    {
        string StatusMessage { get; }
        string CheckInMessage { get; }
        string CheckOutMessage { get; }

        void SetWorkingDay(DayOfWeek day, bool isWorking);
        void Start(string loginId, string password);
        void Stop();
    }
}
