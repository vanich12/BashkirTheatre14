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
    public class InfoServices(IMainApiClient apiClient, ImageLoadingHttpClient loadingHttpClient,
            CreateViewModel<TheatrInfoViewModel, Info> chronicleFactory)
        : BaseListWebStore<TheatrInfoViewModel>
    {
        protected override async IAsyncEnumerable<TheatrInfoViewModel> GetListAsyncOverride()
        {
            var chroniclesList = await apiClient.GetInfoList();
            foreach (var info in chroniclesList)
            {
                info.LocalImagePath = await loadingHttpClient.DownloadImage(info.ImagePath);
                var chronicleViewModel = chronicleFactory(info);

                yield return chronicleViewModel;
            }
        }
    }
}
