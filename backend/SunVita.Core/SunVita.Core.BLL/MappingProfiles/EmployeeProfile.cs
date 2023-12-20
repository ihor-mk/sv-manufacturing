using AutoMapper;
using SunVita.Core.Common.DTO.Employee;
using SunVita.Core.DAL.Entities;

namespace SunVita.Core.BLL.MappingProfiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile() 
        {
            CreateMap<Employee, NewEmployeeDto>();
            CreateMap<NewEmployeeDto, Employee>();
        }
    }
}
