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
            var productivityTask = await _context.DoneTasks
                .Join(_context.ProductionLines,
                t => t.ProductionLineId,
                l => l.Id,
                (task, line) =>
                    new LineRecordProductivityDto
                    {
                        LineTitle = line.Title,
                        Productivity = task.Quantity / (task.FinishedAt - task.StartedAt).TotalMinutes,
                        TeamTitle = task.TeamTitle,
                        DateTime = task.StartedAt,
                    })
                .ToListAsync();

            var result = productivityTask
                .OrderByDescending(x => x.Productivity)
                .DistinctBy(x => x.LineTitle)
                .ToList();

            return result;
        }
    }
}
