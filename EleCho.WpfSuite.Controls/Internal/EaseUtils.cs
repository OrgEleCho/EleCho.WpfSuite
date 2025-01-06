using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace EleCho.WpfSuite.Internal
{
    internal static class EaseUtils
    {
        public static Thickness EaseThickness(Thickness from, Thickness to, double normalizedTime, IEasingFunction? easingFunction)
            => LerpUtils.LerpThickness(from, to, easingFunction?.Ease(normalizedTime) ?? normalizedTime);

        public static CornerRadius EaseCornerRadius(CornerRadius from, CornerRadius to, double normalizedTime, IEasingFunction? easingFunction)
            => LerpUtils.LerpCornerRadius(from, to, easingFunction?.Ease(normalizedTime) ?? normalizedTime);

        public static Color EaseColor(Color from, Color to, double normalizedTime, IEasingFunction? easingFunction)
            => LerpUtils.LerpColor(from, to, easingFunction?.Ease(normalizedTime) ?? normalizedTime);

        public static Brush? EaseBrush(ref Brush? cache, Brush? from, Brush? to, double normalizedTime, IEasingFunction? easingFunction)
            => LerpUtils.LerpBrush(ref cache, from, to, easingFunction?.Ease(normalizedTime) ?? normalizedTime);
    }
}
