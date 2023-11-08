using AutoMapper;
using SunVita.Core.BLL.Interfaces;
using SunVita.Core.BLL.Services.Abstract;
using SunVita.Core.Common.DTO.DoneTask;
using SunVita.Core.DAL.Context;
using SunVita.Core.DAL.Entities;

namespace SunVita.Core.BLL.Services
{
    public class DoneTaskService : BaseService, IDoneTaskService
    {
        public DoneTaskService(SunVitaCoreContext context) : base(context)
        {
        }
        public async Task<DoneTaskFileDto> CreateDoneTask(DoneTaskFileDto file)
        {
            foreach(var emp in file.Employees) 
            {
                var employee = _context.Employees
                    .Where(x => x.FullName == emp.FullName)
                    .FirstOrDefault();

                if (employee == null) 
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



            var nomenklature = _context.Nomenclatures
                .Where(x => x.Title == file.NomenclatureTitle)
                .FirstOrDefault();

            if (nomenklature == null)
            {
                var newNomencl = new Nomenclature {  Title = file.NomenclatureTitle, Number = file.NomenclatureNumber };
                await _context.Nomenclatures.AddAsync(newNomencl);
                await _context.SaveChangesAsync();
            }
            nomenklature = _context.Nomenclatures
                .Where(x => x.Title == file.NomenclatureTitle)
                .FirstOrDefault();

            var line = _context.ProductionLines
                .Where(x => x.Title == file.ProductionLineTitle)
                .FirstOrDefault();


            var newDoneTask = new DoneTask
            {
                Nomenclature = nomenklature!,
                ProductionLine = line!,
                Employees = employees!,
                Quantity = file.Quantity,
                StringNumber = file.StringNumber,
                StartedAt = DateTime.Parse(file.StartedAt),
                FinishedAt = DateTime.Parse(file.FinishedAt)
            };

            await _context.DoneTasks.AddAsync(newDoneTask);

            await _context.SaveChangesAsync();

            return file;
        }

    }
}
