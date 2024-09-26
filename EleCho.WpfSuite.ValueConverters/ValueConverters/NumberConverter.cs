using System;
using System.Globalization;
using System.Windows;

namespace EleCho.WpfSuite.ValueConverters
{
    /// <summary>
    /// Convert number to another type
    /// </summary>
    public class NumberConverter : ValueConverterBase<NumberConverter>
    {
        /// <summary>
        /// Target number type
        /// </summary>
        public Type? TargetType
        {
            get { return (Type)GetValue(TargetTypeProperty); }
            set { SetValue(TargetTypeProperty, value); }
        }

        /// <summary>
        /// DependencyProperty of <see cref="TargetType"/> property.
        /// </summary>
        public static readonly DependencyProperty TargetTypeProperty =
            DependencyProperty.Register(nameof(TargetType), typeof(Type), typeof(NumberConverter), new PropertyMetadata(null));

        /// <inheritdoc/>
        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            targetType = TargetType ?? targetType;

            switch (value)
            {
                case byte byteValue:
                {
                    if (typeof(byte) == targetType)
                    {
                        return (byte)byteValue;
                    }
                    else if (typeof(ushort) == targetType)
                    {
                        return (ushort)byteValue;
                    }
                    else if (typeof(uint) == targetType)
                    {
                        return (uint)byteValue;
                    }
                    else if (typeof(ulong) == targetType)
                    {
                        return (ulong)byteValue;
                    }
                    else if (typeof(short) == targetType)
                    {
                        return (short)byteValue;
                    }
                    else if (typeof(int) == targetType)
                    {
                        return (int)byteValue;
                    }
                    else if (typeof(long) == targetType)
                    {
                        return (long)byteValue;
                    }
                    else if (typeof(float) == targetType)
                    {
                        return (float)byteValue;
                    }
                    else if (typeof(double) == targetType)
                    {
                        return (double)byteValue;
                    }
                    else if (typeof(decimal) == targetType)
                    {
                        return (decimal)byteValue;
                    }
                    else
                    {
                        return DependencyProperty.UnsetValue;
                    }
                }

                case ushort ushortValue:
                {
                    if (typeof(byte) == targetType)
                    {
                        return (byte)ushortValue;
                    }
                    else if (typeof(ushort) == targetType)
                    {
                        return (ushort)ushortValue;
                    }
                    else if (typeof(uint) == targetType)
                    {
                        return (uint)ushortValue;
                    }
                    else if (typeof(ulong) == targetType)
                    {
                        return (ulong)ushortValue;
                    }
                    else if (typeof(short) == targetType)
                    {
                        return (short)ushortValue;
                    }
                    else if (typeof(int) == targetType)
                    {
                        return (int)ushortValue;
                    }
                    else if (typeof(long) == targetType)
                    {
                        return (long)ushortValue;
                    }
                    else if (typeof(float) == targetType)
                    {
                        return (float)ushortValue;
                    }
                    else if (typeof(double) == targetType)
                    {
                        return (double)ushortValue;
                    }
                    else if (typeof(decimal) == targetType)
                    {
                        return (decimal)ushortValue;
                    }
                    else
                    {
                        return DependencyProperty.UnsetValue;
                    }
                }

                case uint uintValue:
                {
                    if (typeof(byte) == targetType)
                    {
                        return (byte)uintValue;
                    }
                    else if (typeof(ushort) == targetType)
                    {
                        return (ushort)uintValue;
                    }
                    else if (typeof(uint) == targetType)
                    {
                        return (uint)uintValue;
                    }
                    else if (typeof(ulong) == targetType)
                    {
                        return (ulong)uintValue;
                    }
                    else if (typeof(short) == targetType)
                    {
                        return (short)uintValue;
                    }
                    else if (typeof(int) == targetType)
                    {
                        return (int)uintValue;
                    }
                    else if (typeof(long) == targetType)
                    {
                        return (long)uintValue;
                    }
                    else if (typeof(float) == targetType)
                    {
                        return (float)uintValue;
                    }
                    else if (typeof(double) == targetType)
                    {
                        return (double)uintValue;
                    }
                    else if (typeof(decimal) == targetType)
                    {
                        return (decimal)uintValue;
                    }
                    else
                    {
                        return DependencyProperty.UnsetValue;
                    }
                }

                case ulong ulongValue:
                {
                    if (typeof(byte) == targetType)
                    {
                        return (byte)ulongValue;
                    }
                    else if (typeof(ushort) == targetType)
                    {
                        return (ushort)ulongValue;
                    }
                    else if (typeof(uint) == targetType)
                    {
                        return (uint)ulongValue;
                    }
                    else if (typeof(ulong) == targetType)
                    {
                        return (ulong)ulongValue;
                    }
                    else if (typeof(short) == targetType)
                    {
                        return (short)ulongValue;
                    }
                    else if (typeof(int) == targetType)
                    {
                        return (int)ulongValue;
                    }
                    else if (typeof(long) == targetType)
                    {
                        return (long)ulongValue;
                    }
                    else if (typeof(float) == targetType)
                    {
                        return (float)ulongValue;
                    }
                    else if (typeof(double) == targetType)
                    {
                        return (double)ulongValue;
                    }
                    else if (typeof(decimal) == targetType)
                    {
                        return (decimal)ulongValue;
                    }
                    else
                    {
                        return DependencyProperty.UnsetValue;
                    }
                }

                case short shortValue:
                {
                    if (typeof(byte) == targetType)
                    {
                        return (byte)shortValue;
                    }
                    else if (typeof(ushort) == targetType)
                    {
                        return (ushort)shortValue;
                    }
                    else if (typeof(uint) == targetType)
                    {
                        return (uint)shortValue;
                    }
                    else if (typeof(ulong) == targetType)
                    {
                        return (ulong)shortValue;
                    }
                    else if (typeof(short) == targetType)
                    {
                        return (short)shortValue;
                    }
                    else if (typeof(int) == targetType)
                    {
                        return (int)shortValue;
                    }
                    else if (typeof(long) == targetType)
                    {
                        return (long)shortValue;
                    }
                    else if (typeof(float) == targetType)
                    {
                        return (float)shortValue;
                    }
                    else if (typeof(double) == targetType)
                    {
                        return (double)shortValue;
                    }
                    else if (typeof(decimal) == targetType)
                    {
                        return (decimal)shortValue;
                    }
                    else
                    {
                        return DependencyProperty.UnsetValue;
                    }
                }

                case int intValue:
                {
                    if (typeof(byte) == targetType)
                    {
                        return (byte)intValue;
                    }
                    else if (typeof(ushort) == targetType)
                    {
                        return (ushort)intValue;
                    }
                    else if (typeof(uint) == targetType)
                    {
                        return (uint)intValue;
                    }
                    else if (typeof(ulong) == targetType)
                    {
                        return (ulong)intValue;
                    }
                    else if (typeof(short) == targetType)
                    {
                        return (short)intValue;
                    }
                    else if (typeof(int) == targetType)
                    {
                        return (int)intValue;
                    }
                    else if (typeof(long) == targetType)
                    {
                        return (long)intValue;
                    }
                    else if (typeof(float) == targetType)
                    {
                        return (float)intValue;
                    }
                    else if (typeof(double) == targetType)
                    {
                        return (double)intValue;
                    }
                    else if (typeof(decimal) == targetType)
                    {
                        return (decimal)intValue;
                    }
                    else
                    {
                        return DependencyProperty.UnsetValue;
                    }
                }

                case long longValue:
                {
                    if (typeof(byte) == targetType)
                    {
                        return (byte)longValue;
                    }
                    else if (typeof(ushort) == targetType)
                    {
                        return (ushort)longValue;
                    }
                    else if (typeof(uint) == targetType)
                    {
                        return (uint)longValue;
                    }
                    else if (typeof(ulong) == targetType)
                    {
                        return (ulong)longValue;
                    }
                    else if (typeof(short) == targetType)
                    {
                        return (short)longValue;
                    }
                    else if (typeof(int) == targetType)
                    {
                        return (int)longValue;
                    }
                    else if (typeof(long) == targetType)
                    {
                        return (long)longValue;
                    }
                    else if (typeof(float) == targetType)
                    {
                        return (float)longValue;
                    }
                    else if (typeof(double) == targetType)
                    {
                        return (double)longValue;
                    }
                    else if (typeof(decimal) == targetType)
                    {
                        return (decimal)longValue;
                    }
                    else
                    {
                        return DependencyProperty.UnsetValue;
                    }
                }

                case float floatValue:
                {
                    if (typeof(byte) == targetType)
                    {
                        return (byte)floatValue;
                    }
                    else if (typeof(ushort) == targetType)
                    {
                        return (ushort)floatValue;
                    }
                    else if (typeof(uint) == targetType)
                    {
                        return (uint)floatValue;
                    }
                    else if (typeof(ulong) == targetType)
                    {
                        return (ulong)floatValue;
                    }
                    else if (typeof(short) == targetType)
                    {
                        return (short)floatValue;
                    }
                    else if (typeof(int) == targetType)
                    {
                        return (int)floatValue;
                    }
                    else if (typeof(long) == targetType)
                    {
                        return (long)floatValue;
                    }
                    else if (typeof(float) == targetType)
                    {
                        return (float)floatValue;
                    }
                    else if (typeof(double) == targetType)
                    {
                        return (double)floatValue;
                    }
                    else if (typeof(decimal) == targetType)
                    {
                        return (decimal)floatValue;
                    }
                    else
                    {
                        return DependencyProperty.UnsetValue;
                    }
                }

                case double doubleValue:
                {
                    if (typeof(byte) == targetType)
                    {
                        return (byte)doubleValue;
                    }
                    else if (typeof(ushort) == targetType)
                    {
                        return (ushort)doubleValue;
                    }
                    else if (typeof(uint) == targetType)
                    {
                        return (uint)doubleValue;
                    }
                    else if (typeof(ulong) == targetType)
                    {
                        return (ulong)doubleValue;
                    }
                    else if (typeof(short) == targetType)
                    {
                        return (short)doubleValue;
                    }
                    else if (typeof(int) == targetType)
                    {
                        return (int)doubleValue;
                    }
                    else if (typeof(long) == targetType)
                    {
                        return (long)doubleValue;
                    }
                    else if (typeof(float) == targetType)
                    {
                        return (float)doubleValue;
                    }
                    else if (typeof(double) == targetType)
                    {
                        return (double)doubleValue;
                    }
                    else if (typeof(decimal) == targetType)
                    {
                        return (decimal)doubleValue;
                    }
                    else
                    {
                        return DependencyProperty.UnsetValue;
                    }
                }

                case decimal decimalValue:
                {
                    if (typeof(byte) == targetType)
                    {
                        return (byte)decimalValue;
                    }
                    else if (typeof(ushort) == targetType)
                    {
                        return (ushort)decimalValue;
                    }
                    else if (typeof(uint) == targetType)
                    {
                        return (uint)decimalValue;
                    }
                    else if (typeof(ulong) == targetType)
                    {
                        return (ulong)decimalValue;
                    }
                    else if (typeof(short) == targetType)
                    {
                        return (short)decimalValue;
                    }
                    else if (typeof(int) == targetType)
                    {
                        return (int)decimalValue;
                    }
                    else if (typeof(long) == targetType)
                    {
                        return (long)decimalValue;
                    }
                    else if (typeof(float) == targetType)
                    {
                        return (float)decimalValue;
                    }
                    else if (typeof(double) == targetType)
                    {
                        return (double)decimalValue;
                    }
                    else if (typeof(decimal) == targetType)
                    {
                        return (decimal)decimalValue;
                    }
                    else
                    {
                        return DependencyProperty.UnsetValue;
                    }
                }

                default:
                {
                    return DependencyProperty.UnsetValue;
                }
            }
        }

        /// <inheritdoc/>
        public override object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return Convert(value, targetType, parameter, culture);
        }
    }
}
