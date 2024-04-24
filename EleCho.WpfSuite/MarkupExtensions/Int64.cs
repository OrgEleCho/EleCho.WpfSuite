using System;
using System.Windows.Markup;

namespace EleCho.WpfSuite.Markup
{
    [MarkupExtensionReturnType(typeof(long))]
    public class Int64 : MarkupExtension
    {
        public Int64() { }
        public Int64(long value)
        {
            Value = value;
        }

        public long Value { get; set; } = 0;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Value;
        }
    }
}
