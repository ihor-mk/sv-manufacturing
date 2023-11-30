using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SunVita.Core.BLL.Interfaces;
using SunVita.Core.BLL.Services.Abstract;
using SunVita.Core.Common.DTO.Filters;
using SunVita.Core.Common.DTO.Rating;
using SunVita.Core.DAL.Context;
using SunVita.Core.DAL.Entities;

namespace SunVita.Core.BLL.Services
{
    public class RatingService : BaseService, IRatingService
    {
        public RatingService(SunVitaCoreContext context, IMapper mapper)
            : base(context, mapper) { }

        public async Task<List<EmployeeQuantityDto>> GetEmployees(RatingFilter filter)
        {
            var doneTasks = await _context.DoneTasks.Include(x => x.Employees).ToListAsync();

            return doneTasks
                .Where(x => x.StartedAt.Month == DateTime.Now.Month + filter.Month)
                .SelectMany(x => x.Employees, (task, employee) =>
                    new EmployeeQuantityDto
                    {
                        FullName = employee.FullName,
                        Quantity = task.Quantity
                    })
                .GroupBy(x => x.FullName)
                .Select(group =>
                    new EmployeeQuantityDto
                    {
                        FullName = group.Key.Split('(')[0],
                        Quantity = group.Sum(x => x.Quantity)
                    })
                .OrderByDescending(x => x.Quantity)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToList();
        }

        public async Task<int> GetEmployeesCount(int month)
        {
            var doneTasks = await _context.DoneTasks.Include(x => x.Employees).ToListAsync();

            return doneTasks
                .Where(x => x.StartedAt.Month == DateTime.Now.Month + month)
                .SelectMany(x => x.Employees, (task, employee) =>
                    new EmployeeQuantityDto
                    {
                        FullName = employee.FullName,
                        Quantity = task.Quantity
                    })
                .GroupBy(x => x.FullName)
                .Count();
        }
    }
}
