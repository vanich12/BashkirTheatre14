using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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
        private IParameterNavigationService<QuizViewModel> _navigationServiceParam;
        private INavigationService _navigationService;
        public Quiz Quiz { get; }
        [ObservableProperty] private ObservableCollection<Question> _questionList = new();
        [ObservableProperty] private ObservableCollection<QuizAnswerViewModel> _quizAnswerList = new();
        [ObservableProperty] private Question? _selectedQuestion;
        [ObservableProperty] private QuizAnswerViewModel? _selectedAnswer;
        public int? QuestionIndex { get; set; }

        public List<QuizAnswerViewModel> CorrectAnswer { get; set; } = new();

        public QuizViewModel(IParameterNavigationService<QuizViewModel> navigationServiceParam, INavigationService navigationService ,Quiz quiz)
        {
           this._navigationServiceParam = navigationServiceParam;
           this._navigationService = navigationService;

            Quiz = quiz;
            foreach (Question quizQuestion in Quiz.Questions)
            {
                QuestionList.Add(quizQuestion);
            }

            SelectedQuestion = QuestionList[0];
            QuestionIndex = QuestionList.IndexOf(SelectedQuestion);

            foreach (var answer in SelectedQuestion.Answers)
            {
                QuizAnswerList.Add(new QuizAnswerViewModel(answer));
            }
        }

        public override async ValueTask DisposeAsync()
        {
        }

        [RelayCommand]
        private void SelectAnswer(QuizAnswerViewModel answer)
        {
            if (SelectedAnswer!=null)
            {
                SelectedAnswer.IsSelected = false;
                SelectedAnswer = null;
            }
            SelectedAnswer= answer;
            SelectedAnswer.IsSelected = true;
        }

        private void GoToResult()
        {
            _navigationServiceParam.Navigate(this);
        }

        [RelayCommand]
        private void GoToMainPage()
        {
            _navigationService.Navigate();
        }

        [RelayCommand]
        private void NextQuestions()
        {
            if (QuestionIndex == null)
            {
                QuestionIndex = 0;
            }
            else if (SelectedAnswer is not null && (QuestionIndex < QuestionList.Count - 1)  )
            {
                if (SelectedAnswer.Answer.Correct)
                {
                    CorrectAnswer.Add(SelectedAnswer);
                }
                QuestionIndex++;
                SelectedAnswer = null;
            }

            if (QuestionIndex < QuestionList.Count)
            {
                SelectedQuestion = QuestionList[QuestionIndex.Value];
                QuizAnswerList.Clear();
                foreach (var answer in SelectedQuestion.Answers)
                {
                    QuizAnswerList.Add(new QuizAnswerViewModel(answer));
                }
            }

            if (QuestionIndex ==QuestionList.Count - 1)
            {
                GoToResult();
            }
        }

        public override Task Load() => Task.CompletedTask;

    }
}