using Microsoft.EntityFrameworkCore;
using SunVita.Core.DAL.Context;

namespace SunVita.Core.WebApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            
        }
        public static void AddSunVitaCoreContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionsString = configuration.GetConnectionString("SunVitaCoreDBConnection");
            services.AddDbContext<SunVitaCoreContext>(options =>
                options.UseSqlServer(
                    connectionsString,
                    opt => opt.MigrationsAssembly(typeof(SunVitaCoreContext).Assembly.GetName().Name)));
        }

    }
}
