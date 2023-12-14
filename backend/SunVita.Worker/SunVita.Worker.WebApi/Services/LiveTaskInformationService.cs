using SunVita.Core.Common.DTO.Live;
using SunVita.Worker.WebApi.Interfaces;

namespace SunVita.Worker.WebApi.Services
{
    public class LiveTaskInformationService : ILiveTaskInformationService
    {
        public List<LiveTaskDto> LiveTasks { get; set; } = new List<LiveTaskDto>();
    }
}
