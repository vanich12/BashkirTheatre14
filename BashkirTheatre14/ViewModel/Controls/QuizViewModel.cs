using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using BashkirTheatre14.Model.Entities;
using BashkirTheatre14.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmNavigationLib.Services;

namespace BashkirTheatre14.ViewModel.Controls
{
    public partial class QuizViewModel : BaseControlViewModel
    {
        private IParameterNavigationService<QuizViewModel> _navigationService;
        public Quiz Quiz { get; }
        private readonly QuizService _quizService;
        [ObservableProperty] private ObservableCollection<Question> _questionList = new();
        [ObservableProperty] private Question? _selectedQuestion;
        [ObservableProperty] private Answer? _selectedAnswer;
        public int? QuestionIndex { get; set; }
        public List<Answer> CorrectAnswer { get; set; } = new();

        public QuizViewModel(IParameterNavigationService<QuizViewModel> navigationService ,Quiz quiz)
        {
           this._navigationService = navigationService;
            Quiz = quiz;
            foreach (Question quizQuestion in Quiz.Questions)
            {
                QuestionList.Add(quizQuestion);
            }

            SelectedQuestion = QuestionList[0];
            QuestionIndex = QuestionList.IndexOf(SelectedQuestion);
        }

        public override async ValueTask DisposeAsync()
        {
        }

        [RelayCommand]
        private void SelectAnswer(Answer answer)
        {
            this._selectedAnswer = answer;
        }

        private void GoToResult()
        {
            _navigationService.Navigate(this);
        }

        [RelayCommand]
        private void NextQuestions()
        {
            if (QuestionIndex == null)
            {
                QuestionIndex = 0;
            }
            else if ((QuestionIndex < QuestionList.Count - 1) )
            {
                if (SelectedAnswer is not null && SelectedAnswer.Correct)
                {
                    CorrectAnswer.Add(SelectedAnswer);
                }
                QuestionIndex++;
            }

            if (QuestionIndex < QuestionList.Count)
            {
                SelectedQuestion = QuestionList[QuestionIndex.Value];
            }

            if (QuestionIndex ==QuestionList.Count - 1)
            {
                GoToResult();
            }
        }

        public override Task Load()
        {
            return Task.CompletedTask;
        }
    }
}