using BashkirTheatre14.Model;
using BashkirTheatre14.Model.Entities;
using BashkirTheatre14.ViewModel.Controls;
using BashkirTheatre14.ViewModel.Pages;
using BashkirTheatre14.ViewModel.Popups;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MvvmNavigationLib.Services;
using MvvmNavigationLib.Services.ServiceCollectionExtensions;
using MvvmNavigationLib.Stores;

namespace BashkirTheatre14.HostBuilders.Navigation
{
    public static class BuildMainNavigationExtensions
    {
        public static IHostBuilder BuildMainNavigation(this IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<NavigationStore>();
                services.AddSingleton<ModalNavigationStore>();

                services.AddNavigationService<MainPageViewModel, NavigationStore>();
               
                services.AddParameterNavigationService<QuizItemViewModel, NavigationStore, Quiz>();

                services.AddSingleton<CompositeNavigationService<MainPageViewModel>>(s=>
                    new CompositeNavigationService<MainPageViewModel>(
                        s.GetRequiredService<NavigationService<MainPageViewModel>>(),
                        s.GetRequiredService<CloseNavigationService<ModalNavigationStore>>()));
            });
            return builder;
        }
    }
}
