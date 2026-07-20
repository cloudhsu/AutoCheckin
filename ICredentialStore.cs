namespace AutoCheckIn
{
    internal interface ICredentialStore
    {
        Credentials Load();
        void Save(string loginId, string password);
    }
}
