using BashkirTheatre14.Model.Entities;
using BashkirTheatre14.Model;
using BashkirTheatre14.ViewModel.Controls;
using MvvmNavigationLib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BashkirTheatre14.Model;
using BashkirTheatre14.ViewModel.Pages;

namespace BashkirTheatre14.Services
{
    public class ChronicleService: BaseListWebStore<ChronicleViewModel>
    {
        private readonly IMainApiClient _apiClient;
        private readonly ImageLoadingHttpClient _loadingHttpClient;
        private readonly CreateViewModel<ChronicleViewModel, Chronicle> _chroniclesFactory;

        public ChronicleService(IMainApiClient apiClient, ImageLoadingHttpClient loadingHttpClient,
            CreateViewModel<ChronicleViewModel, Chronicle> chronicleFactory)
        {
            _apiClient = apiClient;
            _loadingHttpClient = loadingHttpClient;
            _chroniclesFactory = chronicleFactory;
        }

        protected override async IAsyncEnumerable<ChronicleViewModel> GetListAsyncOverride(params object[] args)
        {
            var chroniclesList = await _apiClient.GetChroniclesList();
            foreach (var chronicle in chroniclesList)
            {
                chronicle.LocalImagePath = await _loadingHttpClient.DownloadImage(chronicle.ImagePath);
                var chronicleViewModel = _chroniclesFactory(chronicle);
                
                yield return chronicleViewModel;
            }
        }
    }
}
