using System;
using System.Windows.Markup;

namespace EleCho.WpfSuite.Markup
{
    [MarkupExtensionReturnType(typeof(char))]
    public class Char : MarkupExtension
    {
        public Char() { }
        public Char(char value)
        {
            Value = value;
        }

        public char Value { get; set; } = '\0';

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Value;
        }
    }
}
