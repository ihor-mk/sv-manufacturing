using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SunVita.Core.BLL.Interfaces;
using SunVita.Core.BLL.Services.Abstract;
using SunVita.Core.Common.DTO.Filters;
using SunVita.Core.Common.DTO.Rating;
using SunVita.Core.DAL.Context;
using System;

namespace SunVita.Core.BLL.Services
{
    public class RatingService : BaseService, IRatingService
    {
        public RatingService(SunVitaCoreContext context, IMapper mapper)
            : base(context, mapper) { }

        public async Task<List<EmployeeQuantityDto>> GetEmployees(MainFilter filter)
        {
            var startDate = new DateTime(DateTime.Now.Year, filter.Month, 1);
            var endDate = new DateTime(DateTime.Now.Year, filter.Month, DateTime.DaysInMonth(DateTime.Now.Year, filter.Month));

            var result = await _context.DoneTasks
                 .Where(x => x.WorkDay >= startDate && x.WorkDay <= endDate)
                 .Include(x => x.Employees)
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
                         FullName = group.Key,
                         Quantity = group.Sum(x => x.Quantity)
                     })
                 .OrderByDescending(x => x.Quantity)
                 .Skip((filter.PageNumber - 1) * filter.PageSize)
                 .Take(filter.PageSize)
                 .ToListAsync();

            return result
                .Select(x =>
                    new EmployeeQuantityDto
                    {
                        FullName = x.FullName.Split('(')[0],
                        Quantity = x.Quantity
                    })
                .ToList();
        }

        public async Task<int> GetEmployeesCount(int month)
        {
            var startDate = new DateTime(DateTime.Now.Year, month, 1);
            var endDate = new DateTime(DateTime.Now.Year, month, DateTime.DaysInMonth(DateTime.Now.Year, month));

            return await _context.DoneTasks
                .Where(x => x.WorkDay >= startDate && x.WorkDay <= endDate)
                .Include(x => x.Employees)
                .SelectMany(x => x.Employees, (task, employee) =>
                     new EmployeeQuantityDto
                     {
                         FullName = employee.FullName,
                         Quantity = task.Quantity
                     })
                .GroupBy(x => x.FullName)
                .CountAsync();
        }
    }
}
