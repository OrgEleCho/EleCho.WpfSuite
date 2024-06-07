using System;
using System.Windows.Markup;

namespace EleCho.WpfSuite.Markup
{
    /// <summary>
    /// Provide a value of <see cref="TimeSpan"/>
    /// </summary>
    [MarkupExtensionReturnType(typeof(System.TimeSpan))]
    public class TimeSpan : MarkupExtension
    {
        /// <summary>
        /// Create an instance of this markup extension
        /// </summary>
        public TimeSpan() { }

        /// <summary>
        /// Create an instance of this markup extension with specified value
        /// </summary>
        /// <param name="value">Value</param>
        public TimeSpan(System.TimeSpan value)
        {
            Value = value;
        }

        /// <summary>
        /// Value
        /// </summary>
        public System.TimeSpan Value { get; set; } = System.TimeSpan.Zero;

        /// <inheritdoc/>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Value;
        }
    }
}
