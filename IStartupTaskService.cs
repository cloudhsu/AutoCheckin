using System.Threading.Tasks;

namespace AutoCheckIn
{
    internal interface IStartupTaskService
    {
        Task<TaskScheduleResult> GetStatusAsync();
        Task<TaskScheduleResult> CreateAsync();
        Task<TaskScheduleResult> DeleteAsync();
    }
}
