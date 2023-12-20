using SunVita.Core.Common.DTO.Records;
using SunVita.Core.DAL.Entities;

namespace SunVita.Core.BLL.Interfaces
{
    public interface IRecordsService
    {
        Task<ICollection<LineRecordProductivityDto>> GetLinesProductivityRating();
        Task<ICollection<NomenclatureQuantityDto>> GetNomenclaturesRating();
        Task<ICollection<TeamTopDto>> GetTeamRating();
    }
}
