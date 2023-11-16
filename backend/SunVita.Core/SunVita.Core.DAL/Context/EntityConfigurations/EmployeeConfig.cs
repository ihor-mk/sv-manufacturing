using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SunVita.Core.DAL.Entities;

namespace SunVita.Core.DAL.Context.EntityConfigurations
{
    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("getutcdate()");
        }
    }
}
