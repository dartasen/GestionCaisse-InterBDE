﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace GestionCaisse_MVVM.Converters
{
    public class IntTo3GroupOfDigits : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return 0;

            var number = (int) value;
            var convertedNumber = number.ToString();

            return $"{convertedNumber.Substring(0, 3)} - {convertedNumber.Substring(3, 3)} - {convertedNumber.Substring(6, 3)}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
