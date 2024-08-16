using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BashkirTheatre14.Model;
using BashkirTheatre14.Model.Entities;
using BashkirTheatre14.ViewModel.Controls;
using MvvmNavigationLib.Services;

namespace BashkirTheatre14.Services
{
    public class QuizService:BaseListWebStore<QuizModel>
    {
        private readonly IMainApiClient _apiClient;
        public QuizService(IMainApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        protected override async IAsyncEnumerable<QuizModel> GetListAsyncOverride()
        {
            var quizList = await _apiClient.GetQuizList();
            foreach (var quiz in quizList.Where(q => q.Display))
            {
                yield return new QuizModel(quiz);
            }
        }
    }
}
