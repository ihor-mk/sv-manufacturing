using Microsoft.AspNetCore.Mvc;
using SunVita.Core.BLL.Interfaces;

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
        [HttpGet]
        public ActionResult<int> GetAction(int month)
        {
            var result  = _historyService.GetDoneTaskCount(month);
            return Ok(result);
        }
    }
}
