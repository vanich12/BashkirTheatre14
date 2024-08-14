using BashkirTheatre14.Helpers.Logging;
using BashkirTheatre14.Model;
using BashkirTheatre14.Services;
using BashkirTheatre14.Utlities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;

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
                services.AddSingleton<QuizService>();
                services.AddSingleton<ChronicleService>();
            });
            
            return builder;
        }
    }
}
