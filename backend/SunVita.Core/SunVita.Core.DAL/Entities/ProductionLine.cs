using SunVita.Core.DAL.Entities.Common;

namespace SunVita.Core.DAL.Entities
{
    public class ProductionLine : Entity<long>
    {
        public string Title { get; set; } = string.Empty;
        public string IpAddress { get; set; } = string.Empty;

    }
}
