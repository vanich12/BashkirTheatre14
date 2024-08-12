﻿using BashkirTheatre14.Model.Entities;
using CommunityToolkit.Mvvm.Input;
using MvvmNavigationLib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashkirTheatre14.ViewModel.Controls
{
    public partial class QuizResultViewModel : BaseControlViewModel
    {
        private IParameterNavigationService<Quiz> _navigationService;
        public Quiz Quiz { get; set; }
        private QuizViewModel _quizViewModel;

        public int? PointResult { get; set; }

        public QuizResultViewModel(QuizViewModel quizViewModel = null)
        {
            this._quizViewModel = quizViewModel;

            this.PointResult = _quizViewModel.CorrectAnswer.Count();
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
