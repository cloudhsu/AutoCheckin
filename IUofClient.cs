using System.Threading;
using System.Threading.Tasks;

namespace AutoCheckIn
{
    internal interface IUofClient
    {
        Task<CheckResult> CheckAsync(
            string loginId,
            string password,
            CheckAction action,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
