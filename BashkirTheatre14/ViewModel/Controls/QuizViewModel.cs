using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BashkirTheatre14.Model.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BashkirTheatre14.ViewModel.Controls
{
    public partial class QuizViewModel:BaseControlViewModel
    {
        public Quiz Quiz { get; }
        [ObservableProperty] private bool _isSelected;

        public QuizViewModel(Quiz quiz)
        {
            Quiz = quiz;
        }

        public override async ValueTask DisposeAsync()
        {
        }

        [RelayCommand]
        private void SelectQuiz()
        {
            IsSelected = true;
        }
        public override Task Load()
        {
            return Task.CompletedTask;
        }
    }
}
