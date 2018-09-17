using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace GestionCaisse_MVVM.Converters
{
    public class ResultToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var convertedValue = System.Convert.ToInt32(value);

            if (convertedValue == 0) return new SolidColorBrush((Color) ColorConverter.ConvertFromString("#D91E36"));
            if (convertedValue >= 40) return new SolidColorBrush((Color) ColorConverter.ConvertFromString("#149911"));
            if (convertedValue <= 40 && convertedValue >= 20) return new SolidColorBrush((Color) ColorConverter.ConvertFromString("#A27035"));
            if (convertedValue < 20 && convertedValue > 0) return new SolidColorBrush(Colors.OrangeRed);

            return new SolidColorBrush(Colors.Black);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}