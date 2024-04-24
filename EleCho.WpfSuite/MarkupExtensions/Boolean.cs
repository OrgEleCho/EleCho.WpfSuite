using System;
using System.Windows.Data;
using System.Windows.Markup;

namespace EleCho.WpfSuite.Markup
{
    [MarkupExtensionReturnType(typeof(bool))]
    public class Boolean : MarkupExtension
    {
        public Boolean() { }
        public Boolean(bool value)
        {
            Value = value;
        }

        public bool Value { get; set; } = false;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Value;
        }
    }
}
