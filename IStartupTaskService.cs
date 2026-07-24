using System.Threading.Tasks;

namespace AutoCheckIn
{
    internal interface IStartupTaskService
    {
        TaskScheduleResult GetStatus();
        Task<TaskScheduleResult> CreateAsync();
        Task<TaskScheduleResult> DeleteAsync();
    }
}
