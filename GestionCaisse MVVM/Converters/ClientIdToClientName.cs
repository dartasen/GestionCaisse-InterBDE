using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using GestionCaisse_MVVM.Model.Services;

namespace GestionCaisse_MVVM.Converters
{
    public class ClientIdToClientName : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return string.Empty;
            }

            return ClientService.GetClients().FirstOrDefault(x => x.IdClient == (value as int?)).Nom;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
