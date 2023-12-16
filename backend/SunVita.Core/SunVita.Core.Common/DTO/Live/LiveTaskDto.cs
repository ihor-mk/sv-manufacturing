namespace SunVita.Core.Common.DTO.Live
{
    public class LiveTaskDto
    {
        public string NomenclatureNumber { get; set; } = string.Empty;
        public string NomenclatureTitle { get; set; } = string.Empty;
        public double Quantity { get; set; }
        public string ProductionLineTitle { get; set; } = string.Empty;
        public string ProductionLineCode { get; set; } = string.Empty;
        public int NomenclatureInBox { get; set; } = 0;
    }
}
