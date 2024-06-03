using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace EleCho.WpfSuite
{
    internal enum AnimationType
    {
        Automatic = 0x0,
        FromTo = 0x3,
        FromBy = 0x5,
        From = 0x1,
        To = 0x2,
        By = 0x4
    }

    /// <summary>
    /// Animates the value of a CornerRadius property between two target values using linear interpolation over a specified Duration.
    /// </summary>
    public class CornerRadiusAnimation : CornerRadiusAnimationBase
    {
        #region Data

        /// <summary>
        ///     This is used if the user has specified From, To, and/or By values.
        /// </summary>
        private CornerRadius[]? keyValues;

        private AnimationType animationType;
        private bool isAnimationFunctionValid;

        #endregion

        #region Constructors

        static CornerRadiusAnimation()
        {
            var typeofProp = typeof(CornerRadius?);
            var typeofThis = typeof(CornerRadiusAnimation);
            PropertyChangedCallback propCallback = AnimationFunction_Changed;
            ValidateValueCallback validateCallback = ValidateFromToOrByValue;

            FromProperty = DependencyProperty.Register(
                nameof(From),
                typeofProp,
                typeofThis,
                new PropertyMetadata(null, propCallback),
                validateCallback);

            ToProperty = DependencyProperty.Register(
                nameof(To),
                typeofProp,
                typeofThis,
                new PropertyMetadata(null, propCallback),
                validateCallback);

            ByProperty = DependencyProperty.Register(
                nameof(By),
                typeofProp,
                typeofThis,
                new PropertyMetadata(null, propCallback),
                validateCallback);

            EasingFunctionProperty = DependencyProperty.Register(
                nameof(EasingFunction),
                typeof(IEasingFunction),
                typeofThis);
        }


        public CornerRadiusAnimation() { }

        public CornerRadiusAnimation(CornerRadius toValue, Duration duration)
        {
            To = toValue;
            Duration = duration;
        }

        public CornerRadiusAnimation(CornerRadius toValue, Duration duration, FillBehavior fillBehavior)
        {
            To = toValue;
            Duration = duration;
            FillBehavior = fillBehavior;
        }

        public CornerRadiusAnimation(CornerRadius fromValue, CornerRadius toValue, Duration duration)
        {
            From = fromValue;
            To = toValue;
            Duration = duration;
        }

        public CornerRadiusAnimation(CornerRadius fromValue, CornerRadius toValue, Duration duration, FillBehavior fillBehavior)
        {
            From = fromValue;
            To = toValue;
            Duration = duration;
            FillBehavior = fillBehavior;
        }

        #endregion

        #region Freezable

        public new CornerRadiusAnimation Clone() => (CornerRadiusAnimation)base.Clone();

        //
        // Note that we don't override the Clone virtuals (CloneCore, CloneCurrentValueCore,
        // GetAsFrozenCore, and GetCurrentValueAsFrozenCore) even though this class has state
        // not stored in a DP.
        // 
        // We don't need to clone _animationType and _keyValues because they are the the cached 
        // results of animation function validation, which can be recomputed.  The other remaining
        // field, isAnimationFunctionValid, defaults to false, which causes this recomputation to happen.
        //

        /// <summary>
        ///     Implementation of <see cref="System.Windows.Freezable.CreateInstanceCore">Freezable.CreateInstanceCore</see>.
        /// </summary>
        /// <returns>The new Freezable.</returns>
        protected override Freezable CreateInstanceCore() => new CornerRadiusAnimation();

        #endregion

        #region Methods

        protected override CornerRadius GetCurrentValueCore(CornerRadius defaultOriginValue, CornerRadius defaultDestinationValue, AnimationClock animationClock)
        {
            Debug.Assert(animationClock.CurrentState != ClockState.Stopped);

            if (!isAnimationFunctionValid)
            {
                ValidateAnimationFunction();
            }

            // ReSharper disable once PossibleInvalidOperationException
            var progress = animationClock.CurrentProgress.GetValueOrDefault();

            var easingFunction = EasingFunction;
            if (easingFunction != null)
            {
                progress = easingFunction.Ease(progress);
            }

            var from = new CornerRadius();
            var to = new CornerRadius();
            var accumulated = new CornerRadius();
            var foundation = new CornerRadius();

            // need to validate the default origin and destination values if 
            // the animation uses them as the from, to, or foundation values
            var validateOrigin = false;
            var validateDestination = false;

            switch (animationType)
            {
                case AnimationType.Automatic:
                    from = defaultOriginValue;
                    to = defaultDestinationValue;

                    validateOrigin = true;
                    validateDestination = true;

                    break;
                case AnimationType.From:
                    from = keyValues![0];
                    to = defaultDestinationValue;

                    validateDestination = true;
                    break;
                case AnimationType.To:
                    from = defaultOriginValue;
                    to = keyValues![0];

                    validateOrigin = true;
                    break;
                case AnimationType.By:
                    // According to the SMIL specification, a By animation is
                    // always additive.  But we don't force this so that a
                    // user can re-use a By animation and have it replace the
                    // animations that precede it in the list without having
                    // to manually set the From value to the base value.
                    to = keyValues![0];
                    foundation = defaultOriginValue;

                    validateOrigin = true;

                    break;
                case AnimationType.FromTo:
                    from = keyValues![0];
                    to = keyValues[1];

                    if (IsAdditive)
                    {
                        foundation = defaultOriginValue;
                        validateOrigin = true;
                    }
                    break;
                case AnimationType.FromBy:
                    from = keyValues![0];
                    to = AddCornerRadius(keyValues[0], keyValues[1]);

                    if (IsAdditive)
                    {
                        foundation = defaultOriginValue;
                        validateOrigin = true;
                    }
                    break;
                default:
                    Debug.Fail("Unknown animation type.");
                    break;
            }

            if (validateOrigin && !IsValidAnimationValueCornerRadius())
            {
                throw new InvalidOperationException();
            }

            if (validateDestination && !IsValidAnimationValueCornerRadius())
            {
                throw new InvalidOperationException();
            }


            if (IsCumulative)
            {
                Debug.Assert(animationClock.CurrentIteration != null, nameof(animationClock) + " != null");
                var currentRepeat = (double)(animationClock.CurrentIteration - 1);
                if (currentRepeat > 0.0)
                {
                    var accumulator = SubtractCornerRadius(to, from);
                    accumulated = ScaleCornerRadius(accumulator, currentRepeat);
                }
            }

            // return foundation + accumulated + from + ((to - from) * progress)
            return AddCornerRadius(foundation, AddCornerRadius(accumulated, InterpolateCornerRadius(from, to, progress)));
        }

        private static CornerRadius InterpolateCornerRadius(CornerRadius from, CornerRadius to, double progress) =>
            AddCornerRadius(from, ScaleCornerRadius(SubtractCornerRadius(to, from), progress));

        private static CornerRadius ScaleCornerRadius(CornerRadius first, double currentRepeat) =>
            new(first.TopLeft * currentRepeat,
                first.TopRight * currentRepeat,
                first.BottomRight * currentRepeat,
                first.BottomLeft * currentRepeat);

        private static CornerRadius SubtractCornerRadius(CornerRadius first, CornerRadius second) =>
            new(first.TopLeft - second.TopLeft,
                first.TopRight - second.TopRight,
                first.BottomRight - second.BottomRight,
                first.BottomLeft - second.BottomLeft);

        private static bool IsValidAnimationValueCornerRadius() => true;

        private static CornerRadius AddCornerRadius(CornerRadius first, CornerRadius second) =>
            new(first.TopLeft + second.TopLeft,
                first.TopRight + second.TopRight,
                first.BottomRight + second.BottomRight,
                first.BottomLeft + second.BottomLeft);

        private void ValidateAnimationFunction()
        {
            animationType = AnimationType.Automatic;
            keyValues = null;

            if (From.HasValue)
            {
                if (To.HasValue)
                {
                    animationType = AnimationType.FromTo;
                    keyValues = new CornerRadius[2];
                    keyValues[0] = From.Value;
                    keyValues[1] = To.Value;
                }
                else if (By.HasValue)
                {
                    animationType = AnimationType.FromBy;
                    keyValues = new CornerRadius[2];
                    keyValues[0] = From.Value;
                    keyValues[1] = By.Value;
                }
                else
                {
                    animationType = AnimationType.From;
                    keyValues = new CornerRadius[1];
                    keyValues[0] = From.Value;
                }
            }
            else if (To.HasValue)
            {
                animationType = AnimationType.To;
                keyValues = new CornerRadius[1];
                keyValues[0] = To.Value;
            }
            else if (By.HasValue)
            {
                animationType = AnimationType.By;
                keyValues = new CornerRadius[1];
                keyValues[0] = By.Value;
            }

            isAnimationFunctionValid = true;
        }

        #endregion

        #region Properties

        private static void AnimationFunction_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var a = (CornerRadiusAnimation)d;

            a.isAnimationFunctionValid = false;
            //a.PropertyChanged(e.Property);
        }

        private static bool ValidateFromToOrByValue(object? value)
        {
            var typedValue = (CornerRadius?)value;
            return !typedValue.HasValue || IsValidAnimationValueCornerRadius();
        }

        public static readonly DependencyProperty FromProperty;

        public CornerRadius? From
        {
            get => (CornerRadius?)GetValue(FromProperty);
            set => SetValue(FromProperty, value);
        }

        public static readonly DependencyProperty ToProperty;

        public CornerRadius? To
        {
            get => (CornerRadius?)GetValue(ToProperty);
            set => SetValue(ToProperty, value);
        }

        public static readonly DependencyProperty ByProperty;

        public CornerRadius? By
        {
            get => (CornerRadius?)GetValue(ByProperty);
            set => SetValue(ByProperty, value);
        }

        public static readonly DependencyProperty EasingFunctionProperty;

        public IEasingFunction? EasingFunction
        {
            get => (IEasingFunction?)GetValue(EasingFunctionProperty);
            set => SetValue(EasingFunctionProperty, value);
        }

        /// <summary>
        ///     If this property is set to true the animation will add its value to
        ///     the base value instead of replacing it entirely.
        /// </summary>
        public bool IsAdditive
        {
            get => (bool)GetValue(IsAdditiveProperty);
            set => SetValue(IsAdditiveProperty, value);
        }

        /// <summary>
        ///     It this property is set to true, the animation will accumulate its
        ///     value over repeats.  For instance if you have a From value of 0.0 and
        ///     a To value of 1.0, the animation return values from 1.0 to 2.0 over
        ///     the second reteat cycle, and 2.0 to 3.0 over the third, etc.
        /// </summary>
        public bool IsCumulative
        {
            get => (bool)GetValue(IsCumulativeProperty);
            set => SetValue(IsCumulativeProperty, value);
        }

        #endregion
    }

    /// <summary>
    /// Base class of CornerRadiusAnimation
    /// </summary>
    public abstract class CornerRadiusAnimationBase : AnimationTimeline
    {
        #region Constructors

        /// <Summary>
        /// Creates a new CornerRadiusAnimationBase.
        /// </Summary>
        protected CornerRadiusAnimationBase() { }

        #endregion

        #region Freezable

        /// <summary>
        /// Creates a copy of this CornerRadiusAnimationBase
        /// </summary>
        /// <returns>The copy</returns>
        protected new CornerRadiusAnimationBase Clone() => (CornerRadiusAnimationBase)base.Clone();

        #endregion

        #region IAnimation

        /// <summary>
        /// Calculates the value this animation believes should be the current value for the property.
        /// </summary>
        /// <param name="defaultOriginValue">
        /// This value is the suggested origin value provided to the animation
        /// to be used if the animation does not have its own concept of a
        /// start value. If this animation is the first in a composition chain
        /// this value will be the snapshot value if one is available or the
        /// base property value if it is not; otherwise this value will be the 
        /// value returned by the previous animation in the chain with an 
        /// animationClock that is not Stopped.
        /// </param>
        /// <param name="defaultDestinationValue">
        /// This value is the suggested destination value provided to the animation
        /// to be used if the animation does not have its own concept of an
        /// end value. This value will be the base value if the animation is
        /// in the first composition layer of animations on a property; 
        /// otherwise this value will be the output value from the previous 
        /// composition layer of animations for the property.
        /// </param>
        /// <param name="animationClock">
        /// This is the animationClock which can generate the CurrentTime or
        /// CurrentProgress value to be used by the animation to generate its
        /// output value.
        /// </param>
        /// <returns>
        /// The value this animation believes should be the current value for the property.
        /// </returns>
        public sealed override object GetCurrentValue(object defaultOriginValue, object defaultDestinationValue, AnimationClock animationClock)
        {
            // Verify that object arguments are non-null since we are a value type
            if (defaultOriginValue == null)
            {
                throw new ArgumentNullException(nameof(defaultOriginValue));
            }
            if (defaultDestinationValue == null)
            {
                throw new ArgumentNullException(nameof(defaultDestinationValue));
            }
            return GetCurrentValue((CornerRadius)defaultOriginValue, (CornerRadius)defaultDestinationValue, animationClock);
        }

        /// <summary>
        /// Returns the type of the target property
        /// </summary>
        public sealed override Type TargetPropertyType
        {
            get
            {
                ReadPreamble();

                return typeof(CornerRadius);
            }
        }

        #endregion

        #region Methods


        /// <summary>
        /// Calculates the value this animation believes should be the current value for the property.
        /// </summary>
        /// <param name="defaultOriginValue">
        /// This value is the suggested origin value provided to the animation
        /// to be used if the animation does not have its own concept of a
        /// start value. If this animation is the first in a composition chain
        /// this value will be the snapshot value if one is available or the
        /// base property value if it is not; otherwise this value will be the 
        /// value returned by the previous animation in the chain with an 
        /// animationClock that is not Stopped.
        /// </param>
        /// <param name="defaultDestinationValue">
        /// This value is the suggested destination value provided to the animation
        /// to be used if the animation does not have its own concept of an
        /// end value. This value will be the base value if the animation is
        /// in the first composition layer of animations on a property; 
        /// otherwise this value will be the output value from the previous 
        /// composition layer of animations for the property.
        /// </param>
        /// <param name="animationClock">
        /// This is the animationClock which can generate the CurrentTime or
        /// CurrentProgress value to be used by the animation to generate its
        /// output value.
        /// </param>
        /// <returns>
        /// The value this animation believes should be the current value for the property.
        /// </returns>
        public CornerRadius GetCurrentValue(CornerRadius defaultOriginValue, CornerRadius defaultDestinationValue, AnimationClock animationClock)
        {
            ReadPreamble();

            if (animationClock == null)
            {
                throw new ArgumentNullException(nameof(animationClock));
            }

            if (animationClock.CurrentState == ClockState.Stopped)
            {
                return defaultDestinationValue;
            }

            /*
            if (!AnimatedTypeHelpers.IsValidAnimationValueCornerRadius(defaultDestinationValue))
            {
                throw new ArgumentException(
                    SR.Get(
                        SRID.Animation_InvalidBaseValue,
                        defaultDestinationValue, 
                        defaultDestinationValue.GetType(), 
                        GetType()),
                        "defaultDestinationValue");
            }
            */

            return GetCurrentValueCore(defaultOriginValue, defaultDestinationValue, animationClock);
        }


        /// <summary>
        /// Calculates the value this animation believes should be the current value for the property.
        /// </summary>
        /// <param name="defaultOriginValue">
        /// This value is the suggested origin value provided to the animation
        /// to be used if the animation does not have its own concept of a
        /// start value. If this animation is the first in a composition chain
        /// this value will be the snapshot value if one is available or the
        /// base property value if it is not; otherwise this value will be the 
        /// value returned by the previous animation in the chain with an 
        /// animationClock that is not Stopped.
        /// </param>
        /// <param name="defaultDestinationValue">
        /// This value is the suggested destination value provided to the animation
        /// to be used if the animation does not have its own concept of an
        /// end value. This value will be the base value if the animation is
        /// in the first composition layer of animations on a property; 
        /// otherwise this value will be the output value from the previous 
        /// composition layer of animations for the property.
        /// </param>
        /// <param name="animationClock">
        /// This is the animationClock which can generate the CurrentTime or
        /// CurrentProgress value to be used by the animation to generate its
        /// output value.
        /// </param>
        /// <returns>
        /// The value this animation believes should be the current value for the property.
        /// </returns>
        protected abstract CornerRadius GetCurrentValueCore(CornerRadius defaultOriginValue, CornerRadius defaultDestinationValue, AnimationClock animationClock);

        #endregion
    }
}
