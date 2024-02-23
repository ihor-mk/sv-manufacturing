using SunVita.Core.Common.DTO.Live;

namespace SunVita.Worker.WebApi.Interfaces
{
    public interface ILiveViewCountsUpdateService
    {
        object Locker { get; set; }
        List<LiveViewCountsDto> CurrentLineStatus { get; set; }
        List<LiveViewCountsDto> NewLineStatus { get; set; }
        Task<LivePrinterCounts> GetUpdateFromPrinter(string ipAddress);
        Task SendNewCountsToCore(ICollection<LiveViewCountsDto> updatesCounts);
        LiveViewCountsDto SetCountsForNewNomenclature(LiveViewCountsDto currentCounts, LiveViewCountsDto newCounts);
        LiveViewCountsDto CalculateCounts(LiveViewCountsDto currentCounts, LiveViewCountsDto newCounts);
        void SetNewTaskCounts(LiveTaskDto liveTaskDto);
        void FinishTask(LiveTaskDto liveTaskDto);
    }
}
