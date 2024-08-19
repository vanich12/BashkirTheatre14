using BashkirTheatre14.Model.Entities;
using CommunityToolkit.Mvvm.Input;
using MvvmNavigationLib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BashkirTheatre14.ViewModel.Controls
{
    public partial class QuizResultViewModel(INavigationService navigationService, QuizViewModel quizViewModel)
        : ObservableObject
    {
        [ObservableProperty] private QuizViewModel _quizViewModel = quizViewModel;

        public int PointResult { get;} = quizViewModel.CorrectAnswersCount;

        public string ResultText => GetResultText();
        public string ResultImageUri => GetResultImageUri();

        [RelayCommand]
        private void GoToMainPage()=>navigationService.Navigate();

        private string GetResultText() => PointResult switch
        {
            <= 3 => "Надо тренироваться!",
            < 7 => "Отлично!",
            _ => "Превосходно!"
        };

        private string GetResultImageUri()=> PointResult switch
        {
            <= 3 => "../../Resources/Gif/SadFace.gif",
            < 7 => "../../Resources/Gif/in-love.gif",
            _ => "../../Resources/Gif/party.gif"
        };

    }
}
