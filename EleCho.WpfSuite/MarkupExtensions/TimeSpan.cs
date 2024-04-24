using System;
using System.Windows.Markup;

namespace EleCho.WpfSuite.Markup
{
    [MarkupExtensionReturnType(typeof(System.TimeSpan))]
    public class TimeSpan : MarkupExtension
    {
        public TimeSpan() { }
        public TimeSpan(System.TimeSpan value)
        {
            Value = value;
        }

        public System.TimeSpan Value { get; set; } = System.TimeSpan.Zero;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Value;
        }
    }
}
