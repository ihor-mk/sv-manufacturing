namespace SunVita.Core.Common.DTO.History
{
    public class DoneTaskDto
    {
        public string LineTitle { get; set; } = string.Empty;
        public string DayPart { get; set; } = string.Empty;
        public string NomenclatureTitle { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public int Productivity { get; set; }
        public DateTime StartedAt { get; set; } = DateTime.MinValue;
        public DateTime FinishedAt { get; set;} = DateTime.MinValue;
    }
}
