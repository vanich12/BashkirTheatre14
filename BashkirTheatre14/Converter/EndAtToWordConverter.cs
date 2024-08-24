using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BashkirTheatre14.Converter
{
    internal class EndAtToWordConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var startAt = values[0]?.ToString();
            var endAt = values[1]?.ToString();

            if (string.IsNullOrEmpty(startAt))
                return string.Empty;

            if (string.IsNullOrEmpty(endAt))
                return $"{startAt} ГОД";

            return $"{startAt} - {endAt} ГОДЫ";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
