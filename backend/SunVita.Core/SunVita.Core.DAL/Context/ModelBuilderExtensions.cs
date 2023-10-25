using Microsoft.EntityFrameworkCore;

namespace SunVita.Core.DAL.Context
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            Seeder.Seed(modelBuilder);
        }
    }
}
