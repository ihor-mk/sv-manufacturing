using Microsoft.AspNetCore.Mvc;
using SunVita.Core.BLL.Interfaces;
using SunVita.Core.Common.DTO.Records;
using SunVita.Core.DAL.Entities;

namespace SunVita.Core.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        private readonly IRecordsService _recordsService;

        public RecordsController (IRecordsService recordsService)
        {
            _recordsService = recordsService;
        }

        [HttpGet("lineProductivity")]
        public async Task<ActionResult<ICollection<LineRecordProductivityDto>>> GetLinesProductivityRating()
        {
            var result = await _recordsService.GetLinesProductivityRating();
            return Ok(result);
        }
        
        [HttpGet("nomenclaturesRating")]
        public async Task<ActionResult<ICollection<NomenclatureQuantityDto>>> GetNomenclaturesRating()
        {
            var result = await _recordsService.GetNomenclaturesRating();
            return Ok(result);
        }

        [HttpGet("teamRating")]
        public async Task<ActionResult<ICollection<TeamTopDto>>> GetTeamRating()
        {
            var result = await _recordsService.GetTeamRating();
            return Ok(result);
        }



    }
}
