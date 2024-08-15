using BashkirTheatre14.Model.Entities;
using BashkirTheatre14.Model;
using BashkirTheatre14.ViewModel.Controls;
using MvvmNavigationLib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BashkirTheatre14.ViewModel.Pages;

namespace BashkirTheatre14.Services
{
    public class InfoServices:BaseListWebStore<TheatrInfoViewModel>
    {
        private readonly IMainApiClient _apiClient;
        private readonly ImageLoadingHttpClient _loadingHttpClient;
        private readonly CreateViewModel<TheatrInfoViewModel, Info> _chroniclesFactory;

        public InfoServices(IMainApiClient apiClient, ImageLoadingHttpClient loadingHttpClient,
            CreateViewModel<TheatrInfoViewModel, Info> chronicleFactory)
        {
            _apiClient = apiClient;
            _loadingHttpClient = loadingHttpClient;
            _chroniclesFactory = chronicleFactory;
        }

        protected override async IAsyncEnumerable<TheatrInfoViewModel> GetListAsyncOverride(params object[] args)
        {
            var chroniclesList = await _apiClient.GetInfoList();
            foreach (var info in chroniclesList)
            {
                info.LocalImagePath = await _loadingHttpClient.DownloadImage(info.ImagePath);
                var chronicleViewModel = _chroniclesFactory(info);

                yield return chronicleViewModel;
            }
        }
    }
}
