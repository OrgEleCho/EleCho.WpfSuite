using System;
using System.Globalization;
using System.Windows;

namespace EleCho.WpfSuite
{
    /// <summary>
    /// Value converter that returns a boolean value that indicates that the value not equals parameter
    /// </summary>
    public class NotEqualConverter : SingletonValueConverterBase<NotEqualConverter>
    {
        /// <summary>
        /// The target value to compare
        /// </summary>
        public object? TargetValue
        {
            get { return (object?)GetValue(TargetValueProperty); }
            set { SetValue(TargetValueProperty, value); }
        }

        /// <summary>
        /// The DependencyProperty of <see cref="TargetValue"/>
        /// </summary>
        public static readonly DependencyProperty TargetValueProperty =
            DependencyProperty.Register(nameof(TargetValue), typeof(object), typeof(NotEqualConverter), new PropertyMetadata(null));

        /// <inheritdoc/>
        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var targetValue = parameter ?? TargetValue;

            return !Equals(value, targetValue);
        }
    }
}
