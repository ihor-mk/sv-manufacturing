using SunVita.Core.Common.DTO.Employee;
using SunVita.Core.Common.DTO.Nomenclature;

namespace SunVita.Core.BLL.Interfaces
{
    public interface IDataService
    {
        Task AddNomenclaturesAsync(ICollection<NewNomenclatureDto> newNomenclatures);
        Task AddEmployeesAsync(ICollection<NewEmployeeDto> newEmployees);
    }
}
