using System;
using System.Globalization;
using System.Windows.Data;

namespace PLM.Library.Converters
{
    internal class HeadTransformConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (System.Convert.ToBoolean(value))
            {
                return 0;
            }
            else
            {
                return 180;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
