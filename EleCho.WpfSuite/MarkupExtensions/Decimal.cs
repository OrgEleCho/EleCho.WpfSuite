using System;
using System.Windows.Markup;

namespace EleCho.WpfSuite.Markup
{
    [MarkupExtensionReturnType(typeof(decimal))]
    public class Decimal : MarkupExtension
    {
        public Decimal() { }
        public Decimal(decimal value)
        {
            Value = value;
        }

        public decimal Value { get; set; } = 0;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Value;
        }
    }
}
