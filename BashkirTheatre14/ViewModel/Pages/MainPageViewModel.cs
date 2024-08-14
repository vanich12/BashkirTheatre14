using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using MvvmNavigationLib.Services;

namespace BashkirTheatre14.ViewModel.Pages
{
    public partial class MainPageViewModel:BasePageViewModel
    {
        private readonly INavigationService _openQuizSelectionNavigationService;
        private readonly INavigationService _openChronicleNavigationService;
        private readonly INavigationService _openTheatreInfoNavigationService;

        public MainPageViewModel(INavigationService openQuizSelectionNavigationService, INavigationService openChronicleNavigationService)
        {
            this._openQuizSelectionNavigationService = openQuizSelectionNavigationService;
            this._openChronicleNavigationService = openChronicleNavigationService;
        }

        [RelayCommand]
        private void ToQuizSelection()
        {
            _openQuizSelectionNavigationService.Navigate();
        }

        [RelayCommand]
        private void ToChonicles()
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
