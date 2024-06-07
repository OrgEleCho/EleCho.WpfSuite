using System;
using System.Windows.Data;
using System.Windows.Markup;

namespace EleCho.WpfSuite.Markup
{
    /// <summary>
    /// Provide a value of <see cref="Boolean"/>
    /// </summary>
    [MarkupExtensionReturnType(typeof(bool))]
    public class Boolean : MarkupExtension
    {
        /// <summary>
        /// Create an instance of this markup extension
        /// </summary>
        public Boolean() { }

        /// <summary>
        /// Create an instance of this markup extension with specified value
        /// </summary>
        /// <param name="value">Value</param>
        public Boolean(bool value)
        {
            Value = value;
        }


        /// <summary>
        /// Value
        /// </summary>
        public bool Value { get; set; } = false;

        /// <inheritdoc/>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Value;
        }
    }
}
