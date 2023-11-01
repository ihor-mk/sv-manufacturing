using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SunVita.Core.DAL.Entities;

namespace SunVita.Core.DAL.Context
{
    public class SunVitaCoreContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SunVitaCoreContext(DbContextOptions<SunVitaCoreContext> options,
            IHttpContextAccessor httpContextAccessor
            ) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<DoneTask> DoneTasks => Set<DoneTask>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Nomenclature> Nomenclatures => Set<Nomenclature>();
        public DbSet<ProductionLine> ProductionLines => Set<ProductionLine>();
    }
}
