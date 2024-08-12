using System;
using System.Globalization;
using System.Windows.Data;

namespace BashkirTheatre14.Converter
{
    public class PointResultToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int pointResult)
            {
                if (pointResult <= 2)
                {
                    return "Надо тренироваться!";
                }
                else if (pointResult < 5)
                {
                    return "Отлично!";
                }
                else
                {
                    return "Превосходно!";
                }
            }
            return "Нет данных";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}