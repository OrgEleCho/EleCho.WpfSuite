using System.Windows;
using System.Windows.Media;
using EleCho.WpfSuite.Controls.SourceGeneration;
using EleCho.WpfSuite.Controls.States;

namespace EleCho.WpfSuite.Controls
{
    /// <summary>
    /// ScrollBar 类为自定义滚动条控件提供了可视化和样式支持。
    /// </summary>
    [GenerateStates]
    [GenerateStatesState(State.Hover)]
    [GenerateStatesState(State.Disabled)]
    [GenerateStatesProperty(StateProperty.Background)]
    [GenerateStatesProperty(StateProperty.BorderBrush)]
    [GenerateStatesProperty(StateProperty.BorderThickness)]
    [GenerateStatesProperty(StateProperty.CornerRadius)]
    [GenerateComponentStatesState("Thumb", State.Hover)]
    [GenerateComponentStatesState("Thumb", State.Dragging)]
    [GenerateComponentStatesState("Thumb", State.Disabled)]
    [GenerateComponentStateProperty("Thumb", StateProperty.Background)]
    [GenerateComponentStateProperty("Thumb", StateProperty.BorderBrush)]
    [GenerateComponentStateProperty("Thumb", StateProperty.BorderThickness)]
    [GenerateComponentStateProperty("Thumb", StateProperty.CornerRadius)]
    [GenerateComponentStatesState("Button", State.Highlighted)]
    [GenerateComponentStatesState("Button", State.Hover)]
    [GenerateComponentStatesState("Button", State.Pressed)]
    [GenerateComponentStatesState("Button", State.Disabled)]
    [GenerateComponentStateProperty("Button", StateProperty.Background)]
    [GenerateComponentStateProperty("Button", StateProperty.GlyphBrush)]
    [GenerateComponentStateProperty("Button", StateProperty.BorderBrush)]
    [GenerateComponentStateProperty("Button", StateProperty.Padding)]
    [GenerateComponentStateProperty("Button", StateProperty.BorderThickness)]
    [GenerateComponentStateProperty("Button", StateProperty.CornerRadius)]
    [GenerateCornerRadiusProperty]
    public partial class ScrollBar : System.Windows.Controls.Primitives.ScrollBar
    {
        private static readonly Brush s_thumbBrush =new SolidColorBrush(Color.FromRgb(205, 205, 205));
        private static readonly Brush s_glyphBrush= new SolidColorBrush(Color.FromRgb(96, 96, 96));
        private static readonly Brush s_disabledGlyphBrush =new SolidColorBrush(Color.FromRgb(112, 112, 112));
        private static readonly Geometry s_glyphLeft = Geometry.Parse("M 3.18,7 C3.18,7 5,7 5,7 5,7 1.81,3.5 1.81,3.5 1.81,3.5 5,0 5,0 5,0 3.18,0 3.18,0 3.18,0 0,3.5 0,3.5 0,3.5 3.18,7 3.18,7 z");
        private static readonly Geometry s_glyphRight = Geometry.Parse("M 1.81,7 C1.81,7 0,7 0,7 0,7 3.18,3.5 3.18,3.5 3.18,3.5 0,0 0,0 0,0 1.81,0 1.81,0 1.81,0 5,3.5 5,3.5 5,3.5 1.81,7 1.81,7 z");
        private static readonly Geometry s_glyphUp = Geometry.Parse("M 0,4 C0,4 0,6 0,6 0,6 3.5,2.5 3.5,2.5 3.5,2.5 7,6 7,6 7,6 7,4 7,4 7,4 3.5,0.5 3.5,0.5 3.5,0.5 0,4 0,4 z");
        private static readonly Geometry s_glyphDown = Geometry.Parse("M 0,2.5 C0,2.5 0,0.5 0,0.5 0,0.5 3.5,4 3.5,4 3.5,4 7,0.5 7,0.5 7,0.5 7,2.5 7,2.5 7,2.5 3.5,6 3.5,6 3.5,6 0,2.5 0,2.5 z");

        static ScrollBar()
        {
            s_thumbBrush.Freeze();
            s_glyphBrush.Freeze();
            s_disabledGlyphBrush.Freeze();

            s_glyphLeft.Freeze();
            s_glyphRight.Freeze();
            s_glyphUp.Freeze();
            s_glyphDown.Freeze();

            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScrollBar), new FrameworkPropertyMetadata(typeof(ScrollBar)));
        }

        /// <summary>
        /// 获取或设置左箭头的图形轮廓。
        /// </summary>
        public Geometry? ArrowLeftGlyph
        {
            get { return (Geometry?)GetValue(ArrowLeftGlyphProperty); }
            set { SetValue(ArrowLeftGlyphProperty, value); }
        }

        /// <summary>
        /// 获取或设置右箭头的图形轮廓。
        /// </summary>
        public Geometry? ArrowRightGlyph
        {
            get { return (Geometry?)GetValue(ArrowRightGlyphProperty); }
            set { SetValue(ArrowRightGlyphProperty, value); }
        }

        /// <summary>
        /// 获取或设置上箭头的图形轮廓。
        /// </summary>
        public Geometry? ArrowUpGlyph
        {
            get { return (Geometry?)GetValue(ArrowUpGlyphProperty); }
            set { SetValue(ArrowUpGlyphProperty, value); }
        }

        /// <summary>
        /// 获取或设置下箭头的图形轮廓。
        /// </summary>
        public Geometry? ArrowDownGlyph
        {
            get { return (Geometry?)GetValue(ArrowDownGlyphProperty); }
            set { SetValue(ArrowDownGlyphProperty, value); }
        }

        /// <summary>
        /// 获取或设置按钮的大小。
        /// </summary>
        public double ButtonSize
        {
            get { return (double)GetValue(ButtonSizeProperty); }
            set { SetValue(ButtonSizeProperty, value); }
        }

        /// <summary>
        /// 获取或设置按钮是否可见。
        /// </summary>
        public bool IsButtonVisible
        {
            get { return (bool)GetValue(IsButtonVisibleProperty); }
            set { SetValue(IsButtonVisibleProperty, value); }
        }





        /// <summary>
        /// 表示左箭头图形的依赖属性。
        /// </summary>
        public static readonly DependencyProperty ArrowLeftGlyphProperty =
            DependencyProperty.Register(nameof(ArrowLeftGlyph), typeof(Geometry), typeof(ScrollBar), new FrameworkPropertyMetadata(s_glyphLeft));

        /// <summary>
        /// 表示右箭头图形的依赖属性。
        /// </summary>
        public static readonly DependencyProperty ArrowRightGlyphProperty =
            DependencyProperty.Register(nameof(ArrowRightGlyph), typeof(Geometry), typeof(ScrollBar), new FrameworkPropertyMetadata(s_glyphRight));

        /// <summary>
        /// 表示上箭头图形的依赖属性。
        /// </summary>
        public static readonly DependencyProperty ArrowUpGlyphProperty =
            DependencyProperty.Register(nameof(ArrowUpGlyph), typeof(Geometry), typeof(ScrollBar), new FrameworkPropertyMetadata(s_glyphUp));

        /// <summary>
        /// 表示下箭头图形的依赖属性。
        /// </summary>
        public static readonly DependencyProperty ArrowDownGlyphProperty =
            DependencyProperty.Register(nameof(ArrowDownGlyph), typeof(Geometry), typeof(ScrollBar), new FrameworkPropertyMetadata(s_glyphDown));

        /// <summary>
        /// 表示按钮大小的依赖属性。
        /// </summary>
        public static readonly DependencyProperty ButtonSizeProperty =
            DependencyProperty.Register(nameof(ButtonSize), typeof(double), typeof(ScrollBar), new PropertyMetadata(10.0));

        /// <summary>
        /// 表示按钮可见性的依赖属性。
        /// </summary>
        public static readonly DependencyProperty IsButtonVisibleProperty =
            DependencyProperty.Register(nameof(IsButtonVisible), typeof(bool), typeof(ScrollBar), new FrameworkPropertyMetadata(true));
    }
}
