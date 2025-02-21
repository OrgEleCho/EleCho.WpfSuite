using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace EleCho.WpfSuite.Internal
{
    internal static class VisualTreeUtils
    {
        public static T? FindChild<T>(DependencyObject dependencyObject)
            where T : DependencyObject
        {
            var childCount = VisualTreeHelper.GetChildrenCount(dependencyObject);

            for (int i = 0; i < childCount; i++)
            {
                if (VisualTreeHelper.GetChild(dependencyObject, i) is T result)
                {
                    return result;
                }
            }

            return null;
        }
    }
}
