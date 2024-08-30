using System.Windows;
using System.Windows.Media;

namespace EleCho.WpfSuite
{
    public class Thumb : System.Windows.Controls.Primitives.Thumb
    {
        static Thumb()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Thumb), new FrameworkPropertyMetadata(typeof(Thumb)));
        }

        /// <summary>
        /// The CornerRadius property allows users to control the roundness of the corners independently by
        /// setting a radius value for each corner.  Radius values that are too large are scaled so that they
        /// smoothly blend from corner to corner.
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public Brush HoverBorderBrush
        {
            get { return (Brush)GetValue(HoverBorderBrushProperty); }
            set { SetValue(HoverBorderBrushProperty, value); }
        }

        public Brush DraggingBorderBrush
        {
            get { return (Brush)GetValue(DraggingBorderBrushProperty); }
            set { SetValue(DraggingBorderBrushProperty, value); }
        }

        public Brush DisabledBorderBrush
        {
            get { return (Brush)GetValue(DisabledBorderBrushProperty); }
            set { SetValue(DisabledBorderBrushProperty, value); }
        }

        public Brush HoverBackground
        {
            get { return (Brush)GetValue(HoverBackgroundProperty); }
            set { SetValue(HoverBackgroundProperty, value); }
        }

        public Brush DraggingBackground
        {
            get { return (Brush)GetValue(DraggingBackgroundProperty); }
            set { SetValue(DraggingBackgroundProperty, value); }
        }

        public Brush DisabledBackground
        {
            get { return (Brush)GetValue(DisabledBackgroundProperty); }
            set { SetValue(DisabledBackgroundProperty, value); }
        }


        public static readonly DependencyProperty CornerRadiusProperty =
            Border.CornerRadiusProperty.AddOwner(typeof(Thumb));

        public static readonly DependencyProperty HoverBackgroundProperty =
            DependencyProperty.Register(nameof(HoverBackground), typeof(Brush), typeof(Thumb), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty DraggingBackgroundProperty =
            DependencyProperty.Register(nameof(DraggingBackground), typeof(Brush), typeof(Thumb), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty DisabledBackgroundProperty =
            DependencyProperty.Register(nameof(DisabledBackground), typeof(Brush), typeof(Thumb), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty HoverBorderBrushProperty =
            DependencyProperty.Register(nameof(HoverBorderBrush), typeof(Brush), typeof(Thumb), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty DraggingBorderBrushProperty =
            DependencyProperty.Register(nameof(DraggingBorderBrush), typeof(Brush), typeof(Thumb), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty DisabledBorderBrushProperty =
            DependencyProperty.Register(nameof(DisabledBorderBrush), typeof(Brush), typeof(Thumb), new FrameworkPropertyMetadata(null));
    }
}
