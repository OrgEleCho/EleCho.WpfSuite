using System;
using System.Windows.Markup;

namespace EleCho.WpfSuite.Markup
{
    [MarkupExtensionReturnType(typeof(double))]
    public class Double : MarkupExtension
    {
        public Double() { }
        public Double(double value)
        {
            Value = value;
        }

        public double Value { get; set; } = 0.0;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Value;
        }
    }
}
