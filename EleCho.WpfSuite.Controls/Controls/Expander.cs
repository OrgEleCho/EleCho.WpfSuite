using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using EleCho.WpfSuite.Controls.SourceGeneration;
using EleCho.WpfSuite.Controls.States;

namespace EleCho.WpfSuite.Controls
{
    /// <inheritdoc/>
    /// <summary>
    /// Expander 是一个可展开和折叠的控件，包含头部和内容部分。
    /// </summary>
    [GenerateStates]
    [GenerateComponentStatesState("Header", State.Hover)]
    [GenerateComponentStatesState("Header", State.Pressed)]
    [GenerateComponentStatesState("Header", State.Checked)]
    [GenerateComponentStatesState("Header", State.Disabled)]
    [GenerateComponentStateProperty("Header", StateProperty.Background)]
    [GenerateComponentStateProperty("Header", StateProperty.Foreground)]
    [GenerateComponentStateProperty("Header", StateProperty.BorderBrush)]
    [GenerateComponentStateProperty("Header", StateProperty.Padding)]
    [GenerateComponentStateProperty("Header", StateProperty.BorderThickness)]
    [GenerateComponentStateProperty("Header", StateProperty.CornerRadius)]
    [GenerateComponentStatesState("HeaderCircle", State.Hover)]
    [GenerateComponentStatesState("HeaderCircle", State.Pressed)]
    [GenerateComponentStatesState("HeaderCircle", State.Checked)]
    [GenerateComponentStatesState("HeaderCircle", State.Disabled)]
    [GenerateComponentStateProperty("HeaderCircle", StateProperty.Stroke)]
    [GenerateComponentStateProperty("HeaderCircle", StateProperty.Fill)]
    [GenerateComponentStatesState("HeaderArrow", State.Hover)]
    [GenerateComponentStatesState("HeaderArrow", State.Pressed)]
    [GenerateComponentStatesState("HeaderArrow", State.Checked)]
    [GenerateComponentStatesState("HeaderArrow", State.Disabled)]
    [GenerateComponentStateProperty("HeaderArrow", StateProperty.Stroke)]
    [GenerateCornerRadiusProperty]
    public partial class Expander : System.Windows.Controls.Expander
    {
        static Expander()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Expander), new FrameworkPropertyMetadata(typeof(Expander)));
        }



        /// <summary>
        /// 获取或设置头部内容与 Expander 边缘之间的间距。
        /// </summary>
        public double HeaderSpacing
        {
            get { return (double)GetValue(HeaderSpacingProperty); }
            set { SetValue(HeaderSpacingProperty, value); }
        }

        /// <summary>
        /// 获取或设置头部内容的边距。
        /// </summary>
        public Thickness HeaderMargin
        {
            get { return (Thickness)GetValue(HeaderMarginProperty); }
            set { SetValue(HeaderMarginProperty, value); }
        }

        /// <summary>
        /// 获取或设置头部图标相对于文本的位置。
        /// </summary>
        public ExpanderHeaderIconPosition HeaderIconPosition
        {
            get { return (ExpanderHeaderIconPosition)GetValue(HeaderIconPositionProperty); }
            set { SetValue(HeaderIconPositionProperty, value); }
        }

        /// <summary>
        /// 获取或设置头部圆圈的直径，仅在头部样式中未设置圆角时有效。
        /// </summary>
        public double HeaderCircleDiameter
        {
            get { return (double)GetValue(HeaderCircleDiameterProperty); }
            set { SetValue(HeaderCircleDiameterProperty, value); }
        }

        /// <summary>
        /// 获取或设置头部箭头的形状。
        /// </summary>
        public Geometry HeaderArrowGeometry
        {
            get { return (Geometry)GetValue(HeaderArrowGeometryProperty); }
            set { SetValue(HeaderArrowGeometryProperty, value); }
        }

        /// <summary>
        /// 获取或设置头部展开时箭头的形状。
        /// </summary>
        public Geometry HeaderArrowExpandedGeometry
        {
            get { return (Geometry)GetValue(HeaderArrowExpandedGeometryProperty); }
            set { SetValue(HeaderArrowExpandedGeometryProperty, value); }
        }

        /// <summary>
        /// 获取或设置头部内容的垂直对齐方式。
        /// </summary>
        public VerticalAlignment HeaderVerticalAlignment
        {
            get { return (VerticalAlignment)GetValue(HeaderVerticalAlignmentProperty); }
            set { SetValue(HeaderVerticalAlignmentProperty, value); }
        }

        /// <summary>
        /// 获取或设置头部内容的水平对齐方式。
        /// </summary>
        public HorizontalAlignment HeaderHorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(HeaderHorizontalAlignmentProperty); }
            set { SetValue(HeaderHorizontalAlignmentProperty, value); }
        }



        /// <summary>
        /// 标识 <see cref="HeaderSpacing"/> 属性。
        /// </summary>
        public static readonly DependencyProperty HeaderSpacingProperty =
            DependencyProperty.Register(nameof(HeaderSpacing), typeof(double), typeof(Expander), new PropertyMetadata(4.0));

        /// <summary>
        /// 标识 <see cref="HeaderMargin"/> 属性。
        /// </summary>
        public static readonly DependencyProperty HeaderMarginProperty =
            DependencyProperty.Register(nameof(HeaderMargin), typeof(Thickness), typeof(Expander), new PropertyMetadata(default(Thickness)));

        /// <summary>
        /// 标识 <see cref="HeaderIconPosition"/> 属性。
        /// </summary>
        public static readonly DependencyProperty HeaderIconPositionProperty =
            DependencyProperty.Register(nameof(HeaderIconPosition), typeof(ExpanderHeaderIconPosition), typeof(Expander), new PropertyMetadata(ExpanderHeaderIconPosition.Start));

        /// <summary>
        /// 标识 <see cref="HeaderCircleDiameter"/> 属性。
        /// </summary>
        public static readonly DependencyProperty HeaderCircleDiameterProperty =
            DependencyProperty.Register(nameof(HeaderCircleDiameter), typeof(double), typeof(Expander), new FrameworkPropertyMetadata(0.0));

        /// <summary>
        /// 标识 <see cref="HeaderArrowGeometry"/> 属性。
        /// </summary>
        public static readonly DependencyProperty HeaderArrowGeometryProperty =
            DependencyProperty.Register(nameof(HeaderArrowGeometry), typeof(Geometry), typeof(Expander), new FrameworkPropertyMetadata(null));

        /// <summary>
        /// 标识 <see cref="HeaderArrowExpandedGeometry"/> 属性。
        /// </summary>
        public static readonly DependencyProperty HeaderArrowExpandedGeometryProperty =
            DependencyProperty.Register(nameof(HeaderArrowExpandedGeometry), typeof(Geometry), typeof(Expander), new PropertyMetadata(null));

        /// <summary>
        /// 标识 <see cref="HeaderVerticalAlignment"/> 属性。
        /// </summary>
        public static readonly DependencyProperty HeaderVerticalAlignmentProperty =
            DependencyProperty.Register(nameof(HeaderVerticalAlignment), typeof(VerticalAlignment), typeof(Expander), new PropertyMetadata(VerticalAlignment.Stretch));

        /// <summary>
        /// 标识 <see cref="HeaderHorizontalAlignment"/> 属性。
        /// </summary>
        public static readonly DependencyProperty HeaderHorizontalAlignmentProperty =
            DependencyProperty.Register(nameof(HeaderHorizontalAlignment), typeof(HorizontalAlignment), typeof(Expander), new PropertyMetadata(HorizontalAlignment.Stretch));


    }
}
