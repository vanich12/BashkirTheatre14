using BashkirTheatre14.Model;
using BashkirTheatre14.Model.Entities;
using BashkirTheatre14.ViewModel.Controls;
using BashkirTheatre14.ViewModel.Pages;
using BashkirTheatre14.ViewModel.Popups;
using BashkirTheatre14.ViewModel.Windows;
using CustomKeyboard.Helpers;
using CustomKeyboard.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MvvmNavigationLib.Services;

namespace BashkirTheatre14.HostBuilders.ViewModels
{
    public static class BuildBodyViewModelsExtensions
    {
        public static IHostBuilder BuildBodyViewModels(this IHostBuilder builder)
        {
            builder.ConfigureServices((context,services) =>
            {

                services.AddTransient<MainPageViewModel>(s=>new MainPageViewModel(s.GetRequiredService<NavigationService<QuizSelectionPopupViewModel>>()));

                services.AddSingleton<CreateViewModel<QuizViewModel, Quiz>>(s=>quiz=>new QuizViewModel(quiz));

                services.AddSingleton<CreateViewModel<QuizQuestionViewModel, Question>>(s =>
                    quizModel => new QuizQuestionViewModel(quizModel));

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
