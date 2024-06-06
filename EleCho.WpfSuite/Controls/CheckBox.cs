using System.Windows;
using System.Windows.Media;

namespace EleCho.WpfSuite
{
    /// <summary>
    /// Represents a control that a user can select and clear.
    /// </summary>
    public class CheckBox : System.Windows.Controls.CheckBox
    {
        private static readonly Brush s_glyphBrush = new SolidColorBrush(Color.FromRgb(33, 33, 33));
        private static readonly Brush s_disabledGlyphBrush = new SolidColorBrush(Color.FromRgb(112, 112, 112));
        private static readonly Geometry s_glyph = Geometry.Parse("F1 M 9.97498,1.22334L 4.6983,9.09834L 4.52164,9.09834L 0,5.19331L 1.27664,3.52165L 4.255,6.08833L 8.33331,1.52588e-005L 9.97498,1.22334 Z");

        static CheckBox()
        {
            s_glyphBrush.Freeze();
            s_disabledGlyphBrush.Freeze();
            s_glyph.Freeze();

            DefaultStyleKeyProperty.OverrideMetadata(typeof(CheckBox), new FrameworkPropertyMetadata(typeof(CheckBox)));
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


        public Geometry? Glyph
        {
            get { return (Geometry?)GetValue(GlyphProperty); }
            set { SetValue(GlyphProperty, value); }
        }

        public Brush GlyphBrush
        {
            get { return (Brush)GetValue(GlyphBrushProperty); }
            set { SetValue(GlyphBrushProperty, value); }
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

        public Brush HoverGlyphBrush
        {
            get { return (Brush)GetValue(HoverGlyphBrushProperty); }
            set { SetValue(HoverGlyphBrushProperty, value); }
        }

        public Brush PressedForeground
        {
            get { return (Brush)GetValue(PressedForegroundProperty); }
            set { SetValue(PressedForegroundProperty, value); }
        }

        public Brush PressedBackground
        {
            get { return (Brush)GetValue(PressedBackgroundProperty); }
            set { SetValue(PressedBackgroundProperty, value); }
        }

        public Brush PressedBorderBrush
        {
            get { return (Brush)GetValue(PressedBorderBrushProperty); }
            set { SetValue(PressedBorderBrushProperty, value); }
        }

        public Brush PressedGlyphBrush
        {
            get { return (Brush)GetValue(PressedGlyphBrushProperty); }
            set { SetValue(PressedGlyphBrushProperty, value); }
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

        public Brush DisabledGlyphBrush
        {
            get { return (Brush)GetValue(DisabledGlyphBrushProperty); }
            set { SetValue(DisabledGlyphBrushProperty, value); }
        }




        public static readonly DependencyProperty CornerRadiusProperty =
            Border.CornerRadiusProperty.AddOwner(typeof(CheckBox));

        public static readonly DependencyProperty GlyphProperty =
            DependencyProperty.Register(nameof(Glyph), typeof(Geometry), typeof(CheckBox), new FrameworkPropertyMetadata(s_glyph));

        public static readonly DependencyProperty GlyphBrushProperty =
            DependencyProperty.Register(nameof(GlyphBrush), typeof(Brush), typeof(CheckBox), new FrameworkPropertyMetadata(s_glyphBrush));

        public static readonly DependencyProperty HoverForegroundProperty =
            DependencyProperty.Register(nameof(HoverForeground), typeof(Brush), typeof(CheckBox), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty HoverBackgroundProperty =
            DependencyProperty.Register(nameof(HoverBackground), typeof(Brush), typeof(CheckBox), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty HoverBorderBrushProperty =
            DependencyProperty.Register(nameof(HoverBorderBrush), typeof(Brush), typeof(CheckBox), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty HoverGlyphBrushProperty =
            DependencyProperty.Register(nameof(HoverGlyphBrush), typeof(Brush), typeof(CheckBox), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty PressedForegroundProperty =
            DependencyProperty.Register(nameof(PressedForeground), typeof(Brush), typeof(CheckBox), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty PressedBackgroundProperty =
            DependencyProperty.Register(nameof(PressedBackground), typeof(Brush), typeof(CheckBox), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty PressedBorderBrushProperty =
            DependencyProperty.Register(nameof(PressedBorderBrush), typeof(Brush), typeof(CheckBox), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty PressedGlyphBrushProperty =
            DependencyProperty.Register(nameof(PressedGlyphBrush), typeof(Brush), typeof(CheckBox), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty DisabledForegroundProperty =
            DependencyProperty.Register(nameof(DisabledForeground), typeof(Brush), typeof(CheckBox), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty DisabledBackgroundProperty =
            DependencyProperty.Register(nameof(DisabledBackground), typeof(Brush), typeof(CheckBox), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty DisabledBorderBrushProperty =
            DependencyProperty.Register(nameof(DisabledBorderBrush), typeof(Brush), typeof(CheckBox), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty DisabledGlyphBrushProperty =
            DependencyProperty.Register(nameof(DisabledGlyphBrush), typeof(Brush), typeof(CheckBox), new FrameworkPropertyMetadata(s_disabledGlyphBrush));
    }
}
