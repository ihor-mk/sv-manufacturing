using SunVita.Core.DAL.Entities.Common;

namespace SunVita.Core.DAL.Entities
{
    public class DoneTask : Entity<long>
    {
        public string StringNumber { get; set; } = string.Empty;
        public long NomenclatureId { get; set; }
        public Nomenclature Nomenclature { get; set; } = null!;
        public int Quantity { get; set; }
        public string TeamTitle { get; set; } = string.Empty;
        public ICollection<Employee> Employees { get; set; }
        public long ProductionLineId { get; set; }
        public ProductionLine ProductionLine { get; set; } = null!;
        public DateTime WorkDay { get; set; }
        public string DayPart { get; set; } = string.Empty;
        public DateTime StartedAt { get; set; }
        public DateTime FinishedAt { get; set; }


        public DoneTask()
        {
            Employees = new List<Employee>();
        }
    }
}
