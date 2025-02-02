using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace EleCho.WpfSuite.Media.Animation
{
    /// <summary>
    /// Animated value helper
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ValueAnimator<T> : Animatable
    {
        static ValueAnimator()
        {
            TimelineFactory = ValueAnimatorInitializer.GetDefaultTimelineFactory<T>();
        }

        /// <summary>
        /// Factory for creating <see cref="AnimationTimeline"/> for value easing
        /// </summary>
        public static ValueAnimatorTimelineFactory<T>? TimelineFactory { get; set; }

        /// <summary>
        /// Target value
        /// </summary>
        public T? Value
        {
            get { return (T)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        /// <summary>
        /// Animated current value
        /// </summary>
        public T? AnimatedValue
        {
            get => (T)GetValue(AnimatedValuePropertyKey.DependencyProperty);
        }

        /// <summary>
        /// Animation duration
        /// </summary>
        public Duration Duration
        {
            get { return (Duration)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        /// <summary>
        /// Animation easing function
        /// </summary>
        public IEasingFunction EasingFunction
        {
            get { return (IEasingFunction)GetValue(EasingFunctionProperty); }
            set { SetValue(EasingFunctionProperty, value); }
        }

        /// <summary>
        /// Create a new instance of <see cref="ValueAnimator{T}"/>
        /// </summary>
        public ValueAnimator()
        {

        }

        /// <summary>
        /// Create a new instance of <see cref="ValueAnimator{T}"/> with 
        /// </summary>
        /// <param name="defaultValue"></param>
        public ValueAnimator(T? defaultValue)
        {
            SetValue(ValueProperty, defaultValue);
            SetValue(AnimatedValuePropertyKey, defaultValue);
        }

        /// <summary>
        /// <see cref="DependencyProperty"/> definition of <see cref="Value"/>
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(T), typeof(ValueAnimator<T>), new PropertyMetadata(default(T), propertyChangedCallback: OnValueChanged));

        /// <summary>
        /// <see cref="DependencyProperty"/> definition of <see cref="Duration"/>
        /// </summary>
        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register(nameof(Duration), typeof(Duration), typeof(ValueAnimator<T>), new PropertyMetadata(new Duration(TimeSpan.FromMilliseconds(100)), propertyChangedCallback: null, coerceValueCallback: CoerceDuration));

        /// <summary>
        /// <see cref="DependencyProperty"/> definition of <see cref="EasingFunction"/>
        /// </summary>
        public static readonly DependencyProperty EasingFunctionProperty =
            DependencyProperty.Register(nameof(EasingFunction), typeof(IEasingFunction), typeof(ValueAnimator<T>), new PropertyMetadata(default(IEasingFunction)));

        private static readonly DependencyPropertyKey AnimatedValuePropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(AnimatedValue), typeof(T), typeof(ValueAnimator<T>), new PropertyMetadata(default(T)));

        private static readonly DependencyProperty AnimatedValueProxyProperty =
            DependencyProperty.Register("AnimatedValueProxy", typeof(T), typeof(ValueAnimator<T>), new PropertyMetadata(default(T), propertyChangedCallback: OnAnimatedValueProxyChanged));


        private static object CoerceDuration(DependencyObject d, object baseValue)
        {
            if (baseValue is not Duration duration ||
                !duration.HasTimeSpan)
            {
                throw new ArgumentOutOfRangeException();
            }

            return baseValue;
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not ValueAnimator<T> valueAnimator)
            {
                return;
            }

            if (TimelineFactory is null)
            {
                valueAnimator.SetValue(AnimatedValuePropertyKey, e.NewValue);
                return;
            }

            var oldValue = valueAnimator.AnimatedValue;
            var newValue = (T)e.NewValue;
            var timeline = TimelineFactory.Invoke(oldValue, newValue, valueAnimator.Duration, valueAnimator.EasingFunction);

            valueAnimator.BeginAnimation(AnimatedValueProxyProperty, timeline, HandoffBehavior.SnapshotAndReplace);
        }

        private static void OnAnimatedValueProxyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not ValueAnimator<T> valueAnimator)
            {
                return;
            }

            valueAnimator.SetValue(AnimatedValuePropertyKey, e.NewValue);
        }


        /// <inheritdoc/>
        protected override Freezable CreateInstanceCore()
        {
            return new ValueAnimator<T>();
        }
    }
}
