using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace EleCho.WpfSuite
{
    public class StackPanel : Panel, IScrollInfo
    {
        private Size _viewport;
        private Size _extent;

        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public double Spacing
        {
            get { return (double)GetValue(SpacingProperty); }
            set { SetValue(SpacingProperty, value); }
        }

        [DefaultValue(false)]
        public bool CanVerticallyScroll { get; set; } = false;

        [DefaultValue(false)]
        public bool CanHorizontallyScroll { get; set; } = false;

        public double ExtentWidth => _extent.Width;

        public double ExtentHeight => _extent.Height;

        public double ViewportWidth => _viewport.Width;

        public double ViewportHeight => _viewport.Height;

        public double HorizontalOffset => throw new System.NotImplementedException();

        public double VerticalOffset => throw new System.NotImplementedException();

        public ScrollViewer? ScrollOwner { get; set; }

        protected override Size MeasureOverride(Size availableSize)
        {
            var spacing = Spacing; 
            var panelDesiredSize = new Size();

            if (Orientation == Orientation.Vertical)
            {
                availableSize.Height = double.PositiveInfinity;
                foreach (UIElement child in InternalChildren)
                {
                    child.Measure(availableSize);
                    var childDesiredSize = child.DesiredSize;

                    panelDesiredSize.Height += childDesiredSize.Height;
                    panelDesiredSize.Height += spacing;

                    if (childDesiredSize.Width > panelDesiredSize.Width)
                        panelDesiredSize.Width = childDesiredSize.Width;
                }

                panelDesiredSize.Height -= spacing;
            }
            else
            {
                availableSize.Width = double.PositiveInfinity;
                foreach (UIElement child in InternalChildren)
                {
                    child.Measure(availableSize);
                    var childDesiredSize = child.DesiredSize;

                    panelDesiredSize.Width += childDesiredSize.Width;
                    panelDesiredSize.Width += spacing;

                    if (childDesiredSize.Height > panelDesiredSize.Height)
                        panelDesiredSize.Height = childDesiredSize.Height;
                }

                panelDesiredSize.Width -= spacing;
            }

            return panelDesiredSize;
        }

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
                    offset += spacing;
                }
            }

            return finalSize;
        }

        public void LineUp() => throw new System.NotImplementedException();
        public void LineDown() => throw new System.NotImplementedException();
        public void LineLeft() => throw new System.NotImplementedException();
        public void LineRight() => throw new System.NotImplementedException();
        public void PageUp() => throw new System.NotImplementedException();
        public void PageDown() => throw new System.NotImplementedException();
        public void PageLeft() => throw new System.NotImplementedException();
        public void PageRight() => throw new System.NotImplementedException();
        public void MouseWheelUp() => throw new System.NotImplementedException();
        public void MouseWheelDown() => throw new System.NotImplementedException();
        public void MouseWheelLeft() => throw new System.NotImplementedException();
        public void MouseWheelRight() => throw new System.NotImplementedException();
        public void SetHorizontalOffset(double offset) => throw new System.NotImplementedException();
        public void SetVerticalOffset(double offset) => throw new System.NotImplementedException();
        public Rect MakeVisible(Visual visual, Rect rectangle) => throw new System.NotImplementedException();


        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register(nameof(Orientation), typeof(Orientation), typeof(StackPanel), new FrameworkPropertyMetadata(Orientation.Vertical, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty SpacingProperty =
            DependencyProperty.Register(nameof(Spacing), typeof(double), typeof(StackPanel), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure));
    }
}
