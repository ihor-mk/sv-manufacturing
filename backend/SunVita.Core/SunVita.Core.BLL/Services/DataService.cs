using AutoMapper;
using SunVita.Core.BLL.Interfaces;
using SunVita.Core.BLL.Services.Abstract;
using SunVita.Core.Common.DTO.Employee;
using SunVita.Core.Common.DTO.Nomenclature;
using SunVita.Core.DAL.Context;
using SunVita.Core.DAL.Entities;

namespace SunVita.Core.BLL.Services
{
    public class DataService : BaseService, IDataService
    {
        public DataService(SunVitaCoreContext context, IMapper mapper)
            : base(context, mapper) { }

        public async Task AddNomenclaturesAsync(ICollection<NewNomenclatureDto> newNomenclatures)
        {
            var nomenclatures = _mapper.Map<ICollection<Nomenclature>>(newNomenclatures);

            await _context.Nomenclatures.AddRangeAsync(nomenclatures);

            await _context.SaveChangesAsync();
        }

        public async Task AddEmployeesAsync(ICollection<NewEmployeeDto> newEmployees)
        {
            var employees = _mapper.Map<ICollection<Employee>>(newEmployees);

            await _context.Employees.AddRangeAsync(employees);

            await _context.SaveChangesAsync();
        }
    }
}
