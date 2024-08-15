using BashkirTheatre14.Helpers.Logging;
using BashkirTheatre14.Model;
using BashkirTheatre14.Model.Entities.Map;
using BashkirTheatre14.Services;
using MapControlLib.Helpers;
using MapControlLib.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;
using ApiCachingHttpMessageHandler = BashkirTheatre14.Utlities.ApiCachingHttpMessageHandler;

namespace BashkirTheatre14.HostBuilders
{
    public static class BuildApiServicesExtension
    {
        public static IHostBuilder BuildApiStores(this IHostBuilder builder)
        {
            builder.ConfigureServices((context,services) =>
            {
                var host = new Uri(context.Configuration.GetValue<string>("host") ?? string.Empty);
                services.AddMemoryCache();
                services.AddScoped<ApiCachingHttpMessageHandler>();

                services.AddHttpClient<ImageLoadingHttpClient>(c => c.BaseAddress = host);

                services.AddRefitClient<IMainApiClient>()
                    .ConfigureHttpClient(c =>
                        c.BaseAddress = host)
                    .AddHttpMessageHandler<ApiCachingHttpMessageHandler>();

                services.AddSingleton<ILoggingService>(s => new FileLoggingService("Logs"));

                services.AddMap<Terminal, Floor, MapObject>(host,context.Configuration.GetValue<int>("terminalId"),true,TimeSpan.FromMinutes(10));

                services.AddSingleton<QuizService>();
                services.AddSingleton<ChronicleService>();
                services.AddSingleton<InfoServices>();
                services.AddSingleton<TheatreInfoService>();
            });
            
            return builder;
        }
    }
}
