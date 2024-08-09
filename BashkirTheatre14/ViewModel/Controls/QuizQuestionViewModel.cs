using BashkirTheatre14.Model.Entities;
using BashkirTheatre14.ViewModel.Pages;

namespace BashkirTheatre14.ViewModel.Controls
{
    public partial class QuizQuestionViewModel: BasePageViewModel
    {
        public Question Question { get; }

        public QuizQuestionViewModel(Question question)
        {
            this.Question = question;
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
