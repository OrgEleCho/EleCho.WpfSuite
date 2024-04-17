using System;
using System.Windows;

namespace EleCho.WpfSuite.SimpleDesign
{
    public class SimpleDesignResources : ResourceDictionary
    {
        public SimpleDesignResources()
        {
            MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("pack://application:,,,/EleCho.WpfSuite.SimpleDesign;component/SimpleDesignResources.xaml") });
        }
    }
}
