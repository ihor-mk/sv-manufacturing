using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SunVita.Core.DAL.Entities;
using System;

namespace SunVita.Core.DAL.Context.EntityConfigurations
{
    public class DoneTaskConfig : IEntityTypeConfiguration<DoneTask>
    {
        public void Configure(EntityTypeBuilder<DoneTask> builder)
        {
            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("getutcdate()");
        }
    }
}
