using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace EleCho.WpfSuite
{
    public class ValueIsNullConverter : SingletonValueConverterBase<ValueIsNullConverter>
    {
        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value is null;
        }
    }
}
