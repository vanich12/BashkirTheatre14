using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BashkirTheatre14.Model.Entities;
using BashkirTheatre14.Model;
using BashkirTheatre14.ViewModel.Controls;
using MvvmNavigationLib.Services;

namespace BashkirTheatre14.Services
{
    public class QuestionService: BaseListWebStore<QuizItemViewModel>
    {
        private readonly IMainApiClient _apiClient;
        private readonly ImageLoadingHttpClient _loadingHttpClient;
        private readonly CreateViewModel<QuizItemViewModel, Quiz> _quizFactory;
        protected override IAsyncEnumerable<QuizItemViewModel> GetListAsyncOverride(params object[] args)
        {
            throw new NotImplementedException();
        }
    }
}
