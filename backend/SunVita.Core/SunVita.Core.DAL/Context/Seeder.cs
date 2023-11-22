using Microsoft.EntityFrameworkCore;
using SunVita.Core.DAL.Entities;

namespace SunVita.Core.DAL.Context
{
    public static class Seeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var productionsLines = CreateLines();
            modelBuilder.Entity<ProductionLine>().HasData(productionsLines);
        }
        private static IList<ProductionLine> CreateLines()
        {
            return new List<ProductionLine>()
            {
                new ProductionLine {Id = 1, IpAddress = "10.61.2.21", Title = "Цех №2  (Лінія1)", CreatedAt = DateTime.Now  },
                new ProductionLine {Id = 2, IpAddress = "10.61.2.22", Title = "Цех №2 (Лінія 2)", CreatedAt = DateTime.Now  },
                new ProductionLine {Id = 3, IpAddress = "10.61.2.23", Title = "Цех №5", CreatedAt = DateTime.Now  },
            };
        }
    }
}
