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
    public partial class QuizViewModel : ObservableObject
    {
        private readonly IParameterNavigationService<QuizViewModel> _navigationServiceParam;
        private readonly INavigationService _navigationService;

        public int CorrectAnswersCount { get; private set; }
        public QuizModel Quiz { get; }

        [ObservableProperty] private Question? _selectedQuestion;
        [ObservableProperty] private Answer? _selectedAnswer;
        [ObservableProperty] private int _questionIndex;
        public string QuestionTitle { get; set; }
        public bool CanExecuteNextQuestions => NextQuestionsCommand.CanExecute(null);

        private bool HasSelectedAnswer() => SelectedAnswer != null;

        public QuizViewModel(IParameterNavigationService<QuizViewModel> navigationServiceParam, INavigationService navigationService, QuizModel quiz)
        {
            _navigationServiceParam = navigationServiceParam;
            _navigationService = navigationService;
            Quiz = quiz;
            SelectedQuestion = quiz.ToNextQuestion();
        }

        [RelayCommand]
        private void SelectAnswer(Answer answer)
        {
            SelectedAnswer = answer;
            NextQuestionsCommand.NotifyCanExecuteChanged();
            OnPropertyChanged(nameof(CanExecuteNextQuestions));  // Notify UI about change
        }

        private void GoToResult() => _navigationServiceParam.Navigate(this);

        private void CheckAnswer()
        {
            if (SelectedQuestion is null || SelectedAnswer is null) return;
            if (SelectedQuestion.CheckAnswer(SelectedAnswer)) CorrectAnswersCount++;
        }

        [RelayCommand]
        private void GoToMainPage() => _navigationService.Navigate();

        [RelayCommand(CanExecute = nameof(HasSelectedAnswer))]
        private void NextQuestions()
        {
            if (SelectedAnswer == null)
            {
                return;
            }
            CheckAnswer();
            var nextQuestion = Quiz.ToNextQuestion();
            if (nextQuestion is null)
            {
                GoToResult();
                return;
            }

            QuestionIndex++;
            SelectedAnswer = null;
            SelectedQuestion = nextQuestion;

            NextQuestionsCommand.NotifyCanExecuteChanged();
            OnPropertyChanged(nameof(CanExecuteNextQuestions));  // Notify UI about change
        }
    }
}
