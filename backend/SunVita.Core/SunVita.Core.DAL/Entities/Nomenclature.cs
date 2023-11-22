using SunVita.Core.DAL.Entities.Common;

namespace SunVita.Core.DAL.Entities
{
    public class Nomenclature : Entity<long>
    {
        public string Number { get; set; } = string.Empty; 
        public string Title { get; set; } = string.Empty;
        public int NomenclatureInBox { get; set; }
    }
}
