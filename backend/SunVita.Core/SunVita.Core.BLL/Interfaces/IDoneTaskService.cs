using SunVita.Core.Common.DTO.DoneTask;

namespace SunVita.Core.BLL.Interfaces
{
    public interface IDoneTaskService
    {
        Task<DoneTaskFileDto> CreateDoneTask(DoneTaskFileDto file);
    }
}
