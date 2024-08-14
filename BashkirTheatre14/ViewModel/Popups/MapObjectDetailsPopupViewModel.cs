using CommunityToolkit.Mvvm.ComponentModel;
using MvvmNavigationLib.Services;

namespace BashkirTheatre14.ViewModel.Popups
{
    public partial class MapObjectDetailsPopupViewModel(INavigationService closeModalNavigationService,
            List<string> images)
        : BasePopupViewModel(closeModalNavigationService)
    {
        [ObservableProperty] private List<string> _images = images;
    }
}
