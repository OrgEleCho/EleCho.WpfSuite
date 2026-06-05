using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;

namespace EleCho.WpfSuite.Controls
{
    public class VisibilityTransitioner : SizeTransitioner
    {
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register(nameof(Orientation), typeof(Orientation), typeof(VisibilityTransitioner),
                new FrameworkPropertyMetadata(Orientation.Vertical, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register(nameof(IsOpen), typeof(bool), typeof(VisibilityTransitioner),
                new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsMeasure));

        protected override Size GetTargetMeasureSize(Size constraint)
        {
            var originSize = base.GetTargetMeasureSize(constraint);
            if (IsOpen)
            {
                return originSize;
            }

            if (Orientation == Orientation.Vertical)
            {
                return new Size(originSize.Width, 0);
            }
            else
            {
                return new Size(0, originSize.Height);
            }
        }

        protected override void PrepareTransition(ref Size initialSize, Size targetSize)
        {
            if (Orientation == Orientation.Vertical)
            {
                initialSize.Width = targetSize.Width;
            }
            else
            {
                initialSize.Height = targetSize.Height;
            }
        }
    }
}
