using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace EleCho.WpfSuite.ValueConverters
{
    /// <summary>
    /// Base class of ValueConverter
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    public abstract class ValueConverterBase<TSelf> : DependencyObject, IValueConverter
        where TSelf : ValueConverterBase<TSelf>
    {
        /// <inheritdoc/>
        public abstract object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture);

        /// <inheritdoc/>
        public virtual object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    /// <summary>
    /// Base class of ValueConverter
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <typeparam name="TSourceValue"></typeparam>
    /// <typeparam name="TTargetValue"></typeparam>
    public abstract class ValueConverterBase<TSelf, TSourceValue, TTargetValue> : ValueConverterBase<TSelf>
        where TSelf : ValueConverterBase<TSelf, TSourceValue, TTargetValue>
    {
        /// <summary>
        /// Return value while trying to convert null value
        /// </summary>
        public virtual TTargetValue DefaultTargetValue => throw new InvalidOperationException();

        /// <summary>
        /// Return value while trying to convert null value back
        /// </summary>
        public virtual TSourceValue DefaultSourceValue => throw new InvalidOperationException();

        /// <inheritdoc/>
        public sealed override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not TSourceValue sourceValue)
            {
                if (typeof(TSourceValue).IsValueType)
                {
                    throw new ArgumentException("Invalid type of value", nameof(value));
                }

                return DefaultTargetValue;
            }

            return Convert(sourceValue, targetType, parameter, culture);
        }

        /// <inheritdoc/>
        public sealed override object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not TTargetValue targetValue)
            {
                if (typeof(TTargetValue).IsValueType)
                {
                    throw new ArgumentException("Invalid type of value", nameof(value));
                }

                return DefaultSourceValue;
            }

            return ConvertBack(targetValue, targetType, parameter, culture);
        }

        /// <summary>
        /// Convert from source value to target value
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="targetType">Target value type</param>
        /// <param name="parameter">Parameter</param>
        /// <param name="culture">Culture info</param>
        /// <returns></returns>
        public abstract TTargetValue? Convert(TSourceValue value, Type targetType, object? parameter, CultureInfo culture);

        /// <summary>
        /// Convert target value back to source value
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="targetType">Target value type</param>
        /// <param name="parameter">Parameter</param>
        /// <param name="culture">Culture info</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException">Conversion is not supported</exception>
        public virtual TSourceValue? ConvertBack(TTargetValue value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
