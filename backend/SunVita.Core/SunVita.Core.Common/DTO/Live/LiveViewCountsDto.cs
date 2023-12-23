namespace SunVita.Core.Common.DTO.Live
{
    public class LiveViewCountsDto : ICloneable
    {
        public int LineId { get; set; }
        public string LineCode { get; set; } = string.Empty;
        public string LineTitle { get; set; } = string.Empty;
        public string IpAddress { get; set; } = string.Empty;
        public bool IsPrinterOffline { get; set; } = false;
        public double ProductivityCurrent { get; set; } = 0;
        public double ProductivityTop { get; set; } = 0;
        public double ProductivityAvg { get; set; } = 0;
        public string NomenclatureTitle { get; set; } = string.Empty;
        public bool IsNewNomenclature { get; set; } = false;
        public string NomenclatureOnPrinter { get; set; } = string.Empty;
        public bool IsNewPrinterNomenclature { get; set; } = false;
        public int NomenclatureInBox { get; set; } = 1;
        public int QuantityPlan { get; set; } = 0;
        public int QuantityFact { get; set; } = 0;
        public DateTime StartedAt { get; set; } = DateTime.Now;
        public DateTime FinishedAt { get; set; } = DateTime.Now;
        public double WorkTime { get; set; } = 0;

        public object Clone()
        {
            return new LiveViewCountsDto
            {
                LineId = LineId,
                LineCode = LineCode,
                LineTitle = LineTitle,
                IpAddress = IpAddress,
                ProductivityCurrent = ProductivityCurrent,
                ProductivityTop = ProductivityTop,
                ProductivityAvg = ProductivityAvg,
                NomenclatureTitle = NomenclatureTitle,
                IsNewNomenclature = IsNewNomenclature,
                NomenclatureOnPrinter = NomenclatureOnPrinter,
                IsNewPrinterNomenclature = IsNewPrinterNomenclature,
                NomenclatureInBox = NomenclatureInBox,
                QuantityPlan = QuantityPlan,
                QuantityFact = QuantityFact,
                StartedAt = StartedAt,
                FinishedAt = FinishedAt,
                WorkTime = WorkTime
            };
        }

        public override bool Equals(object? obj)
        {
            if (obj is not LiveViewCountsDto item)
                return false;

            return (this.NomenclatureTitle == item.NomenclatureTitle
                && this.QuantityFact == item.QuantityFact
                && this.NomenclatureOnPrinter == item.NomenclatureOnPrinter
                && this.ProductivityAvg == item.ProductivityAvg);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
