using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SunVita.Core.BLL.Interfaces;
using SunVita.Core.BLL.Services.Abstract;
using SunVita.Core.Common.DTO.Filters;
using SunVita.Core.Common.DTO.History;
using SunVita.Core.DAL.Context;
using System;


namespace SunVita.Core.BLL.Services
{
    public class HistoryService : BaseService, IHistoryService
    {
        public HistoryService(SunVitaCoreContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public async Task<int> GetDoneTaskCount(int month)
        {
            var startDate = new DateTime(DateTime.Now.Year, month, 1);
            var endDate = new DateTime(DateTime.Now.Year, month, DateTime.DaysInMonth(DateTime.Now.Year, month));

            return await _context.DoneTasks
                .Where(x => x.StartedAt >= startDate && x.StartedAt <= endDate)
                .CountAsync();
        }

        public async Task<ICollection<DoneTaskDto>> GetDoneTasks(MainFilter filter)
        {
            var startDate = new DateTime(DateTime.Now.Year, filter.Month, 1);
            var endDate = new DateTime(DateTime.Now.Year, filter.Month, DateTime.DaysInMonth(DateTime.Now.Year, filter.Month));

            return await _context.DoneTasks
                .Where(x => x.WorkDay >= startDate && x.WorkDay <= endDate)
                .OrderByDescending(x => x.StartedAt)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Include(x => x.Employees)
                .Select(x => 
                    new DoneTaskDto 
                    {
                        Employees = x.Employees
                            .Select(e => e.FullName)
                            .ToList(),
                        LineTitle = x.ProductionLine.Title,
                        DayPart = x.DayPart == "Дневная"? "Денна": "Нічна",
                        NomenclatureTitle = x.Nomenclature.Title,
                        Quantity = x.Quantity,
                        StartedAt = x.StartedAt,
                        FinishedAt = x.FinishedAt
                    }
                )
                .ToListAsync();
        }
    }
}
