using System;
using System.Globalization;
using System.Windows;

namespace EleCho.WpfSuite
{
    public class SubstractNumberConverter : SingletonValueConverterBase<SubstractNumberConverter>
    {
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            try
            {
                double current = System.Convert.ToDouble(value);
                if (parameter is double parameterNumber)
                    current -= parameterNumber;

                return current - Value;
            }
            catch
            {
                return DependencyProperty.UnsetValue;
            }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(double), typeof(SubstractNumberConverter), new PropertyMetadata(0.0));
    }
}
