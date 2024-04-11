using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace EleCho.WpfSuite
{

    public abstract class SingletonValueConverterBase<T> : DependencyObject, IValueConverter
        where T : SingletonValueConverterBase<T>, new()
    {
        private static Lazy<T> _laziedInstance = new();

        public static T Instance => _laziedInstance.Value;

        public abstract object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture);

        public virtual object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
