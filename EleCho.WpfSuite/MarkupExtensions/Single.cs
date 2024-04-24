using System;
using System.Windows.Markup;

namespace EleCho.WpfSuite.Markup
{
    [MarkupExtensionReturnType(typeof(float))]
    public class Single : MarkupExtension
    {
        public Single() { }
        public Single(float value)
        {
            Value = value;
        }

        public float Value { get; set; } = 0.0f;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Value;
        }
    }
}
