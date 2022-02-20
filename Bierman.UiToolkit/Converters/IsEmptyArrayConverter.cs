using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Bierman.UiToolkit.Converters
{
    public class IsEmptyArrayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return parameter is Visibility ? parameter : Visibility.Collapsed;

            if ((IEnumerable<object>)value == null)
                return parameter is Visibility ? parameter : Visibility.Collapsed;

            IEnumerable<object> list = (IEnumerable<object>)value;

            return list.Count() > 0 ? Visibility.Visible : ((parameter is Visibility visibility) ? visibility : Visibility.Collapsed);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException("IsNullConverter can only be used OneWay.");
        }
    }
}
