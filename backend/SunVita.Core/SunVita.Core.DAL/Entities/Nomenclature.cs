using SunVita.Core.DAL.Entities.Common;

namespace SunVita.Core.DAL.Entities
{
    public class Nomenclature : Entity<long>
    {
        public string Article { get; set; } = string.Empty; 
        public string Title { get; set; } = string.Empty;
        public int PiecesInBox { get; set; }
    }
}
