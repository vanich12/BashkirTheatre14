using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BashkirTheatre14.Model.Entities;
using CommunityToolkit.Mvvm.Input;
using MvvmNavigationLib.Services;

namespace BashkirTheatre14.ViewModel.Controls
{
    public partial class QuizItemViewModel: BaseControlViewModel
    {
        private IParameterNavigationService<Quiz> _navigationService;
        public Quiz Quiz { get; set; }

        public QuizItemViewModel(IParameterNavigationService<Quiz> navService = null, Quiz quiz = null)
        {
            this.Quiz = quiz;
      
            _navigationService = navService;
            if (navService!=null)
            {
                _navigationService = navService;
            }
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

        public override async ValueTask DisposeAsync()
        {
        }
    }
}
