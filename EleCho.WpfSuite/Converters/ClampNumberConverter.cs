using System;
using System.Globalization;
using System.Windows;

namespace EleCho.WpfSuite
{
    public class ClampNumberConverter : ValueConverterBase<DivideNumberConverter>
    {
        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register(nameof(Minimum), typeof(double), typeof(ClampNumberConverter), new PropertyMetadata(double.NegativeInfinity));

        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register(nameof(Maximum), typeof(double), typeof(ClampNumberConverter), new PropertyMetadata(double.PositiveInfinity));

        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            try
            {
                double current = System.Convert.ToDouble(value);
                if (current < Minimum)
                    current = Minimum;
                if (current > Maximum)
                    current = Maximum;

                var result = current;
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
    }
}
