using System.Collections.ObjectModel;
using BashkirTheatre14.Model.Entities;
using BashkirTheatre14.Services;
using BashkirTheatre14.View.Controls;
using BashkirTheatre14.ViewModel.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmNavigationLib.Services;
using ContentSliderUserControl = BashkirTheatre14.View.Components.ContentSliderUserControl;

namespace BashkirTheatre14.ViewModel.Pages
{
    public enum AnimTrigger
    {
        None,
        Left,
        Right
    }

    public enum LastAnim
    {
        None,
        NotLeftLast,
        NotRightLast,
        PreLast,
        LeftLast,
        RightLast
    }
    public partial class ChroniclesPageViewModel:BasePageViewModel
    {
        private readonly ChronicleService _chronicleService;
        private CancellationTokenSource? _cancellationTokenSource;
        [ObservableProperty] private ObservableCollection<ChronicleViewModel> _chroniclesList = new();
        [ObservableProperty] private ChronicleViewModel _currentChronicle;
        [ObservableProperty] private AnimTrigger _currentAnim;
        [ObservableProperty] private LastAnim _isLast;
        [ObservableProperty] private ChronicleViewModel _newModel;
        private INavigationService _navigationService;
        public ChroniclesPageViewModel(ChronicleService chronicleService, INavigationService navigationService)
        {
            this._chronicleService = chronicleService;
            this._navigationService = navigationService;
            IsLast = LastAnim.LeftLast;
        }

        protected override async Task Loaded()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            ChroniclesList.Clear();
            try
            {
                bool isFirstItem = true;
                await foreach (var chronicle in _chronicleService.WithCancellation(_cancellationTokenSource.Token))
                {
                    ChroniclesList.Add(chronicle);

                    if (isFirstItem)
                    {
                        CurrentChronicle = chronicle;
                        isFirstItem = false;
                    }
                }
            }
            catch (OperationCanceledException)
            {
               
            }
        }


        [RelayCommand]
        private async Task SlideLeft(ContentSliderUserControl slider)
        {
            slider.LeftCommand.Execute(null);
            if (IsLast != LastAnim.RightLast)
                IsLast = slider.CurrentItemIndex - 1 >= 0 ? LastAnim.NotLeftLast : LastAnim.PreLast;
            CurrentAnim = AnimTrigger.Left;
            NewModel = (ChronicleViewModel)slider.CurrentItem;
            await Task.Delay(550);
            CurrentChronicle = (ChronicleViewModel)slider.CurrentItem;
            CurrentAnim = AnimTrigger.None;
            IsLast = slider.CurrentItemIndex - 1 >= 0 ? LastAnim.None : LastAnim.LeftLast;
        }

        [RelayCommand]
        private async Task SlideRight(ContentSliderUserControl slider)
        {
            slider.RightCommand.Execute(null);
            if (IsLast != LastAnim.LeftLast)
                IsLast = slider.CurrentItemIndex + 1 < ChroniclesList.Count ? LastAnim.NotRightLast : LastAnim.PreLast;
            CurrentAnim = AnimTrigger.Right;
            NewModel = (ChronicleViewModel)slider.CurrentItem;
            await Task.Delay(550);
            CurrentChronicle = (ChronicleViewModel)slider.CurrentItem;
            CurrentAnim = AnimTrigger.None;
            IsLast = slider.CurrentItemIndex + 1 < ChroniclesList.Count ? LastAnim.None : LastAnim.RightLast;
        }

        [RelayCommand]
        private void NavigateTo()
        {
          _navigationService.Navigate();  
        }

        protected override Task Unloaded()
        {
            return Task.CompletedTask;
        }
    }
}
