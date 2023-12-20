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
                new ProductionLine {Id = 1, IpAddress = "10.61.2.23", Title = "Цех №1 (Лінія 1)", Code = "000000040", ProductivityAvg = 26.8, CreatedAt = DateTime.Now  },
                new ProductionLine {Id = 2, IpAddress = "10.61.2.22", Title = "Цех №2 (Лінія 1)", Code = "000000009", ProductivityAvg = 44.6, CreatedAt = DateTime.Now  },
                new ProductionLine {Id = 3, IpAddress = "10.61.2.21", Title = "Цех №2 (Лінія 2)", Code = "000000010", ProductivityAvg = 50, CreatedAt = DateTime.Now  },
                new ProductionLine {Id = 4, IpAddress = "10.61.2.22", Title = "Цех №4 (Лінія 1, кросфолд 1)", Code = "000000008", ProductivityAvg = 51.7, CreatedAt = DateTime.Now  },
                new ProductionLine {Id = 5, IpAddress = "10.61.2.23", Title = "Цех №5 (Лінія 1)", Code = "000000047", ProductivityAvg = 62.5, CreatedAt = DateTime.Now  },
                new ProductionLine {Id = 6, IpAddress = "10.61.2.23", Title = "Цех №5 (Лінія 2, кросфолд 2)", Code = "000000026", ProductivityAvg = 66.1, CreatedAt = DateTime.Now  },
                new ProductionLine {Id = 7, IpAddress = "10.61.2.21", Title = "Цех №5 (Лінія 3)", Code = "000000048", ProductivityAvg = 62.5, CreatedAt = DateTime.Now  },
            };
        }
    }
}
