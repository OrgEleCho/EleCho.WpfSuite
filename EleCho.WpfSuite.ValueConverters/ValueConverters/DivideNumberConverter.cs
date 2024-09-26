using System;
using System.Globalization;
using System.Windows;

namespace EleCho.WpfSuite
{
    /// <summary>
    /// Divide by the specified value and the parameter value
    /// </summary>
    public class DivideNumberConverter : SingletonValueConverterBase<DivideNumberConverter>
    {
        /// <summary>
        /// The number to be divided by
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

        /// <summary>
        /// DependencyProperty of <see cref="By"/> property
        /// </summary>
        public static readonly DependencyProperty ByProperty =
            DependencyProperty.Register(nameof(By), typeof(double), typeof(DivideNumberConverter), new PropertyMetadata(1.0));
    }
}
