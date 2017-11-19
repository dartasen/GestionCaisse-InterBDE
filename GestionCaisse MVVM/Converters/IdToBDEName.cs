using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using GestionCaisse_MVVM.Model.Services;

namespace GestionCaisse_MVVM.Converters
{
    public class IdToBDEName : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";

            var bdeId = (int) value;

            return BDEService.GetBDEs().FirstOrDefault(x => x.idBDE == bdeId)?.Name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
