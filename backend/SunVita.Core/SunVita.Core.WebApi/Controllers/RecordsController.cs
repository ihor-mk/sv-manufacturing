using Microsoft.AspNetCore.Mvc;
using SunVita.Core.BLL.Interfaces;
using SunVita.Core.Common.DTO.Records;

namespace SunVita.Core.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        private IRecordsService _recordsService;

        public RecordsController (IRecordsService recordsService)
        {
            _recordsService = recordsService;
        }

        [HttpGet("lineproductivity")]
        public async Task<ActionResult<ICollection<LineRecordProductivityDto>>> GetLinesProductivityRating()
        {
            var result = await _recordsService.GetLinesProductivityRating();
            return Ok(result);
        }
    }
}
