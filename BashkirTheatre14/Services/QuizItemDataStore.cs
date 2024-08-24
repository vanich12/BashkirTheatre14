using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BashkirTheatre14.Model.Entities;

namespace BashkirTheatre14.Services
{
    public class QuizItemDataStore
    {
        public List<QuizItemData> QuizItems { get; set; }

        public QuizItemDataStore(List<QuizItemData> quizItem)
        {
            this.QuizItems = quizItem;
        }

        public QuizItemData GetById(int id)
        {
           var item =  QuizItems.FirstOrDefault(x => x.Id == id) ;
           return item;
        }
    }
}
