using Microsoft.AspNetCore.Mvc;
using SunVita.Core.BLL.Interfaces;
using SunVita.Core.Common.DTO.Live;

namespace SunVita.Core.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LiveViewCountsController : ControllerBase
    {
        private readonly ILiveViewCountsService _liveViewCountsService;

        public LiveViewCountsController(ILiveViewCountsService liveViewCountsService)
        {
            _liveViewCountsService = liveViewCountsService;
        }

        [HttpGet]
        public ICollection<LiveViewCountsDto> Get()
        {
            var lineUpdateDto = _liveViewCountsService.GetLiveViewCounts();
            return lineUpdateDto;
        }

        [HttpPost]
        public IActionResult Post([FromBody] ICollection<LiveViewCountsDto> newLineCounts)
        {
            _liveViewCountsService.UpdateLiveViewCounts(newLineCounts);
            return Ok();
        }
    }
}
