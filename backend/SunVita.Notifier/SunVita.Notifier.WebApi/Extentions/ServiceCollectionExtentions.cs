using SunVita.RabbitMQ.Interfaces;
using SunVita.RabbitMQ.Services;
using SunVita.RabbitMQ;
using SunVita.Notifier.WebApi.Hubs;

namespace SunVita.Notifier.WebApi.Extentions
{
    public static class ServiceCollectionExtentions
    {
        public static void RegisterCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            var hostname = configuration.GetValue<string>("Rabbit");
            services.AddSingleton<IConnectionProvider>(_ => new ConnectionProvider(hostname));
            services.AddTransient<IMessageConsumer, MessageConsumer>();
            services.AddSingleton<BroadcastHub>();
        }
    }
}
