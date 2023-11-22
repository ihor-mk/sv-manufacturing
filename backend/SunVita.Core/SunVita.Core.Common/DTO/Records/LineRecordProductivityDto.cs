namespace SunVita.Core.Common.DTO.Records
{
    public class LineRecordProductivityDto
    {
        public string LineTitle { get; set; } = string.Empty;
        public double Productivity { get; set; }
        public string TeamTitle { get; set; } = string.Empty;
        public DateTime DateTime { get; set; }
    }
}
