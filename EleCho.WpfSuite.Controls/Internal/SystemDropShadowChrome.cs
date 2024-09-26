using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EleCho.WpfSuite.Internal
{
    internal sealed class SystemDropShadowChrome : Decorator
    {
        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register("Color", typeof(Color), typeof(SystemDropShadowChrome), new FrameworkPropertyMetadata(Color.FromArgb(113, 0, 0, 0), FrameworkPropertyMetadataOptions.AffectsRender, ClearBrushes));

        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(SystemDropShadowChrome), new FrameworkPropertyMetadata(default(CornerRadius), FrameworkPropertyMetadataOptions.AffectsRender, ClearBrushes), IsCornerRadiusValid);

        private const double ShadowDepth = 5.0;

        private const int TopLeft = 0;

        private const int Top = 1;

        private const int TopRight = 2;

        private const int Left = 3;

        private const int Center = 4;

        private const int Right = 5;

        private const int BottomLeft = 6;

        private const int Bottom = 7;

        private const int BottomRight = 8;

        private static Brush[]? _commonBrushes;

        private static CornerRadius _commonCornerRadius;

        private static object _resourceAccess = new object();

        private Brush?[]? _brushes;

        public Color Color
        {
            get
            {
                return (Color)GetValue(ColorProperty);
            }
            set
            {
                SetValue(ColorProperty, value);
            }
        }

        public CornerRadius CornerRadius
        {
            get
            {
                return (CornerRadius)GetValue(CornerRadiusProperty);
            }
            set
            {
                SetValue(CornerRadiusProperty, value);
            }
        }

        private static bool IsCornerRadiusValid(object value)
        {
            CornerRadius cornerRadius = (CornerRadius)value;
            if (!(cornerRadius.TopLeft < 0.0) && !(cornerRadius.TopRight < 0.0) && !(cornerRadius.BottomLeft < 0.0) && !(cornerRadius.BottomRight < 0.0) && !double.IsNaN(cornerRadius.TopLeft) && !double.IsNaN(cornerRadius.TopRight) && !double.IsNaN(cornerRadius.BottomLeft) && !double.IsNaN(cornerRadius.BottomRight) && !double.IsInfinity(cornerRadius.TopLeft) && !double.IsInfinity(cornerRadius.TopRight) && !double.IsInfinity(cornerRadius.BottomLeft))
            {
                return !double.IsInfinity(cornerRadius.BottomRight);
            }
            return false;
        }

        private static void ClearBrushes(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((SystemDropShadowChrome)o)._brushes = null;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            CornerRadius cornerRadius = CornerRadius;
            Rect rect = new Rect(new Point(5.0, 5.0), new Size(base.RenderSize.Width, base.RenderSize.Height));
            Color color = Color;
            if (!(rect.Width > 0.0) || !(rect.Height > 0.0) || color.A <= 0)
            {
                return;
            }
            double num = rect.Right - rect.Left - 10.0;
            double num2 = rect.Bottom - rect.Top - 10.0;
            double val = Math.Min(num * 0.5, num2 * 0.5);
            cornerRadius.TopLeft = Math.Min(cornerRadius.TopLeft, val);
            cornerRadius.TopRight = Math.Min(cornerRadius.TopRight, val);
            cornerRadius.BottomLeft = Math.Min(cornerRadius.BottomLeft, val);
            cornerRadius.BottomRight = Math.Min(cornerRadius.BottomRight, val);
            Brush[] brushes = GetBrushes(color, cornerRadius);
            double num3 = rect.Top + 5.0;
            double num4 = rect.Left + 5.0;
            double num5 = rect.Right - 5.0;
            double num6 = rect.Bottom - 5.0;
            double[] array = new double[6]
        {
            num4,
            num4 + cornerRadius.TopLeft,
            num5 - cornerRadius.TopRight,
            num4 + cornerRadius.BottomLeft,
            num5 - cornerRadius.BottomRight,
            num5
        };
            double[] array2 = new double[6]
        {
            num3,
            num3 + cornerRadius.TopLeft,
            num3 + cornerRadius.TopRight,
            num6 - cornerRadius.BottomLeft,
            num6 - cornerRadius.BottomRight,
            num6
        };
            drawingContext.PushGuidelineSet(new GuidelineSet(array, array2));
            cornerRadius.TopLeft += 5.0;
            cornerRadius.TopRight += 5.0;
            cornerRadius.BottomLeft += 5.0;
            cornerRadius.BottomRight += 5.0;
            drawingContext.DrawRectangle(rectangle: new Rect(rect.Left, rect.Top, cornerRadius.TopLeft, cornerRadius.TopLeft), brush: brushes[0], pen: null);
            double num7 = array[2] - array[1];
            if (num7 > 0.0)
            {
                drawingContext.DrawRectangle(rectangle: new Rect(array[1], rect.Top, num7, 5.0), brush: brushes[1], pen: null);
            }
            drawingContext.DrawRectangle(rectangle: new Rect(array[2], rect.Top, cornerRadius.TopRight, cornerRadius.TopRight), brush: brushes[2], pen: null);
            double num8 = array2[3] - array2[1];
            if (num8 > 0.0)
            {
                drawingContext.DrawRectangle(rectangle: new Rect(rect.Left, array2[1], 5.0, num8), brush: brushes[3], pen: null);
            }
            double num9 = array2[4] - array2[2];
            if (num9 > 0.0)
            {
                drawingContext.DrawRectangle(rectangle: new Rect(array[5], array2[2], 5.0, num9), brush: brushes[5], pen: null);
            }
            drawingContext.DrawRectangle(rectangle: new Rect(rect.Left, array2[3], cornerRadius.BottomLeft, cornerRadius.BottomLeft), brush: brushes[6], pen: null);
            double num10 = array[4] - array[3];
            if (num10 > 0.0)
            {
                drawingContext.DrawRectangle(rectangle: new Rect(array[3], array2[5], num10, 5.0), brush: brushes[7], pen: null);
            }
            drawingContext.DrawRectangle(rectangle: new Rect(array[4], array2[4], cornerRadius.BottomRight, cornerRadius.BottomRight), brush: brushes[8], pen: null);
            if (cornerRadius.TopLeft == 5.0 && cornerRadius.TopLeft == cornerRadius.TopRight && cornerRadius.TopLeft == cornerRadius.BottomLeft && cornerRadius.TopLeft == cornerRadius.BottomRight)
            {
                drawingContext.DrawRectangle(rectangle: new Rect(array[0], array2[0], num, num2), brush: brushes[4], pen: null);
            }
            else
            {
                PathFigure pathFigure = new PathFigure();
                if (cornerRadius.TopLeft > 5.0)
                {
                    pathFigure.StartPoint = new Point(array[1], array2[0]);
                    pathFigure.Segments.Add(new LineSegment(new Point(array[1], array2[1]), isStroked: true));
                    pathFigure.Segments.Add(new LineSegment(new Point(array[0], array2[1]), isStroked: true));
                }
                else
                {
                    pathFigure.StartPoint = new Point(array[0], array2[0]);
                }
                if (cornerRadius.BottomLeft > 5.0)
                {
                    pathFigure.Segments.Add(new LineSegment(new Point(array[0], array2[3]), isStroked: true));
                    pathFigure.Segments.Add(new LineSegment(new Point(array[3], array2[3]), isStroked: true));
                    pathFigure.Segments.Add(new LineSegment(new Point(array[3], array2[5]), isStroked: true));
                }
                else
                {
                    pathFigure.Segments.Add(new LineSegment(new Point(array[0], array2[5]), isStroked: true));
                }
                if (cornerRadius.BottomRight > 5.0)
                {
                    pathFigure.Segments.Add(new LineSegment(new Point(array[4], array2[5]), isStroked: true));
                    pathFigure.Segments.Add(new LineSegment(new Point(array[4], array2[4]), isStroked: true));
                    pathFigure.Segments.Add(new LineSegment(new Point(array[5], array2[4]), isStroked: true));
                }
                else
                {
                    pathFigure.Segments.Add(new LineSegment(new Point(array[5], array2[5]), isStroked: true));
                }
                if (cornerRadius.TopRight > 5.0)
                {
                    pathFigure.Segments.Add(new LineSegment(new Point(array[5], array2[2]), isStroked: true));
                    pathFigure.Segments.Add(new LineSegment(new Point(array[2], array2[2]), isStroked: true));
                    pathFigure.Segments.Add(new LineSegment(new Point(array[2], array2[0]), isStroked: true));
                }
                else
                {
                    pathFigure.Segments.Add(new LineSegment(new Point(array[5], array2[0]), isStroked: true));
                }
                pathFigure.IsClosed = true;
                pathFigure.Freeze();
                PathGeometry pathGeometry = new PathGeometry();
                pathGeometry.Figures.Add(pathFigure);
                pathGeometry.Freeze();
                drawingContext.DrawGeometry(brushes[4], null, pathGeometry);
            }
            drawingContext.Pop();
        }

        private static GradientStopCollection CreateStops(Color c, double cornerRadius)
        {
            double num = 1.0 / (cornerRadius + 5.0);
            GradientStopCollection gradientStopCollection = new GradientStopCollection();
            gradientStopCollection.Add(new GradientStop(c, (0.5 + cornerRadius) * num));
            Color color = c;
            color.A = (byte)(0.74336 * (double)(int)c.A);
            gradientStopCollection.Add(new GradientStop(color, (1.5 + cornerRadius) * num));
            color.A = (byte)(0.38053 * (double)(int)c.A);
            gradientStopCollection.Add(new GradientStop(color, (2.5 + cornerRadius) * num));
            color.A = (byte)(0.12389 * (double)(int)c.A);
            gradientStopCollection.Add(new GradientStop(color, (3.5 + cornerRadius) * num));
            color.A = (byte)(0.02654 * (double)(int)c.A);
            gradientStopCollection.Add(new GradientStop(color, (4.5 + cornerRadius) * num));
            color.A = 0;
            gradientStopCollection.Add(new GradientStop(color, (5.0 + cornerRadius) * num));
            gradientStopCollection.Freeze();
            return gradientStopCollection;
        }

        private static Brush[] CreateBrushes(Color c, CornerRadius cornerRadius)
        {
            Brush?[] array = new Brush?[9]
            {
                null,
                null,
                null,
                null,
                new SolidColorBrush(c),
                null,
                null,
                null,
                null
            };

            array[4]!.Freeze();
            GradientStopCollection gradientStopCollection = CreateStops(c, 0.0);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(gradientStopCollection, new Point(0.0, 1.0), new Point(0.0, 0.0));
            linearGradientBrush.Freeze();
            array[1] = linearGradientBrush;
            LinearGradientBrush linearGradientBrush2 = new LinearGradientBrush(gradientStopCollection, new Point(1.0, 0.0), new Point(0.0, 0.0));
            linearGradientBrush2.Freeze();
            array[3] = linearGradientBrush2;
            LinearGradientBrush linearGradientBrush3 = new LinearGradientBrush(gradientStopCollection, new Point(0.0, 0.0), new Point(1.0, 0.0));
            linearGradientBrush3.Freeze();
            array[5] = linearGradientBrush3;
            LinearGradientBrush linearGradientBrush4 = new LinearGradientBrush(gradientStopCollection, new Point(0.0, 0.0), new Point(0.0, 1.0));
            linearGradientBrush4.Freeze();
            array[7] = linearGradientBrush4;
            GradientStopCollection gradientStopCollection2 = ((cornerRadius.TopLeft != 0.0) ? CreateStops(c, cornerRadius.TopLeft) : gradientStopCollection);
            RadialGradientBrush radialGradientBrush = new RadialGradientBrush(gradientStopCollection2);
            radialGradientBrush.RadiusX = 1.0;
            radialGradientBrush.RadiusY = 1.0;
            radialGradientBrush.Center = new Point(1.0, 1.0);
            radialGradientBrush.GradientOrigin = new Point(1.0, 1.0);
            radialGradientBrush.Freeze();
            array[0] = radialGradientBrush;
            GradientStopCollection gradientStopCollection3 = ((cornerRadius.TopRight == 0.0) ? gradientStopCollection : ((cornerRadius.TopRight != cornerRadius.TopLeft) ? CreateStops(c, cornerRadius.TopRight) : gradientStopCollection2));
            RadialGradientBrush radialGradientBrush2 = new RadialGradientBrush(gradientStopCollection3);
            radialGradientBrush2.RadiusX = 1.0;
            radialGradientBrush2.RadiusY = 1.0;
            radialGradientBrush2.Center = new Point(0.0, 1.0);
            radialGradientBrush2.GradientOrigin = new Point(0.0, 1.0);
            radialGradientBrush2.Freeze();
            array[2] = radialGradientBrush2;
            GradientStopCollection gradientStopCollection4 = ((cornerRadius.BottomLeft == 0.0) ? gradientStopCollection : ((cornerRadius.BottomLeft == cornerRadius.TopLeft) ? gradientStopCollection2 : ((cornerRadius.BottomLeft != cornerRadius.TopRight) ? CreateStops(c, cornerRadius.BottomLeft) : gradientStopCollection3)));
            RadialGradientBrush radialGradientBrush3 = new RadialGradientBrush(gradientStopCollection4);
            radialGradientBrush3.RadiusX = 1.0;
            radialGradientBrush3.RadiusY = 1.0;
            radialGradientBrush3.Center = new Point(1.0, 0.0);
            radialGradientBrush3.GradientOrigin = new Point(1.0, 0.0);
            radialGradientBrush3.Freeze();
            array[6] = radialGradientBrush3;
            GradientStopCollection gradientStopCollection5 = ((cornerRadius.BottomRight == 0.0) ? gradientStopCollection : ((cornerRadius.BottomRight == cornerRadius.TopLeft) ? gradientStopCollection2 : ((cornerRadius.BottomRight == cornerRadius.TopRight) ? gradientStopCollection3 : ((cornerRadius.BottomRight != cornerRadius.BottomLeft) ? CreateStops(c, cornerRadius.BottomRight) : gradientStopCollection4))));
            RadialGradientBrush radialGradientBrush4 = new RadialGradientBrush(gradientStopCollection5);
            radialGradientBrush4.RadiusX = 1.0;
            radialGradientBrush4.RadiusY = 1.0;
            radialGradientBrush4.Center = new Point(0.0, 0.0);
            radialGradientBrush4.GradientOrigin = new Point(0.0, 0.0);
            radialGradientBrush4.Freeze();
            array[8] = radialGradientBrush4;
            return array;
        }

        private Brush?[] GetBrushes(Color c, CornerRadius cornerRadius)
        {
            if (_commonBrushes == null)
            {
                lock (_resourceAccess)
                {
                    if (_commonBrushes == null)
                    {
                        _commonBrushes = CreateBrushes(c, cornerRadius);
                        _commonCornerRadius = cornerRadius;
                    }
                }
            }
            if (c == ((SolidColorBrush)_commonBrushes[4]).Color && cornerRadius == _commonCornerRadius)
            {
                _brushes = null;
                return _commonBrushes;
            }
            if (_brushes == null)
            {
                _brushes = CreateBrushes(c, cornerRadius);
            }
            return _brushes;
        }
    }

}
