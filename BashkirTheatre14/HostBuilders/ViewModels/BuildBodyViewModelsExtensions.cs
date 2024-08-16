using BashkirTheatre14.Model.Entities;
using BashkirTheatre14.Model.Entities.Map;
using BashkirTheatre14.Services;
using BashkirTheatre14.ViewModel.Controls;
using BashkirTheatre14.ViewModel.Controls.Map;
using BashkirTheatre14.ViewModel.Pages;
using BashkirTheatre14.ViewModel.Popups;
using BashkirTheatre14.ViewModel.Windows;
using CommunityToolkit.Mvvm.Messaging;
using CustomKeyboard.Helpers;
using CustomKeyboard.ViewModels;
using MapControlLib.Models.Clients;
using MapControlLib.Models.Repositories;
using MapControlLib.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MvvmNavigationLib.Services;
using MvvmNavigationLib.Stores;
using ImageLoadingHttpClient = BashkirTheatre14.Model.ImageLoadingHttpClient;

namespace BashkirTheatre14.HostBuilders.ViewModels
{
    public static class BuildBodyViewModelsExtensions
    {
        public static IHostBuilder BuildBodyViewModels(this IHostBuilder builder)
        {
            builder.ConfigureServices((context,services) =>
            {
                services.AddTransient<MapControlViewModel>(s=>
                    new MapControlViewModel(
                        s.GetRequiredService<IMessenger>(),
                        s.GetRequiredService<MapViewModel<Terminal>>(),
                        s.GetRequiredService<CreateViewModel<MapObjectPopupViewModel,AreaViewModel>>(),
                        s.GetRequiredService<NavigationService<MapSearchPopupViewModel>>()));

                services.AddSingleton<CreateViewModel<MapObjectPopupViewModel, AreaViewModel>>(s=>area=>
                    new MapObjectPopupViewModel(
                        area,s.GetRequiredService<MapNavigationService<Terminal>>(),
                        s.GetRequiredService<MapImageLoadingClient>(),
                        s.GetRequiredService<ParameterNavigationService<MapObjectDetailsPopupViewModel,List<string>>>()));

                services.AddTransient<MainPageViewModel>(s=>
                    new MainPageViewModel(
                        s.GetRequiredService<NavigationService<QuizSelectionPopupViewModel>>(), 
                        s.GetRequiredService<NavigationService<ChroniclesPageViewModel>>(),
                        s.GetRequiredService<NavigationService<TheatreInfoPageViewModel>>(),
                        s.GetRequiredService<MapControlViewModel>()));

                services.AddTransient<TheatreInfoPageViewModel>(s =>
                    new TheatreInfoPageViewModel(s.GetRequiredService<InfoServices>(),s.GetRequiredService<NavigationService<MainPageViewModel>>()));

                services.AddSingleton<CreateViewModel<TheatrInfoViewModel,Info>>(s =>
                {
                    return info => new TheatrInfoViewModel(info);
                });

                //летописи
                services.AddTransient<ChroniclesPageViewModel>(s =>
                    new ChroniclesPageViewModel(s.GetRequiredService<ChronicleService>(),s.GetRequiredService<NavigationService<MainPageViewModel>>()));

                services.AddSingleton<CreateViewModel<ChronicleViewModel, Chronicle>>(s =>
                {
                    var imageService = s.GetRequiredService<ImageLoadingHttpClient>();
                    return chronicle => new ChronicleViewModel(chronicle);
                });


                //квизы
                services.AddSingleton<CreateViewModel<QuizChoiceViewModel, QuizModel>>(s => quiz=>
                    new QuizChoiceViewModel(
                        s.GetRequiredService<ParameterNavigationService<QuizItemViewModel, QuizModel>>(),
                        quiz));
                
                services.AddSingleton<CreateViewModel<QuizResultViewModel, QuizViewModel>>(s =>
                {
                    var navigationService = s.GetRequiredService<NavigationService<MainPageViewModel>>();
                    return quiz => new QuizResultViewModel(navigationService,quiz);
                });  
                
                services.AddSingleton<CreateViewModel<QuizItemViewModel, QuizModel>>(s => quiz=>
                    new QuizItemViewModel(
                        s.GetRequiredService<ParameterNavigationService<QuizViewModel, QuizModel>>(),
                        quiz));

                services.AddSingleton<CreateViewModel<QuizViewModel, QuizModel>>(s => quiz =>
                    new QuizViewModel(
                        s.GetRequiredService<ParameterNavigationService<QuizResultViewModel, QuizViewModel>>(),
                        s.GetRequiredService<NavigationService<MainPageViewModel>>(),
                        quiz));

                services.AddTransient<KeyboardControlViewModel>(_ =>
                {
                    var vm = new KeyboardControlViewModel();
                    vm.SetLayouts(DefaultKeyboardLayouts.Get());
                    return vm;
                });

                services.AddSingleton<MainWindowViewModel>();
            });
            return builder;
        }
    }
}
