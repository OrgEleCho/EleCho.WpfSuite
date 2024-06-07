using System;
using System.Windows.Markup;

namespace EleCho.WpfSuite.Markup
{
    /// <summary>
    /// Provide a value of <see cref="String"/>
    /// </summary>
    [MarkupExtensionReturnType(typeof(string))]
    public class String : MarkupExtension
    {
        /// <summary>
        /// Create an instance of this markup extension
        /// </summary>
        public String() { }

        /// <summary>
        /// Create an instance of this markup extension with specified value
        /// </summary>
        /// <param name="value">Value</param>
        public String(string value)
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
