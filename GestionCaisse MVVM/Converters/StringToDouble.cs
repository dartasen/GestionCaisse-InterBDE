using System;
using System.Globalization;
using System.Windows.Data;

namespace GestionCaisse_MVVM.Converters
{
    public class StringToDouble : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var stringToConvert = value as string;

                if (string.IsNullOrEmpty(stringToConvert))
                    return 0.00;

                double.TryParse(stringToConvert, NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out var convertedString);

                return convertedString;
            }
            catch (ArgumentException)
            {
                return 0.00;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
