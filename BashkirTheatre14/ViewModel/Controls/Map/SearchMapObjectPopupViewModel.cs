using BashkirTheatre14.Model.Entities.Map;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MapControlLib.ViewModels;
using MvvmNavigationLib.Services;

namespace BashkirTheatre14.ViewModel.Controls.Map
{
    public partial class SearchMapObjectPopupViewModel(
            INavigationService closeModalNavigationService, 
            MapObjectPopupViewModel mapObjectPopupViewModel,
            IMessenger messenger) 
        :BaseSearchItemViewModel<MapObject>(closeModalNavigationService, mapObjectPopupViewModel.MapObject)
    {
        public MapObjectPopupViewModel MapObjectPopupViewModel { get; } = mapObjectPopupViewModel;
        [ObservableProperty] private string? _image = mapObjectPopupViewModel.Images?.FirstOrDefault();

        [RelayCommand]
        private async Task Navigate()
        {
            closeModalNavigationService.Navigate();
            await MapObjectPopupViewModel.NavigateCommand.ExecuteAsync(null);
        }

        [RelayCommand]
        private void ShowDetails()
        {
            closeModalNavigationService.Navigate();
            messenger.Send(new MapObjectSelectedMessage(MapObjectPopupViewModel.Area));
        }
    }
}
