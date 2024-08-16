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
    public partial class QuizSelectionPopupViewModel(INavigationService closeModalNavigationService,
            QuizService quizService,
            CreateViewModel<QuizChoiceViewModel, QuizModel> quizItemFactory)
        : BasePopupViewModel(closeModalNavigationService)
    {
        private CancellationTokenSource? _cancellationTokenSource;
        [ObservableProperty] private ObservableCollection<QuizChoiceViewModel> _quizList = new();
        [ObservableProperty] private QuizChoiceViewModel? _selectedQuiz;

        [RelayCommand]
        private async Task Loaded()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            QuizList.Clear();
            try
            {
                await foreach (var quiz in quizService.WithCancellation(_cancellationTokenSource.Token))
                {
                    QuizList.Add(quizItemFactory(quiz));
                }
            }
            catch (OperationCanceledException)
            {
            }
        }

        [RelayCommand]
        private void SelectQuiz(QuizChoiceViewModel quiz)
        {
            if(SelectedQuiz is not null) SelectedQuiz.IsSelected = false;
            SelectedQuiz = quiz;
            SelectedQuiz.IsSelected = true;
        }

        [RelayCommand]
        private async Task Unloaded()
        {
            if(_cancellationTokenSource is null) return;
            await _cancellationTokenSource.CancelAsync();
            _cancellationTokenSource.Dispose();
        }

     
    }
}
