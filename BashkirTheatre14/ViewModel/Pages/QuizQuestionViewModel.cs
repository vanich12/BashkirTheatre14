using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BashkirTheatre14.Model.Entities;

namespace BashkirTheatre14.ViewModel.Pages
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
