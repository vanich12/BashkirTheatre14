using BashkirTheatre14.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using MvvmNavigationLib.Services;

namespace BashkirTheatre14.ViewModel.Controls
{
    public abstract partial class BaseSearchItemViewModel<TEntity>(
            INavigationService closeModalNavigationService,
            TEntity entity)
        : ObservableObject
        where TEntity : ISearchable
    {
        protected readonly INavigationService CloseModalNavigationService = closeModalNavigationService;
        [ObservableProperty] private TEntity _entity = entity;
    }
}
