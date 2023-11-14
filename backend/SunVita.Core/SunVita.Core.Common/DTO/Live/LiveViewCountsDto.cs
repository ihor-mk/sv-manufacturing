namespace SunVita.Core.Common.DTO.Live
{
    public class LiveViewCountsDto
    {
        public int LineId { get; set; } = -1;
        public string LineTitle { get;set; } = string.Empty;
        public int ProductivityCurrent { get; set; } = 0;
        public int ProductivityTop { get; set;} = 0;
        public int ProductivityAvg { get; set; } = 0;
        public string NomenclatureTitle { get; set; } = string.Empty;
        public int QuantityPlan { get; set; } = 0;
        public int QuantityFact { get; set; } = 0;
        //public DateTime StartedAt { get; set; } = DateTime.Now;
        //public DateTime FinishedAt { get ; set; } = DateTime.Now;

        public override bool Equals(object? obj)
        {
            var item = obj as LiveViewCountsDto;

            if (item == null) 
                return false;

            return (this.NomenclatureTitle == item.NomenclatureTitle
                && this.QuantityFact == item.QuantityFact);
        }
    }
}
