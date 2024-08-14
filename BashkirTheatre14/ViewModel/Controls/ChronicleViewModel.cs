using BashkirTheatre14.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BashkirTheatre14.ViewModel.Controls
{
    public partial class ChronicleViewModel: BaseControlViewModel
    {
        public Chronicle Chronicle { get; set; }

        [ObservableProperty]
        private string _imagePath;


        public ChronicleViewModel(Chronicle chronicle)
        {
            this.Chronicle = chronicle;
            ImagePath = chronicle.LocalImagePath;
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
