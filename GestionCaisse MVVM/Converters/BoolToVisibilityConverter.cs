using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace GestionCaisse_MVVM.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Boolean && (bool)value)
            {
                return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Visibility))
            {
                return false;
            }

            if (value as Visibility? == Visibility.Visible)
            {
                return true;
            }

            return false;
        }
    }
}
