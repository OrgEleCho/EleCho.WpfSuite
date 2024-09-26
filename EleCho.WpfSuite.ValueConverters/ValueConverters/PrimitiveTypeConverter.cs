using System;
using System.Globalization;
using System.Windows;

namespace EleCho.WpfSuite.ValueConverters
{
    /// <summary>
    /// Convert value between primitive types.
    /// </summary>
    public class PrimitiveTypeConverter : ValueConverterBase<PrimitiveTypeConverter>
    {
        /// <summary>
        /// Target type
        /// </summary>
        public Type? TargetType
        {
            get { return (Type?)GetValue(TargetTypeProperty); }
            set { SetValue(TargetTypeProperty, value); }
        }

        /// <summary>
        /// The DependencyProperty of <see cref="TargetType"/> property
        /// </summary>
        public static readonly DependencyProperty TargetTypeProperty =
            DependencyProperty.Register(nameof(TargetType), typeof(Type), typeof(PrimitiveTypeConverter), new PropertyMetadata(null));


        /// <inheritdoc/>
        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return System.Convert.ChangeType(value, TargetType ?? targetType);
        }


        /// <inheritdoc/>
        public override object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return System.Convert.ChangeType(value, TargetType ?? targetType);
        }
    }
}

