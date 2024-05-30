using System;
using System.Globalization;
using System.Windows;

namespace EleCho.WpfSuite
{
    public class BooleanToValueConverter<TValue> : SingletonValueConverterBase<BooleanToValueConverter<TValue>>
    {
        public TValue ValueWhenTrue
        {
            get { return (TValue)GetValue(ValueWhenTrueProperty); }
            set { SetValue(ValueWhenTrueProperty, value); }
        }

        public TValue ValueWhenFalse
        {
            get { return (TValue)GetValue(ValueWhenFalseProperty); }
            set { SetValue(ValueWhenFalseProperty, value); }
        }


        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            try
            {
                var current = System.Convert.ToBoolean(value);

                if (current)
                    return ValueWhenTrue;
                else
                    return ValueWhenFalse;
            }
            catch
            {
                return DependencyProperty.UnsetValue;
            }
        }

        public static readonly DependencyProperty ValueWhenTrueProperty =
            DependencyProperty.Register(nameof(ValueWhenTrue), typeof(TValue), typeof(BooleanToValueConverter<TValue>), new PropertyMetadata(default(TValue)));

        public static readonly DependencyProperty ValueWhenFalseProperty =
            DependencyProperty.Register(nameof(ValueWhenFalse), typeof(TValue), typeof(BooleanToValueConverter<TValue>), new PropertyMetadata(default(TValue)));


    }
}
