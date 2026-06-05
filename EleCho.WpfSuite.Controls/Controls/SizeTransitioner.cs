using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace EleCho.WpfSuite.Controls
{
    public class SizeTransitioner : Decorator
    {
        private Size _targetMeasureSize;
        private static readonly IEasingFunction _easing;

        static SizeTransitioner()
        {
            var easing = new SineEase() { EasingMode = EasingMode.EaseOut };
            easing.Freeze();
            _easing = easing;
        }

        private Size MeasureSizeAnimated
        {
            get { return (Size)GetValue(MeasureSizeAnimatedProperty); }
            set { SetValue(MeasureSizeAnimatedProperty, value); }
        }

        public TimeSpan Duration
        {
            get { return (TimeSpan)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        public IEasingFunction Easinng
        {
            get { return (IEasingFunction)GetValue(EasinngProperty); }
            set { SetValue(EasinngProperty, value); }
        }


        private static readonly DependencyProperty MeasureSizeAnimatedProperty =
            DependencyProperty.Register(nameof(MeasureSizeAnimated), typeof(Size), typeof(SizeTransitioner),
                new FrameworkPropertyMetadata(default(Size), FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register(nameof(Duration), typeof(TimeSpan), typeof(SizeTransitioner),
                new FrameworkPropertyMetadata(TimeSpan.FromSeconds(0.15), FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty EasinngProperty =
            DependencyProperty.Register(nameof(Easinng), typeof(IEasingFunction), typeof(SizeTransitioner),
                new FrameworkPropertyMetadata(_easing, FrameworkPropertyMetadataOptions.AffectsMeasure));

        protected virtual Size GetTargetMeasureSize(Size constraint)
        {
            return base.MeasureOverride(constraint);
        }

        protected virtual void PrepareTransition(ref Size initialSize, Size targetSize)
        {

        }

        protected sealed override Size MeasureOverride(Size constraint)
        {
            var measureSizeAnimated = MeasureSizeAnimated;
            var currentMeasureSize = GetTargetMeasureSize(constraint);

            if (ReadLocalValue(MeasureSizeAnimatedProperty) == DependencyProperty.UnsetValue)
            {
                _targetMeasureSize = currentMeasureSize;
                MeasureSizeAnimated = currentMeasureSize;
                return currentMeasureSize;
            }

            if (currentMeasureSize != _targetMeasureSize)
            {
                var initialSize = DesiredSize;
                var targetSizze = currentMeasureSize;

                PrepareTransition(ref initialSize, targetSizze);

                BeginAnimation(MeasureSizeAnimatedProperty, null);
                BeginAnimation(MeasureSizeAnimatedProperty, new SizeAnimation
                {
                    From = initialSize,
                    To = currentMeasureSize,
                    Duration = Duration,
                    FillBehavior = FillBehavior.HoldEnd,
                    EasingFunction = _easing
                });

                _targetMeasureSize = currentMeasureSize;
                return DesiredSize;
            }

            return measureSizeAnimated;
        }

        protected sealed override Size ArrangeOverride(Size arrangeBounds)
        {
            if (VisualChildrenCount > 0 &&
                GetVisualChild(0) is UIElement elementChild)
            {
                var childRect = new Rect(0, 0, arrangeBounds.Width, arrangeBounds.Height);
                elementChild.Arrange(childRect);
            }

            return arrangeBounds;
        }
    }
}
