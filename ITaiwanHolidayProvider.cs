using System;
using System.Threading;
using System.Threading.Tasks;

namespace AutoCheckIn
{
    /// <summary>
    /// Supplies one calendar year's Taiwan government holiday data. Implementations
    /// may cache, download, or fake this for tests; DailyCheck only depends on this
    /// narrow contract, never on the concrete HTTP/filesystem details.
    /// </summary>
    internal interface ITaiwanHolidayProvider : IDisposable
    {
        Task<HolidayCalendarResult> GetCalendarAsync(
            int year, CancellationToken cancellationToken = default(CancellationToken));
    }
}
