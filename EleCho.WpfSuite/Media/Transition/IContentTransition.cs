using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace EleCho.WpfSuite.Media.Transition
{
    /// <summary>
    /// Content transition
    /// </summary>
    public interface IContentTransition
    {
        /// <summary>
        /// Run the content transition
        /// </summary>
        /// <param name="container">Container UIElement</param>
        /// <param name="oldContent">Old content UIElement</param>
        /// <param name="newContent">New content UIElement</param>
        /// <param name="forward">This transition is forward</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns></returns>
        public Task Run(FrameworkElement container, FrameworkElement? oldContent, FrameworkElement? newContent, bool forward, CancellationToken cancellationToken);
    }
}
