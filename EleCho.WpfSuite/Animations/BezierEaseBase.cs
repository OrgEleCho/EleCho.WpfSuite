using System.Windows.Media.Animation;

namespace EleCho.WpfSuite
{
    public abstract class BezierEaseBase : IEasingFunction
    {
        protected abstract double GetSampleRate(double time);
        protected abstract double GetSampleValue(double rate);

        public double Ease(double normalizedTime) => GetSampleValue(GetSampleRate(normalizedTime));
    }
}
