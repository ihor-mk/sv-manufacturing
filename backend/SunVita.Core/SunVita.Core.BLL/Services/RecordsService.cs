using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SunVita.Core.BLL.Interfaces;
using SunVita.Core.BLL.Services.Abstract;
using SunVita.Core.Common.DTO.Records;
using SunVita.Core.DAL.Context;

namespace SunVita.Core.BLL.Services
{
    public class RecordsService : BaseService, IRecordsService
    {
        public RecordsService(SunVitaCoreContext context, IMapper mapper)
            : base(context, mapper) { }

        public async Task<ICollection<LineRecordProductivityDto>> GetLinesProductivityRating()
        {
            var result = await _context.DoneTasks
               .Include(x => x.Employees)
               .Include(x => x.ProductionLine)
               .GroupBy(task => task.ProductionLine.Code)
               .Select(x => x.OrderByDescending(task => task.Productivity).First())
               .ToListAsync();
               
           return result
                .OrderByDescending(x => x.Productivity)
                .Select(x =>
                    new LineRecordProductivityDto
                    {
                        LineTitle = x.ProductionLine.Title,
                        Productivity = x.Productivity,
                        TeamTitle = x.TeamTitle,
                        DateTime = x.WorkDay,
                        Employees = x.Employees.Select(x => x.FullName.Split("(")[0]).ToList()
                    })
               .ToList(); 
        }
        public async Task<ICollection<NomenclatureQuantityDto>> GetNomenclaturesRating()
        {

            return await _context.DoneTasks
                .GroupBy(x => x.Nomenclature,
                (key, group) =>
                    new NomenclatureQuantityDto
                    {
                        NomenclatureTitle = key.Title,
                        Quantity = group.Sum(x => x.Quantity)
                    })
                .OrderByDescending(x => x.Quantity)
                .Take(8)
                .ToListAsync();
        }

        public async Task<ICollection<TeamTopDto>> GetTeamRating()
        {
            return await _context.DoneTasks
              .GroupBy(x => new {x.WorkDay, x.DayPart, x.TeamTitle}, 
                  (key, group) => 
                  new TeamTopDto
                  {
                      WorkDay = key.WorkDay,
                      TeamTitle = key.TeamTitle,
                      Quantity = group.Sum(a => a.Quantity)
                  })
              .OrderByDescending(x => x.Quantity)
              .Take(8)
              .ToListAsync();
        }
    }
}
