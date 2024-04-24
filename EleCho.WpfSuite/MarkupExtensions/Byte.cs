using System;
using System.Windows.Markup;

namespace EleCho.WpfSuite.Markup
{
    [MarkupExtensionReturnType(typeof(byte))]
    public class Byte : MarkupExtension
    {
        public Byte() { }
        public Byte(byte value)
        {
            Value = value;
        }

        public byte Value { get; set; } = 0;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Value;
        }
    }
}
