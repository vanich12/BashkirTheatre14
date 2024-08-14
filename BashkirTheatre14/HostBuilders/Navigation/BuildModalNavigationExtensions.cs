using BashkirTheatre14.ViewModel.Pages;
using BashkirTheatre14.ViewModel.Popups;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MvvmNavigationLib.Services;
using MvvmNavigationLib.Services.ServiceCollectionExtensions;
using MvvmNavigationLib.Stores;

namespace BashkirTheatre14.HostBuilders.Navigation
{
    public static class BuildModalNavigationExtensions
    {
        public static IHostBuilder BuildModalNavigation(this IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<CloseNavigationService<ModalNavigationStore>>();
                services.AddNavigationService<PasswordPopupViewModel, ModalNavigationStore>();
                services.AddNavigationService<MapSearchPopupViewModel, ModalNavigationStore>();
                services.AddNavigationService<QuizSelectionPopupViewModel, ModalNavigationStore>();
                services.AddParameterNavigationService<MapObjectDetailsPopupViewModel, ModalNavigationStore, List<string>>();
            });
            return builder;
        }
    }
}
