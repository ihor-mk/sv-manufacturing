using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SunVita.Core.DAL.Entities;

namespace SunVita.Core.DAL.Context
{
    public class SunVitaCoreContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DbSet<DoneTask> DoneTasks => Set<DoneTask>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Nomenclature> Nomenclatures => Set<Nomenclature>();
        public DbSet<ProductionLine> ProductionLines => Set<ProductionLine>();

        public SunVitaCoreContext(DbContextOptions<SunVitaCoreContext> options,
            IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Configure();
            modelBuilder.Seed();
        }
    }
}
