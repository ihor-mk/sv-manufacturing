using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace SunVita.Core.DAL.Context
{
    public class SunVitaCoreContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SunVitaCoreContext(DbContextOptions<SunVitaCoreContext> options, IHttpContextAccessor httpContextAccessor) : base(options) 
        {
            _httpContextAccessor = httpContextAccessor;
        }
    }
}
