using System;
using System.Globalization;
using System.Windows;

namespace EleCho.WpfSuite
{
    public class InvertThicknessConverter : SingletonValueConverterBase<InvertThicknessConverter>
    {
        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not Thickness thickness)
                throw new ArgumentException("Invalid type of value", nameof(value));

            return new Thickness(-thickness.Left, -thickness.Top, -thickness.Right, -thickness.Bottom);
        }

        public override object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not Thickness thickness)
                throw new ArgumentException("Invalid type of value", nameof(value));

            return new Thickness(-thickness.Left, -thickness.Top, -thickness.Right, -thickness.Bottom);
        }
    }
}
