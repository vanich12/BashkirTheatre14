using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmNavigationLib.Services;

namespace BashkirTheatre14.ViewModel.Popups
{
    public partial class BasePopupViewModel:ObservableObject
    {
        [ObservableProperty] private bool _toClose;
        [ObservableProperty] private bool _toCloseWithModal;
        private readonly INavigationService _closeModalNavigationService;

        public BasePopupViewModel(INavigationService closeModalNavigationService)
        {
            _closeModalNavigationService = closeModalNavigationService;
        }

        protected virtual void OnClosed(){}

        [RelayCommand]
        private void Close()
        {
            if(ToCloseWithModal)
                _closeModalNavigationService.Navigate();
            OnClosed();
        }

        [RelayCommand]
        private void CloseContainer(object withoutModal)
        {
            if (withoutModal is not true)
                ToCloseWithModal = true;
            else
                ToClose = true;
        }
    }
}
