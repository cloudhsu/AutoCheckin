namespace AutoCheckIn
{
    internal sealed class CheckResult
    {
        private CheckResult(bool succeeded, string message)
        {
            Succeeded = succeeded;
            Message = message;
        }

        public bool Succeeded { get; }
        public string Message { get; }

        public static CheckResult Success(string message) => new CheckResult(true, message);
        public static CheckResult Failure(string message) => new CheckResult(false, message);
    }
}
