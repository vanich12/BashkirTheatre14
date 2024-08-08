using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BashkirTheatre14.Model.Entities;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BashkirTheatre14.Model
{
    public partial class QuizModel:ObservableObject
    {
        public string Title { get; set; }

        public string Description { get; set; }

        [ObservableProperty] private ObservableCollection<Question> _questions;
    }
}
