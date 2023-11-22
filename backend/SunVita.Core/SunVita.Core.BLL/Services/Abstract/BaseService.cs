using AutoMapper;
using SunVita.Core.DAL.Context;

namespace SunVita.Core.BLL.Services.Abstract
{
    public abstract class BaseService
    {
        private protected readonly SunVitaCoreContext _context;
        private protected readonly IMapper _mapper;

        public BaseService(SunVitaCoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
