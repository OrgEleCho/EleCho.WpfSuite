using System;
using System.Globalization;
using System.Windows;

namespace EleCho.WpfSuite
{
    /// <summary>
    /// Convert value between basic types.
    /// </summary>
    public class BasicDataTypeConverter : ValueConverterBase<BasicDataTypeConverter>
    {
        public Type? TargetType
        {
            get { return (Type?)GetValue(TargetTypeProperty); }
            set { SetValue(TargetTypeProperty, value); }
        }


        public static readonly DependencyProperty TargetTypeProperty =
            DependencyProperty.Register(nameof(TargetType), typeof(Type), typeof(BasicDataTypeConverter), new PropertyMetadata(null));

        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return System.Convert.ChangeType(value, TargetType ?? targetType);
        }

        public override object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return System.Convert.ChangeType(value, TargetType ?? targetType);
        }
    }
}

