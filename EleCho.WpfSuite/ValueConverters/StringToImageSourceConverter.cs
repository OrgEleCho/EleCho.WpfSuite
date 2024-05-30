using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media.Imaging;

namespace EleCho.WpfSuite
{
    public class StringToImageSourceConverter : SingletonValueConverterBase<StringToImageSourceConverter>
    {
        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string path)
            {
                if (Uri.TryCreate(path, UriKind.RelativeOrAbsolute, out var uri))
                {
                    return new BitmapImage(uri);
                }
            }

            return DependencyProperty.UnsetValue;
        }
    }
}

