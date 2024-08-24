using BashkirTheatre14.Model.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmNavigationLib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashkirTheatre14.ViewModel.Controls;
public partial class BaseQuizViewModel(IParameterNavigationService<QuizModel> navService, QuizModel quiz) : ObservableObject
{
    [ObservableProperty] private QuizModel _quiz = quiz;
    [RelayCommand] private void ToNextPage() => navService.Navigate(Quiz);
}

public partial class QuizItemViewModel(IParameterNavigationService<QuizModel> navService, QuizModel quiz)
    : BaseQuizViewModel(navService, quiz)
{

}

public partial class QuizChoiceViewModel(IParameterNavigationService<QuizModel> navService, QuizModel quiz)
    : BaseQuizViewModel(navService, quiz)
{
    [ObservableProperty] private bool _isSelected;
}
