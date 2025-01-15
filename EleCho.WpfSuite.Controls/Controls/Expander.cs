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



        public double HeaderSpacing
        {
            get { return (double)GetValue(HeaderSpacingProperty); }
            set { SetValue(HeaderSpacingProperty, value); }
        }

        public Thickness HeaderMargin
        {
            get { return (Thickness)GetValue(HeaderMarginProperty); }
            set { SetValue(HeaderMarginProperty, value); }
        }

        public ExpanderHeaderIconPosition HeaderIconPosition
        {
            get { return (ExpanderHeaderIconPosition)GetValue(HeaderIconPositionProperty); }
            set { SetValue(HeaderIconPositionProperty, value); }
        }

        public double HeaderCircleDiameter
        {
            get { return (double)GetValue(HeaderCircleDiameterProperty); }
            set { SetValue(HeaderCircleDiameterProperty, value); }
        }

        public Geometry HeaderArrowGeometry
        {
            get { return (Geometry)GetValue(HeaderArrowGeometryProperty); }
            set { SetValue(HeaderArrowGeometryProperty, value); }
        }

        public Geometry HeaderArrowExpandedGeometry
        {
            get { return (Geometry)GetValue(HeaderArrowExpandedGeometryProperty); }
            set { SetValue(HeaderArrowExpandedGeometryProperty, value); }
        }

        public VerticalAlignment HeaderVerticalAlignment
        {
            get { return (VerticalAlignment)GetValue(HeaderVerticalAlignmentProperty); }
            set { SetValue(HeaderVerticalAlignmentProperty, value); }
        }

        public HorizontalAlignment HeaderHorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(HeaderHorizontalAlignmentProperty); }
            set { SetValue(HeaderHorizontalAlignmentProperty, value); }
        }



        public static readonly DependencyProperty HeaderSpacingProperty =
            DependencyProperty.Register(nameof(HeaderSpacing), typeof(double), typeof(Expander), new PropertyMetadata(4.0));

        public static readonly DependencyProperty HeaderMarginProperty =
            DependencyProperty.Register(nameof(HeaderMargin), typeof(Thickness), typeof(Expander), new PropertyMetadata(default(Thickness)));

        public static readonly DependencyProperty HeaderIconPositionProperty =
            DependencyProperty.Register(nameof(HeaderIconPosition), typeof(ExpanderHeaderIconPosition), typeof(Expander), new PropertyMetadata(ExpanderHeaderIconPosition.Start));

        public static readonly DependencyProperty HeaderCircleDiameterProperty =
            DependencyProperty.Register(nameof(HeaderCircleDiameter), typeof(double), typeof(Expander), new FrameworkPropertyMetadata(0.0));

        public static readonly DependencyProperty HeaderArrowGeometryProperty =
            DependencyProperty.Register(nameof(HeaderArrowGeometry), typeof(Geometry), typeof(Expander), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty HeaderArrowExpandedGeometryProperty =
            DependencyProperty.Register(nameof(HeaderArrowExpandedGeometry), typeof(Geometry), typeof(Expander), new PropertyMetadata(null));

        public static readonly DependencyProperty HeaderVerticalAlignmentProperty =
            DependencyProperty.Register(nameof(HeaderVerticalAlignment), typeof(VerticalAlignment), typeof(Expander), new PropertyMetadata(VerticalAlignment.Stretch));

        public static readonly DependencyProperty HeaderHorizontalAlignmentProperty =
            DependencyProperty.Register(nameof(HeaderHorizontalAlignment), typeof(HorizontalAlignment), typeof(Expander), new PropertyMetadata(HorizontalAlignment.Stretch));


    }
}
