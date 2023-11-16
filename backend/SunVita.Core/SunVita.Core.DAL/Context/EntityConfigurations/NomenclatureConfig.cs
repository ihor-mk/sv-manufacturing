using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SunVita.Core.DAL.Entities;

namespace SunVita.Core.DAL.Context.EntityConfigurations
{
    public class NomenclatureConfig : IEntityTypeConfiguration<Nomenclature>
    {
        public void Configure(EntityTypeBuilder<Nomenclature> builder)
        {
            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("getutcdate()");
        }
    }
}
