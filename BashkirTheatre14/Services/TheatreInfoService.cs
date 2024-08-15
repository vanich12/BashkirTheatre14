using BashkirTheatre14.Model.Entities;
using BashkirTheatre14.Model;
using BashkirTheatre14.ViewModel.Controls;
using MvvmNavigationLib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashkirTheatre14.Services
{
    public partial class TheatreInfoService: BaseSingleWebStore<TheatrInfoViewModel>
    {
        private readonly IMainApiClient _apiClient;
        private readonly ImageLoadingHttpClient _loadingHttpClient;

        public TheatreInfoService(IMainApiClient apiClient, ImageLoadingHttpClient loadingHttpClient)
        {
            _apiClient = apiClient;
            _loadingHttpClient = loadingHttpClient;
        }


        protected override async Task<TheatrInfoViewModel?> GetSingleOrDefaultAsyncOverride(params object[] args)
        {
            var info = await _apiClient.GetInfo();

            if (info != null)
            {
                var theatrInfoViewModel = new TheatrInfoViewModel(info);

                return theatrInfoViewModel;
            }
            return null;
        }


    }
}
