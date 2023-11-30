using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SunVita.Core.BLL.Interfaces;
using SunVita.Core.BLL.Services.Abstract;
using SunVita.Core.Common.DTO.Records;
using SunVita.Core.DAL.Context;
using SunVita.Core.DAL.Entities;

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

        public async Task<ICollection<DoneTask>> GetTestDate()
        {
            var doneTasks = await _context.DoneTasks.ToListAsync();

            var test = doneTasks
                .OrderByDescending(x => x.StartedAt)
                .Where(x => (x.StartedAt.Month <= DateTime.Now.Month - 1 && x.StartedAt.Month >= DateTime.Now.Month - 2))
                .ToList();



            return test;
        }

        public async Task<ICollection<NomenclatureQuantityDto>> GetNomenclaturesRating()
        {
            await this.GetTeamRating();

            return await _context.DoneTasks
                .GroupBy(x => x.Nomenclature)
                .Select(x =>
                    new NomenclatureQuantityDto
                    {
                        Quantity = x.Sum(y => y.Quantity),
                        NomenclatureTitle = x.Key.Title
                    })
                .OrderByDescending(x => x.Quantity)
                .Take(8)
                .ToListAsync();
        }

        public async Task<ICollection<TeamTopDto>> GetTeamRating()
        {
            var doneTasks = await _context.DoneTasks.ToListAsync();

            var teamDay = doneTasks
                .Where(task => task.StartedAt.Hour >= 6 && task.StartedAt.Hour < 18)
                .GroupBy(task => (task.StartedAt.Date))
                .Select(dateGroup =>
                    new
                    {
                        Date = dateGroup.Key,
                        List = dateGroup.GroupBy(task => task.TeamTitle)
                            .SelectMany(dateTeamGroup => dateTeamGroup, (dateTeamGroup, task) =>
                                new
                                {
                                    TeamTitle = dateTeamGroup.Key,
                                    Quantity = dateTeamGroup.Sum(a => a.Quantity)
                                })
                    })
                .SelectMany(dateGroup => dateGroup.List, (dateGroup, team) =>
                    new TeamTopDto
                    {
                        Date = dateGroup.Date,
                        TeamTitle = team.TeamTitle,
                        Quantity = team.Quantity
                    })
                .ToList();

            var teamNight = doneTasks
                .Where(task => task.StartedAt.Hour < 6 || task.StartedAt.Hour >= 18)
                .GroupBy(task => (task.StartedAt.Date))
                .Select(dateGroup =>
                    new
                    {
                        Date = dateGroup.Key,
                        List = dateGroup.GroupBy(task => task.TeamTitle)
                            .SelectMany(dateTeamGroup => dateTeamGroup, (dateTeamGroup, task) =>
                                new
                                {
                                    TeamTitle = dateTeamGroup.Key,
                                    Quantity = dateTeamGroup.Sum(a => a.Quantity)
                                })
                    })
                .SelectMany(dateGroup => dateGroup.List, (dateGroup, team) =>
                    new TeamTopDto
                    {
                        Date = dateGroup.Date,
                        TeamTitle = team.TeamTitle,
                        Quantity = team.Quantity
                    })
                .ToList();

            teamDay.AddRange(teamNight.ToList());

            return teamDay
                .OrderByDescending(team => team.Quantity)
                .Take(8)
                .ToList();
        }
    }
}
