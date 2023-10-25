using SunVita.Core.DAL.Entities.Common;

namespace SunVita.Core.DAL.Entities
{
    public class DoneTask : Entity<long>
    {
        public string StringNumber { get; set; } = string.Empty;
        public long NomenclatureId { get; set; }
        public Nomenclature Nomenclature { get; set; } = null!;
        public int Count { get; set; }
        public long TeamId { get; set; }
        public Team Team { get; set; } = null!;
    }
}
