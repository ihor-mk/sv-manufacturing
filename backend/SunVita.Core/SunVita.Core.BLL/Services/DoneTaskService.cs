using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SunVita.Core.BLL.Interfaces;
using SunVita.Core.BLL.Services.Abstract;
using SunVita.Core.Common.DTO.DoneTask;
using SunVita.Core.DAL.Context;
using SunVita.Core.DAL.Entities;
using System.Threading.Tasks;

namespace SunVita.Core.BLL.Services
{
    public class DoneTaskService : BaseService, IDoneTaskService
    {
        public DoneTaskService(SunVitaCoreContext context, IMapper mapper) : base(context, mapper)
        {
        }
        public async Task<DoneTaskFileDto> CreateDoneTask(DoneTaskFileDto file)
        {
            foreach(var emp in file.Employees) 
            {
                var employee = await _context.Employees
                    .Where(x => x.FullName == emp.FullName)
                    .FirstOrDefaultAsync();

                if (employee is null) 
                {
                    var newEmployee = new Employee { FullName = emp.FullName };

                    await _context.Employees.AddAsync(newEmployee);
                    await _context.SaveChangesAsync();
                }
            }

            var employees = file.Employees
                .Join(_context.Employees,
                    fileEmp => fileEmp.FullName,
                    contextEmp => contextEmp.FullName,
                    (fileEmp, contextEmp) => contextEmp)
                .ToList();


            var nomenklature = await _context.Nomenclatures
                .Where(x => x.Number == file.NomenclatureNumber)
                .FirstOrDefaultAsync();

            if (nomenklature is null)
            {
                var newNomencl = new Nomenclature 
                {  
                    Title = file.NomenclatureTitle, 
                    Number = file.NomenclatureNumber,
                    NomenclatureInBox = file.NomenclatureInBox,
                };
                await _context.Nomenclatures.AddAsync(newNomencl);
                await _context.SaveChangesAsync();

                nomenklature = await _context.Nomenclatures
                .Where(x => x.Number == file.NomenclatureNumber)
                .FirstOrDefaultAsync();
            }

            


            var line = await _context.ProductionLines
                .Where(x => x.Code == file.ProductionLineCode)
                .FirstOrDefaultAsync();

            if (line is null)
            {
                var newLine = new ProductionLine 
                { 
                    Title = file.ProductionLineTitle,
                    Code = file.ProductionLineCode,
                };
                await _context.ProductionLines.AddAsync(newLine);
                await _context.SaveChangesAsync();

                line = await _context.ProductionLines
                .Where(x => x.Code == file.ProductionLineCode)
                .FirstOrDefaultAsync();
            }

            

            var newDoneTask = new DoneTask
            {
                Nomenclature = nomenklature!,
                ProductionLine = line!,
                TeamTitle = file.TeamTitle,
                WorkDay = DateTime.Parse(file.WorkDay),
                DayPart = file.DayPart,
                Employees = employees!,
                Quantity = file.Quantity,
                StringNumber = file.StringNumber,
                StartedAt = DateTime.Parse(file.StartedAt),
                FinishedAt = DateTime.Parse(file.FinishedAt),
                Productivity = file.Quantity / (DateTime.Parse(file.FinishedAt) - DateTime.Parse(file.StartedAt)).TotalMinutes
        };
            await _context.DoneTasks.AddAsync(newDoneTask);
            await _context.SaveChangesAsync();

            return file;
        }

    }
}
