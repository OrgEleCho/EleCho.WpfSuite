using System.Collections.Generic;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EleCho.WpfSuite.Layouts
{
    /// <summary>
    /// Arranges child elements into a single line that can be oriented horizontally or vertically.
    /// </summary>
    public class StackPanel : Panel
    {
        /// <summary>
        /// Layout orientation
        /// </summary>
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        /// <summary>
        /// Spacing between children
        /// </summary>
        public double Spacing
        {
            get { return (double)GetValue(SpacingProperty); }
            set { SetValue(SpacingProperty, value); }
        }

        /// <inheritdoc/>
        protected override Size MeasureOverride(Size availableSize)
        {
            var spacing = Spacing;
            var panelDesiredSize = new Size();
            var hasTailingSpacing = false;

            if (Orientation == Orientation.Vertical)
            {
                availableSize.Height = double.PositiveInfinity;
                foreach (UIElement child in InternalChildren)
                {
                    child.Measure(availableSize);
                    var childDesiredSize = child.DesiredSize;

                    panelDesiredSize.Height += childDesiredSize.Height;

                    if (child.Visibility != Visibility.Collapsed)
                    {
                        hasTailingSpacing = true;
                        panelDesiredSize.Height += spacing;
                    }
                    else
                    {
                        hasTailingSpacing = false;
                    }

                    if (childDesiredSize.Width > panelDesiredSize.Width)
                    {
                        panelDesiredSize.Width = childDesiredSize.Width;
                    }
                }

                if (hasTailingSpacing)
                {
                    panelDesiredSize.Height -= spacing;
                }
            }
            else
            {
                availableSize.Width = double.PositiveInfinity;
                foreach (UIElement child in InternalChildren)
                {
                    child.Measure(availableSize);
                    var childDesiredSize = child.DesiredSize;

                    panelDesiredSize.Width += childDesiredSize.Width;

                    if (child.Visibility != Visibility.Collapsed)
                    {
                        hasTailingSpacing = true;
                        panelDesiredSize.Width += spacing;
                    }
                    else
                    {
                        hasTailingSpacing = false;
                    }

                    if (childDesiredSize.Height > panelDesiredSize.Height)
                    {
                        panelDesiredSize.Height = childDesiredSize.Height;
                    }
                }

                if (hasTailingSpacing)
                {
                    panelDesiredSize.Width -= spacing;
                }
            }

            return panelDesiredSize;
        }

        /// <inheritdoc/>
        protected override Size ArrangeOverride(Size finalSize)
        {
            var offset = 0.0;
            var spacing = Spacing;

            if (Orientation == Orientation.Vertical)
            {
                foreach (UIElement child in InternalChildren)
                {
                    var childDesiredSize = child.DesiredSize;

                    child.Arrange(new Rect(0, offset, finalSize.Width, childDesiredSize.Height));

                    offset += childDesiredSize.Height;

                    if (child.Visibility != Visibility.Collapsed)
                        offset += spacing;
                }
            }
            else
            {
                foreach (UIElement child in InternalChildren)
                {
                    var childDesiredSize = child.DesiredSize;

                    child.Arrange(new Rect(offset, 0, childDesiredSize.Width, finalSize.Height));

                    offset += childDesiredSize.Width;
                    if (child.Visibility != Visibility.Collapsed)
                        offset += spacing;
                }
            }

            return finalSize;
        }


        /// <summary>
        /// The DependencyProperty of <see cref="Orientation"/> property
        /// </summary>
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register(nameof(Orientation), typeof(Orientation), typeof(StackPanel), new FrameworkPropertyMetadata(Orientation.Vertical, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// The DependencyProperty of <see cref="Spacing"/> property
        /// </summary>
        public static readonly DependencyProperty SpacingProperty =
            DependencyProperty.Register(nameof(Spacing), typeof(double), typeof(StackPanel), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure), ValidationUtils.IsInRangeOfPosDoubleIncludeZero);
    }
}
