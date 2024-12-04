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

        public ExpanderHeaderIconPosition HeaderIconPosition
        {
            get { return (ExpanderHeaderIconPosition)GetValue(HeaderIconPositionProperty); }
            set { SetValue(HeaderIconPositionProperty, value); }
        }



        public Brush HeaderForeground
        {
            get { return (Brush)GetValue(HeaderForegroundProperty); }
            set { SetValue(HeaderForegroundProperty, value); }
        }

        public Brush HeaderBackground
        {
            get { return (Brush)GetValue(HeaderBackgroundProperty); }
            set { SetValue(HeaderBackgroundProperty, value); }
        }

        public Brush HeaderBorderBrush
        {
            get { return (Brush)GetValue(HeaderBorderBrushProperty); }
            set { SetValue(HeaderBorderBrushProperty, value); }
        }

        public Brush HeaderHoverForeground
        {
            get { return (Brush)GetValue(HeaderHoverForegroundProperty); }
            set { SetValue(HeaderHoverForegroundProperty, value); }
        }

        public Brush HeaderHoverBackground
        {
            get { return (Brush)GetValue(HeaderHoverBackgroundProperty); }
            set { SetValue(HeaderHoverBackgroundProperty, value); }
        }

        public Brush HeaderHoverBorderBrush
        {
            get { return (Brush)GetValue(HeaderHoverBorderBrushProperty); }
            set { SetValue(HeaderHoverBorderBrushProperty, value); }
        }

        public Brush HeaderPressedForeground
        {
            get { return (Brush)GetValue(HeaderPressedForegroundProperty); }
            set { SetValue(HeaderPressedForegroundProperty, value); }
        }

        public Brush HeaderPressedBackground
        {
            get { return (Brush)GetValue(HeaderPressedBackgroundProperty); }
            set { SetValue(HeaderPressedBackgroundProperty, value); }
        }

        public Brush HeaderPressedBorderBrush
        {
            get { return (Brush)GetValue(HeaderPressedBorderBrushProperty); }
            set { SetValue(HeaderPressedBorderBrushProperty, value); }
        }

        public Brush HeaderCheckedForeground
        {
            get { return (Brush)GetValue(HeaderCheckedForegroundProperty); }
            set { SetValue(HeaderCheckedForegroundProperty, value); }
        }

        public Brush HeaderCheckedBackground
        {
            get { return (Brush)GetValue(HeaderCheckedBackgroundProperty); }
            set { SetValue(HeaderCheckedBackgroundProperty, value); }
        }

        public Brush HeaderCheckedBorderBrush
        {
            get { return (Brush)GetValue(HeaderCheckedBorderBrushProperty); }
            set { SetValue(HeaderCheckedBorderBrushProperty, value); }
        }

        public Brush HeaderDisabledForeground
        {
            get { return (Brush)GetValue(HeaderDisabledForegroundProperty); }
            set { SetValue(HeaderDisabledForegroundProperty, value); }
        }

        public Brush HeaderDisabledBackground
        {
            get { return (Brush)GetValue(HeaderDisabledBackgroundProperty); }
            set { SetValue(HeaderDisabledBackgroundProperty, value); }
        }

        public Brush HeaderDisabledBorderBrush
        {
            get { return (Brush)GetValue(HeaderDisabledBorderBrushProperty); }
            set { SetValue(HeaderDisabledBorderBrushProperty, value); }
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

        public static readonly DependencyProperty HeaderBorderThicknessProperty =
            DependencyProperty.Register(nameof(HeaderBorderThickness), typeof(Thickness), typeof(Expander), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty HeaderCornerRadiusProperty =
            DependencyProperty.Register(nameof(HeaderCornerRadius), typeof(CornerRadius), typeof(Expander), new FrameworkPropertyMetadata(default(CornerRadius)));

        public static readonly DependencyProperty HeaderMarginProperty =
            DependencyProperty.Register(nameof(HeaderMargin), typeof(Thickness), typeof(Expander), new PropertyMetadata(default(Thickness)));

        public static readonly DependencyProperty HeaderPaddingProperty =
            DependencyProperty.Register(nameof(HeaderPadding), typeof(Thickness), typeof(Expander), new PropertyMetadata(default(Thickness)));

        public static readonly DependencyProperty HeaderIconPositionProperty =
            DependencyProperty.Register(nameof(HeaderIconPosition), typeof(ExpanderHeaderIconPosition), typeof(Expander), new PropertyMetadata(ExpanderHeaderIconPosition.Start));

        public static readonly DependencyProperty HeaderForegroundProperty =
            DependencyProperty.Register(nameof(HeaderForeground), typeof(Brush), typeof(Expander), new PropertyMetadata(null));

        public static readonly DependencyProperty HeaderBackgroundProperty =
            DependencyProperty.Register(nameof(HeaderBackground), typeof(Brush), typeof(Expander), new PropertyMetadata(null));

        public static readonly DependencyProperty HeaderBorderBrushProperty =
            DependencyProperty.Register(nameof(HeaderBorderBrush), typeof(Brush), typeof(Expander), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty HeaderHoverForegroundProperty =
            DependencyProperty.Register(nameof(HeaderHoverForeground), typeof(Brush), typeof(Expander), new PropertyMetadata(null));

        public static readonly DependencyProperty HeaderHoverBackgroundProperty =
            DependencyProperty.Register(nameof(HeaderHoverBackground), typeof(Brush), typeof(Expander), new PropertyMetadata(null));

        public static readonly DependencyProperty HeaderHoverBorderBrushProperty =
            DependencyProperty.Register(nameof(HeaderHoverBorderBrush), typeof(Brush), typeof(Expander), new PropertyMetadata(null));

        public static readonly DependencyProperty HeaderPressedForegroundProperty =
            DependencyProperty.Register(nameof(HeaderPressedForeground), typeof(Brush), typeof(Expander), new PropertyMetadata(null));

        public static readonly DependencyProperty HeaderPressedBackgroundProperty =
            DependencyProperty.Register(nameof(HeaderPressedBackground), typeof(Brush), typeof(Expander), new PropertyMetadata(null));

        public static readonly DependencyProperty HeaderPressedBorderBrushProperty =
            DependencyProperty.Register(nameof(HeaderPressedBorderBrush), typeof(Brush), typeof(Expander), new PropertyMetadata(null));

        public static readonly DependencyProperty HeaderCheckedForegroundProperty =
            DependencyProperty.Register(nameof(HeaderCheckedForeground), typeof(Brush), typeof(Expander), new PropertyMetadata(null));

        public static readonly DependencyProperty HeaderCheckedBackgroundProperty =
            DependencyProperty.Register(nameof(HeaderCheckedBackground), typeof(Brush), typeof(Expander), new PropertyMetadata(null));

        public static readonly DependencyProperty HeaderCheckedBorderBrushProperty =
            DependencyProperty.Register(nameof(HeaderCheckedBorderBrush), typeof(Brush), typeof(Expander), new PropertyMetadata(null));

        public static readonly DependencyProperty HeaderDisabledForegroundProperty =
            DependencyProperty.Register(nameof(HeaderDisabledForeground), typeof(Brush), typeof(Expander), new PropertyMetadata(null));

        public static readonly DependencyProperty HeaderDisabledBackgroundProperty =
            DependencyProperty.Register(nameof(HeaderDisabledBackground), typeof(Brush), typeof(Expander), new PropertyMetadata(null));

        public static readonly DependencyProperty HeaderDisabledBorderBrushProperty =
            DependencyProperty.Register(nameof(HeaderDisabledBorderBrush), typeof(Brush), typeof(Expander), new PropertyMetadata(null));

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
