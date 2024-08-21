using BashkirTheatre14.Model.Entities.Map;
using BashkirTheatre14.ViewModel.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MapControlLib.Components;
using MapControlLib.ViewModels;
using MapControlLib.ViewModels.Messages;
using MvvmNavigationLib.Services;

namespace BashkirTheatre14.ViewModel.Controls.Map
{
    public partial class MapControlViewModel: ObservableObject, IRecipient<MapObjectSelectedMessage>, IRecipient<IsNavigatingChangedMessage>,IRecipient<NavigatedMessage>
    {
        private readonly CreateViewModel<MapObjectPopupViewModel, AreaViewModel> _mapObjectPopupFactory;
        private readonly INavigationService _toSearchMapObjectPopup;

        [ObservableProperty] private MapViewModel<Terminal> _map;
        [ObservableProperty] private MapObjectPopupViewModel? _mapObjectPopup;
        [ObservableProperty] private bool _isLoading;
        public bool ShowOverlay => Map.IsNavigating || (MapObjectPopup?.IsOpen ?? false);

        public MapControlViewModel(IMessenger messenger,MapViewModel<Terminal> map,CreateViewModel<MapObjectPopupViewModel,AreaViewModel> mapObjectPopupFactory,
            INavigationService toSearchMapObjectPopup)
        {
            _map = map;
            _mapObjectPopupFactory = mapObjectPopupFactory;
            _toSearchMapObjectPopup = toSearchMapObjectPopup;
            messenger.RegisterAll(this);
        }

        [RelayCommand]
        private void ToSearchMapObjectPopup()
        {
            _toSearchMapObjectPopup.Navigate();
        }

        public async void Receive(MapObjectSelectedMessage message)
        {
            if (message.Value.NavigationObject is not MapObject) return;
            MapObjectPopup = _mapObjectPopupFactory(message.Value);
            await MapObjectPopup.Load();
            OnPropertyChanged(nameof(ShowOverlay));
        }

        public void Receive(IsNavigatingChangedMessage message)
        {
            OnPropertyChanged(nameof(ShowOverlay));
        }

        public void Receive(NavigatedMessage message)
        {
            if(!message.Value.Any()) return;
            OnPropertyChanged(nameof(ShowOverlay));
        }

        [RelayCommand]
        private async Task LoadMap(ZoomableContentControl zoomableContentControl)
        {
            IsLoading = true;
            await Map.Load(zoomableContentControl);
            IsLoading = false;
        }

        public async Task Unloaded()
        {
            Map.MapNavigation.CancelNavigation();
            await Map.DisposeAsync();
        }
    }
}
