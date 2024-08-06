using BashkirTheatre14.HostBuilders.ViewModels;
using Microsoft.Extensions.Hosting;

namespace BashkirTheatre14.HostBuilders
{
    public static class BuildViewModelsExtensions
    {
        public static IHostBuilder BuildViewModels(this IHostBuilder hostBuilder)
        {
            return hostBuilder.BuildBodyViewModels().BuildViewModalModels();
        }
    }
}
