using BashkirTheatre14.Model.Entities;
using BashkirTheatre14.Services;
using BashkirTheatre14.ViewModel.Controls;
using BashkirTheatre14.ViewModel.Controls.Map;
using BashkirTheatre14.ViewModel.Pages;
using BashkirTheatre14.ViewModel.Popups;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MvvmNavigationLib.Services;
using MvvmNavigationLib.Stores;

namespace BashkirTheatre14.HostBuilders.ViewModels
{
    public static class BuildModalViewModels
    {
        public static IHostBuilder BuildViewModalModels(this IHostBuilder builder)
        {
            builder.ConfigureServices((context,services) =>
            {
                services.AddTransient<PasswordPopupViewModel>(s => new PasswordPopupViewModel(
                    s.GetRequiredService<CloseNavigationService<ModalNavigationStore>>(),
                    context.Configuration.GetValue<string>("exitPassword")??"123"));

                services.AddTransient<QuizSelectionPopupViewModel>(s=>
                    new QuizSelectionPopupViewModel(
                        s.GetRequiredService<CloseNavigationService<ModalNavigationStore>>(),
                        s.GetRequiredService<QuizService>(),
                        s.GetRequiredService<CreateViewModel<QuizChoiceViewModel,QuizModel>>()));

                services.AddSingleton<CreateViewModel<MapObjectDetailsPopupViewModel, List<string>>>(s => images => 
                    new MapObjectDetailsPopupViewModel(
                        s.GetRequiredService<CloseNavigationService<ModalNavigationStore>>(),
                        images));

                services.AddTransient<MapSearchPopupViewModel>();
                services.AddSingleton<CreateViewModel<SearchMapObjectPopupViewModel, MapObjectPopupViewModel>>(s =>
                    item =>
                        new SearchMapObjectPopupViewModel(
                            s.GetRequiredService<CloseNavigationService<ModalNavigationStore>>(),
                            item, s.GetRequiredService<IMessenger>()));
            });
            return builder;
        }
    }
}
