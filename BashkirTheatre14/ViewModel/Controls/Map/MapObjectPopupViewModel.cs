using BashkirTheatre14.Model.Entities.Map;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MapControlLib.Models.Clients;
using MapControlLib.Models.Repositories;
using MapControlLib.ViewModels;
using MvvmNavigationLib.Services;

namespace BashkirTheatre14.ViewModel.Controls.Map
{
    public partial class MapObjectPopupViewModel:ObservableObject
    {
        [ObservableProperty] private AreaViewModel _area;
        [ObservableProperty] private bool _isOpen = true;
        [ObservableProperty] private List<string>? _images;
        [ObservableProperty] private bool _isDetailsOpen;
        [ObservableProperty] private MapObject? _mapObject;
        [ObservableProperty] private bool _isLoaded;
        

        private readonly MapNavigationService<Terminal> _mapNavigation;
        private readonly MapImageLoadingClient _mapImageLoadingClient;
        private readonly IParameterNavigationService<List<string>> _toDetailsNavigationService;
        public bool IsNotInfrastructure => !MapObject?.IsInfrastructure??false;
        public bool HasPictures => (Images?.Any() ?? false)&&IsNotInfrastructure;

        public MapObjectPopupViewModel(AreaViewModel area,MapNavigationService<Terminal> mapNavigation,MapImageLoadingClient mapImageLoadingClient,
            IParameterNavigationService<List<string>> toDetailsNavigationService)
        {
            _area = area;
            if (_area.NavigationObject is MapObject mapObject) _mapObject = mapObject;
            _mapNavigation = mapNavigation;
            _mapImageLoadingClient = mapImageLoadingClient;
            _toDetailsNavigationService = toDetailsNavigationService;
        }

       
        public async Task Load()
        {
            if(MapObject is null) return;
            var images = new List<string>();
            foreach (var image in MapObject.Images)
            {
                var imagePath =await _mapImageLoadingClient.DownloadImage(image.Url);
                if(imagePath is null) continue;
                images.Add(imagePath);
            }

            IsLoaded = true;
            Images = images;
            OnPropertyChanged(nameof(HasPictures));
        }

        [RelayCommand]
        private void Close()
        {
            IsOpen = false;
            _mapNavigation.CancelNavigation();
        }

        [RelayCommand]
        private void ShowDetails()
        {
            
            IsDetailsOpen = !IsDetailsOpen;
        }

        [RelayCommand]
        private void ShowImages()
        {
            _toDetailsNavigationService.Navigate(Images!);
        }

        [RelayCommand]
        private async Task Navigate()
        {
            IsOpen = false;
            if(MapObject is null) return;
            await _mapNavigation.Navigate(MapObject);
        }
    }
}
