using System;
using System.Globalization;
using System.Windows;

namespace EleCho.WpfSuite.ValueConverters
{
    /// <summary>
    /// Multiply the specified value with the parameter value
    /// </summary>
    public class MultiplyNumberConverter : SingletonValueConverterBase<MultiplyNumberConverter>
    {
        /// <summary>
        /// Value to be multiplied
        /// </summary>
        public double By
        {
            get { return (double)GetValue(ByProperty); }
            set { SetValue(ByProperty, value); }
        }

        /// <inheritdoc/>
        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            try
            {
                double current = System.Convert.ToDouble(value);

                if (parameter is not null)
                {
                    try
                    {
                        current *= System.Convert.ToDouble(parameter);
                    }
                    catch { }
                }

                var result = current * By;
                if (typeof(IConvertible).IsAssignableFrom(targetType))
                {
                    return System.Convert.ChangeType(result, targetType);
                }
                else
                {
                    return result;
                }
            }
            catch
            {
                return DependencyProperty.UnsetValue;
            }
        }

        /// <inheritdoc/>
        public override object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            try
            {
                double current = System.Convert.ToDouble(value);

                if (parameter is not null)
                {
                    try
                    {
                        current /= System.Convert.ToDouble(parameter);
                    }
                    catch { }
                }

                var result = current / By;
                if (typeof(IConvertible).IsAssignableFrom(targetType))
                {
                    return System.Convert.ChangeType(result, targetType);
                }
                else
                {
                    return result;
                }
            }
            catch
            {
                return DependencyProperty.UnsetValue;
            }
        }

        /// <summary>
        /// The DependencyProperty of <see cref="By"/> property
        /// </summary>
        public static readonly DependencyProperty ByProperty =
            DependencyProperty.Register(nameof(By), typeof(double), typeof(MultiplyNumberConverter), new PropertyMetadata(1.0));
    }
}
