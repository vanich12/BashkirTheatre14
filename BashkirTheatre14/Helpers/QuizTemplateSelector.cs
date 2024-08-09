using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using BashkirTheatre14.ViewModel.Controls;

namespace BashkirTheatre14.Helpers
{
    public class QuizTemplateSelector:DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var element = container as FrameworkElement;
            return item switch
            {
                QuizQuestionViewModel => App.Current.FindResource("QuizPage") as DataTemplate, 
                null => App.Current.FindResource("StartPage") as DataTemplate, 
                _ => new DataTemplate()
            };
        }
    }
}
