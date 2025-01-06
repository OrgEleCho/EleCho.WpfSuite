using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using EleCho.WpfSuite.Internal;
using static EleCho.WpfSuite.Controls.Border;

#pragma warning disable CS1591

namespace EleCho.WpfSuite.Controls.StateMachines
{
    public class TransitioningState : DependencyObject, IState
    {
        private Brush? _backgroundCache;
        private Brush? _foregroundCache;
        private Brush? _borderBrushCache;

        public string? Name => null;

        public IState From { get; }
        public State To { get; }


        public double TimeMilliseconds
        {
            get { return (double)GetValue(TimeMillisecondsProperty); }
            set { SetValue(TimeMillisecondsProperty, value); }
        }

        public Brush Background => (Brush)GetValue(BackgroundProperty);

        public Brush Foreground => (Brush)GetValue(ForegroundProperty);

        public Brush BorderBrush => (Brush)GetValue(BorderBrushProperty);

        public Thickness Padding => (Thickness)GetValue(PaddingProperty);

        public Thickness Margin => (Thickness)GetValue(MarginProperty);

        public CornerRadius CornerRadius => (CornerRadius)GetValue(CornerRadiusProperty);

        public TransitioningState(IState from, State to)
        {
            if (from is null)
            {
                throw new ArgumentNullException(nameof(from));
            }

            if (to is null)
            {
                throw new ArgumentNullException(nameof(to));
            }

            From = from;
            To = to;

            SetValue(BackgroundPropertyKey, from.Background);
            SetValue(ForegroundPropertyKey, from.Foreground);
            SetValue(BorderBrushPropertyKey, from.BorderBrush);
            SetValue(PaddingPropertyKey, from.Padding);
            SetValue(MarginPropertyKey, from.Margin);
            SetValue(CornerRadiusPropertyKey, from.CornerRadius);
        }

        private static readonly DependencyPropertyKey BackgroundPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(Background), typeof(Brush), typeof(TransitioningState), new PropertyMetadata(null));

        private static readonly DependencyPropertyKey ForegroundPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(Foreground), typeof(Brush), typeof(TransitioningState), new PropertyMetadata(null));

        private static readonly DependencyPropertyKey BorderBrushPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(BorderBrush), typeof(Brush), typeof(TransitioningState), new PropertyMetadata(null));

        private static readonly DependencyPropertyKey PaddingPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(Padding), typeof(Thickness), typeof(TransitioningState), new PropertyMetadata(default(Thickness)));

        private static readonly DependencyPropertyKey MarginPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(Margin), typeof(Thickness), typeof(TransitioningState), new PropertyMetadata(default(Thickness)));

        private static readonly DependencyPropertyKey CornerRadiusPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(CornerRadius), typeof(CornerRadius), typeof(TransitioningState), new PropertyMetadata(default(CornerRadius)));

        public static readonly DependencyProperty BackgroundProperty = BackgroundPropertyKey.DependencyProperty;

        public static readonly DependencyProperty ForegroundProperty = ForegroundPropertyKey.DependencyProperty;

        public static readonly DependencyProperty BorderBrushProperty = BorderBrushPropertyKey.DependencyProperty;

        public static readonly DependencyProperty PaddingProperty = PaddingPropertyKey.DependencyProperty;

        public static readonly DependencyProperty MarginProperty = MarginPropertyKey.DependencyProperty;

        public static readonly DependencyProperty CornerRadiusProperty = CornerRadiusPropertyKey.DependencyProperty;

        public static readonly DependencyProperty TimeMillisecondsProperty =
            DependencyProperty.Register(nameof(TimeMilliseconds), typeof(double), typeof(TransitioningState), new PropertyMetadata(0.0, propertyChangedCallback: OnRatioChanged));

        private static void OnRatioChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not TransitioningState presentationState)
            {
                return;
            }

            double timeMilliseconds = (double)e.NewValue;

            d.SetValue(BackgroundPropertyKey, EaseUtils.EaseBrush(
                ref presentationState._backgroundCache,
                presentationState.From.Background,
                presentationState.To.Background,
                MathHelper.Clamp01(timeMilliseconds / (presentationState.To.BackgroundTransitionDuration ?? presentationState.To.UniformTransitionDuration).TimeSpan.TotalMilliseconds),
                presentationState.To.BackgroundEasingFunction ?? presentationState.To.UniformEasingFunction));

            d.SetValue(ForegroundPropertyKey, EaseUtils.EaseBrush(
                ref presentationState._foregroundCache,
                presentationState.From.Foreground,
                presentationState.To.Foreground,
                MathHelper.Clamp01(timeMilliseconds / (presentationState.To.ForegroundTransitionDuration ?? presentationState.To.UniformTransitionDuration).TimeSpan.TotalMilliseconds),
                presentationState.To.ForegroundEasingFunction ?? presentationState.To.UniformEasingFunction));

            d.SetValue(BorderBrushPropertyKey, EaseUtils.EaseBrush(
                ref presentationState._borderBrushCache,
                presentationState.From.BorderBrush,
                presentationState.To.BorderBrush,
                MathHelper.Clamp01(timeMilliseconds / (presentationState.To.BorderBrushTransitionDuration ?? presentationState.To.UniformTransitionDuration).TimeSpan.TotalMilliseconds),
                presentationState.To.BorderBrushEasingFunction ?? presentationState.To.UniformEasingFunction));

            d.SetValue(PaddingPropertyKey, EaseUtils.EaseThickness(
                presentationState.From.Padding,
                presentationState.To.Padding,
                MathHelper.Clamp01(timeMilliseconds / (presentationState.To.PaddingTransitionDuration ?? presentationState.To.UniformTransitionDuration).TimeSpan.TotalMilliseconds),
                presentationState.To.PaddingEasingFunction ?? presentationState.To.UniformEasingFunction));

            d.SetValue(MarginPropertyKey, EaseUtils.EaseThickness(
                presentationState.From.Margin,
                presentationState.To.Margin,
                MathHelper.Clamp01(timeMilliseconds / (presentationState.To.MarginTransitionDuration ?? presentationState.To.UniformTransitionDuration).TimeSpan.TotalMilliseconds),
                presentationState.To.MarginEasingFunction ?? presentationState.To.UniformEasingFunction));

            d.SetValue(CornerRadiusPropertyKey, EaseUtils.EaseCornerRadius(
                presentationState.From.CornerRadius,
                presentationState.To.CornerRadius,
                MathHelper.Clamp01(timeMilliseconds / (presentationState.To.CornerRadiusTransitionDuration ?? presentationState.To.UniformTransitionDuration).TimeSpan.TotalMilliseconds),
                presentationState.To.CornerRadiusEasingFunction ?? presentationState.To.UniformEasingFunction));
        }
    }
}
