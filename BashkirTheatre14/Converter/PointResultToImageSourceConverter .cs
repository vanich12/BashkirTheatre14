using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace BashkirTheatre14.Converter
{
    internal class PointResultToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int pointResult)
            {
                string uriString;
                if (pointResult <= 2)
                {
                    uriString = "../../Resources/Gif/SadFace.gif";
                }
                else if (pointResult < 7)
                {
                    uriString = "../../Resources/Gif/in-love.gif";
                }
                else
                {
                    uriString = "../../Resources/Gif/party.gif";
                }

                return uriString;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}