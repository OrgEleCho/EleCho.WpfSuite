﻿using System;
using System.Globalization;

namespace EleCho.WpfSuite.ValueConverters
{
    /// <summary>
    /// Value converter that returns a Boolean value that indicates that the string is not null or whitespace
    /// </summary>
    public class StringIsNotNullOrWhiteSpaceConverter : SingletonValueConverterBase<StringIsNotNullOrWhiteSpaceConverter>
    {
        /// <inheritdoc/>
        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return !string.IsNullOrWhiteSpace(value as string);
        }
    }
}
