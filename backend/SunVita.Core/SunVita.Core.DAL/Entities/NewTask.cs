using SunVita.Core.DAL.Entities.Common;

namespace SunVita.Core.DAL.Entities
{
    public class NewTask : Entity<long>
    {
        public string StringNumber { get; set; } = string.Empty;
        public long NomenclatureId { get; set; }
        public Nomenclature Nomenclature { get; set; } = null!;
        public int Сount { get; set; }
        public long ProductionLineId { get; set; }
        public ProductionLine ProductionLine { get; set; } = null!;

    }
}
