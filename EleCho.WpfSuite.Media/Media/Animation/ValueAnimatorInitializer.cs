using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
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
        private static Func<Rotation3D, Quaternion> _internalQuaternionOfRotation3D = (Func<Rotation3D, Quaternion>)
            typeof(Rotation3D).GetProperty("InternalQuaternion", BindingFlags.Instance | BindingFlags.NonPublic)!
            .GetGetMethod(true)!
            .CreateDelegate(typeof(Func<Rotation3D, Quaternion>));

        private static void EnsureComponentsBuffer(ref double[]? components, int componentCount)
        {
            if (components is null ||
                components.Length != componentCount)
            {
                components = new double[componentCount];
            }
        }

        private static void SplitIntoOne<T>(T value, ref double[]? components, Func<T, double> componentGetter)
        {
            EnsureComponentsBuffer(ref components, 1);
            components![0] = componentGetter.Invoke(value);
        }

        private static void SplitIntoTwo<T>(T value, ref double[]? components,
            Func<T, double> componentGetter1,
            Func<T, double> componentGetter2)
        {
            EnsureComponentsBuffer(ref components, 2);
            components![0] = componentGetter1.Invoke(value);
            components![1] = componentGetter2.Invoke(value);
        }

        private static void SplitIntoThree<T>(T value, ref double[]? components,
            Func<T, double> componentGetter1,
            Func<T, double> componentGetter2,
            Func<T, double> componentGetter3)
        {
            EnsureComponentsBuffer(ref components, 3);
            components![0] = componentGetter1.Invoke(value);
            components![1] = componentGetter2.Invoke(value);
            components![2] = componentGetter3.Invoke(value);
        }

        private static void SplitIntoFour<T>(T value, ref double[]? components,
            Func<T, double> componentGetter1,
            Func<T, double> componentGetter2,
            Func<T, double> componentGetter3,
            Func<T, double> componentGetter4)
        {
            EnsureComponentsBuffer(ref components, 4);
            components![0] = componentGetter1.Invoke(value);
            components![1] = componentGetter2.Invoke(value);
            components![1] = componentGetter2.Invoke(value);
            components![3] = componentGetter4.Invoke(value);
        }

        static ValueAnimatorInitializer()
        {
            Specializer<Byte>.DefaultComponentSplitter = static (Byte v, ref double[]? components)
                => SplitIntoOne(v, ref components, static v2 => (double)v2);
            Specializer<Int16>.DefaultComponentSplitter = static (Int16 v, ref double[]? components)
                => SplitIntoOne(v, ref components, static v2 => (double)v2);
            Specializer<Int32>.DefaultComponentSplitter = static (Int32 v, ref double[]? components)
                => SplitIntoOne(v, ref components, static v2 => (double)v2);
            Specializer<Int64>.DefaultComponentSplitter = static (Int64 v, ref double[]? components)
                => SplitIntoOne(v, ref components, static v2 => (double)v2);
            Specializer<Single>.DefaultComponentSplitter = static (Single v, ref double[]? components)
                => SplitIntoOne(v, ref components, static v2 => (double)v2);
            Specializer<Double>.DefaultComponentSplitter = static (Double v, ref double[]? components)
                => SplitIntoOne(v, ref components, static v2 => (double)v2);
            Specializer<Decimal>.DefaultComponentSplitter = static (Decimal v, ref double[]? components)
                => SplitIntoOne(v, ref components, static v2 => (double)v2);
            Specializer<Color>.DefaultComponentSplitter = static (Color v, ref double[]? components)
                => SplitIntoFour(v, ref components, static v2 => v2.ScA, static v2 => v2.ScR, static v2 => v2.ScG, static v2 => v2.ScB);
            Specializer<Point>.DefaultComponentSplitter = static (Point v, ref double[]? components)
                => SplitIntoTwo(v, ref components, static v2 => v2.X, static v2 => v2.Y);
            Specializer<Size>.DefaultComponentSplitter = static (Size v, ref double[]? components)
                => SplitIntoTwo(v, ref components, static v2 => v2.Width, static v2 => v2.Height);
            Specializer<Rect>.DefaultComponentSplitter = static (Rect v, ref double[]? components)
                => SplitIntoFour(v, ref components, static v2 => v2.X, static v2 => v2.Y, static v2 => v2.Width, static v2 => v2.Height);
            Specializer<Vector>.DefaultComponentSplitter = static (Vector v, ref double[]? components)
                => SplitIntoTwo(v, ref components, static v2 => v2.X, static v2 => v2.Y);
            Specializer<Vector3D>.DefaultComponentSplitter = static (Vector3D v, ref double[]? components)
                => SplitIntoThree(v, ref components, static v2 => v2.X, static v2 => v2.Y, static v2 => v2.Z);
            Specializer<Point3D>.DefaultComponentSplitter = static (Point3D v, ref double[]? components)
                => SplitIntoThree(v, ref components, static v2 => v2.X, static v2 => v2.Y, static v2 => v2.Z);
            Specializer<Quaternion>.DefaultComponentSplitter = static (Quaternion v, ref double[]? components)
                => SplitIntoFour(v, ref components, static v2 => v2.X, static v2 => v2.Y, static v2 => v2.Z, static v2 => v2.W);
            Specializer<Thickness>.DefaultComponentSplitter = static (Thickness v, ref double[]? components)
                => SplitIntoFour(v, ref components, static v2 => v2.Left, static v2 => v2.Top, static v2 => v2.Right, static v2 => v2.Bottom);
            Specializer<CornerRadius>.DefaultComponentSplitter = static (CornerRadius v, ref double[]? components)
                => SplitIntoFour(v, ref components, static v2 => v2.TopLeft, static v2 => v2.TopRight, static v2 => v2.BottomRight, static v2 => v2.BottomLeft);

            Specializer<Byte>.DefaultComponentMerger = static comps => (Byte)comps[0];
            Specializer<Int16>.DefaultComponentMerger = static comps => (Int16)comps[0];
            Specializer<Int32>.DefaultComponentMerger = static comps => (Int32)comps[0];
            Specializer<Int64>.DefaultComponentMerger = static comps => (Int64)comps[0];
            Specializer<Single>.DefaultComponentMerger = static comps => (Single)comps[0];
            Specializer<Double>.DefaultComponentMerger = static comps => (Double)comps[0];
            Specializer<Decimal>.DefaultComponentMerger = static comps => (Decimal)comps[0];
            Specializer<Color>.DefaultComponentMerger = static comps => Color.FromScRgb((float)comps[0], (float)comps[1], (float)comps[2], (float)comps[3]);
            Specializer<Point>.DefaultComponentMerger = static comps => new Point(comps[0], comps[1]);
            Specializer<Size>.DefaultComponentMerger = static comps => new Size(comps[0], comps[1]);
            Specializer<Rect>.DefaultComponentMerger = static comps => new Rect(comps[0], comps[1], comps[2], comps[3]);
            Specializer<Vector>.DefaultComponentMerger = static comps => new Vector(comps[0], comps[1]);
            Specializer<Vector3D>.DefaultComponentMerger = static comps => new Vector3D(comps[0], comps[1], comps[2]);
            Specializer<Point3D>.DefaultComponentMerger = static comps => new Point3D(comps[0], comps[1], comps[2]);
            Specializer<Quaternion>.DefaultComponentMerger = static comps => new Quaternion(comps[0], comps[1], comps[2], comps[3]);
            Specializer<Thickness>.DefaultComponentMerger = static comps => new Thickness(comps[0], comps[1], comps[2], comps[3]);
            Specializer<CornerRadius>.DefaultComponentMerger = static comps => new CornerRadius(comps[0], comps[1], comps[2], comps[3]);

            Specializer<Rotation3D>.DefaultComponentSplitter = static (Rotation3D v, ref double[]? components) =>
            {
                EnsureComponentsBuffer(ref components, 4);
                var q = _internalQuaternionOfRotation3D(v);
                components![0] = q.X;
                components![1] = q.Y;
                components![2] = q.Z;
                components![3] = q.W;
            };

            Specializer<Rotation3D>.DefaultComponentMerger = static comps =>
            {
                return new QuaternionRotation3D(new Quaternion(comps[0], comps[1], comps[2], comps[3]));
            };

            // Brush not supported
            //Specializer<Brush?>.DefaultComponentSplitter = static (Brush v, ref double[]? components) => BrushAnimation.LerpBrushWithoutCache(from, to, t);
        }

        public static ComponentSplitter<T>? GetDefaultComponentSplitter<T>()
            => Specializer<T>.DefaultComponentSplitter;

        internal static ComponentMerger<T>? GetDefaultComponentMerger<T>()
            => Specializer<T>.DefaultComponentMerger;

        private static class Specializer<T>
        {
            public static ComponentSplitter<T>? DefaultComponentSplitter { get; set; }
            public static ComponentMerger<T>? DefaultComponentMerger { get; set; }
        }
    }
}
