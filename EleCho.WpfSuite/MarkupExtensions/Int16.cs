using System;
using System.Windows.Markup;

namespace EleCho.WpfSuite.Markup
{
    [MarkupExtensionReturnType(typeof(uint))]
    public class Int16 : MarkupExtension
    {
        public Int16() { }
        public Int16(uint value)
        {
            Value = value;
        }

        public uint Value { get; set; } = 0;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Value;
        }
    }
}
