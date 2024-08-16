using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Brushes = System.Windows.Media.Brushes;
using Color = System.Windows.Media.Color;

namespace BashkirTheatre14.Converters
{
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool hasSelectedQuiz)
            {
                // Если значение true, возвращаем основной цвет
                return hasSelectedQuiz
                    ? new SolidColorBrush(Color.FromRgb(150, 53, 140))
                    : new SolidColorBrush(Color.FromRgb(109, 35, 94)); 
            }
            return Brushes.Transparent; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}