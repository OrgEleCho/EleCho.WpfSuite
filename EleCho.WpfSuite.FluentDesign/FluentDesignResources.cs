using System;
using System.Windows;

namespace EleCho.WpfSuite.FluentDesign
{
    public class FluentDesignResources : ResourceDictionary
    {
        public FluentDesignResources()
        {
            MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("pack://application:,,,/EleCho.WpfSuite.FluentDesign;component/FluentDesignResources.xaml") });
        }
    }
}
