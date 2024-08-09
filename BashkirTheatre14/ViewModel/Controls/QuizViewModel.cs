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
    public partial class QuizViewModel:BaseControlViewModel
    {
        public Quiz Quiz { get; }
        private readonly QuizService _quizService;
        [ObservableProperty] private bool _isSelected;
        [ObservableProperty] private ObservableCollection<QuizQuestionViewModel> _questionList = new();
        [ObservableProperty]
        private QuizQuestionViewModel _selectedQuestion;

        public QuizViewModel(Quiz quiz, QuizService quizService)
        {
            Quiz = quiz;
            _quizService = quizService;
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
