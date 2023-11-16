namespace SunVita.Core.Common.DTO.DoneTask
{
    public class DoneTaskFileDto
    {
        public string StringNumber { get; set; } = string.Empty;
        public string NomenclatureNumber { get; set; } = string.Empty;
        public string NomenclatureTitle { get; set; } = null!;
        public int Quantity { get; set; }
        public string ProductionLineTitle { get; set; } = string.Empty;
        public string StartedAt { get; set; } = string.Empty;
        public string FinishedAt { get; set; } = string.Empty;
        public string TeamTitle { get; set; } = string.Empty;
        public ICollection<EmployeeDto> Employees { get; set; }

        public DoneTaskFileDto() 
        {
            Employees = new List<EmployeeDto>();
        }
    }
}
