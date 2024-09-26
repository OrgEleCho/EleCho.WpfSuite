using System;
using System.Globalization;
using System.Windows;

namespace EleCho.WpfSuite
{
    /// <summary>
    /// Convert boolean to any other type
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class BooleanToValueConverter<TSelf, TValue> : SingletonValueConverterBase<TSelf>
        where TSelf : BooleanToValueConverter<TSelf, TValue>, new()
    {
        /// <summary>
        /// Result value when the boolean value is true
        /// </summary>
        public TValue ValueWhenTrue
        {
            get { return (TValue)GetValue(ValueWhenTrueProperty); }
            set { SetValue(ValueWhenTrueProperty, value); }
        }

        /// <summary>
        /// Result value when the boolean value is false
        /// </summary>
        public TValue ValueWhenFalse
        {
            get { return (TValue)GetValue(ValueWhenFalseProperty); }
            set { SetValue(ValueWhenFalseProperty, value); }
        }

        /// <inheritdoc/>
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

        /// <summary>
        /// The DependencyProperty of <see cref="ValueWhenTrue"/> property
        /// </summary>
        public static readonly DependencyProperty ValueWhenTrueProperty =
            DependencyProperty.Register(nameof(ValueWhenTrue), typeof(TValue), typeof(BooleanToValueConverter<TSelf, TValue>), new PropertyMetadata(default(TValue)));

        /// <summary>
        /// The DependencyProperty of <see cref="ValueWhenFalse"/> property
        /// </summary>
        public static readonly DependencyProperty ValueWhenFalseProperty =
            DependencyProperty.Register(nameof(ValueWhenFalse), typeof(TValue), typeof(BooleanToValueConverter<TSelf, TValue>), new PropertyMetadata(default(TValue)));


    }
}
