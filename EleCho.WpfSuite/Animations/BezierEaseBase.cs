using System.Windows.Media.Animation;

namespace EleCho.WpfSuite
{
    /// <summary>
    /// Base class of bezier curve easing function
    /// </summary>
    public abstract class BezierEaseBase : IEasingFunction
    {
        /// <summary>
        /// Calculate the progress rate of bezier curve from normalized time
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        protected abstract double GetSampleRate(double time);

        /// <summary>
        /// Calculate the bezier curve value from progress rate
        /// </summary>
        /// <param name="rate"></param>
        /// <returns></returns>
        protected abstract double GetSampleValue(double rate);

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double Ease(double normalizedTime) => GetSampleValue(GetSampleRate(normalizedTime));
    }
}
