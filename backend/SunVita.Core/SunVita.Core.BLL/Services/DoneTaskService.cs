using SunVita.Core.BLL.Interfaces;
using SunVita.Core.Common.DTO.DoneTask;

namespace SunVita.Core.BLL.Services
{
    public class DoneTaskService : IDoneTaskService
    {
        //private readonly SunVitaCoreContext _context;
        //public DoneTaskService(SunVitaCoreContext context)
        //{
        //    _context = context;
        //}

        public async Task<DoneTaskFileDto> CreateDoneTask(DoneTaskFileDto file)
        {

            //ICollection<Employee> employees = new List<Employee>();

            //foreach (var employeeDto in file.Employees)
            //{
            //    var employee = _context.Employees
            //        .Where(x => x.FullName == employeeDto.FullName)
            //        .FirstOrDefault();

            //    if (employee != null) 
            //    {
            //        employees.Add(employee);
            //    }
            //}

            //var employees = file.Employees
            //    .Join(_context.Employees,
            //        fileEmp => fileEmp.FullName,
            //        contextEmp => contextEmp.FullName,
            //        (fileEmp, contextEmp) => contextEmp)
            //    .ToList();

            //if (employees is not  null)
            //{
            //    var team = new Team { Employees = employees };
            //}
            

            //var nomenklature = _context.Nomenclatures
            //    .Where(x => x.Title == file.NomenclatureTitle)
            //    .FirstOrDefault();

            //var line = _context.ProductionLines
            //    .Where(x => x.Title == file.ProductionLineTitle)
            //    .FirstOrDefault();

            //await _context.DoneTasks.AddAsync(
            //    new DoneTask
            //    {
            //        Nomenclature = nomenklature!,
            //        ProductionLine = line!,
            //        Employees = employees!,
            //        Quantity = file.Quantity,
            //        StringNumber = file.StringNumber,
            //        StartedAt = DateTime.Parse(file.StartedAt),
            //        FinishedAt = DateTime.Parse(file.FinishedAt)
            //    });

            //await _context.SaveChangesAsync();

            return file;
        }

    }
}
