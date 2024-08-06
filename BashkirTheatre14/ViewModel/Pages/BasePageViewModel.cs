using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BashkirTheatre14.ViewModel.Pages
{
    public abstract partial class BasePageViewModel:ObservableObject
    {
        [RelayCommand]
        protected abstract Task Loaded();

        [RelayCommand]
        protected abstract Task Unloaded();

        
    }
}
