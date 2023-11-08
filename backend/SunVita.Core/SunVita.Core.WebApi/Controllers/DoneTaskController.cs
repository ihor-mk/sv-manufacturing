using Microsoft.AspNetCore.Mvc;
using SunVita.Core.BLL.Interfaces;
using SunVita.Core.Common.DTO.DoneTask;

namespace SunVita.Core.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DoneTaskController : ControllerBase
    {
        private readonly IDoneTaskService _doneTaskService;
        public DoneTaskController(IDoneTaskService doneTaskService)
        {
            _doneTaskService = doneTaskService;
        }

        [HttpPost]
        public async Task<ActionResult<DoneTaskFileDto>> Post([FromBody] DoneTaskFileDto doneTaskFile)
        {
            var doneTask = await _doneTaskService.CreateDoneTask(doneTaskFile);
            return doneTaskFile;
        }
    }
}
