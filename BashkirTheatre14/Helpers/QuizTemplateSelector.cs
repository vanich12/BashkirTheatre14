using System;
using System.Windows;
using System.Windows.Controls;
using BashkirTheatre14.ViewModel.Controls;

namespace BashkirTheatre14.Helpers
{
    public class QuizTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            // Преобразуем item в QuizViewModel, если это возможно
            var quizViewModel = item as QuizViewModel;

            // Получаем доступ к элементу, чтобы использовать его ресурсы
            var element = container as FrameworkElement;

            if (quizViewModel != null)
            {
                // Проверяем состояние SelectedQuestion и выбираем шаблон
                if (quizViewModel.SelectedQuestion != null)
                {
                    return element?.FindResource("QuizPage") as DataTemplate;
                }
                else
                {
                    return element?.FindResource("StartPage") as DataTemplate;
                }
            }
            else
            {
                return element?.FindResource("StartPage") as DataTemplate;
            }
        }
    }
}