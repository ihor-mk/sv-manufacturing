using SunVita.Core.Common.DTO.Live;

namespace SunVita.Worker.WebApi.Interfaces
{
    public interface ILiveViewCountsUpdateService
    {
        Task<LiveViewCountsDto> GetUpdateFromPrinter(int lineId);
        Task SendNewCountsToCore(ICollection<LiveViewCountsDto> updatesCounts);
        LiveViewCountsDto SetCountsForNewNomenclature(LiveViewCountsDto currentCounts, LiveViewCountsDto newCounts);
        LiveViewCountsDto CalculateCounts(LiveViewCountsDto currentCounts, LiveViewCountsDto newCounts);
    }
}
