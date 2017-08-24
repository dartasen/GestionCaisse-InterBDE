using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace GestionCaisse_MVVM.Converters
{
    public class SellsScoreToSmileyImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var convertedValue = System.Convert.ToInt32(value);

                string smiley = "";
                if (convertedValue >= 20) smiley = "fireworks";
                else if (convertedValue < 20 && convertedValue >= 15) smiley = "in-love";
                else if (convertedValue < 15 && convertedValue >= 10) smiley = "really-happy";
                else if (convertedValue < 10 && convertedValue >= 5) smiley = "very-happy";
                else if (convertedValue == 0) return new BitmapImage();
                else smiley = "happy";

                var image = new BitmapImage(new Uri($"pack://application:,,,/Assets/smileys/{smiley}.png"));

                return image;
            }
            catch { }

            return new BitmapImage();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
