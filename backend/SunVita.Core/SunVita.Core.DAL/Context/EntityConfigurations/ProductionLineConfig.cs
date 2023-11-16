using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SunVita.Core.DAL.Entities;

namespace SunVita.Core.DAL.Context.EntityConfigurations
{
    public class ProductionLineConfig : IEntityTypeConfiguration<ProductionLine>
    {
        public void Configure(EntityTypeBuilder<ProductionLine> builder)
        {
            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("getutcdate()");
        }
    }
}
