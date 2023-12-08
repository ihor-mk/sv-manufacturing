using Microsoft.EntityFrameworkCore;
using SunVita.Core.BLL.Interfaces;
using SunVita.Core.BLL.MappingProfiles;
using SunVita.Core.BLL.Services;
using SunVita.Core.DAL.Context;

namespace SunVita.Core.WebApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ILiveViewCountsService, LiveViewCountsService>();
            services.AddTransient<IDoneTaskService, DoneTaskService>();
            services.AddTransient<IDataService, DataService>();
            services.AddTransient<IRecordsService, RecordsService>();
            services.AddTransient<IRatingService, RatingService>();
            services.AddTransient<IHistoryService, HistoryService>();
        }
        public static void AddSunVitaCoreContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionsString = configuration.GetConnectionString("SunVitaCoreDBConnection");
            services.AddDbContext<SunVitaCoreContext>(options =>
                options.UseSqlServer(
                    connectionsString,
                    opt => opt.MigrationsAssembly(typeof(SunVitaCoreContext).Assembly.GetName().Name)));
        }
        public static void RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<NomenclatureProfile>();
                cfg.AddProfile<EmployeeProfile>();
            });
        }
    }
}
