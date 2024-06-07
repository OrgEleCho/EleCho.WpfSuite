using System;
using System.Windows.Markup;

namespace EleCho.WpfSuite.Markup
{
    /// <summary>
    /// Provide a value of <see cref="Int32"/>
    /// </summary>
    [MarkupExtensionReturnType(typeof(int))]
    public class Int32 : MarkupExtension
    {
        /// <summary>
        /// Create an instance of this markup extension
        /// </summary>
        public Int32() { }

        /// <summary>
        /// Create an instance of this markup extension with specified value
        /// </summary>
        /// <param name="value">Value</param>
        public Int32(int value)
        {
            Value = value;
        }

        /// <summary>
        /// Value
        /// </summary>
        public int Value { get; set; } = 0;

        /// <inheritdoc/>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Value;
        }
    }
}
