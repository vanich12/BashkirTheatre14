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
    public class QuizService:BaseListWebStore<QuizItemViewModel>
    {
        private readonly IMainApiClient _apiClient;
        private readonly ImageLoadingHttpClient _loadingHttpClient;
        private readonly CreateViewModel<QuizItemViewModel, Quiz> _quizFactory;
        private readonly CreateViewModel<QuizQuestionViewModel, Question> _questionFactory;
        public QuizService(IMainApiClient apiClient,ImageLoadingHttpClient loadingHttpClient,CreateViewModel<QuizItemViewModel,Quiz> quizFactory,CreateViewModel<QuizQuestionViewModel,Question> questionFactory)
        {
            _apiClient = apiClient;
            _loadingHttpClient = loadingHttpClient;
            _quizFactory = quizFactory;
            _questionFactory = questionFactory;
        }

        protected override async IAsyncEnumerable<QuizItemViewModel> GetListAsyncOverride(params object[] args)
        {
            var quizList = await _apiClient.GetQuizList();
            foreach (var quiz in quizList.Where(q => q.Display))
            {
                var quizViewModel = _quizFactory(quiz);

                var questionViewModels = new ObservableCollection<QuizQuestionViewModel>(
                    quiz.Questions.Select(question => _questionFactory(question))
                );

                yield return quizViewModel;
            }
        }
    }
}
