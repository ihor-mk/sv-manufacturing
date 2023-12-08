using Microsoft.AspNetCore.Mvc;
using SunVita.Core.BLL.Interfaces;
using SunVita.Core.Common.DTO.Filters;
using SunVita.Core.Common.DTO.Rating;

namespace SunVita.Core.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingController (IRatingService ratingService) 
        {
            _ratingService = ratingService;
        }
        [HttpGet("{month}")]
        public async Task<ActionResult<int>> Get(int month) 
        {
            var result = await _ratingService.GetEmployeesCount(month);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ICollection<EmployeeQuantityDto>>> Post([FromBody] MainFilter filter)
        {
            var result = await _ratingService.GetEmployees(filter);
            return Ok(result);
        }
    }
}
