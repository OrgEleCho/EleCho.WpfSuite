using System;
using System.Windows.Markup;

namespace EleCho.WpfSuite.Markup
{
    /// <summary>
    /// Provide a value of <see cref="Char"/>
    /// </summary>
    [MarkupExtensionReturnType(typeof(char))]
    public class CharExtension : MarkupExtension
    {
        /// <summary>
        /// Create an instance of this markup extension
        /// </summary>
        public CharExtension() { }

        /// <summary>
        /// Create an instance of this markup extension with specified value
        /// </summary>
        /// <param name="value">Value</param>
        public CharExtension(char value)
        {
            Value = value;
        }


        /// <summary>
        /// Value
        /// </summary>
        public char Value { get; set; } = '\0';

        /// <inheritdoc/>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Value;
        }
    }
}
