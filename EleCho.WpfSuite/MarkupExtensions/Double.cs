using System;
using System.Windows.Markup;

namespace EleCho.WpfSuite.Markup
{
    /// <summary>
    /// Provide a value of <see cref="Double"/>
    /// </summary>
    [MarkupExtensionReturnType(typeof(double))]
    public class Double : MarkupExtension
    {
        /// <summary>
        /// Create an instance of this markup extension
        /// </summary>
        public Double() { }

        /// <summary>
        /// Create an instance of this markup extension with specified value
        /// </summary>
        /// <param name="value">Value</param>
        public Double(double value)
        {
            Value = value;
        }

        /// <summary>
        /// Value
        /// </summary>
        public double Value { get; set; } = 0.0;

        /// <inheritdoc/>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Value;
        }
    }
}
