using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace EleCho.WpfSuite
{
    public interface IContentTransition
    {
        public Task Run(FrameworkElement container, FrameworkElement? oldContent, FrameworkElement? newContent, bool forward, CancellationToken cancellationToken);
    }
}
