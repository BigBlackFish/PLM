using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace PLM.Library.Converters
{
    public class FileTypeImageConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new ImageSourceConverter().ConvertToString($"/Library/Image/{value.ToString().Replace(".", string.Empty)}.png");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
