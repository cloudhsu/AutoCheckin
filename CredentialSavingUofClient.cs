using System;
using System.Threading;
using System.Threading.Tasks;

namespace AutoCheckIn
{
    internal sealed class CredentialSavingUofClient : IUofClient
    {
        private readonly IUofClient m_inner;
        private readonly ICredentialStore m_store;

        public CredentialSavingUofClient(IUofClient inner, ICredentialStore store)
        {
            m_inner = inner ?? throw new ArgumentNullException(nameof(inner));
            m_store = store ?? throw new ArgumentNullException(nameof(store));
        }

        public async Task<CheckResult> CheckAsync(
            string loginId,
            string password,
            CheckAction action,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await m_inner.CheckAsync(loginId, password, action, cancellationToken)
                .ConfigureAwait(false);
            if (!result.Succeeded)
            {
                return result;
            }

            try
            {
                m_store.Save(loginId, password);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Check succeeded but credentials could not be saved: " + ex.Message);
                return CheckResult.Success(result.Message + " (Credentials could not be saved.)");
            }
        }
    }
}
