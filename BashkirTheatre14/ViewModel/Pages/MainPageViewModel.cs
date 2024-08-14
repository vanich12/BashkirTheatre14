using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BashkirTheatre14.ViewModel.Controls.Map;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmNavigationLib.Services;

namespace BashkirTheatre14.ViewModel.Pages
{
    public partial class MainPageViewModel:BasePageViewModel
    {
        private readonly INavigationService _openQuizSelectionNavigationService;
        private readonly INavigationService _openChronicleNavigationService;
        private readonly INavigationService _openTheatreInfoNavigationService;

        [ObservableProperty] private MapControlViewModel _mapControlViewModel;

        public MainPageViewModel(INavigationService openQuizSelectionNavigationService, INavigationService openChronicleNavigationService,MapControlViewModel mapControlViewModel)
        {
            this._openQuizSelectionNavigationService = openQuizSelectionNavigationService;
            this._openChronicleNavigationService = openChronicleNavigationService;
            _mapControlViewModel = mapControlViewModel;
        }

        [RelayCommand]
        private void ToQuizSelection()
        {
            _openQuizSelectionNavigationService.Navigate();
        }

        [RelayCommand]
        private void ToChronicles()
        {
            _openChronicleNavigationService.Navigate();
        }

        protected override Task Loaded()
        {
            return Task.CompletedTask;
        }

        protected override Task Unloaded()
        {
            return Task.CompletedTask;
        }
    }
}
