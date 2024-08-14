using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BashkirTheatre14.Converter
{
    public class BooleanToHiddenVisibilityConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not bool b) return Visibility.Hidden;
            return b ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
