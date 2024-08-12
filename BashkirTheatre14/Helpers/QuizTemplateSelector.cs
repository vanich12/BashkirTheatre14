using System;
using System.Windows;
using System.Windows.Controls;
using BashkirTheatre14.Model.Entities;
using BashkirTheatre14.ViewModel.Controls;

namespace BashkirTheatre14.Helpers
{
    public class QuizTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var element = container as FrameworkElement;
            return item switch
            {
                QuizViewModel => App.Current.FindResource("QuizPage") as DataTemplate,
                QuizResultViewModel => App.Current.FindResource("ResultPage") as DataTemplate,
                QuizItemViewModel => App.Current.FindResource("StartPage") as DataTemplate,
                _ => new DataTemplate()
            };
        }
    }
}