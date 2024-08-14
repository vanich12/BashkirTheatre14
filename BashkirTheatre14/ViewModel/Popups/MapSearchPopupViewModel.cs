using System.Collections.ObjectModel;
using BashkirTheatre14.Model.Entities.Map;
using BashkirTheatre14.ViewModel.Controls.Map;
using CustomKeyboard.ViewModels;
using MapControlLib.Models.Repositories;
using MapControlLib.ViewModels;
using MvvmNavigationLib.Services;
using MvvmNavigationLib.Stores;

namespace BashkirTheatre14.ViewModel.Popups
{
    public class MapSearchPopupViewModel:BaseSearchPopupViewModel<SearchMapObjectPopupViewModel,MapObject>
    {
        private readonly IRepository<List<AreaViewModel>> _mapClient;
        private readonly CreateViewModel<SearchMapObjectPopupViewModel, MapObjectPopupViewModel> _searchViewModelFactory;
        private readonly CreateViewModel<MapObjectPopupViewModel, AreaViewModel> _mapObjectViewModelFactory;

        public MapSearchPopupViewModel(
            CloseNavigationService<ModalNavigationStore> closeModalNavigationService, 
            KeyboardControlViewModel keyboardViewModel,
            IRepository<List<AreaViewModel>> mapClient,
            CreateViewModel<SearchMapObjectPopupViewModel,MapObjectPopupViewModel> searchViewModelFactory,
            CreateViewModel<MapObjectPopupViewModel,AreaViewModel> mapObjectViewModelFactory) : base(closeModalNavigationService, keyboardViewModel)
        {
            _mapClient = mapClient;
            _searchViewModelFactory = searchViewModelFactory;
            _mapObjectViewModelFactory = mapObjectViewModelFactory;
        }

        protected override async Task Loaded()
        {
            FilteredItems = new ObservableCollection<SearchMapObjectPopupViewModel>();
            AllItems = new ObservableCollection<SearchMapObjectPopupViewModel>();
            var mapObjects = await _mapClient.Resource.Value;
            foreach (var mapPopup in mapObjects
                         .Where(m=>m is not TerminalAreaViewModel)
                         .Select(mapObject => _mapObjectViewModelFactory(mapObject)))
            {
                await mapPopup.Load();
                var vm = _searchViewModelFactory(mapPopup);
                AllItems.Add(vm);
                AddToFilteredItems(vm);
            }
        }
    }
}
