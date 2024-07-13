using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EleCho.WpfSuite.FluentDesign
{
    public class FluentDesignResourceKey : ResourceKey
    {
        public FluentDesignResourceKey()
        {
            Key = string.Empty;
        }

        public FluentDesignResourceKey(string key)
        {
            Key = key;
        }

        public override Assembly Assembly => typeof(FluentDesignResourceKey).Assembly;

        public string Key { get; set; }
    }
}
