using System;
using System.Windows.Markup;

namespace EleCho.WpfSuite.Markup
{
    [MarkupExtensionReturnType(typeof(int))]
    public class Int32 : MarkupExtension
    {
        public Int32() { }
        public Int32(int value)
        {
            Value = value;
        }

        public int Value { get; set; } = 0;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Value;
        }
    }
}
