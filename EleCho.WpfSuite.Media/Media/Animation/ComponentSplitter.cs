using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows;

namespace EleCho.WpfSuite.Media.Animation
{
    /// <summary>
    /// Splitter to split a value into multiply number of components
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <param name="components"></param>
    public delegate void ComponentSplitter<T>(T value, ref double[]? components);

    /// <summary>
    /// Merger to merge multiple components into a single value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="components"></param>
    /// <returns></returns>
    public delegate T ComponentMerger<T>(double[] components);
}
