using BashkirTheatre14.Model;
using BashkirTheatre14.Model.Entities;
using BashkirTheatre14.Services;
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
using MvvmNavigationLib.Stores;

namespace BashkirTheatre14.HostBuilders.ViewModels
{
    public static class BuildBodyViewModelsExtensions
    {
        public static IHostBuilder BuildBodyViewModels(this IHostBuilder builder)
        {
            builder.ConfigureServices((context,services) =>
            {
                services.AddTransient<MainPageViewModel>(s=>new MainPageViewModel(s.GetRequiredService<NavigationService<QuizSelectionPopupViewModel>>()));
                
                services.AddSingleton<CreateViewModel<QuizChoiceViewModel, Quiz>>(s =>
                {
                    var parameterNavigationService = s.GetRequiredService<ParameterNavigationService<QuizItemViewModel, Quiz>>();
                    return quiz => new QuizChoiceViewModel(parameterNavigationService, quiz);
                });

                services.AddSingleton<CreateViewModel<QuizAnswerViewModel, Answer>>(s =>
                    quiz => new QuizAnswerViewModel(quiz));
                
                services.AddSingleton<CreateViewModel<QuizResultViewModel, QuizViewModel>>(s =>
                {
                    var navigationService = s.GetRequiredService<NavigationService<MainPageViewModel>>();
                    return quiz => new QuizResultViewModel(navigationService,quiz);
                });  
                
                services.AddSingleton<CreateViewModel<QuizItemViewModel, Quiz>>(s =>
                {
                    var parameterNavigationService = s.GetRequiredService<ParameterNavigationService<QuizViewModel, Quiz>>();
                    return quiz => new QuizItemViewModel(parameterNavigationService, quiz);
                }); 

                services.AddSingleton<CreateViewModel<QuizViewModel, Quiz>>(s =>
                {
                    var parameterNavigationService = s.GetRequiredService<ParameterNavigationService<QuizResultViewModel, QuizViewModel>>();
                    var navigationService = s.GetRequiredService<NavigationService<MainPageViewModel>>();
                    return quiz => new QuizViewModel(parameterNavigationService,navigationService ,quiz);
                });

                //services.AddSingleton<CreateViewModel<QuizAnswerViewModel, Question>>(s =>
                //   quizModel => new QuizAnswerViewModel(quizModel));

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
