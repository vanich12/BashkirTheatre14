using BashkirTheatre14.Model.Entities;
using BashkirTheatre14.Services;
using BashkirTheatre14.ViewModel.Pages;
using BashkirTheatre14.ViewModel.Popups;
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
                        s.GetRequiredService<ParameterNavigationService<QuizQuestionViewModel, IReadOnlyList<Question>>>()));
            });
            return builder;
        }
    }
}
