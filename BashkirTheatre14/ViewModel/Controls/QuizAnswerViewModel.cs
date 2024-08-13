using BashkirTheatre14.Model.Entities;
using BashkirTheatre14.ViewModel.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BashkirTheatre14.ViewModel.Controls
{
    public partial class QuizAnswerViewModel: BasePageViewModel
    {
        public Answer Answer { get; }
        [ObservableProperty] private bool _isSelected;

        public QuizAnswerViewModel(Answer question)
        {
            this.Answer = question;
        }


        protected override Task Loaded()
        {
            throw new NotImplementedException();
        }

        protected override Task Unloaded()
        {
            throw new NotImplementedException();
        }
    }
}
