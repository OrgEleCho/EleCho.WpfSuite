using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace EleCho.WpfSuite.Controls
{
    [ContentProperty(nameof(Children))]
    public class Grid : System.Windows.Controls.Grid
    {
        public static readonly DependencyProperty ColumnDefinitionsProperty =
              DependencyProperty.Register(
                  nameof(ColumnDefinitions),
                  typeof(ColumnDefinitionCollection),
                  typeof(Grid),
                  new PropertyMetadata(null, OnColumnDefinitionsChanged));

        [TypeConverter(typeof(ColumnDefinitionsConverter))]
        public new ColumnDefinitionCollection ColumnDefinitions
        {
            get { return base.ColumnDefinitions; }
            set { SetValue(ColumnDefinitionsProperty, value); }
        }

        private static void OnColumnDefinitionsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Grid grid && e.NewValue is ColumnDefinitionCollection columnDefinitions)
            {
                grid.UpdateColumnDefinitions(columnDefinitions);
            }
        }

        private void UpdateColumnDefinitions(ColumnDefinitionCollection columnDefinitions)
        {
            base.ColumnDefinitions.Clear();
            foreach (ColumnDefinition columnDefinition in columnDefinitions)
            {
                base.ColumnDefinitions.Add(new ColumnDefinition { Width = columnDefinition.Width });
            }
        }

        public static readonly DependencyProperty RowDefinitionsProperty =
              DependencyProperty.Register(
                  nameof(RowDefinitions),
                  typeof(RowDefinitionCollection),
                  typeof(Grid),
                  new PropertyMetadata(null, OnRowDefinitionsChanged));

        [TypeConverter(typeof(RowDefinitionsConverter))]
        public new RowDefinitionCollection RowDefinitions
        {
            get { return base.RowDefinitions; }
            set { SetValue(RowDefinitionsProperty, value); }
        }

        private static void OnRowDefinitionsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Grid grid && e.NewValue is RowDefinitionCollection rowDefinitions)
            {
                grid.UpdateRowDefinitions(rowDefinitions);
            }
        }

        private void UpdateRowDefinitions(RowDefinitionCollection rowDefinitions)
        {
            base.RowDefinitions.Clear();
            foreach (RowDefinition rowDefinition in rowDefinitions)
            {
                base.RowDefinitions.Add(new RowDefinition { Height = rowDefinition.Height });
            }
        }
    }

    internal sealed class ColumnDefinitionsConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        {
            return sourceType == typeof(ColumnDefinitionCollection) || base.CanConvertFrom(context, sourceType);
        }

        public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        {
            bool hotReload = false;

            try
            {
                if (value is string strValue)
                {
                    System.Windows.Controls.Grid _grid = null!;

                    if (context is IProvideValueTarget target)
                    {
                        _grid = (System.Windows.Controls.Grid)target.TargetObject;
                    }
                    else
                    {
                        // Support for Hot Reload (WpfVisualTreeService.LiveMarkup.TapTypeDescriptorContext).
                        _grid = new System.Windows.Controls.Grid();
                        hotReload = true;
                    }

                    ColumnDefinitionCollection columnDefinitions = (ColumnDefinitionCollection)Activator.CreateInstance(
                        typeof(ColumnDefinitionCollection),
                        BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.OptionalParamBinding,
                        null!,
                        [_grid],
                        null!)!;
                    GridLengthConverter converter = new();
                    string[] definitions = strValue.Split(',');

                    foreach (string definition in definitions)
                    {
                        GridLength gridLength = (GridLength)converter.ConvertFromString(definition.Trim())!;
                        columnDefinitions.Add(new ColumnDefinition { Width = gridLength });
                    }

                    return columnDefinitions;
                }
                else if (value is ColumnDefinitionCollection columnDefinitions)
                {
                    return columnDefinitions;
                }
            }
            catch (Exception e)
            {
                if (!hotReload)
                {
                    throw new InvalidOperationException($"Invalid ColumnDefinitions value \"{value}\".{Environment.NewLine}" + e);
                }
                return DependencyProperty.UnsetValue;
            }

            return base.ConvertFrom(context, culture, value);
        }
    }

    internal sealed class RowDefinitionsConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        {
            return sourceType == typeof(RowDefinitionCollection) || base.CanConvertFrom(context, sourceType);
        }

        public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        {
            bool hotReload = false;

            try
            {
                if (value is string strValue)
                {
                    System.Windows.Controls.Grid _grid = null!;

                    if (context is IProvideValueTarget target)
                    {
                        _grid = (System.Windows.Controls.Grid)target.TargetObject;
                    }
                    else
                    {
                        // Support for Hot Reload (WpfVisualTreeService.LiveMarkup.TapTypeDescriptorContext).
                        _grid = new System.Windows.Controls.Grid();
                        hotReload = true;
                    }

                    RowDefinitionCollection rowDefinitions = (RowDefinitionCollection)Activator.CreateInstance(
                        typeof(RowDefinitionCollection),
                        BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.OptionalParamBinding,
                        null!,
                        [_grid],
                        null!)!;
                    GridLengthConverter converter = new();
                    string[] definitions = strValue.Split(',');

                    foreach (string definition in definitions)
                    {
                        GridLength gridLength = (GridLength)converter.ConvertFromString(definition.Trim())!;
                        rowDefinitions.Add(new RowDefinition { Height = gridLength });
                    }

                    return rowDefinitions;
                }
                else if (value is RowDefinitionCollection rowDefinitions)
                {
                    return rowDefinitions;
                }
            }
            catch (Exception e)
            {
                if (!hotReload)
                {
                    throw new InvalidOperationException($"Invalid RowDefinitions value \"{value}\".{Environment.NewLine}" + e);
                }
                return DependencyProperty.UnsetValue;
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}
