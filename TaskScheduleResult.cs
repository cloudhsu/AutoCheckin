namespace AutoCheckIn
{
    internal sealed class TaskScheduleResult
    {
        public TaskScheduleResult(bool succeeded, bool exists, string message)
        {
            Succeeded = succeeded;
            Exists = exists;
            Message = message;
        }

        public bool Succeeded { get; }
        public bool Exists { get; }
        public string Message { get; }
    }
}
