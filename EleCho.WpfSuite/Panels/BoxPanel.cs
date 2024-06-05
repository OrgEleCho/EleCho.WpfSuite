using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EleCho.WpfSuite
{
    /// <summary>
    /// Box
    /// </summary>
    public class BoxPanel : Panel
    {
        /// <inheritdoc/>
        protected override Size MeasureOverride(Size availableSize)
        {
            var finalSize = new Size(0, 0);

            foreach (UIElement child in InternalChildren)
            {
                child.Measure(availableSize);

                var childDesiredSize = child.DesiredSize;
                if (childDesiredSize.Width > finalSize.Width)
                    finalSize.Width = childDesiredSize.Width;
                if (childDesiredSize.Height > finalSize.Height)
                    finalSize.Height = childDesiredSize.Height;
            }

            return finalSize;
        }

        /// <inheritdoc/>
        protected override Size ArrangeOverride(Size finalSize)
        {
            foreach (UIElement child in InternalChildren)
            {
                child.Arrange(new Rect(0, 0, finalSize.Width, finalSize.Height));
            }

            return finalSize;
        }
    }
}
