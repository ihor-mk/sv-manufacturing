using AutoMapper;
using SunVita.Core.BLL.Interfaces;
using SunVita.Core.BLL.Services.Abstract;
using SunVita.Core.Common.DTO.Filters;
using SunVita.Core.Common.DTO.History;
using SunVita.Core.DAL.Context;


namespace SunVita.Core.BLL.Services
{
    public class HistoryService : BaseService, IHistoryService
    {
        public HistoryService(SunVitaCoreContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public int GetDoneTaskCount(int month)
        {
            var startDate = new DateTime(DateTime.Now.Year, month, 1);
            var endDate = new DateTime(DateTime.Now.Year, month, DateTime.DaysInMonth(DateTime.Now.Year, month));

            return _context.DoneTasks
                .Where(x => x.StartedAt >= startDate && startDate <= endDate)
                .Count();
        }

        public Task<ICollection<DoneTaskDto>> GetDoneTasks(MainFilter filter)
        {
            throw new NotImplementedException();
        }
    }
}
