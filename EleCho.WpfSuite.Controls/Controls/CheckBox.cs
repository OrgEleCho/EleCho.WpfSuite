using System.Windows;
using System.Windows.Media;
using EleCho.WpfSuite.Controls.SourceGeneration;
using EleCho.WpfSuite.Controls.States;

namespace EleCho.WpfSuite.Controls
{
    /// <summary>
    /// Represents a control that a user can select and clear.
    /// </summary>

    [GenerateStates]
    [GenerateStatesState(State.Hover)]
    [GenerateStatesState(State.Pressed)]
    [GenerateStatesState(State.Disabled)]
    [GenerateStatesProperty(StateProperty.Background)]
    [GenerateStatesProperty(StateProperty.Foreground)]
    [GenerateStatesProperty(StateProperty.BorderBrush)]
    [GenerateStatesProperty(StateProperty.GlyphBrush)]
    [GenerateStatesProperty(StateProperty.Padding)]
    [GenerateStatesProperty(StateProperty.CheckerPadding)]
    [GenerateStatesProperty(StateProperty.BorderThickness)]
    [GenerateStatesProperty(StateProperty.CornerRadius)]
    [GenerateCornerRadiusProperty]
    public partial class CheckBox : System.Windows.Controls.CheckBox
    {
        private static readonly Geometry s_glyph = Geometry.Parse("F1 M 9.97498,1.22334L 4.6983,9.09834L 4.52164,9.09834L 0,5.19331L 1.27664,3.52165L 4.255,6.08833L 8.33331,1.52588e-005L 9.97498,1.22334 Z");

        static CheckBox()
        {
            s_glyph.Freeze();

            HorizontalContentAlignmentProperty.OverrideMetadata(typeof(CheckBox), new FrameworkPropertyMetadata(HorizontalAlignment.Left));
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CheckBox), new FrameworkPropertyMetadata(typeof(CheckBox)));
        }


        public Geometry? Glyph
        {
            get { return (Geometry?)GetValue(GlyphProperty); }
            set { SetValue(GlyphProperty, value); }
        }

        public double Spacing
        {
            get { return (double)GetValue(SpacingProperty); }
            set { SetValue(SpacingProperty, value); }
        }

        public HorizontalAlignment HorizontalCheckerAlignment
        {
            get { return (HorizontalAlignment)GetValue(HorizontalCheckerAlignmentProperty); }
            set { SetValue(HorizontalCheckerAlignmentProperty, value); }
        }

        public VerticalAlignment VerticalCheckerAlignment
        {
            get { return (VerticalAlignment)GetValue(VerticalCheckerAlignmentProperty); }
            set { SetValue(VerticalCheckerAlignmentProperty, value); }
        }




        public static readonly DependencyProperty GlyphProperty =
            DependencyProperty.Register(nameof(Glyph), typeof(Geometry), typeof(CheckBox), new FrameworkPropertyMetadata(s_glyph));

        public static readonly DependencyProperty SpacingProperty =
            DependencyProperty.Register(nameof(Spacing), typeof(double), typeof(CheckBox), new FrameworkPropertyMetadata(2.0));

        public static readonly DependencyProperty HorizontalCheckerAlignmentProperty =
            DependencyProperty.Register(nameof(HorizontalCheckerAlignment), typeof(HorizontalAlignment), typeof(CheckBox), new FrameworkPropertyMetadata(HorizontalAlignment.Center));

        public static readonly DependencyProperty VerticalCheckerAlignmentProperty =
            DependencyProperty.Register(nameof(VerticalCheckerAlignment), typeof(VerticalAlignment), typeof(CheckBox), new FrameworkPropertyMetadata(VerticalAlignment.Center));
    }
}
