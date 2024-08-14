using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace BashkirTheatre14.Converter
{
    internal class PointResultToImageSourceConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int pointResult)
            {
                if (pointResult <= 2)
                {
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Images/BadResult.png"));
                }
                else if (pointResult < 7)
                {
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Images/GoodResult.png"));
                }
                else
                {
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Images/PerfectResult.png"));
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
