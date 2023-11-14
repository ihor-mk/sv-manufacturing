namespace SunVita.Core.Common.DTO.Live
{
    public class LineUpdateDto
    {
        public long Id { get; set; }
        public int CurrentQuantity { get; set; }
        public string CurrentNomenclature { get; set; } = string.Empty;

        public override int GetHashCode()
        {
            return this.Id.GetHashCode() + this.CurrentQuantity.GetHashCode() + this.CurrentNomenclature.GetHashCode();
        }
    }
}
