using System;

namespace EleCho.WpfSuite
{
    /// <summary>
    /// Quadratic bezier curve easing function
    /// </summary>
    public class QuadraticBezierEase : BezierEaseBase
    {
        private double x;
        private double y;

        /// <summary>
        /// X value of control point
        /// </summary>
        public double X { get => x; set => x = value; }

        /// <summary>
        /// Y value of control point
        /// </summary>
        public double Y { get => y; set => y = value; }

        private double GetSamplePoint(double cp, double rate)
        {
            return 2 * rate * (1 - rate) * cp + rate * rate * 1;
        }

        private double PickAppropriateRate(double cp, double p, params double[] rates)
        {
            double result = double.NaN;
            double diffNow = double.MaxValue;
            foreach (var rate in rates)
                if (rate >= 0 && rate <= 1 && Math.Abs(GetSamplePoint(cp, rate) - p) < diffNow)
                    result = rate;
            return result;
        }

        private double GetSampleRate(double cp, double p)
        {
            double
                a = 1 - 2 * cp,
                b = 2 * cp,
                c = -p;
            double
                delta = b * b - 4 * a * c;
            if (delta > 0)
            {
                double
                    x1 = (-b + Math.Sqrt(delta)) / (2 * a),
                    x2 = (-b - Math.Sqrt(delta)) / (2 * a);
                double rst = PickAppropriateRate(cp, p, x1, x2);
                return rst;
            }
            else if (delta == 0)
            {
                double
                    x = -b / (2 * a);
                return x;
            }
            else
            {
                throw new Exception("Fuck");
            }
        }

        /// <inheritdoc/>
        protected override double GetSampleRate(double time) => GetSampleRate(this.X, time);

        /// <inheritdoc/>
        protected override double GetSampleValue(double rate) => GetSamplePoint(this.X, rate);
    }
}
