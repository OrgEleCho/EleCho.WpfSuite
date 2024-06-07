using System;
using System.Globalization;
using System.Windows;

namespace EleCho.WpfSuite
{
    /// <summary>
    /// Compare numbers and return the boolean result <br/>
    /// Compare the value and specified target value. But if the parameter can be converted to a number, compare the value with the parameter number.
    /// </summary>
    public class NumberCompareConverter : ValueConverterBase<NumberCompareConverter>
    {
        /// <summary>
        /// Value to compare with
        /// </summary>
        public double TargetValue
        {
            get { return (double)GetValue(TargetValueProperty); }
            set { SetValue(TargetValueProperty, value); }
        }

        /// <summary>
        /// Comparison rule
        /// </summary>
        public NumberComparison Comparison
        {
            get { return (NumberComparison)GetValue(ComparisonProperty); }
            set { SetValue(ComparisonProperty, value); }
        }

        /// <inheritdoc/>
        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            try
            {
                var current = System.Convert.ToDouble(value);
                var targetValue = TargetValue;

                if (parameter is not null)
                {
                    try
                    {
                        targetValue = System.Convert.ToDouble(parameter);
                    }
                    catch { }
                }

                return Comparison switch
                {
                    NumberComparison.Equal => current == targetValue,
                    NumberComparison.NotEqual => current != targetValue,
                    NumberComparison.GreaterThan => current > targetValue,
                    NumberComparison.GreaterOrEqual => current >= targetValue,
                    NumberComparison.LessThan => current < targetValue,
                    NumberComparison.LessOrEqual => current <= targetValue,
                    _ => DependencyProperty.UnsetValue
                };
            }
            catch
            {
                return DependencyProperty.UnsetValue;
            }
        }

        /// <summary>
        /// DependencyProperty of <see cref="TargetValue"/> property
        /// </summary>
        public static readonly DependencyProperty TargetValueProperty =
            DependencyProperty.Register(nameof(TargetValue), typeof(double), typeof(NumberCompareConverter), new PropertyMetadata(0.0));

        /// <summary>
        /// DependencyProperty of <see cref="Comparison"/> property
        /// </summary>
        public static readonly DependencyProperty ComparisonProperty =
            DependencyProperty.Register(nameof(Comparison), typeof(NumberComparison), typeof(NumberCompareConverter), new PropertyMetadata(NumberComparison.Equal));
    }
}
