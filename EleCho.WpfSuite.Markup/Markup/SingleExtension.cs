﻿using System;
using System.Windows.Markup;

namespace EleCho.WpfSuite.Markup
{
    /// <summary>
    /// Provide a value of <see cref="Single"/>
    /// </summary>
    [MarkupExtensionReturnType(typeof(float))]
    public class SingleExtension : MarkupExtension
    {
        /// <summary>
        /// Create an instance of this markup extension
        /// </summary>
        public SingleExtension() { }

        /// <summary>
        /// Create an instance of this markup extension with specified value
        /// </summary>
        /// <param name="value">Value</param>
        public SingleExtension(float value)
        {
            Value = value;
        }

        /// <summary>
        /// Value
        /// </summary>
        public float Value { get; set; } = 0.0f;

        /// <inheritdoc/>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Value;
        }
    }
}
