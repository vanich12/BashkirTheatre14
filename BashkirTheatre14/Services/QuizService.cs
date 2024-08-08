using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BashkirTheatre14.Model;
using BashkirTheatre14.Model.Entities;
using BashkirTheatre14.ViewModel.Controls;
using MvvmNavigationLib.Services;

namespace BashkirTheatre14.Services
{
    public class QuizService:BaseListWebStore<QuizItemViewModel>
    {
        private readonly IMainApiClient _apiClient;
        private readonly ImageLoadingHttpClient _loadingHttpClient;
        private readonly CreateViewModel<QuizItemViewModel, Quiz> _quizFactory;

        public QuizService(IMainApiClient apiClient,ImageLoadingHttpClient loadingHttpClient,CreateViewModel<QuizItemViewModel,Quiz> quizFactory)
        {
            _apiClient = apiClient;
            _loadingHttpClient = loadingHttpClient;
            _quizFactory = quizFactory;
        }

        protected override async IAsyncEnumerable<QuizItemViewModel> GetListAsyncOverride(params object[] args)
        {
            var quizList = await _apiClient.GetQuizList();
            foreach (var quiz in quizList.Where(q=>q.Display))
            {
                yield return _quizFactory(quiz);
            }
        }
    }
}
