using System;
using System.Reflection;
using System.Windows;

namespace EleCho.WpfSuite.FluentDesign
{
    public class FluentDesignResourceKey : ResourceKey
    {
        private static readonly Assembly s_thisAssembly = Assembly.GetExecutingAssembly();

        public override Assembly Assembly => s_thisAssembly;
    }

    public class FluentDesignResources : ResourceDictionary
    {
        public FluentDesignResources()
        {
            MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("pack://application:,,,/EleCho.WpfSuite.FluentDesign;component/FluentDesignResources.xaml") });
        }

        
    }
}
