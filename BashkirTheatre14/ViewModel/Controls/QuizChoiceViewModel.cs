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
    public partial class QuizChoiceViewModel : BaseControlViewModel
    {
        private IParameterNavigationService<Quiz> _navigationService;

        public Quiz Quiz { get; set; }
        [ObservableProperty] private bool _isSelected;

        public QuizChoiceViewModel(IParameterNavigationService<Quiz> navService = null, Quiz quiz = null)
        {
            this.Quiz = quiz;

            this._navigationService = navService;
        }

        public override Task Load()
        {
            throw new NotImplementedException();
        }

        [RelayCommand]
        private void GoToQuestions()
        {
            if (_navigationService is not null)
            {
                _navigationService.Navigate(Quiz);
            }
        }

        public override async ValueTask DisposeAsync()
        {
        }
    }
}
