using SunVita.Core.Common.DTO.Filters;
using SunVita.Core.Common.DTO.History;

namespace SunVita.Core.BLL.Interfaces
{
    public interface IHistoryService
    {
        Task<int> GetDoneTaskCount(int month);
        Task<ICollection<DoneTaskDto>> GetDoneTasks(MainFilter filter);
    }
}
