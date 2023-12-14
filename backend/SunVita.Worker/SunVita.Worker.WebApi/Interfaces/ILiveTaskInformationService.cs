using SunVita.Core.Common.DTO.Live;

namespace SunVita.Worker.WebApi.Interfaces
{
    public interface ILiveTaskInformationService
    {
        List<LiveTaskDto> LiveTasks { get; set; }
    }
}
