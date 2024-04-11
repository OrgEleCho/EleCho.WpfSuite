using System;
using System.Globalization;
using System.Windows;

namespace EleCho.WpfSuite
{
    public class ObjectCompareConverter : ValueConverterBase<ObjectCompareConverter>
    {
        public object TargetValue
        {
            get { return (object)GetValue(TargetValueProperty); }
            set { SetValue(TargetValueProperty, value); }
        }

        public ObjectComparison Comparison
        {
            get { return (ObjectComparison)GetValue(ComparisonProperty); }
            set { SetValue(ComparisonProperty, value); }
        }

        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return Comparison switch
            {
                ObjectComparison.Equal => Object.Equals(value, TargetValue),
                ObjectComparison.NotEqual => !Object.Equals(value, TargetValue),
                _ => DependencyProperty.UnsetValue
            };
        }

        public static readonly DependencyProperty TargetValueProperty =
            DependencyProperty.Register(nameof(TargetValue), typeof(object), typeof(NumberCompareConverter), new PropertyMetadata(null));

        public static readonly DependencyProperty ComparisonProperty =
            DependencyProperty.Register(nameof(Comparison), typeof(ObjectComparison), typeof(NumberCompareConverter), new PropertyMetadata(ObjectComparison.Equal));
    }
}
