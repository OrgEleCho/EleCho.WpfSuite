using System;
using System.Globalization;
using System.Windows;

namespace EleCho.WpfSuite
{
    public class MultiplyNumberConverter : SingletonValueConverterBase<MultiplyNumberConverter>
    {
        public double By
        {
            get { return (double)GetValue(ByProperty); }
            set { SetValue(ByProperty, value); }
        }

        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            try
            {
                double current = System.Convert.ToDouble(value);
                if (parameter is double parameterNumber)
                    current *= parameterNumber;

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

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ByProperty =
            DependencyProperty.Register(nameof(By), typeof(double), typeof(MultiplyNumberConverter), new PropertyMetadata(1.0));
    }
}
