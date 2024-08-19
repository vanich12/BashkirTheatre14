using BashkirTheatre14.Model;
using BashkirTheatre14.Model.Entities;
using BashkirTheatre14.ViewModel.Controls;
using MvvmNavigationLib.Services;

namespace BashkirTheatre14.Services
{
    public class ChronicleService(IMainApiClient apiClient, ImageLoadingHttpClient loadingHttpClient,
            CreateViewModel<ChronicleViewModel, Chronicle> chronicleFactory)
        : BaseListWebStore<ChronicleViewModel>
    {
        protected override async IAsyncEnumerable<ChronicleViewModel> GetListAsyncOverride()
        {
            var chroniclesList = await apiClient.GetChroniclesList();
            foreach (var chronicle in chroniclesList)
            {
                chronicle.LocalImagePath = await loadingHttpClient.DownloadImage(chronicle.ImagePath);
                var chronicleViewModel = chronicleFactory(chronicle);
                
                yield return chronicleViewModel;
            }
        }
    }
}
