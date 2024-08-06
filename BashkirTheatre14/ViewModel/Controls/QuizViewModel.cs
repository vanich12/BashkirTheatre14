using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BashkirTheatre14.Model.Entities;

namespace BashkirTheatre14.ViewModel.Controls
{
    public class QuizViewModel:BaseControlViewModel
    {
        public Quiz Quiz { get; }

        public QuizViewModel(Quiz quiz)
        {
            Quiz = quiz;
        }

        public override async ValueTask DisposeAsync()
        {
        }

        public override Task Load()
        {
            return Task.CompletedTask;
        }
    }
}
