using SunVita.Core.Common.DTO.Filters;
using SunVita.Core.Common.DTO.Rating;

namespace SunVita.Core.BLL.Interfaces
{
    public interface IRatingService
    {
        Task<int> GetEmployeesCount(int month);
        Task<List<EmployeeQuantityDto>> GetEmployees(MainFilter filter);
    }
}
