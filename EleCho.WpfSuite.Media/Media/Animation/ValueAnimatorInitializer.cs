using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;

namespace EleCho.WpfSuite.Media.Animation
{
    internal static class ValueAnimatorInitializer
    {
        static ValueAnimatorInitializer()
        {
            Specializer<Byte>.DefaultTimelineFactory = static (from, to, duration, easingFunction) => new ByteAnimation()
            {
                From = from,
                To = to,
                Duration = duration,
                EasingFunction = easingFunction,
            };

            Specializer<Int16>.DefaultTimelineFactory = static (from, to, duration, easingFunction) => new Int16Animation()
            {
                From = from,
                To = to,
                Duration = duration,
                EasingFunction = easingFunction,
            };

            Specializer<Int32>.DefaultTimelineFactory = static (from, to, duration, easingFunction) => new Int32Animation()
            {
                From = from,
                To = to,
                Duration = duration,
                EasingFunction = easingFunction,
            };

            Specializer<Int64>.DefaultTimelineFactory = static (from, to, duration, easingFunction) => new Int64Animation()
            {
                From = from,
                To = to,
                Duration = duration,
                EasingFunction = easingFunction,
            };

            Specializer<Single>.DefaultTimelineFactory = static (from, to, duration, easingFunction) => new SingleAnimation()
            {
                From = from,
                To = to,
                Duration = duration,
                EasingFunction = easingFunction,
            };

            Specializer<Double>.DefaultTimelineFactory = static (from, to, duration, easingFunction) => new DoubleAnimation()
            {
                From = from,
                To = to,
                Duration = duration,
                EasingFunction = easingFunction,
            };

            Specializer<Decimal>.DefaultTimelineFactory = static (from, to, duration, easingFunction) => new DecimalAnimation()
            {
                From = from,
                To = to,
                Duration = duration,
                EasingFunction = easingFunction,
            };

            Specializer<Color>.DefaultTimelineFactory = static (from, to, duration, easingFunction) => new ColorAnimation()
            {
                From = from,
                To = to,
                Duration = duration,
                EasingFunction = easingFunction,
            };

            Specializer<Point>.DefaultTimelineFactory = static (from, to, duration, easingFunction) => new PointAnimation()
            {
                From = from,
                To = to,
                Duration = duration,
                EasingFunction = easingFunction,
            };

            Specializer<Size>.DefaultTimelineFactory = static (from, to, duration, easingFunction) => new SizeAnimation()
            {
                From = from,
                To = to,
                Duration = duration,
                EasingFunction = easingFunction,
            };

            Specializer<Rect>.DefaultTimelineFactory = static (from, to, duration, easingFunction) => new RectAnimation()
            {
                From = from,
                To = to,
                Duration = duration,
                EasingFunction = easingFunction,
            };

            Specializer<Vector>.DefaultTimelineFactory = static (from, to, duration, easingFunction) => new VectorAnimation()
            {
                From = from,
                To = to,
                Duration = duration,
                EasingFunction = easingFunction,
            };

            Specializer<Vector3D>.DefaultTimelineFactory = static (from, to, duration, easingFunction) => new Vector3DAnimation()
            {
                From = from,
                To = to,
                Duration = duration,
                EasingFunction = easingFunction,
            };

            Specializer<Point3D>.DefaultTimelineFactory = static (from, to, duration, easingFunction) => new Point3DAnimation()
            {
                From = from,
                To = to,
                Duration = duration,
                EasingFunction = easingFunction,
            };

            Specializer<Quaternion>.DefaultTimelineFactory = static (from, to, duration, easingFunction) => new QuaternionAnimation()
            {
                From = from,
                To = to,
                Duration = duration,
                EasingFunction = easingFunction,
            };

            Specializer<Rotation3D>.DefaultTimelineFactory = static (from, to, duration, easingFunction) => new Rotation3DAnimation()
            {
                From = from,
                To = to,
                Duration = duration,
                EasingFunction = easingFunction,
            };

            Specializer<Thickness>.DefaultTimelineFactory = static (from, to, duration, easingFunction) => new ThicknessAnimation()
            {
                From = from,
                To = to,
                Duration = duration,
                EasingFunction = easingFunction,
            };

            Specializer<CornerRadius>.DefaultTimelineFactory = static (from, to, duration, easingFunction) => new CornerRadiusAnimation()
            {
                From = from,
                To = to,
                Duration = duration,
                EasingFunction = easingFunction,
            };

            Specializer<CornerRadius>.DefaultTimelineFactory = static (from, to, duration, easingFunction) => new CornerRadiusAnimation()
            {
                From = from,
                To = to,
                Duration = duration,
                EasingFunction = easingFunction,
            };

            Specializer<Brush>.DefaultTimelineFactory = static (from, to, duration, easingFunction) => new BrushAnimation()
            {
                From = from,
                To = to,
                Duration = duration,
                EasingFunction = easingFunction,
            };
        }

        public static ValueAnimatorTimelineFactory<T>? GetDefaultTimelineFactory<T>()
            => Specializer<T>.DefaultTimelineFactory;

        private static class Specializer<T>
        {
            public static ValueAnimatorTimelineFactory<T>? DefaultTimelineFactory { get; set; }
        }
    }
}
