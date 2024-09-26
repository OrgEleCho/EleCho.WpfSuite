using System;
using System.Windows;
using System.Windows.Markup;

namespace EleCho.WpfSuite.Markup
{
    /// <summary>
    /// Provide an array that contains all values of enum type
    /// </summary>
    public class EnumValuesExtension : MarkupExtension
    {
        /// <summary>
        /// Target enum type
        /// </summary>
        public Type? Type { get; set; }

        /// <summary>
        /// Create an empty instance of this markup extension
        /// </summary>
        public EnumValuesExtension()
        {

        }

        /// <summary>
        /// Create an instance of this markup extension with specified type
        /// </summary>
        /// <param name="type"></param>
        public EnumValuesExtension(Type? type)
        {
            Type = type;
        }

        /// <inheritdoc/>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Type is null)
            {
                return DependencyProperty.UnsetValue;
            }

            if (!Type.IsEnum)
            {
                throw new ArgumentException("Specified type is not enum type");
            }

            return Enum.GetValues(Type);
        }
    }
}
