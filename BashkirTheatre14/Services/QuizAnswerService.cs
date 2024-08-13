using BashkirTheatre14.Model.Entities;
using BashkirTheatre14.Model;
using BashkirTheatre14.ViewModel.Controls;
using MvvmNavigationLib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashkirTheatre14.Services
{
    public class QuizAnswerService: BaseListWebStore<QuizAnswerViewModel>
    {
        private readonly IMainApiClient _apiClient;
        private readonly ImageLoadingHttpClient _loadingHttpClient;
        private readonly CreateViewModel<QuizAnswerViewModel, Quiz> _answerFactory;

        public QuizAnswerService(IMainApiClient apiClient, ImageLoadingHttpClient loadingHttpClient,
            CreateViewModel<QuizAnswerViewModel, Quiz> answerFactory)
        {
            _apiClient = apiClient;
            _loadingHttpClient = loadingHttpClient;
            _answerFactory = answerFactory;
        }

        protected override async IAsyncEnumerable<QuizAnswerViewModel> GetListAsyncOverride(params object[] args)
        {
            var quizList = await _apiClient.GetQuizList();
            foreach (var quiz in quizList.Where(q => q.Display))
            {
                var quizAnswerViewModel = _answerFactory(quiz);


                yield return quizAnswerViewModel;
            }
        }
    }
}
