using System;
using System.Reflection;
using System.Windows;

namespace EleCho.WpfSuite.FluentDesign
{

    public class FluentResources : ResourceDictionary
    {
        FluentThemeResources _themeResource;
        ResourceDictionary _overviewResources;

        public FluentResources()
        {
            _themeResource = new FluentThemeResources();
            _overviewResources = new ResourceDictionary() { Source = new Uri("pack://application:,,,/EleCho.WpfSuite.FluentDesign;component/Styles/OverviewResources.xaml") };

            MergedDictionaries.Add(_themeResource);
            MergedDictionaries.Add(_overviewResources);
        }

        public bool IsDarkMode 
        { 
            get => _themeResource.IsDarkMode;  
            set => _themeResource.IsDarkMode = value; 
        }
    }
}
