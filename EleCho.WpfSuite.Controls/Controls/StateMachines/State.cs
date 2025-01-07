using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

#pragma warning disable CS1591

namespace EleCho.WpfSuite.Controls.StateMachines
{
    public class State : Animatable, IState
    {
        public string? Name { get; set; }


        public Brush Background
        {
            get { return (Brush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        public Brush Foreground
        {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        public Brush BorderBrush
        {
            get { return (Brush)GetValue(BorderBrushProperty); }
            set { SetValue(BorderBrushProperty, value); }
        }


        public Thickness Padding
        {
            get { return (Thickness)GetValue(PaddingProperty); }
            set { SetValue(PaddingProperty, value); }
        }

        public Thickness BorderThickness
        {
            get { return (Thickness)GetValue(BorderThicknessProperty); }
            set { SetValue(BorderThicknessProperty, value); }
        }

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }



        public Duration UniformTransitionDuration
        {
            get { return (Duration)GetValue(UniformTransitionDurationProperty); }
            set { SetValue(UniformTransitionDurationProperty, value); }
        }

        public Duration? BackgroundTransitionDuration
        {
            get { return (Duration?)GetValue(BackgroundTransitionDurationProperty); }
            set { SetValue(BackgroundTransitionDurationProperty, value); }
        }

        public Duration? ForegroundTransitionDuration
        {
            get { return (Duration?)GetValue(ForegroundTransitionDurationProperty); }
            set { SetValue(ForegroundTransitionDurationProperty, value); }
        }

        public Duration? BorderBrushTransitionDuration
        {
            get { return (Duration?)GetValue(BorderBrushTransitionDurationProperty); }
            set { SetValue(BorderBrushTransitionDurationProperty, value); }
        }

        public Duration? PaddingTransitionDuration
        {
            get { return (Duration?)GetValue(PaddingTransitionDurationProperty); }
            set { SetValue(PaddingTransitionDurationProperty, value); }
        }

        public Duration? BorderThicknessTransitionDuration
        {
            get { return (Duration?)GetValue(BorderThicknessTransitionDurationProperty); }
            set { SetValue(BorderThicknessTransitionDurationProperty, value); }
        }

        public Duration? CornerRadiusTransitionDuration
        {
            get { return (Duration?)GetValue(CornerRadiusTransitionDurationProperty); }
            set { SetValue(CornerRadiusTransitionDurationProperty, value); }
        }



        public IEasingFunction UniformEasingFunction
        {
            get { return (IEasingFunction)GetValue(UniformEasingFunctionProperty); }
            set { SetValue(UniformEasingFunctionProperty, value); }
        }

        public IEasingFunction BackgroundEasingFunction
        {
            get { return (IEasingFunction)GetValue(BackgroundEasingFunctionProperty); }
            set { SetValue(BackgroundEasingFunctionProperty, value); }
        }

        public IEasingFunction ForegroundEasingFunction
        {
            get { return (IEasingFunction)GetValue(ForegroundEasingFunctionProperty); }
            set { SetValue(ForegroundEasingFunctionProperty, value); }
        }

        public IEasingFunction BorderBrushEasingFunction
        {
            get { return (IEasingFunction)GetValue(BorderBrushEasingFunctionProperty); }
            set { SetValue(BorderBrushEasingFunctionProperty, value); }
        }

        public IEasingFunction PaddingEasingFunction
        {
            get { return (IEasingFunction)GetValue(PaddingEasingFunctionProperty); }
            set { SetValue(PaddingEasingFunctionProperty, value); }
        }

        public IEasingFunction BorderThicknessEasingFunction
        {
            get { return (IEasingFunction)GetValue(BorderThicknessEasingFunctionProperty); }
            set { SetValue(BorderThicknessEasingFunctionProperty, value); }
        }

        public IEasingFunction CornerRadiusEasingFunction
        {
            get { return (IEasingFunction)GetValue(CornerRadiusEasingFunctionProperty); }
            set { SetValue(CornerRadiusEasingFunctionProperty, value); }
        }

        protected override Freezable CreateInstanceCore()
        {
            return new State();
        }

        public static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register(nameof(Background), typeof(Brush), typeof(State), new PropertyMetadata(null));

        public static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register(nameof(Foreground), typeof(Brush), typeof(State), new PropertyMetadata(null));

        public static readonly DependencyProperty BorderBrushProperty =
            DependencyProperty.Register(nameof(BorderBrush), typeof(Brush), typeof(State), new PropertyMetadata(null));

        public static readonly DependencyProperty PaddingProperty =
            DependencyProperty.Register(nameof(Padding), typeof(Thickness), typeof(State), new PropertyMetadata(default(Thickness)));

        public static readonly DependencyProperty BorderThicknessProperty =
            DependencyProperty.Register(nameof(BorderThickness), typeof(Thickness), typeof(State), new PropertyMetadata(default(Thickness)));

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(State), new PropertyMetadata(default(CornerRadius)));



        public static readonly DependencyProperty UniformTransitionDurationProperty =
            DependencyProperty.Register(nameof(UniformTransitionDuration), typeof(Duration), typeof(State), new PropertyMetadata(new Duration(TimeSpan.Zero), null, coerceValueCallback: CoerceDuration));

        public static readonly DependencyProperty BackgroundTransitionDurationProperty =
            DependencyProperty.Register(nameof(BackgroundTransitionDuration), typeof(Duration?), typeof(State), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        public static readonly DependencyProperty ForegroundTransitionDurationProperty =
            DependencyProperty.Register(nameof(ForegroundTransitionDuration), typeof(Duration?), typeof(State), new PropertyMetadata(default(Duration?)));

        public static readonly DependencyProperty BorderBrushTransitionDurationProperty =
            DependencyProperty.Register(nameof(BorderBrushTransitionDuration), typeof(Duration?), typeof(State), new PropertyMetadata(default(Duration?)));

        public static readonly DependencyProperty PaddingTransitionDurationProperty =
            DependencyProperty.Register(nameof(PaddingTransitionDuration), typeof(Duration?), typeof(State), new PropertyMetadata(default(Duration?)));

        public static readonly DependencyProperty BorderThicknessTransitionDurationProperty =
            DependencyProperty.Register(nameof(BorderThicknessTransitionDuration), typeof(Duration?), typeof(State), new PropertyMetadata(default(Duration?)));

        public static readonly DependencyProperty CornerRadiusTransitionDurationProperty =
            DependencyProperty.Register(nameof(CornerRadiusTransitionDuration), typeof(Duration?), typeof(State), new PropertyMetadata(default(Duration?)));



        public static readonly DependencyProperty UniformEasingFunctionProperty =
            DependencyProperty.Register(nameof(UniformEasingFunction), typeof(IEasingFunction), typeof(State), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty BackgroundEasingFunctionProperty =
            DependencyProperty.Register(nameof(BackgroundEasingFunction), typeof(IEasingFunction), typeof(State), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty ForegroundEasingFunctionProperty =
            DependencyProperty.Register(nameof(ForegroundEasingFunction), typeof(IEasingFunction), typeof(State), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty BorderBrushEasingFunctionProperty =
            DependencyProperty.Register(nameof(BorderBrushEasingFunction), typeof(IEasingFunction), typeof(State), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty PaddingEasingFunctionProperty =
            DependencyProperty.Register(nameof(PaddingEasingFunction), typeof(IEasingFunction), typeof(State), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty BorderThicknessEasingFunctionProperty =
            DependencyProperty.Register(nameof(BorderThicknessEasingFunction), typeof(IEasingFunction), typeof(State), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty CornerRadiusEasingFunctionProperty =
            DependencyProperty.Register(nameof(CornerRadiusEasingFunction), typeof(IEasingFunction), typeof(State), new PropertyMetadata(default(IEasingFunction)));



        private static object CoerceDuration(DependencyObject d, object baseValue)
        {
            var value = (Duration)baseValue;
            if (!value.HasTimeSpan)
            {
                throw new ArgumentException();
            }

            return baseValue;
        }

        private static object CoerceNullableDuration(DependencyObject d, object baseValue)
        {
            var value = (Duration?)baseValue;
            if (value.HasValue && !value.Value.HasTimeSpan)
            {
                throw new ArgumentException();
            }

            return baseValue;
        }


        public TimeSpan TotalEasingDuration
        {
            get
            {
                TimeSpan duration = default(TimeSpan);

                if (UniformTransitionDuration.TimeSpan > duration)
                {
                    duration = UniformTransitionDuration.TimeSpan;
                }

                if (BackgroundTransitionDuration.HasValue && BackgroundTransitionDuration.Value.TimeSpan > duration)
                {
                    duration = BackgroundTransitionDuration.Value.TimeSpan;
                }

                if (ForegroundTransitionDuration.HasValue && ForegroundTransitionDuration.Value.TimeSpan > duration)
                {
                    duration = ForegroundTransitionDuration.Value.TimeSpan;
                }

                if (BorderBrushTransitionDuration.HasValue && BorderBrushTransitionDuration.Value.TimeSpan > duration)
                {
                    duration = BorderBrushTransitionDuration.Value.TimeSpan;
                }

                if (PaddingTransitionDuration.HasValue && PaddingTransitionDuration.Value.TimeSpan > duration)
                {
                    duration = PaddingTransitionDuration.Value.TimeSpan;
                }

                if (CornerRadiusTransitionDuration.HasValue && CornerRadiusTransitionDuration.Value.TimeSpan > duration)
                {
                    duration = CornerRadiusTransitionDuration.Value.TimeSpan;
                }

                return duration;
            }
        }
    }
}
