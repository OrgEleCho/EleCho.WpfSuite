using System.Windows;
using System.Windows.Media;

namespace EleCho.WpfSuite.Controls
{
    public class ComboBoxItem : System.Windows.Controls.ComboBoxItem
    {
        static ComboBoxItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ComboBoxItem), new FrameworkPropertyMetadata(typeof(ComboBoxItem)));
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

        public Brush FocusedForeground
        {
            get { return (Brush)GetValue(FocusedForegroundProperty); }
            set { SetValue(FocusedForegroundProperty, value); }
        }

        public Brush FocusedBackground
        {
            get { return (Brush)GetValue(FocusedBackgroundProperty); }
            set { SetValue(FocusedBackgroundProperty, value); }
        }

        public Brush FocusedBorderBrush
        {
            get { return (Brush)GetValue(FocusedBorderBrushProperty); }
            set { SetValue(FocusedBorderBrushProperty, value); }
        }

        public Brush HoverSelectedForeground
        {
            get { return (Brush)GetValue(HoverSelectedForegroundProperty); }
            set { SetValue(HoverSelectedForegroundProperty, value); }
        }

        public Brush HoverSelectedBackground
        {
            get { return (Brush)GetValue(HoverSelectedBackgroundProperty); }
            set { SetValue(HoverSelectedBackgroundProperty, value); }
        }

        public Brush HoverSelectedBorderBrush
        {
            get { return (Brush)GetValue(HoverSelectedBorderBrushProperty); }
            set { SetValue(HoverSelectedBorderBrushProperty, value); }
        }

        public Brush SelectedFocusedForeground
        {
            get { return (Brush)GetValue(SelectedFocusedForegroundProperty); }
            set { SetValue(SelectedFocusedForegroundProperty, value); }
        }

        public Brush SelectedFocusedBackground
        {
            get { return (Brush)GetValue(SelectedFocusedBackgroundProperty); }
            set { SetValue(SelectedFocusedBackgroundProperty, value); }
        }

        public Brush SelectedFocusedBorderBrush
        {
            get { return (Brush)GetValue(SelectedFocusedBorderBrushProperty); }
            set { SetValue(SelectedFocusedBorderBrushProperty, value); }
        }

        public Brush HoverFocusedForeground
        {
            get { return (Brush)GetValue(HoverFocusedForegroundProperty); }
            set { SetValue(HoverFocusedForegroundProperty, value); }
        }

        public Brush HoverFocusedBackground
        {
            get { return (Brush)GetValue(HoverFocusedBackgroundProperty); }
            set { SetValue(HoverFocusedBackgroundProperty, value); }
        }

        public Brush HoverFocusedBorderBrush
        {
            get { return (Brush)GetValue(HoverFocusedBorderBrushProperty); }
            set { SetValue(HoverFocusedBorderBrushProperty, value); }
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


        public static readonly DependencyProperty CornerRadiusProperty =
            Border.CornerRadiusProperty.AddOwner(typeof(ComboBoxItem));


        public static readonly DependencyProperty HoverForegroundProperty =
            DependencyProperty.Register(nameof(HoverForeground), typeof(Brush), typeof(ComboBoxItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty HoverBackgroundProperty =
            DependencyProperty.Register(nameof(HoverBackground), typeof(Brush), typeof(ComboBoxItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty HoverBorderBrushProperty =
            DependencyProperty.Register(nameof(HoverBorderBrush), typeof(Brush), typeof(ComboBoxItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty SelectedForegroundProperty =
            DependencyProperty.Register(nameof(SelectedForeground), typeof(Brush), typeof(ComboBoxItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty SelectedBackgroundProperty =
            DependencyProperty.Register(nameof(SelectedBackground), typeof(Brush), typeof(ComboBoxItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty SelectedBorderBrushProperty =
            DependencyProperty.Register(nameof(SelectedBorderBrush), typeof(Brush), typeof(ComboBoxItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty FocusedForegroundProperty =
            DependencyProperty.Register(nameof(FocusedForeground), typeof(Brush), typeof(ComboBoxItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty FocusedBackgroundProperty =
            DependencyProperty.Register(nameof(FocusedBackground), typeof(Brush), typeof(ComboBoxItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty FocusedBorderBrushProperty =
            DependencyProperty.Register(nameof(FocusedBorderBrush), typeof(Brush), typeof(ComboBoxItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty HoverSelectedForegroundProperty =
            DependencyProperty.Register(nameof(HoverSelectedForeground), typeof(Brush), typeof(ComboBoxItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty HoverSelectedBackgroundProperty =
            DependencyProperty.Register(nameof(HoverSelectedBackground), typeof(Brush), typeof(ComboBoxItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty HoverSelectedBorderBrushProperty =
            DependencyProperty.Register(nameof(HoverSelectedBorderBrush), typeof(Brush), typeof(ComboBoxItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty SelectedFocusedForegroundProperty =
            DependencyProperty.Register(nameof(SelectedFocusedForeground), typeof(Brush), typeof(ComboBoxItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty SelectedFocusedBackgroundProperty =
            DependencyProperty.Register(nameof(SelectedFocusedBackground), typeof(Brush), typeof(ComboBoxItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty SelectedFocusedBorderBrushProperty =
            DependencyProperty.Register(nameof(SelectedFocusedBorderBrush), typeof(Brush), typeof(ComboBoxItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty HoverFocusedForegroundProperty =
            DependencyProperty.Register(nameof(HoverFocusedForeground), typeof(Brush), typeof(ComboBoxItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty HoverFocusedBackgroundProperty =
            DependencyProperty.Register(nameof(HoverFocusedBackground), typeof(Brush), typeof(ComboBoxItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty HoverFocusedBorderBrushProperty =
            DependencyProperty.Register(nameof(HoverFocusedBorderBrush), typeof(Brush), typeof(ComboBoxItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty DisabledForegroundProperty =
            DependencyProperty.Register(nameof(DisabledForeground), typeof(Brush), typeof(ComboBoxItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty DisabledBackgroundProperty =
            DependencyProperty.Register(nameof(DisabledBackground), typeof(Brush), typeof(ComboBoxItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty DisabledBorderBrushProperty =
            DependencyProperty.Register(nameof(DisabledBorderBrush), typeof(Brush), typeof(ComboBoxItem), new FrameworkPropertyMetadata(null));
    }
}
