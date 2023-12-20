using Microsoft.AspNetCore.Mvc;
using SunVita.Core.BLL.Interfaces;
using SunVita.Core.Common.DTO.Filters;
using SunVita.Core.Common.DTO.History;

namespace SunVita.Core.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly IHistoryService _historyService;
        public HistoryController(IHistoryService historyService)
        {
            _historyService = historyService;
        }
        [HttpGet("{month}")]
        public async Task<ActionResult<int>> GetAction(int month)
        {
            var result = await _historyService.GetDoneTaskCount(month);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ICollection<DoneTaskDto>>> Post([FromBody] MainFilter filter)
        {
            var result = await _historyService.GetDoneTasks(filter);
            return Ok(result);
        }

    }
}
