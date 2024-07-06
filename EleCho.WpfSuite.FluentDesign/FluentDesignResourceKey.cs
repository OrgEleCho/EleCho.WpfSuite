using System.Reflection;
using System.Windows;

namespace EleCho.WpfSuite.FluentDesign
{
    public class FluentDesignResourceKey : ResourceKey
    {
        private static readonly Assembly s_thisAssembly = Assembly.GetExecutingAssembly();

        public override Assembly Assembly => s_thisAssembly;

        public string Name { get; set; } = string.Empty;

        public FluentDesignResourceKey(string name)
        {
            Name = name;
        }

        public FluentDesignResourceKey()
        {
        }
    }
}
