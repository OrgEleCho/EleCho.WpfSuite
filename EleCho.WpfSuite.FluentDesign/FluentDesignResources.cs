using System;
using System.Reflection;
using System.Windows;

namespace EleCho.WpfSuite.FluentDesign
{

    public class FluentDesignResources : ResourceDictionary
    {
        FluentDesignThemeResources _themeResource;
        ResourceDictionary _overviewResources;

        public FluentDesignResources()
        {
            _themeResource = new FluentDesignThemeResources();
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
