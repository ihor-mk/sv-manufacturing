using SunVita.Core.BLL.Interfaces;
using SunVita.Core.Common.DTO.Live;

namespace SunVita.Core.BLL.Services
{
    public class LiveViewCountsService : ILiveViewCountsService
    {
        private ICollection<LiveViewCountsDto> LiveCounts { get; set; }

        public LiveViewCountsService()
        {
            LiveCounts = new List<LiveViewCountsDto>();
        }
        public ICollection<LiveViewCountsDto> GetLiveViewCounts()
        { 
            return LiveCounts; 
        }

        public void UpdateLiveViewCounts(ICollection<LiveViewCountsDto> newLineCounts)
        {
            LiveCounts = newLineCounts;
        }
    }
}
