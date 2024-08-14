using System.Collections.ObjectModel;
using BashkirTheatre14.Model;
using BashkirTheatre14.ViewModel.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CustomKeyboard.ViewModels;
using MvvmNavigationLib.Services;

namespace BashkirTheatre14.ViewModel.Popups
{
    public abstract partial class BaseSearchPopupViewModel<TSearchItemViewModel,TEntity>:BasePopupViewModel
    where TSearchItemViewModel:BaseSearchItemViewModel<TEntity>
    where TEntity:ISearchable
    {
        protected readonly CancellationTokenSource CancellationTokenSource = new();

        [ObservableProperty] private string? _searchText;
        [ObservableProperty] private KeyboardControlViewModel _keyboardViewModel;
        [ObservableProperty] private ObservableCollection<TSearchItemViewModel>? _allItems;
        [ObservableProperty] private ObservableCollection<TSearchItemViewModel>? _filteredItems;

        protected BaseSearchPopupViewModel(INavigationService closeModalNavigationService,KeyboardControlViewModel keyboardViewModel) : base(closeModalNavigationService)
        {
            _keyboardViewModel = keyboardViewModel;
        }

        partial void OnSearchTextChanged(string? value)
        {
            if (AllItems is null) return;
            if (string.IsNullOrEmpty(value)) FilteredItems = new ObservableCollection<TSearchItemViewModel>(AllItems);
            else FilterItems(value);
        }

        private void FilterItems(string searchText)
        {
            if(AllItems is null) return;
            FilteredItems ??= new ObservableCollection<TSearchItemViewModel>();
            foreach (var item in AllItems)
            {
                var isFilteredContainsItem = FilteredItems.Contains(item);
                var isMatch = item.Entity.SearchProperty.Contains(searchText, StringComparison.OrdinalIgnoreCase);

                switch (isMatch)
                {
                    case true when !isFilteredContainsItem:
                        FilteredItems.Add(item);
                        continue;
                    case false when isFilteredContainsItem:
                        FilteredItems.Remove(item);
                        break;
                }
            }
        }

        protected void AddToFilteredItems(TSearchItemViewModel item)
        {
            if (string.IsNullOrEmpty(SearchText)
                || item.Entity.SearchProperty.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                FilteredItems?.Add(item);
        }

        [RelayCommand]
        protected abstract Task Loaded();

        [RelayCommand]
        private async Task Unloaded()
        {
            await CancellationTokenSource.CancelAsync();
            CancellationTokenSource.Dispose();
        }
    }
}
