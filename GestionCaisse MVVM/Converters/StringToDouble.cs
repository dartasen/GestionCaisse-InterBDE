using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace GestionCaisse_MVVM.Converters
{
    public class StringToDouble : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string stringToConvert = value as string;
                if (string.IsNullOrEmpty(stringToConvert))
                    return 0.00;

                double convertedString;

                Double.TryParse(stringToConvert, NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out convertedString);

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
