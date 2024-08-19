using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Navigation;
using BashkirTheatre14.Helpers;
using BashkirTheatre14.Helpers.Logging;
using BashkirTheatre14.HostBuilders;
using BashkirTheatre14.HostBuilders.Navigation;
using BashkirTheatre14.HostBuilders.ViewModels;
using BashkirTheatre14.View.Pages;
using BashkirTheatre14.View.Windows;
using BashkirTheatre14.ViewModel.Pages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MvvmNavigationLib.Services;
using Refit;
using Application = System.Windows.Application;

namespace BashkirTheatre14
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _appHost = CreateHostBuilder().Build();

        public App()
        {
            DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            if (DebugHelper.IsRunningInDebugMode) throw e.Exception;
            var logger = _appHost.Services.GetRequiredService<ILoggingService>();
            var exception = e.Exception;
            if (exception is ApiException)
                logger.Log(exception, "Server Error");
            else
                logger.Log(exception);
            e.Handled = true;
        }

        private static IHostBuilder CreateHostBuilder(string[]? args = null)
        {
            return Host.CreateDefaultBuilder(args)
                .BuildConfiguration()
                .BuildApiStores()
                .BuildMainNavigation()
                .BuildModalNavigation()
                .BuildBodyViewModels()
                .BuildViewModalModels()
                .BuildViews();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            var initialNavigationService = _appHost.Services.GetRequiredService<CompositeNavigationService<MainPageViewModel>>();
            initialNavigationService.Navigate();

            MainWindow = _appHost.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            await _appHost.StartAsync();
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _appHost.StopAsync();
            _appHost.Dispose();
            base.OnExit(e);
        }
    }

}
