using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace EleCho.WpfSuite
{
    public abstract class ValueConverterBase<TSelf> : DependencyObject, IValueConverter
        where TSelf : ValueConverterBase<TSelf>
    {
        public abstract object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture);

        public virtual object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public abstract class ValueConverterBase<TSelf, TSourceValue, TTargetValue> : ValueConverterBase<TSelf>
        where TSelf : ValueConverterBase<TSelf, TSourceValue, TTargetValue>
    {
        public sealed override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not TSourceValue sourceValue)
                throw new ArgumentException("Invalid type of value", nameof(value));

            return Convert(sourceValue, targetType, parameter, culture);
        }

        public sealed override object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not TTargetValue targetValue)
                throw new ArgumentException("Invalid type of value", nameof(value));

            return ConvertBack(targetValue, targetType, parameter, culture);
        }

        public abstract TTargetValue? Convert(TSourceValue? value, Type targetType, object? parameter, CultureInfo culture);

        public virtual TSourceValue? ConvertBack(TTargetValue? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
