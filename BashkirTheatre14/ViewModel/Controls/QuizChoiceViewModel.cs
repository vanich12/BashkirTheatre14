using BashkirTheatre14.Model.Entities;
using CommunityToolkit.Mvvm.Input;
using MvvmNavigationLib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashkirTheatre14.ViewModel.Controls
{
    public partial class QuizChoiceViewModel : BaseControlViewModel
    {
        private IParameterNavigationService<Quiz> _navigationService;
        public Quiz Quiz { get; set; }

        public QuizChoiceViewModel(IParameterNavigationService<Quiz> navService = null, Quiz quiz = null)
        {
            this.Quiz = quiz;

            this._navigationService = navService;
        }

        [RelayCommand]
        private void SelectQuiz()
        {
            _navigationService.Navigate(Quiz);
        }

        public override Task Load()
        {
            throw new NotImplementedException();
        }

        [RelayCommand]
        private void GoToQuestions()
        {
            _navigationService.Navigate(Quiz);
        }

        public override async ValueTask DisposeAsync()
        {
        }
    }
}
