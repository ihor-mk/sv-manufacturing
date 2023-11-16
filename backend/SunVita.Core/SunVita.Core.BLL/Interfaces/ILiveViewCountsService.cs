using SunVita.Core.Common.DTO.Live;

namespace SunVita.Core.BLL.Interfaces
{
    public interface ILiveViewCountsService
    {
        ICollection<LiveViewCountsDto> GetLiveViewCounts();
        void UpdateLiveViewCounts(ICollection<LiveViewCountsDto> newLineCounts);
    }
}
