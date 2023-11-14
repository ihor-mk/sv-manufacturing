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
                new ProductionLine {Id = 1, IpAddress = "10.0.0.1", Title = "Цех №1  (Лінія1)"  },
                new ProductionLine {Id = 2, IpAddress = "10.0.0.2", Title = "Цех №2  (Лінія1)"  },
                new ProductionLine {Id = 3, IpAddress = "10.0.0.3", Title = "Цех №3  (Лінія1)"  },
                new ProductionLine {Id = 4, IpAddress = "10.0.0.4", Title = "Цех №3  (Лінія2)"  },
            };
        }
    }
}
