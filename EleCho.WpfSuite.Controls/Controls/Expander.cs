using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace EleCho.WpfSuite.Controls
{
    /// <inheritdoc/>
    public class Expander : System.Windows.Controls.Expander
    {
        static Expander()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Expander), new FrameworkPropertyMetadata(typeof(Expander)));
        }



        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public Brush HeaderBorderBrush
        {
            get { return (Brush)GetValue(HeaderBorderBrushProperty); }
            set { SetValue(HeaderBorderBrushProperty, value); }
        }

        public Thickness HeaderBorderThickness
        {
            get { return (Thickness)GetValue(HeaderBorderThicknessProperty); }
            set { SetValue(HeaderBorderThicknessProperty, value); }
        }

        public CornerRadius HeaderCornerRadius
        {
            get { return (CornerRadius)GetValue(HeaderCornerRadiusProperty); }
            set { SetValue(HeaderCornerRadiusProperty, value); }
        }

        public Thickness HeaderMargin
        {
            get { return (Thickness)GetValue(HeaderMarginProperty); }
            set { SetValue(HeaderMarginProperty, value); }
        }

        public Thickness HeaderPadding
        {
            get { return (Thickness)GetValue(HeaderPaddingProperty); }
            set { SetValue(HeaderPaddingProperty, value); }
        }


        public double CircleDiameter
        {
            get { return (double)GetValue(CircleDiameterProperty); }
            set { SetValue(CircleDiameterProperty, value); }
        }

        public Geometry ArrowGeometry
        {
            get { return (Geometry)GetValue(ArrowGeometryProperty); }
            set { SetValue(ArrowGeometryProperty, value); }
        }

        public Geometry ExpandedArrowGeometry
        {
            get { return (Geometry)GetValue(ExpandedArrowGeometryProperty); }
            set { SetValue(ExpandedArrowGeometryProperty, value); }
        }




        public Brush CircleStroke
        {
            get { return (Brush)GetValue(CircleStrokeProperty); }
            set { SetValue(CircleStrokeProperty, value); }
        }

        public Brush CircleFill
        {
            get { return (Brush)GetValue(CircleFillProperty); }
            set { SetValue(CircleFillProperty, value); }
        }

        public Brush ArrowStroke
        {
            get { return (Brush)GetValue(ArrowStrokeProperty); }
            set { SetValue(ArrowStrokeProperty, value); }
        }

        public Brush HoverCircleStroke
        {
            get { return (Brush)GetValue(HoverCircleStrokeProperty); }
            set { SetValue(HoverCircleStrokeProperty, value); }
        }

        public Brush HoverCircleFill
        {
            get { return (Brush)GetValue(HoverCircleFillProperty); }
            set { SetValue(HoverCircleFillProperty, value); }
        }

        public Brush HoverArrowStroke
        {
            get { return (Brush)GetValue(HoverArrowStrokeProperty); }
            set { SetValue(HoverArrowStrokeProperty, value); }
        }

        public Brush PressedCircleStroke
        {
            get { return (Brush)GetValue(PressedCircleStrokeProperty); }
            set { SetValue(PressedCircleStrokeProperty, value); }
        }

        public Brush PressedCircleFill
        {
            get { return (Brush)GetValue(PressedCircleFillProperty); }
            set { SetValue(PressedCircleFillProperty, value); }
        }

        public Brush PressedArrowStroke
        {
            get { return (Brush)GetValue(PressedArrowStrokeProperty); }
            set { SetValue(PressedArrowStrokeProperty, value); }
        }

        public Brush DisabledCircleStroke
        {
            get { return (Brush)GetValue(DisabledCircleStrokeProperty); }
            set { SetValue(DisabledCircleStrokeProperty, value); }
        }

        public Brush DisabledCircleFill
        {
            get { return (Brush)GetValue(DisabledCircleFillProperty); }
            set { SetValue(DisabledCircleFillProperty, value); }
        }

        public Brush DisabledArrowStroke
        {
            get { return (Brush)GetValue(DisabledArrowStrokeProperty); }
            set { SetValue(DisabledArrowStrokeProperty, value); }
        }





        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(Expander), new FrameworkPropertyMetadata(default(CornerRadius)));

        public static readonly DependencyProperty HeaderBorderBrushProperty =
            DependencyProperty.Register(nameof(HeaderBorderBrush), typeof(Brush), typeof(Expander), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty HeaderBorderThicknessProperty =
            DependencyProperty.Register(nameof(HeaderBorderThickness), typeof(Thickness), typeof(Expander), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty HeaderCornerRadiusProperty =
            DependencyProperty.Register(nameof(HeaderCornerRadius), typeof(CornerRadius), typeof(Expander), new FrameworkPropertyMetadata(default(CornerRadius)));

        public static readonly DependencyProperty HeaderMarginProperty =
            DependencyProperty.Register(nameof(HeaderMargin), typeof(Thickness), typeof(Expander), new PropertyMetadata(default(Thickness)));

        public static readonly DependencyProperty HeaderPaddingProperty =
            DependencyProperty.Register(nameof(HeaderPadding), typeof(Thickness), typeof(Expander), new PropertyMetadata(default(Thickness)));

        public static readonly DependencyProperty CircleDiameterProperty =
            DependencyProperty.Register(nameof(CircleDiameter), typeof(double), typeof(Expander), new FrameworkPropertyMetadata(0.0));

        public static readonly DependencyProperty ArrowGeometryProperty =
            DependencyProperty.Register(nameof(ArrowGeometry), typeof(Geometry), typeof(Expander), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty ExpandedArrowGeometryProperty =
            DependencyProperty.Register(nameof(ExpandedArrowGeometry), typeof(Geometry), typeof(Expander), new PropertyMetadata(null));


        public static readonly DependencyProperty CircleStrokeProperty =
            DependencyProperty.Register(nameof(CircleStroke), typeof(Brush), typeof(Expander), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty CircleFillProperty =
            DependencyProperty.Register(nameof(CircleFill), typeof(Brush), typeof(Expander), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty ArrowStrokeProperty =
            DependencyProperty.Register(nameof(ArrowStroke), typeof(Brush), typeof(Expander), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty HoverCircleStrokeProperty =
            DependencyProperty.Register(nameof(HoverCircleStroke), typeof(Brush), typeof(Expander), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty HoverCircleFillProperty =
            DependencyProperty.Register(nameof(HoverCircleFill), typeof(Brush), typeof(Expander), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty HoverArrowStrokeProperty =
            DependencyProperty.Register(nameof(HoverArrowStroke), typeof(Brush), typeof(Expander), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty PressedCircleStrokeProperty =
            DependencyProperty.Register(nameof(PressedCircleStroke), typeof(Brush), typeof(Expander), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty PressedCircleFillProperty =
            DependencyProperty.Register(nameof(PressedCircleFill), typeof(Brush), typeof(Expander), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty PressedArrowStrokeProperty =
            DependencyProperty.Register(nameof(PressedArrowStroke), typeof(Brush), typeof(Expander), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty DisabledCircleStrokeProperty =
            DependencyProperty.Register(nameof(DisabledCircleStroke), typeof(Brush), typeof(Expander), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty DisabledCircleFillProperty =
            DependencyProperty.Register(nameof(DisabledCircleFill), typeof(Brush), typeof(Expander), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty DisabledArrowStrokeProperty =
            DependencyProperty.Register(nameof(DisabledArrowStroke), typeof(Brush), typeof(Expander), new FrameworkPropertyMetadata(null));


    }
}
