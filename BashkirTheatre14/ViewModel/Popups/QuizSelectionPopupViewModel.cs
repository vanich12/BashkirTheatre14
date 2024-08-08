using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BashkirTheatre14.Model.Entities;
using BashkirTheatre14.Services;
using BashkirTheatre14.ViewModel.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmNavigationLib.Services;

namespace BashkirTheatre14.ViewModel.Popups
{
    public partial class QuizSelectionPopupViewModel:BasePopupViewModel
    {
        private readonly QuizService _quizService;
        private CancellationTokenSource? _cancellationTokenSource;
        [ObservableProperty] private ObservableCollection<QuizViewModel> _quizList = new();
        private IParameterNavigationService<IReadOnlyList<Question>> _parameterNavigationService;

        public QuizSelectionPopupViewModel(INavigationService closeModalNavigationService,QuizService quizService, IParameterNavigationService<IReadOnlyList<Question>> param) : base(closeModalNavigationService)
        {
            _quizService = quizService;
            _parameterNavigationService = param;
        }

        [RelayCommand]
        private async Task Loaded()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            QuizList.Clear();
            try
            {
                await foreach (var quiz in _quizService.GetListAsync(_cancellationTokenSource.Token))
                {
                    QuizList.Add(quiz);
                }
            }
            catch (OperationCanceledException)
            {
            }
        }

        [RelayCommand]
        private async Task Unloaded()
        {
            if(_cancellationTokenSource is null) return;
            await _cancellationTokenSource.CancelAsync();
            _cancellationTokenSource.Dispose();
        }

        [RelayCommand]
        private void SelectQuiz()
        {
            var selectViewModel = QuizList.First(x => x.IsSelected);
            _parameterNavigationService.Navigate(selectViewModel.Quiz.Questions);
        }
    }
}
