using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace EleCho.WpfSuite.Markup
{
    /// <summary>
    /// Provide a value of <see cref="Boolean"/>
    /// </summary>
    [MarkupExtensionReturnType(typeof(bool))]
    public class BooleanExtension : MarkupExtension
    {
        /// <summary>
        /// Create an instance of this markup extension
        /// </summary>
        public BooleanExtension() { }

        /// <summary>
        /// Create an instance of this markup extension with specified value
        /// </summary>
        /// <param name="value">Value</param>
        public BooleanExtension(bool value)
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
