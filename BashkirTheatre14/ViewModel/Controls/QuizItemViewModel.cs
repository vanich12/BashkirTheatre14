using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BashkirTheatre14.Model.Entities;
using BashkirTheatre14.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmNavigationLib.Services;

namespace BashkirTheatre14.ViewModel.Controls
{
    public partial class QuizItemViewModel: BaseControlViewModel
    {
        private IParameterNavigationService<Quiz> _navigationService;
        public Quiz Quiz { get; set; }

        public QuizItemViewModel(IParameterNavigationService<Quiz> navService = null, Quiz quiz = null)
        {
            this.Quiz = quiz;
      
            this. _navigationService = navService;
        }

        [RelayCommand]
        private void SelectQuiz()
        {
            _navigationService.Navigate(Quiz);
        }

        [RelayCommand]
        private void GoToQuestions()
        {
            _navigationService.Navigate(Quiz);
        }

        public override async ValueTask DisposeAsync()
        {
        }

        public override Task Load()
        {
            throw new NotImplementedException();
        }
    }
}
