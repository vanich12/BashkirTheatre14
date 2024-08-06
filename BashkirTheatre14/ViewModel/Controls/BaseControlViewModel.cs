using CommunityToolkit.Mvvm.ComponentModel;

namespace BashkirTheatre14.ViewModel.Controls
{
    public abstract class BaseControlViewModel:ObservableObject,IAsyncDisposable
    {
        public abstract ValueTask DisposeAsync();
        public abstract Task Load();
    }
}
