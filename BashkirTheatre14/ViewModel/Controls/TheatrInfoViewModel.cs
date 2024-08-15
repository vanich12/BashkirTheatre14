using BashkirTheatre14.Model.Entities;
using CommunityToolkit.Mvvm.Input;
using MvvmNavigationLib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BashkirTheatre14.ViewModel.Controls
{
    public partial class TheatrInfoViewModel:BaseControlViewModel
    {

        public Info Info { get; set; }

        [ObservableProperty] private string _imagePath;

        public TheatrInfoViewModel(Info info)
        {
            this.Info = info;
            ImagePath = info.LocalImagePath;
        }

        public override ValueTask DisposeAsync()
        {
            throw new NotImplementedException();
        }

        public override Task Load()
        {
            throw new NotImplementedException();
        }
    }
}
