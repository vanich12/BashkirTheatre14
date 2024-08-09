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

        public QuizViewModel(Quiz quiz)
        {
            Quiz = quiz;
            foreach (Question quizQuestion in Quiz.Questions)
            {
                QuestionList.Add(new QuizQuestionViewModel(quizQuestion));
            }
        }

        public override async ValueTask DisposeAsync()
        {
            
        }
        
        [RelayCommand]
        private void SelectQuiz()
        {
            IsSelected = true;
        }

        [RelayCommand]
        private void NextQuestions()
        {
            _selectedQuestion = QuestionList[0];
        }

        public override Task Load()
        {
            return Task.CompletedTask;
        }
    }
}
