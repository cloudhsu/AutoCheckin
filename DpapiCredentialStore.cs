using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AutoCheckIn
{
    internal sealed class DpapiCredentialStore : ICredentialStore
    {
        private static readonly byte[] Entropy = Encoding.UTF8.GetBytes("AutoCheckIn.Credentials.v1");
        private readonly string m_filePath;

        public DpapiCredentialStore()
        {
            m_filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "credentials.dat");
        }

        public Credentials Load()
        {
            if (!File.Exists(m_filePath))
            {
                return null;
            }

            try
            {
                var encrypted = File.ReadAllBytes(m_filePath);
                var clear = ProtectedData.Unprotect(encrypted, Entropy, DataProtectionScope.CurrentUser);
                try
                {
                    using (var stream = new MemoryStream(clear))
                    using (var reader = new BinaryReader(stream, Encoding.UTF8))
                    {
                        var version = reader.ReadByte();
                        if (version != 1) return null;
                        return new Credentials(reader.ReadString(), reader.ReadString());
                    }
                }
                finally
                {
                    Array.Clear(clear, 0, clear.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to load saved credentials: " + ex.Message);
                return null;
            }
        }

        public void Save(string loginId, string password)
        {
            if (string.IsNullOrWhiteSpace(loginId) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Credentials cannot be empty.");
            }

            byte[] clear;
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream, Encoding.UTF8, true))
                {
                    writer.Write((byte)1);
                    writer.Write(loginId);
                    writer.Write(password);
                }
                clear = stream.ToArray();
            }

            try
            {
                var encrypted = ProtectedData.Protect(clear, Entropy, DataProtectionScope.CurrentUser);
                var temporaryPath = m_filePath + ".tmp";
                File.WriteAllBytes(temporaryPath, encrypted);
                if (File.Exists(m_filePath))
                {
                    File.Replace(temporaryPath, m_filePath, null);
                }
                else
                {
                    File.Move(temporaryPath, m_filePath);
                }
            }
            finally
            {
                Array.Clear(clear, 0, clear.Length);
            }
        }
    }
}
