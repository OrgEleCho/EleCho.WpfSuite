
using System.Windows;
using System.Windows.Media.Animation;

namespace EleCho.WpfSuite.Media.Animation
{
    public interface IValueAnimator
    {
        /// <summary>
        /// Target value
        /// </summary>
        object? Value { get; set; }
        /// <summary>
        /// Animated current value
        /// </summary>
        object? AnimatedValue { get; }
    }

    public interface IValueAnimator<T> : IValueAnimator
    {
        /// <summary>
        /// Target value
        /// </summary>
        new T? Value { get; set; }
        /// <summary>
        /// Animated current value
        /// </summary>
        new T? AnimatedValue { get; }
    }
}
