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

        public MainPageViewModel(INavigationService openQuizSelectionNavigationService)
        {
            _openQuizSelectionNavigationService = openQuizSelectionNavigationService;
        }

        [RelayCommand]
        private void ToQuizSelection()
        {
            _openQuizSelectionNavigationService.Navigate();
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
