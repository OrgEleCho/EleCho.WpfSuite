using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace EleCho.WpfSuite
{
    /// <summary>
    /// DataTemplate selector based on the type of data
    /// </summary>
    [ContentProperty(nameof(Templates))]
    public class TypeMatchDataTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// Matches for DataTemplate selection
        /// </summary>
        public List<DataTemplate> Templates { get; } = new();

        /// <inheritdoc/>
        public override DataTemplate? SelectTemplate(object data, DependencyObject container)
        {
            if (data is null)
                return null;

            var dataType = data.GetType();
            var fallbackTemplate = default(DataTemplate);

            foreach (var template in Templates)
            {
                if (template.DataType is not Type templateDataType)
                {
                    if (template.DataType is not null)
                    {
                        throw new InvalidOperationException($"Invalid DataTemplate for TypeMatchDataTemplateSelector, 'DataType' property of DataTemplate must be 'System.Type', not '{template.DataType.GetType()}'");
                    }

                    fallbackTemplate = template;
                    continue;
                }

                if (templateDataType.IsAssignableFrom(dataType))
                    return template;
            }

            return fallbackTemplate;
        }
    }
}
