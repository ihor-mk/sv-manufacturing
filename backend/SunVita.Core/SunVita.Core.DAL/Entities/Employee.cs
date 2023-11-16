using SunVita.Core.DAL.Entities.Common;

namespace SunVita.Core.DAL.Entities
{
    public class Employee : Entity<long>
    {
        public string FullName { get; set; } = String.Empty;
        public ICollection<DoneTask> DoneTasks { get; set; }

        public Employee() 
        {
            DoneTasks = new List<DoneTask>();
        }
    }
}
