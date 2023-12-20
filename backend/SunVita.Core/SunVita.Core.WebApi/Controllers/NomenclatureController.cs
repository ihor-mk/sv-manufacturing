using Microsoft.AspNetCore.Mvc;
using SunVita.Core.BLL.Interfaces;
using SunVita.Core.Common.DTO.Nomenclature;

namespace SunVita.Core.WebApi.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class NomenclatureController : ControllerBase
    {
        private readonly IDataService _dataService;
        public NomenclatureController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ICollection<NewNomenclatureDto> newNomenclatures)
        {
            await _dataService.AddNomenclaturesAsync(newNomenclatures);
            return Ok();
        }
    }
}
