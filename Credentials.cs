namespace AutoCheckIn
{
    internal sealed class Credentials
    {
        public Credentials(string loginId, string password)
        {
            LoginId = loginId;
            Password = password;
        }

        public string LoginId { get; }
        public string Password { get; }
    }
}
