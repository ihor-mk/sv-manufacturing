using AutoMapper;
using SunVita.Core.DAL.Context;

namespace SunVita.Core.BLL.Services.Abstract
{
    public abstract class BaseService
    {
        private protected readonly SunVitaCoreContext _context;

        public BaseService(SunVitaCoreContext context)
        {
            _context = context;
        }
    }
}
