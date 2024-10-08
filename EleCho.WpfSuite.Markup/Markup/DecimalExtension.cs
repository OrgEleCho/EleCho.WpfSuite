﻿using System;
using System.Windows.Markup;

namespace EleCho.WpfSuite.Markup
{
    /// <summary>
    /// Provide a value of <see cref="Decimal"/>
    /// </summary>
    [MarkupExtensionReturnType(typeof(decimal))]
    public class DecimalExtension : MarkupExtension
    {
        /// <summary>
        /// Create an instance of this markup extension
        /// </summary>
        public DecimalExtension() { }

        /// <summary>
        /// Create an instance of this markup extension with specified value
        /// </summary>
        /// <param name="value">Value</param>
        public DecimalExtension(decimal value)
        {
            Value = value;
        }


        /// <summary>
        /// Value
        /// </summary>
        public decimal Value { get; set; } = 0;

        /// <inheritdoc/>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Value;
        }
    }
}
