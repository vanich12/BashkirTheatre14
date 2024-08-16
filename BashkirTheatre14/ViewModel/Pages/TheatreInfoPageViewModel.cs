using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BashkirTheatre14.Model.Entities;
using BashkirTheatre14.Services;
using BashkirTheatre14.ViewModel.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmNavigationLib.Services;

namespace BashkirTheatre14.ViewModel.Pages
{
    public partial class TheatreInfoPageViewModel:BasePageViewModel
    {
        private readonly InfoServices _infoService;
        private INavigationService _navigationService;
        private CancellationTokenSource? _cancellationTokenSource;
        [ObservableProperty] private TheatrInfoViewModel _currentInfo;
        [ObservableProperty] private ObservableCollection<TheatrInfoViewModel> _theatrInfos = new();
        public TheatreInfoPageViewModel(InfoServices infoSercie, INavigationService navigationService)
        {
            this._infoService = infoSercie;
            this._navigationService = navigationService;    
        }

        protected override async Task Loaded()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            try
            {
                await foreach (var info in _infoService.WithCancellation(_cancellationTokenSource.Token))
                {
                    TheatrInfos.Add(info);
                    if (TheatrInfos.Any() && CurrentInfo == null)
                    {
                        CurrentInfo = TheatrInfos[0];
                    }

                    await Task.Delay(100);
                }
                
            }
            catch (OperationCanceledException)
            {
            }
        }

        [RelayCommand]
        private void NavigateToMain()
        {
            _navigationService.Navigate();
        }

        protected override Task Unloaded()
        {
            return Task.CompletedTask;
        }
    }
}
