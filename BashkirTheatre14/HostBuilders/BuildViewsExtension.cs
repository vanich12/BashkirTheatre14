using BashkirTheatre14.Helpers;
using BashkirTheatre14.View.Windows;
using BashkirTheatre14.ViewModel.Windows;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BashkirTheatre14.HostBuilders
{
    public static class BuildViewsExtension
    {
        public static IHostBuilder BuildViews(this IHostBuilder builder)
        {
            builder.ConfigureServices((context,services) =>
            {
                services.AddSingleton<IMessenger>(s => new WeakReferenceMessenger());

                services.AddSingleton<InactivityHelper>(s=>new InactivityHelper(context.Configuration.GetValue<int>("inactivityTime")));
                services.AddSingleton<PasswordInactivityHelper>(s=>new PasswordInactivityHelper(context.Configuration.GetValue<int>("passwordInactivityTime")));

                services.AddSingleton(s => new MainWindow()
                {
                    DataContext = s.GetRequiredService<MainWindowViewModel>()
                });

            });
            return builder;
        }
    }
}
