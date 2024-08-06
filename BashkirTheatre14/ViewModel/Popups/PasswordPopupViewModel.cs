using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmNavigationLib.Services;
using Application = System.Windows.Application;

namespace BashkirTheatre14.ViewModel.Popups
{
    public partial class PasswordPopupViewModel:BasePopupViewModel
    {
        private readonly string _password;
        [ObservableProperty] private bool _isPinPadOpen=true;

        private string _currentPassword;
        public string CurrentPassword
        {
            get => _currentPassword;
            set
            {   
                SetProperty(ref _currentPassword, value);
                OnPropertyChanged(nameof(IsValid));
            }
        }

        public bool IsValid => CurrentPassword == _password;

        public PasswordPopupViewModel(INavigationService closeModalNavigationService,string password) : base(closeModalNavigationService)
        {
            _password = password;
            _currentPassword = string.Empty;
        }


        [RelayCommand]
        private void Exit()
        {
            Application.Current.Shutdown();
        }

        [RelayCommand]
        private void RemoveSymbol()
        {
            if (CurrentPassword.Length > 0) CurrentPassword = CurrentPassword[..^1];
            OnPropertyChanged(nameof(IsValid));
        }

        [RelayCommand]
        private void AddSymbol(string symbol)
        {
            CurrentPassword += symbol;
            OnPropertyChanged(nameof(IsValid));
        }

        [RelayCommand]
        private void OpenPinPad()
        {
            IsPinPadOpen = true;
        }

        [RelayCommand]
        private void ClosePinPad()
        {
            IsPinPadOpen = false;
        }

    }
}
