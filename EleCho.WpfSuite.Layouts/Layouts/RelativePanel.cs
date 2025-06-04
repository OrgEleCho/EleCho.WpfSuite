// Ported from https://learn.microsoft.com/en-us/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.relativepanel
// Code: 
//   - https://github.com/microsoft/microsoft-ui-xaml/blob/main/src/dxaml/xcp/components/relativepanel/inc/RPGraph.h
//   - https://github.com/microsoft/microsoft-ui-xaml/blob/main/src/dxaml/xcp/components/relativepanel/inc/RPNode.h
//   - https://github.com/microsoft/microsoft-ui-xaml/blob/main/src/dxaml/xcp/components/relativepanel/lib/RPGraph.cpp
//   - https://github.com/microsoft/microsoft-ui-xaml/blob/main/src/dxaml/xcp/components/relativepanel/lib/RPNode.cpp
//   - https://github.com/microsoft/microsoft-ui-xaml/blob/main/src/dxaml/xcp/core/inc/RelativePanel.h
//   - https://github.com/microsoft/microsoft-ui-xaml/blob/main/src/dxaml/xcp/core/core/elements/RelativePanel.cpp
// Origin Commit ID: https://github.com/microsoft/microsoft-ui-xaml/commit/66a7b0ae71c19f89c6a7d86a1986794ad1a1bf09

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace EleCho.WpfSuite.Layouts
{
    /// <summary>
    /// Defines an area within which you can position and align child objects in relation to each other or the parent panel.
    /// </summary>
    public partial class RelativePanel : Panel
    {
        private readonly Graph _graph = new();

        #region Panel alignment

        /// <summary>
        /// Identifies the AlignLeftWithPanel attached property
        /// </summary>
        public static readonly DependencyProperty AlignLeftWithPanelProperty = DependencyProperty.RegisterAttached(
            "AlignLeftWithPanel", typeof(bool), typeof(RelativePanel), new FrameworkPropertyMetadata(ValueBoxes.FalseBox, FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsParentArrange));

        /// <summary>
        /// Identifies the AlignTopWithPanel attached property
        /// </summary>
        public static readonly DependencyProperty AlignTopWithPanelProperty = DependencyProperty.RegisterAttached(
            "AlignTopWithPanel", typeof(bool), typeof(RelativePanel), new FrameworkPropertyMetadata(ValueBoxes.FalseBox, FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsParentArrange));

        /// <summary>
        /// Identifies the AlignRightWithPanel attached property
        /// </summary>
        public static readonly DependencyProperty AlignRightWithPanelProperty = DependencyProperty.RegisterAttached(
            "AlignRightWithPanel", typeof(bool), typeof(RelativePanel), new FrameworkPropertyMetadata(ValueBoxes.FalseBox, FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsParentArrange));

        /// <summary>
        /// Identifies the AlignBottomWithPanel attached property
        /// </summary>
        public static readonly DependencyProperty AlignBottomWithPanelProperty = DependencyProperty.RegisterAttached(
            "AlignBottomWithPanel", typeof(bool), typeof(RelativePanel), new FrameworkPropertyMetadata(ValueBoxes.FalseBox, FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsParentArrange));

        /// <summary>
        /// Set value of AlignLeftWithPanel attached property
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetAlignLeftWithPanel(DependencyObject element, bool value)
            => element.SetValue(AlignLeftWithPanelProperty, ValueBoxes.BooleanBox(value));

        /// <summary>
        /// Get value of AlignLeftWithPanel attached property
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool GetAlignLeftWithPanel(DependencyObject element)
            => (bool)element.GetValue(AlignLeftWithPanelProperty);

        /// <summary>
        /// Set value of AlignTopWithPanel attached property
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetAlignTopWithPanel(DependencyObject element, bool value)
            => element.SetValue(AlignTopWithPanelProperty, ValueBoxes.BooleanBox(value));

        /// <summary>
        /// Get value of AlignTopWithPanel attached property
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool GetAlignTopWithPanel(DependencyObject element)
            => (bool)element.GetValue(AlignTopWithPanelProperty);

        /// <summary>
        /// Set value of AlignRightWithPanel attached property
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetAlignRightWithPanel(DependencyObject element, bool value)
            => element.SetValue(AlignRightWithPanelProperty, ValueBoxes.BooleanBox(value));

        /// <summary>
        /// Get value of AlignRightWithPanel attached property
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool GetAlignRightWithPanel(DependencyObject element)
            => (bool)element.GetValue(AlignRightWithPanelProperty);

        /// <summary>
        /// Set value of AlignBottomWithPanel attached property
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetAlignBottomWithPanel(DependencyObject element, bool value)
            => element.SetValue(AlignBottomWithPanelProperty, ValueBoxes.BooleanBox(value));

        /// <summary>
        /// Get value of AlignBottomWithPanel attached property
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool GetAlignBottomWithPanel(DependencyObject element)
            => (bool)element.GetValue(AlignBottomWithPanelProperty);

        #endregion

        #region Sibling alignment

        /// <summary>
        /// Identifies the AlignLeftWith attached property
        /// </summary>
        public static readonly DependencyProperty AlignLeftWithProperty = DependencyProperty.RegisterAttached(
            "AlignLeftWith", typeof(UIElement), typeof(RelativePanel), new FrameworkPropertyMetadata(default(UIElement), FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsParentArrange));

        /// <summary>
        /// Identifies the AlignTopWith attached property
        /// </summary>
        public static readonly DependencyProperty AlignTopWithProperty = DependencyProperty.RegisterAttached(
            "AlignTopWith", typeof(UIElement), typeof(RelativePanel), new FrameworkPropertyMetadata(default(UIElement), FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsParentArrange));

        /// <summary>
        /// Identifies the AlignRightWith attached property
        /// </summary>
        public static readonly DependencyProperty AlignRightWithProperty = DependencyProperty.RegisterAttached(
            "AlignRightWith", typeof(UIElement), typeof(RelativePanel), new FrameworkPropertyMetadata(default(UIElement), FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsParentArrange));

        /// <summary>
        /// Identifies the AlignBottomWith attached property
        /// </summary>
        public static readonly DependencyProperty AlignBottomWithProperty = DependencyProperty.RegisterAttached(
            "AlignBottomWith", typeof(UIElement), typeof(RelativePanel), new FrameworkPropertyMetadata(default(UIElement), FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsParentArrange));

        /// <summary>
        /// Set value of AlignLeftWith attached property
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetAlignLeftWith(DependencyObject element, UIElement value)
            => element.SetValue(AlignLeftWithProperty, value);

        /// <summary>
        /// Get value of AlignLeftWith attached property
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        [TypeConverter(typeof(NameReferenceConverter))]
        public static UIElement GetAlignLeftWith(DependencyObject element)
            => (UIElement)element.GetValue(AlignLeftWithProperty);

        /// <summary>
        /// Set value of AlignTopWith attached property
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetAlignTopWith(DependencyObject element, UIElement value)
            => element.SetValue(AlignTopWithProperty, value);

        /// <summary>
        /// Get value of AlignTopWith attached property
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        [TypeConverter(typeof(NameReferenceConverter))]
        public static UIElement GetAlignTopWith(DependencyObject element)
            => (UIElement)element.GetValue(AlignTopWithProperty);

        /// <summary>
        /// Set value of AlignRightWith attached property
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetAlignRightWith(DependencyObject element, UIElement value)
            => element.SetValue(AlignRightWithProperty, value);

        /// <summary>
        /// Get value of AlignRightWith attached property
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        [TypeConverter(typeof(NameReferenceConverter))]
        public static UIElement GetAlignRightWith(DependencyObject element)
            => (UIElement)element.GetValue(AlignRightWithProperty);

        /// <summary>
        /// Set value of AlignBottomWith attached property
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetAlignBottomWith(DependencyObject element, UIElement value)
            => element.SetValue(AlignBottomWithProperty, value);

        /// <summary>
        /// Get value of AlignBottomWith attached property
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        [TypeConverter(typeof(NameReferenceConverter))]
        public static UIElement GetAlignBottomWith(DependencyObject element)
            => (UIElement)element.GetValue(AlignBottomWithProperty);

        #endregion

        #region Sibling positional

        /// <summary>
        /// Identifies the LeftOf attached property
        /// </summary>
        public static readonly DependencyProperty LeftOfProperty = DependencyProperty.RegisterAttached(
            "LeftOf", typeof(UIElement), typeof(RelativePanel), new FrameworkPropertyMetadata(default(UIElement), FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsParentArrange));

        /// <summary>
        /// Identifies the Above attached property
        /// </summary>
        public static readonly DependencyProperty AboveProperty = DependencyProperty.RegisterAttached(
            "Above", typeof(UIElement), typeof(RelativePanel), new FrameworkPropertyMetadata(default(UIElement), FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsParentArrange));

        /// <summary>
        /// Identifies the RightOf attached property
        /// </summary>
        public static readonly DependencyProperty RightOfProperty = DependencyProperty.RegisterAttached(
            "RightOf", typeof(UIElement), typeof(RelativePanel), new FrameworkPropertyMetadata(default(UIElement), FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsParentArrange));

        /// <summary>
        /// Identifies the Below attached property
        /// </summary>
        public static readonly DependencyProperty BelowProperty = DependencyProperty.RegisterAttached(
            "Below", typeof(UIElement), typeof(RelativePanel), new FrameworkPropertyMetadata(default(UIElement), FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsParentArrange));

        /// <summary>
        /// Set value of LeftOf attached property
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetLeftOf(DependencyObject element, UIElement value)
            => element.SetValue(LeftOfProperty, value);

        /// <summary>
        /// Get value of LeftOf attached property
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        [TypeConverter(typeof(NameReferenceConverter))]
        public static UIElement GetLeftOf(DependencyObject element)
            => (UIElement)element.GetValue(LeftOfProperty);

        /// <summary>
        /// Set value of Above attached property
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetAbove(DependencyObject element, UIElement value)
            => element.SetValue(AboveProperty, value);

        /// <summary>
        /// Get value of Above attached property
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        [TypeConverter(typeof(NameReferenceConverter))]
        public static UIElement GetAbove(DependencyObject element)
            => (UIElement)element.GetValue(AboveProperty);

        /// <summary>
        /// Set value of RightOf attached property
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetRightOf(DependencyObject element, UIElement value)
            => element.SetValue(RightOfProperty, value);

        /// <summary>
        /// Get value of RightOf attached property
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        [TypeConverter(typeof(NameReferenceConverter))]
        public static UIElement GetRightOf(DependencyObject element)
            => (UIElement)element.GetValue(RightOfProperty);

        /// <summary>
        /// Set value of Below attached property
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetBelow(DependencyObject element, UIElement value)
            => element.SetValue(BelowProperty, value);

        /// <summary>
        /// Get value of Below attached property
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        [TypeConverter(typeof(NameReferenceConverter))]
        public static UIElement GetBelow(DependencyObject element)
            => (UIElement)element.GetValue(BelowProperty);

        #endregion

        #region Center alignment

        /// <summary>
        /// Identifies the AlignHorizontalCenterWithPanel attached property
        /// </summary>
        public static readonly DependencyProperty AlignHorizontalCenterWithPanelProperty = DependencyProperty.RegisterAttached(
            "AlignHorizontalCenterWithPanel", typeof(bool), typeof(RelativePanel), new FrameworkPropertyMetadata(ValueBoxes.FalseBox, FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsParentArrange));

        /// <summary>
        /// Identifies the AlignVerticalCenterWithPanel attached property
        /// </summary>
        public static readonly DependencyProperty AlignVerticalCenterWithPanelProperty = DependencyProperty.RegisterAttached(
            "AlignVerticalCenterWithPanel", typeof(bool), typeof(RelativePanel), new FrameworkPropertyMetadata(ValueBoxes.FalseBox, FrameworkPropertyMetadataOptions.AffectsParentMeasure| FrameworkPropertyMetadataOptions.AffectsParentArrange));

        /// <summary>
        /// Identifies the AlignHorizontalCenterWith attached property
        /// </summary>
        public static readonly DependencyProperty AlignHorizontalCenterWithProperty = DependencyProperty.RegisterAttached(
            "AlignHorizontalCenterWith", typeof(UIElement), typeof(RelativePanel), new FrameworkPropertyMetadata(default(UIElement), FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsParentArrange));

        /// <summary>
        /// Identifies the AlignVerticalCenterWith attached property
        /// </summary>
        public static readonly DependencyProperty AlignVerticalCenterWithProperty = DependencyProperty.RegisterAttached(
            "AlignVerticalCenterWith", typeof(UIElement), typeof(RelativePanel), new FrameworkPropertyMetadata(default(UIElement), FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsParentArrange));

        /// <summary>
        /// Set value of AlignHorizontalCenterWithPanel attached property
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetAlignHorizontalCenterWithPanel(DependencyObject element, bool value)
            => element.SetValue(AlignHorizontalCenterWithPanelProperty, ValueBoxes.BooleanBox(value));

        /// <summary>
        /// Get value of AlignHorizontalCenterWithPanel attached property
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool GetAlignHorizontalCenterWithPanel(DependencyObject element)
            => (bool)element.GetValue(AlignHorizontalCenterWithPanelProperty);

        /// <summary>
        /// Set value of AlignVerticalCenterWithPanel attached property
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetAlignVerticalCenterWithPanel(DependencyObject element, bool value)
            => element.SetValue(AlignVerticalCenterWithPanelProperty, ValueBoxes.BooleanBox(value));

        /// <summary>
        /// Get value of AlignVerticalCenterWithPanel attached property
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool GetAlignVerticalCenterWithPanel(DependencyObject element)
            => (bool)element.GetValue(AlignVerticalCenterWithPanelProperty);

        /// <summary>
        /// Set value of AlignHorizontalCenterWith attached property
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetAlignHorizontalCenterWith(DependencyObject element, UIElement value)
            => element.SetValue(AlignHorizontalCenterWithProperty, value);

        /// <summary>
        /// Get value of AlignHorizontalCenterWith attached property
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        [TypeConverter(typeof(NameReferenceConverter))]
        public static UIElement GetAlignHorizontalCenterWith(DependencyObject element)
            => (UIElement)element.GetValue(AlignHorizontalCenterWithProperty);

        /// <summary>
        /// Set value of AlignVerticalCenterWith attached property
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetAlignVerticalCenterWith(DependencyObject element, UIElement value)
            => element.SetValue(AlignVerticalCenterWithProperty, value);

        /// <summary>
        /// Get value of AlignVerticalCenterWith attached property
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        [TypeConverter(typeof(NameReferenceConverter))]
        public static UIElement GetAlignVerticalCenterWith(DependencyObject element)
            => (UIElement)element.GetValue(AlignVerticalCenterWithProperty);

        #endregion


        /// <inheritdoc/>
        protected override Size MeasureOverride(Size availableSize)
        {
            GenerateGraph();

            _graph.MeasureNodes(availableSize);

            var desiredSizeOfChildren = _graph.CalculateDesiredSize();

            return desiredSizeOfChildren;
        }

        /// <inheritdoc/>
        protected override Size ArrangeOverride(Size finalSize)
        {
            _graph.ArrangeNodes(new Rect(0, 0, finalSize.Width, finalSize.Height));

            return finalSize;
        }

        private void GenerateGraph()
        {
            _graph.Nodes.Clear();

            foreach (UIElement child in InternalChildren)
            {
                _graph.Nodes.Add(new GraphNode(child));
            }

            _graph.ResolveConstraints();
        }
    }
}
