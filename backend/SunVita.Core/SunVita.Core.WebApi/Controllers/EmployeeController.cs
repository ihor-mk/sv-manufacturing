using Microsoft.AspNetCore.Mvc;
using SunVita.Core.BLL.Interfaces;
using SunVita.Core.Common.DTO.Employee;

namespace SunVita.Core.WebApi.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IDataService _dataService;
        public EmployeeController( IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ICollection<NewEmployeeDto> newNomenclatures)
        {
            await _dataService.AddEmployeesAsync(newNomenclatures);
            return Ok();
        }
    }
}
