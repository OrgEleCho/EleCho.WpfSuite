using System.Windows;
using System.Windows.Media;

namespace EleCho.WpfSuite.Controls
{
    public class TabItem : System.Windows.Controls.TabItem
    {
        static TabItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TabItem), new FrameworkPropertyMetadata(typeof(TabItem)));
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
        public Brush HoverForeground
        {
            get { return (Brush)GetValue(HoverForegroundProperty); }
            set { SetValue(HoverForegroundProperty, value); }
        }

        public Brush HoverBackground
        {
            get { return (Brush)GetValue(HoverBackgroundProperty); }
            set { SetValue(HoverBackgroundProperty, value); }
        }

        public Brush HoverBorderBrush
        {
            get { return (Brush)GetValue(HoverBorderBrushProperty); }
            set { SetValue(HoverBorderBrushProperty, value); }
        }

        public Brush SelectedForeground
        {
            get { return (Brush)GetValue(SelectedForegroundProperty); }
            set { SetValue(SelectedForegroundProperty, value); }
        }

        public Brush SelectedBackground
        {
            get { return (Brush)GetValue(SelectedBackgroundProperty); }
            set { SetValue(SelectedBackgroundProperty, value); }
        }

        public Brush SelectedBorderBrush
        {
            get { return (Brush)GetValue(SelectedBorderBrushProperty); }
            set { SetValue(SelectedBorderBrushProperty, value); }
        }

        public Brush DisabledForeground
        {
            get { return (Brush)GetValue(DisabledForegroundProperty); }
            set { SetValue(DisabledForegroundProperty, value); }
        }

        public Brush DisabledBackground
        {
            get { return (Brush)GetValue(DisabledBackgroundProperty); }
            set { SetValue(DisabledBackgroundProperty, value); }
        }

        public Brush DisabledBorderBrush
        {
            get { return (Brush)GetValue(DisabledBorderBrushProperty); }
            set { SetValue(DisabledBorderBrushProperty, value); }
        }

        public double DisabledOpacity
        {
            get { return (double)GetValue(DisabledOpacityProperty); }
            set { SetValue(DisabledOpacityProperty, value); }
        }


        public static readonly DependencyProperty CornerRadiusProperty =
            Border.CornerRadiusProperty.AddOwner(typeof(TabItem));


        public static readonly DependencyProperty HoverForegroundProperty =
            DependencyProperty.Register(nameof(HoverForeground), typeof(Brush), typeof(TabItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty HoverBackgroundProperty =
            DependencyProperty.Register(nameof(HoverBackground), typeof(Brush), typeof(TabItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty HoverBorderBrushProperty =
            DependencyProperty.Register(nameof(HoverBorderBrush), typeof(Brush), typeof(TabItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty SelectedForegroundProperty =
            DependencyProperty.Register(nameof(SelectedForeground), typeof(Brush), typeof(TabItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty SelectedBackgroundProperty =
            DependencyProperty.Register(nameof(SelectedBackground), typeof(Brush), typeof(TabItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty SelectedBorderBrushProperty =
            DependencyProperty.Register(nameof(SelectedBorderBrush), typeof(Brush), typeof(TabItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty DisabledForegroundProperty =
            DependencyProperty.Register(nameof(DisabledForeground), typeof(Brush), typeof(TabItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty DisabledBackgroundProperty =
            DependencyProperty.Register(nameof(DisabledBackground), typeof(Brush), typeof(TabItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty DisabledBorderBrushProperty =
            DependencyProperty.Register(nameof(DisabledBorderBrush), typeof(Brush), typeof(TabItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty DisabledOpacityProperty =
            DependencyProperty.Register(nameof(DisabledOpacity), typeof(double), typeof(TabItem), new PropertyMetadata(0.56));
    }
}
