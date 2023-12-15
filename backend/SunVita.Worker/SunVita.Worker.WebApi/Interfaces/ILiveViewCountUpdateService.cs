using SunVita.Core.Common.DTO.Live;

namespace SunVita.Worker.WebApi.Interfaces
{
    public interface ILiveViewCountsUpdateService
    {
        List<LiveViewCountsDto> CurrentLineStatus { get; set; }
        Task<LiveViewCountsDto> GetUpdateFromPrinter(int lineId);
        Task SendNewCountsToCore(ICollection<LiveViewCountsDto> updatesCounts);
        LiveViewCountsDto SetCountsForNewNomenclature(LiveViewCountsDto currentCounts, LiveViewCountsDto newCounts);
        LiveViewCountsDto CalculateCounts(LiveViewCountsDto currentCounts, LiveViewCountsDto newCounts);
        void SetNewTaskCounts(LiveTaskDto liveTaskDto);
    }
}
