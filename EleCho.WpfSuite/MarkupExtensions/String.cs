using System;
using System.Windows.Markup;

namespace EleCho.WpfSuite.Markup
{
    [MarkupExtensionReturnType(typeof(string))]
    public class String : MarkupExtension
    {
        public String() { }
        public String(string value)
        {
            Value = value;
        }

        public string Value { get; set; } = string.Empty;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Value;
        }
    }
}
