namespace SunVita.Core.Common.DTO.Records
{
    public class LineRecordProductivityDto
    {
        public string LineTitle { get; set; } = string.Empty;
        public double Productivity { get; set; }
        public string TeamTitle { get; set; } = string.Empty;
        public DateTime DateTime { get; set; }
        public List<string> Employees { get; set; }

        public LineRecordProductivityDto()
        {
            Employees = new List<string>();
        }
    }
}
