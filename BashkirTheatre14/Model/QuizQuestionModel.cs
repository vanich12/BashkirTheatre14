using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashkirTheatre14.Model
{
    public partial class QuizQuestionModel
    {
        public int QuestionNumber { get; set; }
        public string Question { get; set; }

        public string Description { get; set; }
        public List<string> OptionsAnswer { get; set; }

        public string RightAnswer { get; set; } 
    }
}
