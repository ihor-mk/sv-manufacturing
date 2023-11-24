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
        private IRecordsService _recordsService;

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

        [HttpGet("test")]
        public async Task<ActionResult<ICollection<DoneTask>>> GetTest()
        {
            var result = await _recordsService.GetTestDate();
            return Ok(result);
        }

        [HttpGet("nomenclaturesRating")]
        public async Task<ActionResult<ICollection<NomenclatureQuantityDto>>> GetNomenclaturesRating()
        {
            var result = await _recordsService.GetNomenclaturesRating();
            return Ok(result);
        }
        


    }
}
