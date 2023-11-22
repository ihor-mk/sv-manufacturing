using SunVita.Core.Common.DTO.Records;

namespace SunVita.Core.BLL.Interfaces
{
    public interface IRecordsService
    {
        Task<ICollection<LineRecordProductivityDto>> GetLinesProductivityRating();
    }
}
