using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfGraphTestApp
{
    public class VisistedToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool? valueAsBool = value as bool?;

            if(!valueAsBool.HasValue)
            {
                return null;
            }

            if(valueAsBool.Value)
            {
                return new SolidColorBrush(Colors.Blue);
            }
            else
            {
                return new SolidColorBrush(Colors.Red);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
