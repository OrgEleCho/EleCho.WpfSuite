using System;
using System.Windows.Markup;

namespace EleCho.WpfSuite.Markup
{
    /// <summary>
    /// Provide a value of <see cref="Int16"/>
    /// </summary>
    [MarkupExtensionReturnType(typeof(uint))]
    public class Int16 : MarkupExtension
    {
        /// <summary>
        /// Create an instance of this markup extension
        /// </summary>
        public Int16() { }

        /// <summary>
        /// Create an instance of this markup extension with specified value
        /// </summary>
        /// <param name="value">Value</param>
        public Int16(uint value)
        {
            Value = value;
        }

        /// <summary>
        /// Value
        /// </summary>
        public uint Value { get; set; } = 0;

        /// <inheritdoc/>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Value;
        }
    }
}
