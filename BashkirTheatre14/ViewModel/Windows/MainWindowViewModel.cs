using System.Windows.Threading;
using BashkirTheatre14.Helpers;
using BashkirTheatre14.ViewModel.Pages;
using BashkirTheatre14.ViewModel.Popups;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MvvmNavigationLib.Services;
using MvvmNavigationLib.Stores;
using MvvmNavigationLib.Stores.Messages;
using ObservableObject = CommunityToolkit.Mvvm.ComponentModel.ObservableObject;


namespace BashkirTheatre14.ViewModel.Windows
{
    public partial class MainWindowViewModel:ObservableObject,IRecipient<ViewModelChangedMessage>,IRecipient<ModalViewModelChangedMessage>
    {
        private readonly DispatcherTimer _timer = new();
        private int _sec;

        private readonly NavigationStore _navigationStore;
        private readonly InactivityHelper _inactivityHelper;
        private readonly PasswordInactivityHelper _passwordInactivityHelper;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly NavigationService<MainPageViewModel> _initialNavigationService;
        private readonly NavigationService<PasswordPopupViewModel> _passwordNavigationService;


        public ObservableObject? CurrentViewModel => _navigationStore.CurrentViewModel;
        public ObservableObject? CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;
        public bool IsModalOpen => _modalNavigationStore.IsOpen;

        public MainWindowViewModel(IMessenger messenger,NavigationStore navigationStore, InactivityHelper inactivityHelper, PasswordInactivityHelper passwordInactivityHelper,
            ModalNavigationStore modalNavigationStore, NavigationService<MainPageViewModel> initialNavigationService,
            NavigationService<PasswordPopupViewModel> passwordNavigationService)
        {
            _navigationStore = navigationStore;
            _inactivityHelper = inactivityHelper;
            _passwordInactivityHelper = passwordInactivityHelper;
            _inactivityHelper.OnInactivity += _inactivityHelper_OnInactivity;
            _passwordInactivityHelper.OnInactivity += _passwordInactivityHelper_OnInactivity;
            _modalNavigationStore = modalNavigationStore;
            _initialNavigationService = initialNavigationService;
            _passwordNavigationService = passwordNavigationService;
            messenger.RegisterAll(this);
        }

        private void _passwordInactivityHelper_OnInactivity(int inactivityTime)
        {
            if (CurrentModalViewModel is PasswordPopupViewModel passwordPopup)
                passwordPopup.CloseContainerCommand.Execute(false);
        }

        private void _inactivityHelper_OnInactivity(int inactivityTime)
        {
            _initialNavigationService.Navigate();
            
        }

        private void Timer(object sender, EventArgs eventArgs)
        {
            _sec++;
            if (_sec < 7) return;
            _passwordNavigationService.Navigate();

        }


        [RelayCommand]
        private void Loaded()
        {
            ExplorerHelper.KillExplorer();
        }

        [RelayCommand]
        private void Closing()
        {
            ExplorerHelper.RunExplorer();
        }

        [RelayCommand]
        private void StopTimer()
        {
            _timer.Tick -= Timer;
            _timer.Stop();
            _sec = 0;
        }

        [RelayCommand]
        private void StartTimer()
        {
            _timer?.Stop();
            _sec = 0;
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer;
            _timer.Start();
        }

        public void Receive(ViewModelChangedMessage message)
        {
            OnPropertyChanged(nameof(CurrentViewModel));   
        }

        public void Receive(ModalViewModelChangedMessage message)
        {
            OnPropertyChanged(nameof(CurrentModalViewModel));
            OnPropertyChanged(nameof(IsModalOpen));
        }
    }
}
