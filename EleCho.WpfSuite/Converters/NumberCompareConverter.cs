using System;
using System.Globalization;
using System.Windows;

namespace EleCho.WpfSuite
{
    public class NumberCompareConverter : ValueConverterBase<NumberCompareConverter>
    {
        public double TargetValue
        {
            get { return (double)GetValue(TargetValueProperty); }
            set { SetValue(TargetValueProperty, value); }
        }

        public NumberComparison Comparison
        {
            get { return (NumberComparison)GetValue(ComparisonProperty); }
            set { SetValue(ComparisonProperty, value); }
        }

        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            try
            {
                var current = System.Convert.ToDouble(value);

                return Comparison switch
                {
                    NumberComparison.Equal => current == TargetValue,
                    NumberComparison.NotEqual => current != TargetValue,
                    NumberComparison.GreatorThan => current > TargetValue,
                    NumberComparison.GreatorOrEqual => current >= TargetValue,
                    NumberComparison.LessThan => current < TargetValue,
                    NumberComparison.LessOrEqual => current <= TargetValue,
                    _ => DependencyProperty.UnsetValue
                };
            }
            catch
            {
                return DependencyProperty.UnsetValue;
            }
        }

        public static readonly DependencyProperty TargetValueProperty =
            DependencyProperty.Register(nameof(TargetValue), typeof(double), typeof(NumberCompareConverter), new PropertyMetadata(0.0));

        public static readonly DependencyProperty ComparisonProperty =
            DependencyProperty.Register(nameof(Comparison), typeof(NumberComparison), typeof(NumberCompareConverter), new PropertyMetadata(NumberComparison.Equal));
    }
}
