using BashkirTheatre14.Model.Entities;
using BashkirTheatre14.ViewModel.Pages;

namespace BashkirTheatre14.ViewModel.Controls
{
    public partial class QuizAnswerViewModel: BasePageViewModel
    {
        public Answer Answer { get; }

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
