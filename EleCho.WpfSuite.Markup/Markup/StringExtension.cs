using System;
using System.Windows.Markup;

namespace EleCho.WpfSuite.Markup
{
    /// <summary>
    /// Provide a value of <see cref="String"/>
    /// </summary>
    [MarkupExtensionReturnType(typeof(string))]
    public class StringExtension : MarkupExtension
    {
        /// <summary>
        /// Create an instance of this markup extension
        /// </summary>
        public StringExtension() { }

        /// <summary>
        /// Create an instance of this markup extension with specified value
        /// </summary>
        /// <param name="value">Value</param>
        public StringExtension(string value)
        {
            Value = value;
        }

        /// <summary>
        /// Value
        /// </summary>
        public string Value { get; set; } = string.Empty;

        /// <inheritdoc/>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Value;
        }
    }
}
