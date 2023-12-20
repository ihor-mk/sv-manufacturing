using Microsoft.EntityFrameworkCore;
using SunVita.Core.DAL.Entities;

namespace SunVita.Core.DAL.Context
{
    public static class ModelBuilderExtensions
    {
        public static void Configure(this ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<DoneTask>()
            //    .HasMany(e => e.Employees)
            //    .WithMany(e => e.DoneTasks)
            //    .UsingEntity(
            //    "DoneTaskEmployee",
            //    l => l.HasOne(typeof(DoneTask)).WithMany().HasForeignKey("EmployeeId").HasPrincipalKey(nameof(DoneTask.Id)),
            //    r => r.HasOne(typeof(Employee)).WithMany().HasForeignKey("DoneTaskId").HasPrincipalKey(nameof(Employee.Id)),
            //    j => j.HasKey("DoneTaskId", "EmployeeId"));

            modelBuilder.Entity<DoneTask>()
                .HasMany(e => e.Employees)
                .WithMany(e => e.DoneTasks)
                .UsingEntity<DoneTaskEmployee>();
        }

        public static void Seed(this ModelBuilder modelBuilder)
        {
            Seeder.Seed(modelBuilder);
        }
    }
}
