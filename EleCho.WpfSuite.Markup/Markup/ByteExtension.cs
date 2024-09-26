using System;
using System.Windows.Markup;

namespace EleCho.WpfSuite.Markup
{
    /// <summary>
    /// Provide a value of <see cref="Byte"/>
    /// </summary>
    [MarkupExtensionReturnType(typeof(byte))]
    public class ByteExtension : MarkupExtension
    {
        /// <summary>
        /// Create an instance of this markup extension
        /// </summary>
        public ByteExtension() { }

        /// <summary>
        /// Create an instance of this markup extension with specified value
        /// </summary>
        /// <param name="value">Value</param>
        public ByteExtension(byte value)
        {
            Value = value;
        }

        /// <summary>
        /// Value
        /// </summary>
        public byte Value { get; set; } = 0;

        /// <inheritdoc/>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Value;
        }
    }
}
