using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using Microsoft.Win32;

namespace EleCho.WpfSuite.FluentDesign
{

    /// <summary>
    /// Provides a combined Fluent resource dictionary that includes theme and common resources.
    /// </summary>
    public class FluentResources : ResourceDictionary
    {
        FluentThemeResources _themeResource;
        ResourceDictionary _commonResources;

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentResources"/> class.
        /// </summary>
        public FluentResources()
        {
            _themeResource = new FluentThemeResources();
            _commonResources = new ResourceDictionary() { Source = new Uri("pack://application:,,,/EleCho.WpfSuite.FluentDesign;component/Styles/CommonResources.xaml") };

            MergedDictionaries.Add(_themeResource);
            MergedDictionaries.Add(_commonResources);
        }

        /// <summary>
        /// Gets or sets the configured application theme.
        /// </summary>
        public ApplicationTheme Theme 
        { 
            get => _themeResource.Theme;  
            set => _themeResource.Theme = value; 
        }

        /// <summary>
        /// Gets the currently applied theme.
        /// </summary>
        public ApplicationTheme ActualTheme => _themeResource.ActualTheme;
    }
}
