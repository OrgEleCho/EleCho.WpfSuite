using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using EleCho.WpfSuite.Internal;

namespace EleCho.WpfSuite.Media.Animation
{
    /// <summary>
    /// Animated value helper
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ValueAnimator<T> : Animatable, IValueAnimator<T>
    {
        static ValueAnimator()
        {
            DefaultComponentSplitter = ValueAnimatorInitializer.GetDefaultComponentSplitter<T>();
            DefaultComponentMerger = ValueAnimatorInitializer.GetDefaultComponentMerger<T>();
        }

        /// <summary>
        /// Default component splitter for <see cref="ValueAnimator{T}"/> type
        /// </summary>
        public static ComponentSplitter<T>? DefaultComponentSplitter { get; set; }

        /// <summary>
        /// Default component merger for <see cref="ValueAnimator{T}"/> type
        /// </summary>
        public static ComponentMerger<T>? DefaultComponentMerger { get; set; }

        private bool _suppressAnimation;
        private double[]? _currentComponentValues;
        private double[]? _animatedComponentValues;
        private SecondOrderDynamics[]? _secondOrderDynamicsForComponents;
        private Stopwatch? _animationStopwatch;

        /// <summary>
        /// Component splitter of current instance of <see cref="ValueAnimator{T}"/>
        /// </summary>
        public ComponentSplitter<T>? ComponentSplitter { get; set; }

        /// <summary>
        /// Component merger of current instance of <see cref="ValueAnimator{T}"/>
        /// </summary>
        public ComponentMerger<T>? ComponentMerger { get; set; }

        /// <summary>
        /// Create a new instance of <see cref="ValueAnimator{T}"/> with default component splitter and merger
        /// </summary>
        public ValueAnimator()
        {
            ComponentSplitter = DefaultComponentSplitter;
            ComponentMerger = DefaultComponentMerger;

            UpdateForCurrentValue();
        }

        /// <summary>
        /// Create a new instance of <see cref="ValueAnimator{T}"/> with specified initial value
        /// </summary>
        /// <param name="value"></param>
        public ValueAnimator(T value) : this()
        {
            UpdateValueDirectly(value);
        }

        /// <summary>
        /// Target value
        /// </summary>
        public T Value
        {
            get { return (T)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        /// <summary>
        /// Animated current value
        /// </summary>
        public T AnimatedValue
        {
            get => (T)GetValue(AnimatedValuePropertyKey.DependencyProperty);
            private set => SetValue(AnimatedValuePropertyKey, value);
        }

        object? IValueAnimator.Value
        {
            get => Value;
            set
            {
                if (value is null &&
                    !typeof(T).IsValueType)
                {
                    Value = default;
                }
                else if (value is T typedValue)
                {
                    Value = (T)value;
                }
                else
                {
                    throw new ArgumentException($"Value must be of type {typeof(T).Name}", nameof(value));
                }
            }
        }

        object? IValueAnimator.AnimatedValue => AnimatedValue;

        /// <summary>
        /// Event raised when <see cref="Value"/> is changed
        /// </summary>
        public event EventHandler? ValueChanged;

        /// <summary>
        /// Event raised when <see cref="AnimatedValue"/> is changed
        /// </summary>
        public event EventHandler? AnimatedValueChanged;

        /// <summary>
        /// <see cref="DependencyProperty"/> definition of <see cref="Value"/>
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(T), typeof(ValueAnimator<T>), new FrameworkPropertyMetadata(default(T), propertyChangedCallback: OnValueChanged));


        private static readonly DependencyPropertyKey AnimatedValuePropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(AnimatedValue), typeof(T), typeof(ValueAnimator<T>), new FrameworkPropertyMetadata(default(T)));

        /// <summary>
        /// <see cref="DependencyProperty"/> definition of <see cref="AnimatedValue"/>
        /// </summary>
        public static readonly DependencyProperty AnimatedValueProperty = AnimatedValuePropertyKey.DependencyProperty;


        public double Frequency
        {
            get { return (double)GetValue(FrequencyProperty); }
            set { SetValue(FrequencyProperty, value); }
        }

        public double Damping
        {
            get { return (double)GetValue(DampingProperty); }
            set { SetValue(DampingProperty, value); }
        }

        public double InitialResponse
        {
            get { return (double)GetValue(InitialResponseProperty); }
            set { SetValue(InitialResponseProperty, value); }
        }

        public static readonly DependencyProperty FrequencyProperty =
            DependencyProperty.Register("Frequency", typeof(double), typeof(ValueAnimator<T>),
                new PropertyMetadata(5.0, propertyChangedCallback: OnDynamicsPropertyChanged));

        public static readonly DependencyProperty DampingProperty =
            DependencyProperty.Register("Damping", typeof(double), typeof(ValueAnimator<T>),
                new PropertyMetadata(1.0, propertyChangedCallback: OnDynamicsPropertyChanged));

        public static readonly DependencyProperty InitialResponseProperty =
            DependencyProperty.Register("InitialResponse", typeof(double), typeof(ValueAnimator<T>),
                new PropertyMetadata(1.0, propertyChangedCallback: OnDynamicsPropertyChanged));

        /// <summary>
        /// Set the value of <see cref="Value"/> directly without animation
        /// </summary>
        /// <param name="value"></param>
        public void UpdateValueDirectly(T value)
        {
            _suppressAnimation = !IsRunning;

            Value = value;
            UpdateForCurrentValue();

            for (int i = 0; i < _currentComponentValues.Length; i++)
            {
                _secondOrderDynamicsForComponents[i].UpdateDirectly(_currentComponentValues[i]);
            }

            _suppressAnimation = false;
        }

        /// <summary>
        /// Stop the animation immediately and set the animated value to specified value
        /// </summary>
        /// <param name="value"></param>
        public void StopImmediately(T value)
        {
            EnsureAnimationStopped();
            UpdateValueDirectly(value);
        }

        /// <summary>
        /// Stop the animation immediately and set the animated value to current value
        /// </summary>
        public void StopImmediately()
        {
            EnsureAnimationStopped();
            UpdateValueDirectly(Value);
        }

        /// <summary>
        /// Indicate whether the animation is currently running
        /// </summary>
        public bool IsRunning { get; private set; }

        [MemberNotNull(nameof(ComponentSplitter))]
        [MemberNotNull(nameof(ComponentMerger))]
        private void EnsureStateForAnimation()
        {
            if (ComponentSplitter is null)
            {
                throw new InvalidOperationException($"ComponentSplitter must be set for {nameof(ValueAnimator<T>)}");
            }

            if (ComponentMerger is null)
            {
                throw new InvalidOperationException($"ComponentMerger must be set for {nameof(ValueAnimator<T>)}");
            }
        }

        [MemberNotNull(nameof(_currentComponentValues))]
        [MemberNotNull(nameof(_secondOrderDynamicsForComponents))]
        private void UpdateForCurrentValue()
        {
            EnsureStateForAnimation();

            if (Value is null)
            {
                throw new InvalidOperationException($"Value must be set for {nameof(ValueAnimator<T>)}");
            }

            ComponentSplitter.Invoke(Value, ref _currentComponentValues);

            if (_currentComponentValues is null ||
                _currentComponentValues.Length == 0)
            {
                throw new InvalidOperationException($"ComponentSplitter must return a non-null and non-empty value for {nameof(ValueAnimator<T>)}");
            }

            if (_secondOrderDynamicsForComponents is null ||
                _secondOrderDynamicsForComponents.Length != _currentComponentValues.Length)
            {
                _secondOrderDynamicsForComponents = new SecondOrderDynamics[_currentComponentValues.Length];

                for (int i = 0; i < _currentComponentValues.Length; i++)
                {
                    _secondOrderDynamicsForComponents[i] = new SecondOrderDynamics(Frequency, Damping, InitialResponse, _currentComponentValues[i]);
                }
            }
        }

        private void EnsureAnimationRunning()
        {
            EnsureStateForAnimation();

            if (_currentComponentValues is null)
            {
                UpdateForCurrentValue();
            }

            _animationStopwatch ??= Stopwatch.StartNew();

            IsRunning = true;
            CompositionTarget.Rendering -= AnimationTick;
            CompositionTarget.Rendering += AnimationTick;
        }

        private void EnsureAnimationStopped()
        {
            _animationStopwatch = null;

            IsRunning = false;
            CompositionTarget.Rendering -= AnimationTick;
        }

        private void AnimationTick(object? sender, EventArgs e)
        {
            if (_currentComponentValues is null ||
                _animationStopwatch is null)
            {
                AnimatedValue = Value;
                return;
            }

            if (_secondOrderDynamicsForComponents is null ||
                _secondOrderDynamicsForComponents.Length != _currentComponentValues.Length)
            {
                _secondOrderDynamicsForComponents = new SecondOrderDynamics[_currentComponentValues.Length];

                for (int i = 0; i < _currentComponentValues.Length; i++)
                {
                    _secondOrderDynamicsForComponents[i] = new SecondOrderDynamics(Frequency, Damping, InitialResponse, _currentComponentValues[i]);
                }
            }

            if (_animatedComponentValues is null ||
                _animatedComponentValues.Length != _currentComponentValues.Length)
            {
                _animatedComponentValues = new double[_currentComponentValues.Length];
            }

            var deltaTime = _animationStopwatch.Elapsed.TotalSeconds;
            var shouldKeepAnimationRunning = false;
            for (int i = 0; i < _secondOrderDynamicsForComponents.Length; i++)
            {
                SecondOrderDynamics? dynamics = _secondOrderDynamicsForComponents[i];
                _animatedComponentValues[i] = dynamics.Update(deltaTime, _currentComponentValues[i], out var deltaY);

                if (deltaY > 0.0001 ||
                    deltaY < -0.0001)
                {
                    // if the component is still changing, we should keep the animation running
                    shouldKeepAnimationRunning = true;
                }
            }

            AnimatedValue = ComponentMerger!.Invoke(_animatedComponentValues);
            _animationStopwatch.Restart();

            AnimatedValueChanged?.Invoke(this, EventArgs.Empty);

            if (!shouldKeepAnimationRunning)
            {
                EnsureAnimationStopped();
            }
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not ValueAnimator<T> valueAnimator)
            {
                return;
            }

            valueAnimator.ValueChanged?.Invoke(valueAnimator, EventArgs.Empty);

            if (valueAnimator._suppressAnimation)
            {
                valueAnimator.SetValue(AnimatedValuePropertyKey, valueAnimator.GetValue(ValueProperty));
                valueAnimator.AnimatedValueChanged?.Invoke(valueAnimator, EventArgs.Empty);
                return;
            }

            valueAnimator.EnsureAnimationRunning();
            valueAnimator.UpdateForCurrentValue();
        }

        private static void OnDynamicsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not ValueAnimator<T> valueAnimator)
            {
                return;
            }

            if (valueAnimator._secondOrderDynamicsForComponents is { } dynamicsForComponents)
            {
                foreach (var dynamics in dynamicsForComponents)
                {
                    dynamics.SetMotionValues(valueAnimator.Frequency, valueAnimator.Damping, valueAnimator.InitialResponse);
                }
            }
        }

        /// <inheritdoc/>
        protected override Freezable CreateInstanceCore()
        {
            return new ValueAnimator<T>();
        }
    }

    public class ByteValueAnimator : ValueAnimator<Byte> { }
    public class Int16ValueAnimator : ValueAnimator<Int16> { }
    public class Int32ValueAnimator : ValueAnimator<Int32> { }
    public class Int64ValueAnimator : ValueAnimator<Int64> { }
    public class SingleValueAnimator : ValueAnimator<Single> { }
    public class DoubleValueAnimator : ValueAnimator<Double> { }
    public class DecimalValueAnimator : ValueAnimator<Decimal> { }
    public class ColorValueAnimator : ValueAnimator<Color> { }
    public class PointValueAnimator : ValueAnimator<Point> { }
    public class SizeValueAnimator : ValueAnimator<Size> { }
    public class RectValueAnimator : ValueAnimator<Rect> { }
    public class VectorValueAnimator : ValueAnimator<Vector> { }
    public class Vector3DValueAnimator : ValueAnimator<Vector3D> { }
    public class Point3DValueAnimator : ValueAnimator<Point3D> { }
    public class QuaternionValueAnimator : ValueAnimator<Quaternion> { }
    public class Rotation3DValueAnimator : ValueAnimator<Rotation3D> { }
    public class ThicknessValueAnimator : ValueAnimator<Thickness> { }
    public class CornerRadiusValueAnimator : ValueAnimator<CornerRadius> { }
    public class BrushValueAnimator : ValueAnimator<Brush> { }


}
