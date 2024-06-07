using System;
using System.Globalization;
using System.Windows;

namespace EleCho.WpfSuite
{
    /// <summary>
    /// Subtract the specified other value and the parameter value
    /// </summary>
    public class SubtractNumberConverter : SingletonValueConverterBase<SubtractNumberConverter>
    {
        /// <summary>
        /// The value to be subtracted
        /// </summary>
        public double Other
        {
            get { return (double)GetValue(OtherProperty); }
            set { SetValue(OtherProperty, value); }
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
                        current -= System.Convert.ToDouble(parameter);
                    }
                    catch { }
                }

                var result = current - Other;
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
        /// The DependencyProperty of <see cref="Other"/> Property
        /// </summary>
        public static readonly DependencyProperty OtherProperty =
            DependencyProperty.Register(nameof(Other), typeof(double), typeof(SubtractNumberConverter), new PropertyMetadata(0.0));
    }
}
