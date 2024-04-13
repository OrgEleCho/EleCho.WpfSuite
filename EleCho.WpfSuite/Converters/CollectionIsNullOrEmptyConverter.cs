using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Windows;

namespace EleCho.WpfSuite
{
    public class CollectionIsNullOrEmptyConverter : SingletonValueConverterBase<CollectionIsNullOrEmptyConverter>
    {
        static readonly Type _typeGenericCollection = typeof(ICollection<>);
        static readonly PropertyInfo _genericCollectionCountProperty = _typeGenericCollection.GetProperty(nameof(ICollection<object>.Count))!;

        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is null)
                return true;

            if (value is ICollection nonGenericCollection)
            {
                return nonGenericCollection.Count == 0;
            }
            else
            {
                var valueType = value.GetType();
                if (valueType.IsGenericType && valueType.GetGenericTypeDefinition() == _typeGenericCollection)
                {
                    return (int)(_genericCollectionCountProperty.GetValue(value)!) == 0;
                }
            }

            return DependencyProperty.UnsetValue;
        }
    }
}
