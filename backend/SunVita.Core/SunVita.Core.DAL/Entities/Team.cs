using SunVita.Core.DAL.Entities.Common;

namespace SunVita.Core.DAL.Entities
{
    public  class Team : Entity<long>
    {
        public string Title { get; set; } = String.Empty;
        public ICollection<Employee> Employees { get; set; }

        public Team() 
        {
            Employees = new List<Employee>();
        }
    }
}
