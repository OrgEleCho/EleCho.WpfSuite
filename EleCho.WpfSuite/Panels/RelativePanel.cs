using System.Collections.Generic;
using System.Windows;
using System;
using System.Windows.Controls;
using System.ComponentModel;
using System.Linq;
using System.Windows.Markup;

namespace EleCho.WpfSuite
{
    /// <summary>
    /// Defines an area within which you can position and align child objects in relation to each other or the parent panel.
    /// </summary>
    public class RelativePanel : Panel
    {
        private readonly Dictionary<UIElement, Rect> _childLayouts = new();
        private readonly HashSet<UIElement> _layoutQueue = new();

        #region Panel alignment

        /// <summary>
        /// Identifies the AlignLeftWithPanel attached property
        /// </summary>
        public static readonly DependencyProperty AlignLeftWithPanelProperty = DependencyProperty.RegisterAttached(
            "AlignLeftWithPanel", typeof(bool), typeof(RelativePanel), new FrameworkPropertyMetadata(ValueBoxes.FalseBox, FrameworkPropertyMetadataOptions.AffectsArrange));

        /// <summary>
        /// Identifies the AlignTopWithPanel attached property
        /// </summary>
        public static readonly DependencyProperty AlignTopWithPanelProperty = DependencyProperty.RegisterAttached(
            "AlignTopWithPanel", typeof(bool), typeof(RelativePanel), new FrameworkPropertyMetadata(ValueBoxes.FalseBox, FrameworkPropertyMetadataOptions.AffectsArrange));

        /// <summary>
        /// Identifies the AlignRightWithPanel attached property
        /// </summary>
        public static readonly DependencyProperty AlignRightWithPanelProperty = DependencyProperty.RegisterAttached(
            "AlignRightWithPanel", typeof(bool), typeof(RelativePanel), new FrameworkPropertyMetadata(ValueBoxes.FalseBox, FrameworkPropertyMetadataOptions.AffectsArrange));

        /// <summary>
        /// Identifies the AlignBottomWithPanel attached property
        /// </summary>
        public static readonly DependencyProperty AlignBottomWithPanelProperty = DependencyProperty.RegisterAttached(
            "AlignBottomWithPanel", typeof(bool), typeof(RelativePanel), new FrameworkPropertyMetadata(ValueBoxes.FalseBox, FrameworkPropertyMetadataOptions.AffectsArrange));

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
            "AlignLeftWith", typeof(UIElement), typeof(RelativePanel), new FrameworkPropertyMetadata(default(UIElement), FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Identifies the AlignTopWith attached property
        /// </summary>
        public static readonly DependencyProperty AlignTopWithProperty = DependencyProperty.RegisterAttached(
            "AlignTopWith", typeof(UIElement), typeof(RelativePanel), new FrameworkPropertyMetadata(default(UIElement), FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Identifies the AlignRightWith attached property
        /// </summary>
        public static readonly DependencyProperty AlignRightWithProperty = DependencyProperty.RegisterAttached(
            "AlignRightWith", typeof(UIElement), typeof(RelativePanel), new FrameworkPropertyMetadata(default(UIElement), FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Identifies the AlignBottomWith attached property
        /// </summary>
        public static readonly DependencyProperty AlignBottomWithProperty = DependencyProperty.RegisterAttached(
            "AlignBottomWith", typeof(UIElement), typeof(RelativePanel), new FrameworkPropertyMetadata(default(UIElement), FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsMeasure));

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
            "LeftOf", typeof(UIElement), typeof(RelativePanel), new FrameworkPropertyMetadata(default(UIElement), FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Identifies the Above attached property
        /// </summary>
        public static readonly DependencyProperty AboveProperty = DependencyProperty.RegisterAttached(
            "Above", typeof(UIElement), typeof(RelativePanel), new FrameworkPropertyMetadata(default(UIElement), FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Identifies the RightOf attached property
        /// </summary>
        public static readonly DependencyProperty RightOfProperty = DependencyProperty.RegisterAttached(
            "RightOf", typeof(UIElement), typeof(RelativePanel), new FrameworkPropertyMetadata(default(UIElement), FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Identifies the Below attached property
        /// </summary>
        public static readonly DependencyProperty BelowProperty = DependencyProperty.RegisterAttached(
            "Below", typeof(UIElement), typeof(RelativePanel), new FrameworkPropertyMetadata(default(UIElement), FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsMeasure));

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
            "AlignHorizontalCenterWithPanel", typeof(bool), typeof(RelativePanel), new FrameworkPropertyMetadata(ValueBoxes.FalseBox, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Identifies the AlignVerticalCenterWithPanel attached property
        /// </summary>
        public static readonly DependencyProperty AlignVerticalCenterWithPanelProperty = DependencyProperty.RegisterAttached(
            "AlignVerticalCenterWithPanel", typeof(bool), typeof(RelativePanel), new FrameworkPropertyMetadata(ValueBoxes.FalseBox, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Identifies the AlignHorizontalCenterWith attached property
        /// </summary>
        public static readonly DependencyProperty AlignHorizontalCenterWithProperty = DependencyProperty.RegisterAttached(
            "AlignHorizontalCenterWith", typeof(UIElement), typeof(RelativePanel), new FrameworkPropertyMetadata(default(UIElement), FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Identifies the AlignVerticalCenterWith attached property
        /// </summary>
        public static readonly DependencyProperty AlignVerticalCenterWithProperty = DependencyProperty.RegisterAttached(
            "AlignVerticalCenterWith", typeof(UIElement), typeof(RelativePanel), new FrameworkPropertyMetadata(default(UIElement), FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsMeasure));

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

        private Rect MeasureChild(UIElement uiElement, Size availableSize)
        {
            if (_layoutQueue.Contains(uiElement))
            {
                throw new InvalidOperationException("Circular dependency detected");
            }

            _layoutQueue.Add(uiElement);

            uiElement.Measure(availableSize);

            Rect layoutInfo = new()
            {
                Size = uiElement.DesiredSize,
                X = double.NaN,
                Y = double.NaN,
            };

            #region Horizontal Position

            if (GetAlignLeftWithPanel(uiElement))
            {
                layoutInfo.X = 0;
            }

            if (GetAlignRightWithPanel(uiElement))
            {
                if (!double.IsNaN(layoutInfo.Left))
                {
                    throw new InvalidOperationException("Horizontal position of UIElement can be set only once");
                }

                layoutInfo.X = 0;
            }

            if (GetAlignLeftWith(uiElement) is UIElement alignLeftWith)
            {
                if (!double.IsNaN(layoutInfo.Left))
                {
                    throw new InvalidOperationException("Horizontal position of UIElement can be set only once");
                }

                if (!_childLayouts.TryGetValue(alignLeftWith, out var alignLeftWithLayout))
                {
                    _childLayouts[alignLeftWith] = alignLeftWithLayout = MeasureChild(alignLeftWith, availableSize);
                }

                layoutInfo.X = alignLeftWithLayout.Left;
            }

            if (GetAlignRightWith(uiElement) is UIElement alignRightWith)
            {
                if (!double.IsNaN(layoutInfo.Left))
                {
                    throw new InvalidOperationException("Horizontal position of UIElement can be set only once");
                }

                if (!_childLayouts.TryGetValue(alignRightWith, out var alignRightWithLayout))
                {
                    _childLayouts[alignRightWith] = alignRightWithLayout = MeasureChild(alignRightWith, availableSize);
                }

                layoutInfo.X = alignRightWithLayout.Left + alignRightWithLayout.Size.Width - layoutInfo.Size.Width;
            }

            if (GetRightOf(uiElement) is UIElement rightOf)
            {
                if (!double.IsNaN(layoutInfo.Left))
                {
                    throw new InvalidOperationException("Horizontal position of UIElement can be set only once");
                }

                if (!_childLayouts.TryGetValue(rightOf, out var rightOfLayout))
                {
                    _childLayouts[rightOf] = rightOfLayout = MeasureChild(rightOf, availableSize);
                }

                layoutInfo.X = rightOfLayout.Left + rightOfLayout.Size.Width;
            }

            if (GetLeftOf(uiElement) is UIElement leftOf)
            {
                if (!double.IsNaN(layoutInfo.Left))
                {
                    throw new InvalidOperationException("Horizontal position of UIElement can be set only once");
                }

                if (!_childLayouts.TryGetValue(leftOf, out var leftOfLayout))
                {
                    _childLayouts[leftOf] = leftOfLayout = MeasureChild(leftOf, availableSize);
                }

                layoutInfo.X = leftOfLayout.Left - layoutInfo.Size.Width;
            }

            if (GetAlignHorizontalCenterWithPanel(uiElement))
            {
                if (!double.IsNaN(layoutInfo.Left))
                {
                    throw new InvalidOperationException("Horizontal position of UIElement can be set only once");
                }

                layoutInfo.X = 0;
            }

            if (GetAlignHorizontalCenterWith(uiElement) is UIElement alignHorizontalCenterWith)
            {
                if (!double.IsNaN(layoutInfo.Left))
                {
                    throw new InvalidOperationException("Horizontal position of UIElement can be set only once");
                }

                if (!_childLayouts.TryGetValue(alignHorizontalCenterWith, out var alignHorizontalCenterWithLayout))
                {
                    _childLayouts[alignHorizontalCenterWith] = alignHorizontalCenterWithLayout = MeasureChild(alignHorizontalCenterWith, availableSize);
                }

                layoutInfo.X = alignHorizontalCenterWithLayout.Left - (alignHorizontalCenterWithLayout.Size.Width - layoutInfo.Size.Width) / 2;
            }

            if (double.IsNaN(layoutInfo.X))
            {
                layoutInfo.X = 0;
            }

            #endregion

            #region Vertical position

            if (GetAlignTopWithPanel(uiElement))
            {
                layoutInfo.Y = 0;
            }

            if (GetAlignRightWithPanel(uiElement))
            {
                layoutInfo.Y = 0;
            }

            if (GetAlignTopWith(uiElement) is UIElement alignTopWith)
            {
                if (!double.IsNaN(layoutInfo.Top))
                {
                    throw new InvalidOperationException("Horizontal position of UIElement can be set only once");
                }

                if (!_childLayouts.TryGetValue(alignTopWith, out var alignTopWithLayout))
                {
                    _childLayouts[alignTopWith] = alignTopWithLayout = MeasureChild(alignTopWith, availableSize);
                }

                layoutInfo.Y = alignTopWithLayout.Top;
            }

            if (GetAlignBottomWith(uiElement) is UIElement alignBottomWith)
            {
                if (!double.IsNaN(layoutInfo.Top))
                {
                    throw new InvalidOperationException("Horizontal position of UIElement can be set only once");
                }

                if (!_childLayouts.TryGetValue(alignBottomWith, out var alignBottomWithLayout))
                {
                    _childLayouts[alignBottomWith] = alignBottomWithLayout = MeasureChild(alignBottomWith, availableSize);
                }

                layoutInfo.Y = alignBottomWithLayout.Top + alignBottomWithLayout.Size.Height - layoutInfo.Size.Height;
            }

            if (GetBelow(uiElement) is UIElement below)
            {
                if (!double.IsNaN(layoutInfo.Top))
                {
                    throw new InvalidOperationException("Horizontal position of UIElement can be set only once");
                }

                if (!_childLayouts.TryGetValue(below, out var belowLayout))
                {
                    _childLayouts[below] = belowLayout = MeasureChild(below, availableSize);
                }

                layoutInfo.Y = belowLayout.Top + belowLayout.Size.Height;
            }

            if (GetAbove(uiElement) is UIElement above)
            {
                if (!double.IsNaN(layoutInfo.Top))
                {
                    throw new InvalidOperationException("Horizontal position of UIElement can be set only once");
                }

                if (!_childLayouts.TryGetValue(above, out var aboveLayout))
                {
                    _childLayouts[above] = aboveLayout = MeasureChild(above, availableSize);
                }

                layoutInfo.Y = aboveLayout.Top - layoutInfo.Size.Height;
            }

            if (GetAlignVerticalCenterWithPanel(uiElement))
            {
                if (!double.IsNaN(layoutInfo.Top))
                {
                    throw new InvalidOperationException("Horizontal position of UIElement can be set only once");
                }

                layoutInfo.Y = 0;
            }

            if (GetAlignVerticalCenterWith(uiElement) is UIElement alignVerticalCenterWith)
            {
                if (!double.IsNaN(layoutInfo.Top))
                {
                    throw new InvalidOperationException("Horizontal position of UIElement can be set only once");
                }

                if (!_childLayouts.TryGetValue(alignVerticalCenterWith, out var alignVerticalCenterWithLayout))
                {
                    _childLayouts[alignVerticalCenterWith] = alignVerticalCenterWithLayout = MeasureChild(alignVerticalCenterWith, availableSize);
                }

                layoutInfo.Y = alignVerticalCenterWithLayout.Top - (alignVerticalCenterWithLayout.Size.Height - layoutInfo.Size.Height) / 2;
            }

            if (double.IsNaN(layoutInfo.Y))
            {
                layoutInfo.Y = 0;
            }

            #endregion

            _layoutQueue.Remove(uiElement);

            return layoutInfo;
        }

        private Rect ArrangeChild(UIElement uiElement, Size arrangeSize)
        {
            if (_layoutQueue.Contains(uiElement))
            {
                throw new InvalidOperationException("Circular dependency detected");
            }

            _layoutQueue.Add(uiElement);

            if (arrangeSize.Width < uiElement.DesiredSize.Width ||
                arrangeSize.Height < uiElement.DesiredSize.Height)
            {
                uiElement.Measure(arrangeSize);
            }


            Rect layoutInfo = new()
            {
                Size = uiElement.DesiredSize,
                X = double.NaN,
                Y = double.NaN,
            };

            #region Horizontal Position

            if (GetAlignLeftWithPanel(uiElement))
            {
                layoutInfo.X = 0;
            }

            if (GetAlignRightWithPanel(uiElement))
            {
                if (!double.IsNaN(layoutInfo.Left))
                {
                    throw new InvalidOperationException("Horizontal position of UIElement can be set only once");
                }

                layoutInfo.X = arrangeSize.Width - layoutInfo.Size.Width;
            }

            if (GetAlignLeftWith(uiElement) is UIElement alignLeftWith)
            {
                if (!double.IsNaN(layoutInfo.Left))
                {
                    throw new InvalidOperationException("Horizontal position of UIElement can be set only once");
                }

                if (!_childLayouts.TryGetValue(alignLeftWith, out var alignLeftWithLayout))
                {
                    _childLayouts[alignLeftWith] = alignLeftWithLayout = ArrangeChild(alignLeftWith, arrangeSize);
                }

                layoutInfo.X = alignLeftWithLayout.Left;
            }

            if (GetAlignRightWith(uiElement) is UIElement alignRightWith)
            {
                if (!double.IsNaN(layoutInfo.Left))
                {
                    throw new InvalidOperationException("Horizontal position of UIElement can be set only once");
                }

                if (!_childLayouts.TryGetValue(alignRightWith, out var alignRightWithLayout))
                {
                    _childLayouts[alignRightWith] = alignRightWithLayout = ArrangeChild(alignRightWith, arrangeSize);
                }

                layoutInfo.X = alignRightWithLayout.Left + alignRightWithLayout.Size.Width - layoutInfo.Size.Width;
            }

            if (GetRightOf(uiElement) is UIElement rightOf)
            {
                if (!double.IsNaN(layoutInfo.Left))
                {
                    throw new InvalidOperationException("Horizontal position of UIElement can be set only once");
                }

                if (!_childLayouts.TryGetValue(rightOf, out var rightOfLayout))
                {
                    _childLayouts[rightOf] = rightOfLayout = ArrangeChild(rightOf, arrangeSize);
                }

                layoutInfo.X = rightOfLayout.Left + rightOfLayout.Size.Width;
            }

            if (GetLeftOf(uiElement) is UIElement leftOf)
            {
                if (!double.IsNaN(layoutInfo.Left))
                {
                    throw new InvalidOperationException("Horizontal position of UIElement can be set only once");
                }

                if (!_childLayouts.TryGetValue(leftOf, out var leftOfLayout))
                {
                    _childLayouts[leftOf] = leftOfLayout = ArrangeChild(leftOf, arrangeSize);
                }

                layoutInfo.X = leftOfLayout.Left - layoutInfo.Size.Width;
            }

            if (GetAlignHorizontalCenterWithPanel(uiElement))
            {
                if (!double.IsNaN(layoutInfo.Left))
                {
                    throw new InvalidOperationException("Horizontal position of UIElement can be set only once");
                }

                layoutInfo.X = (arrangeSize.Width - layoutInfo.Size.Width) / 2;
            }

            if (GetAlignHorizontalCenterWith(uiElement) is UIElement alignHorizontalCenterWith)
            {
                if (!double.IsNaN(layoutInfo.Left))
                {
                    throw new InvalidOperationException("Horizontal position of UIElement can be set only once");
                }

                if (!_childLayouts.TryGetValue(alignHorizontalCenterWith, out var alignHorizontalCenterWithLayout))
                {
                    _childLayouts[alignHorizontalCenterWith] = alignHorizontalCenterWithLayout = ArrangeChild(alignHorizontalCenterWith, arrangeSize);
                }

                layoutInfo.X = alignHorizontalCenterWithLayout.Left - (alignHorizontalCenterWithLayout.Size.Width - layoutInfo.Size.Width) / 2;
            }

            if (double.IsNaN(layoutInfo.X))
            {
                layoutInfo.X = 0;
            }

            #endregion

            #region Vertical position

            if (GetAlignTopWithPanel(uiElement))
            {
                layoutInfo.Y = 0;
            }

            if (GetAlignBottomWithPanel(uiElement))
            {
                if (!double.IsNaN(layoutInfo.Top))
                {
                    throw new InvalidOperationException("Vertical position of UIElement can be set only once");
                }

                layoutInfo.Y = 0;
            }

            if (GetAlignTopWith(uiElement) is UIElement alignTopWith)
            {
                if (!double.IsNaN(layoutInfo.Top))
                {
                    throw new InvalidOperationException("Vertical position of UIElement can be set only once");
                }

                if (!_childLayouts.TryGetValue(alignTopWith, out var alignTopWithLayout))
                {
                    _childLayouts[alignTopWith] = alignTopWithLayout = ArrangeChild(alignTopWith, arrangeSize);
                }

                layoutInfo.Y = alignTopWithLayout.Top;
            }

            if (GetAlignBottomWith(uiElement) is UIElement alignBottomWith)
            {
                if (!double.IsNaN(layoutInfo.Top))
                {
                    throw new InvalidOperationException("Vertical position of UIElement can be set only once");
                }

                if (!_childLayouts.TryGetValue(alignBottomWith, out var alignBottomWithLayout))
                {
                    _childLayouts[alignBottomWith] = alignBottomWithLayout = ArrangeChild(alignBottomWith, arrangeSize);
                }

                layoutInfo.Y = alignBottomWithLayout.Top + alignBottomWithLayout.Size.Height - layoutInfo.Size.Height;
            }

            if (GetBelow(uiElement) is UIElement below)
            {
                if (!double.IsNaN(layoutInfo.Top))
                {
                    throw new InvalidOperationException("Vertical position of UIElement can be set only once");
                }

                if (!_childLayouts.TryGetValue(below, out var belowLayout))
                {
                    _childLayouts[below] = belowLayout = ArrangeChild(below, arrangeSize);
                }

                layoutInfo.Y = belowLayout.Top + belowLayout.Size.Height;
            }

            if (GetAbove(uiElement) is UIElement above)
            {
                if (!double.IsNaN(layoutInfo.Top))
                {
                    throw new InvalidOperationException("Vertical position of UIElement can be set only once");
                }

                if (!_childLayouts.TryGetValue(above, out var aboveLayout))
                {
                    _childLayouts[above] = aboveLayout = ArrangeChild(above, arrangeSize);
                }

                layoutInfo.Y = aboveLayout.Top - layoutInfo.Size.Height;
            }

            if (GetAlignVerticalCenterWithPanel(uiElement))
            {
                if (!double.IsNaN(layoutInfo.Top))
                {
                    throw new InvalidOperationException("Vertical position of UIElement can be set only once");
                }

                layoutInfo.Y = (arrangeSize.Height - layoutInfo.Size.Height) / 2;
            }

            if (GetAlignVerticalCenterWith(uiElement) is UIElement alignVerticalCenterWith)
            {
                if (!double.IsNaN(layoutInfo.Top))
                {
                    throw new InvalidOperationException("Vertical position of UIElement can be set only once");
                }

                if (!_childLayouts.TryGetValue(alignVerticalCenterWith, out var alignVerticalCenterWithLayout))
                {
                    _childLayouts[alignVerticalCenterWith] = alignVerticalCenterWithLayout = ArrangeChild(alignVerticalCenterWith, arrangeSize);
                }

                layoutInfo.Y = alignVerticalCenterWithLayout.Top - (alignVerticalCenterWithLayout.Size.Height - layoutInfo.Size.Height) / 2;
            }

            if (double.IsNaN(layoutInfo.Y))
            {
                layoutInfo.Y = 0;
            }

            #endregion

            _layoutQueue.Remove(uiElement);

            uiElement.Arrange(new Rect(layoutInfo.Left, layoutInfo.Top, layoutInfo.Size.Width, layoutInfo.Size.Height));

            return layoutInfo;
        }

        /// <inheritdoc/>
        protected override Size MeasureOverride(Size availableSize)
        {
            foreach (UIElement child in InternalChildren)
            {
                if (!_childLayouts.ContainsKey(child))
                {
                    _childLayouts[child] = MeasureChild(child, availableSize);
                }
            }

            double left = 0;
            double top = 0;
            double right = 0;
            double bottom = 0;

            foreach (var layout in _childLayouts.Values)
            {
                left = Math.Min(left, layout.Left);
                top = Math.Min(top, layout.Top);
                right = Math.Max(right, layout.Left + layout.Size.Width);
                bottom = Math.Max(bottom, layout.Top + layout.Size.Height);
            }

            var size = new Size(right - left, bottom - top);
            size.Width = Math.Min(size.Width, availableSize.Width);
            size.Height = Math.Min(size.Height, availableSize.Height);

            _childLayouts.Clear();

            return size;
        }

        /// <inheritdoc/>
        protected override Size ArrangeOverride(Size arrangeSize)
        {
            foreach (UIElement child in InternalChildren)
            {
                if (!_childLayouts.ContainsKey(child))
                {
                    _childLayouts[child] = ArrangeChild(child, arrangeSize);
                }
            }

            _childLayouts.Clear();

            return arrangeSize;
        }
    }
}
