using System;
using System.Windows.Media.Animation;

namespace EleCho.WpfSuite
{
    /// <summary>
    /// Cubic bezier curve easing function
    /// </summary>
    public class CubicBezierEase : BezierEaseBase
    {
        private double x1;
        private double y1;
        private double x2;
        private double y2;

        /// <summary>
        /// X value of the first control point
        /// </summary>
        public double X1
        {
            get => x1; set
            {
                if (value < 0 || value > 1)
                    throw new ArgumentOutOfRangeException();

                x1 = value;
            }
        }

        /// <summary>
        /// Y value of the first control point
        /// </summary>
        public double Y1 { get => y1; set => y1 = value; }

        /// <summary>
        /// X value of the second control point
        /// </summary>
        public double X2
        {
            get => x2; set
            {
                if (value < 0 || value > 1)
                    throw new ArgumentOutOfRangeException();

                x2 = value;
            }
        }

        /// <summary>
        /// Y value of the second control point
        /// </summary>
        public double Y2 { get => y2; set => y2 = value; }

        private double MathCbrt(double num)
        {
            return num < 0 ? -Math.Pow(-num, 1d / 3) : Math.Pow(num, 1d / 3);
        }
        private double PickAppropriateRate(double cp1, double cp2, double p, params double[] rates)
        {
            double result = double.NaN;
            double diffNow = double.MaxValue;
            foreach (var rate in rates)
                if (!double.IsNaN(rate) && Math.Abs(GetSamplePoint(cp1, cp2, rate) - p) < diffNow)
                    result = rate;
            return result;
        }
        private double GetSampleRate(double cp1, double cp2, double p)
        {
            // 警告, 这里是魔法, 是求根公式

            double
                a = 3 * cp1 - 3 * cp2 + 1,
                b = -6 * cp1 + 3 * cp2,
                c = 3 * cp1,
                d = -p;
            double
                A = b * b - 3 * a * c,
                B = b * c - 9 * a * d,
                C = c * c - 3 * b * d;
            double
                delta = B * B - 4 * A * C;
            if (A == B)
            {
                double x = -c / b; // -b/3a -c/b -3d/c
                double rst = x;
                return rst;
            }
            else if (delta > 0)
            {
                //double I = double.NaN;
                double
                    y1 = A * b + 3 * a * ((-B + Math.Sqrt(delta)) / 2),
                    y2 = A * b + 3 * a * ((-B - Math.Sqrt(delta)) / 2);
                double
                    xtmp1 = MathCbrt(y1) + MathCbrt(y2); //,
                                                         //xtmp2 = Cubic(y1) - Cubic(y2);
                double   // what the fuck is I? virtual number wtf... impossible for now (((
                    x1 = (-b - xtmp1) / (3 * a); //,
                                                 //x2 = (-2 * b + xtmp1 + Math.Sqrt(3) * xtmp2 * I) / (6 * a),
                                                 //x3 = (-2 * b + xtmp1 - Math.Sqrt(3) * xtmp2 * I) / (6 * a);
                double rst = x1;
                return rst;
            }
            else if (delta == 0)
            {
                double k = B / A;
                double
                    x1 = -b / a + k,
                    x2 = -k / 2;
                double rst = PickAppropriateRate(cp1, cp2, p, x1, x2);
                return rst;
            }
            else  // delta < 0
            {
                double
                    t = (2 * A * b - 3 * a * B) / (2 * A * Math.Sqrt(A)),
                    sita = Math.Acos(t);
                double
                    x1 = (-b - 2 * Math.Sqrt(A) * Math.Cos(sita / 3)) / (3 * a),
                    x2 = (-b + Math.Sqrt(A) * (Math.Cos(sita / 3) + Math.Sqrt(3) * Math.Sin(sita / 3))) / (3 * a),
                    x3 = (-b + Math.Sqrt(A) * (Math.Cos(sita / 3) - Math.Sqrt(3) * Math.Sin(sita / 3))) / (3 * a);
                double rst = PickAppropriateRate(cp1, cp2, p, x1, x2, x3);
                return rst;
            }
        }
        private double GetSamplePoint(double cp1, double cp2, double rate)
        {
            return 3d * cp1 * rate * (1d - rate) * (1d - rate) + 3d * cp2 * rate * rate * (1d - rate) + rate * rate * rate;
            // return 3 * cp1 * rate - 3 * cp1 * 2 * rate * rate + 3 * cp1 * rate * rate * rate + 3 * cp2 * rate * rate - 3 * cp2 * rate * rate * rate + rate * rate * rate;
        }

        /// <inheritdoc/>
        protected override double GetSampleRate(double time)
        {
            return GetSampleRate(X1, X2, time);
        }

        /// <inheritdoc/>
        protected override double GetSampleValue(double rate)
        {
            return GetSamplePoint(Y1, Y2, rate);
        }
    }
}
