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
    public partial class QuizResultViewModel : BaseControlViewModel
    {
        private IParameterNavigationService<Quiz> _navigationServiceParam;

        private INavigationService _navigationService;
        public Quiz Quiz { get; set; }

        [ObservableProperty]
        private QuizViewModel _quizViewModel;

        public int? PointResult { get; set; }

        public QuizResultViewModel(INavigationService navigationService,QuizViewModel quizViewModel)
        {
            this._quizViewModel = quizViewModel;
            this._navigationService = navigationService;
            this.PointResult = _quizViewModel.CorrectAnswer.Count();
        }

        [RelayCommand]
        private void SelectQuiz()
        {
            _navigationServiceParam.Navigate(Quiz);
        }

        [RelayCommand]
        private void GoToMainPage()
        {
            _navigationService.Navigate();
        }

        public override Task Load()
        {
            throw new NotImplementedException();
        }

        [RelayCommand]
        //private void GoToQuestions()
        //{
        //    _navigationService.Navigate(Quiz);
        //}

#pragma warning disable MVVMTK0007 // Invalid RelayCommand method signature
        public override async ValueTask DisposeAsync()
#pragma warning restore MVVMTK0007 // Invalid RelayCommand method signature
        {
        }

    }
}
