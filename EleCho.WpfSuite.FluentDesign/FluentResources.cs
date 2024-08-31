using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using Microsoft.Win32;

namespace EleCho.WpfSuite.FluentDesign
{

    public class FluentResources : ResourceDictionary
    {
        FluentThemeResources _themeResource;
        ResourceDictionary _commonResources;

        public FluentResources()
        {
            _themeResource = new FluentThemeResources();
            _commonResources = new ResourceDictionary() { Source = new Uri("pack://application:,,,/EleCho.WpfSuite.FluentDesign;component/Styles/CommonResources.xaml") };

            MergedDictionaries.Add(_themeResource);
            MergedDictionaries.Add(_commonResources);
        }

        public ApplicationTheme Theme 
        { 
            get => _themeResource.Theme;  
            set => _themeResource.Theme = value; 
        }

        public ApplicationTheme ActualTheme => _themeResource.ActualTheme;
    }
}
