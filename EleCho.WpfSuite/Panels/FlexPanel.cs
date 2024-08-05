using System.Collections.Generic;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Linq;

namespace EleCho.WpfSuite
{
    // 这里吐槽一句, FlexPanel 是基于 HandyControl 的 FlexPanel 改的, 加了 Spacing, 改了些 bug
    // 但随之而来的就是,,, 代码也越来越屎山了

    /// <summary>
    /// Flex layout panel
    /// </summary>
    public class FlexPanel : Panel
    {
        private UVSize _uvConstraint;

        private int _lineCount;

        private readonly List<FlexItemInfo> _orderList = new();

        #region Panel

        public FlexDirection Direction
        {
            get => (FlexDirection)GetValue(DirectionProperty);
            set => SetValue(DirectionProperty, value);
        }

        public FlexWrap Wrap
        {
            get => (FlexWrap)GetValue(WrapProperty);
            set => SetValue(WrapProperty, value);
        }

        public FlexMainAlignment MainAlignment
        {
            get => (FlexMainAlignment)GetValue(MainAlignmentProperty);
            set => SetValue(MainAlignmentProperty, value);
        }

        public FlexCrossAlignment CrossAlignment
        {
            get => (FlexCrossAlignment)GetValue(CrossAlignmentProperty);
            set => SetValue(CrossAlignmentProperty, value);
        }

        public FlexItemsAlignment ItemsAlignment
        {
            get => (FlexItemsAlignment)GetValue(ItemsAlignmentProperty);
            set => SetValue(ItemsAlignmentProperty, value);
        }

        public double UniformGrow
        {
            get { return (double)GetValue(UniformGrowProperty); }
            set { SetValue(UniformGrowProperty, value); }
        }

        public double UniformShrink
        {
            get { return (double)GetValue(UniformShrinkProperty); }
            set { SetValue(UniformShrinkProperty, value); }
        }

        public double MainSpacing
        {
            get { return (double)GetValue(MainSpacingProperty); }
            set { SetValue(MainSpacingProperty, value); }
        }

        public double CrossSpacing
        {
            get { return (double)GetValue(CrossSpacingProperty); }
            set { SetValue(CrossSpacingProperty, value); }
        }



        public static readonly DependencyProperty DirectionProperty =
            DependencyProperty.Register(nameof(Direction), typeof(FlexDirection), typeof(FlexPanel),
                new FrameworkPropertyMetadata(default(FlexDirection), FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty WrapProperty =
            DependencyProperty.Register(nameof(Wrap), typeof(FlexWrap), typeof(FlexPanel),
                new FrameworkPropertyMetadata(default(FlexWrap), FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty MainAlignmentProperty =
            DependencyProperty.Register(nameof(MainAlignment), typeof(FlexMainAlignment), typeof(FlexPanel),
                new FrameworkPropertyMetadata(default(FlexMainAlignment), FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty CrossAlignmentProperty =
            DependencyProperty.Register(nameof(CrossAlignment), typeof(FlexCrossAlignment), typeof(FlexPanel),
                new FrameworkPropertyMetadata(FlexCrossAlignment.Stretch, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty ItemsAlignmentProperty =
            DependencyProperty.Register(nameof(ItemsAlignment), typeof(FlexItemsAlignment), typeof(FlexPanel),
                new FrameworkPropertyMetadata(FlexItemsAlignment.Stretch, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty UniformGrowProperty =
            DependencyProperty.Register(nameof(UniformGrow), typeof(double), typeof(FlexPanel),
                new FrameworkPropertyMetadata(ValueBoxes.Double0Box,  FrameworkPropertyMetadataOptions.AffectsMeasure),
                ValidationUtils.IsInRangeOfPosDoubleIncludeZero);

        public static readonly DependencyProperty UniformShrinkProperty =
            DependencyProperty.Register(nameof(UniformShrink), typeof(double), typeof(FlexPanel),
                new FrameworkPropertyMetadata(ValueBoxes.Double1Box, FrameworkPropertyMetadataOptions.AffectsMeasure),
                ValidationUtils.IsInRangeOfPosDoubleIncludeZero);

        public static readonly DependencyProperty CrossSpacingProperty =
            DependencyProperty.Register(nameof(CrossSpacing), typeof(double), typeof(FlexPanel),
                new FrameworkPropertyMetadata(ValueBoxes.Double0Box, FrameworkPropertyMetadataOptions.AffectsMeasure),
                ValidationUtils.IsInRangeOfPosDoubleIncludeZero);

        public static readonly DependencyProperty MainSpacingProperty =
            DependencyProperty.Register(nameof(MainSpacing), typeof(double), typeof(FlexPanel),
                new FrameworkPropertyMetadata(ValueBoxes.Double0Box, FrameworkPropertyMetadataOptions.AffectsMeasure),
                ValidationUtils.IsInRangeOfPosDoubleIncludeZero);

        #endregion

        #region Item

        public static void SetOrder(DependencyObject element, int value)
            => element.SetValue(OrderProperty, value);

        public static int GetOrder(DependencyObject element)
            => (int)element.GetValue(OrderProperty);

        public static void SetGrow(DependencyObject element, double value)
            => element.SetValue(GrowProperty, value);

        public static double GetGrow(DependencyObject element)
            => (double)element.GetValue(GrowProperty);

        public static void SetShrink(DependencyObject element, double value)
            => element.SetValue(ShrinkProperty, value);

        public static double GetShrink(DependencyObject element)
            => (double)element.GetValue(ShrinkProperty);

        public static void SetBasis(DependencyObject element, double value)
            => element.SetValue(BasisProperty, value);

        public static double GetBasis(DependencyObject element)
            => (double)element.GetValue(BasisProperty);

        public static void SetAlignment(DependencyObject element, FlexItemAlignment value)
            => element.SetValue(AlignmentProperty, value);

        public static FlexItemAlignment GetAlignment(DependencyObject element)
            => (FlexItemAlignment)element.GetValue(AlignmentProperty);

        public static readonly DependencyProperty OrderProperty = DependencyProperty.RegisterAttached(
            "Order", typeof(int), typeof(FlexPanel),
            new FrameworkPropertyMetadata(ValueBoxes.Int0Box, FrameworkPropertyMetadataOptions.AffectsMeasure, OnItemPropertyChanged));

        public static readonly DependencyProperty GrowProperty = DependencyProperty.RegisterAttached(
            "Grow", typeof(double), typeof(FlexPanel),
            new FrameworkPropertyMetadata(ValueBoxes.DoubleNaNBox, FrameworkPropertyMetadataOptions.AffectsMeasure, OnItemPropertyChanged),
            ValidationUtils.IsNotDoubleInfinity);

        public static readonly DependencyProperty ShrinkProperty = DependencyProperty.RegisterAttached(
            "Shrink", typeof(double), typeof(FlexPanel),
            new FrameworkPropertyMetadata(ValueBoxes.DoubleNaNBox, FrameworkPropertyMetadataOptions.AffectsMeasure, OnItemPropertyChanged),
            ValidationUtils.IsNotDoubleInfinity);

        public static readonly DependencyProperty BasisProperty = DependencyProperty.RegisterAttached(
            "Basis", typeof(double), typeof(FlexPanel),
            new FrameworkPropertyMetadata(double.NaN, FrameworkPropertyMetadataOptions.AffectsMeasure, OnItemPropertyChanged));

        public static readonly DependencyProperty AlignmentProperty = DependencyProperty.RegisterAttached(
            "Alignment", typeof(FlexItemAlignment), typeof(FlexPanel),
            new FrameworkPropertyMetadata(FlexItemAlignment.Auto, FrameworkPropertyMetadataOptions.AffectsMeasure, OnItemPropertyChanged));

        #endregion

        private static void OnItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement element)
            {
                if (VisualTreeHelper.GetParent(element) is FlexPanel p)
                {
                    p.InvalidateMeasure();
                }
            }
        }

        /// <inheritdoc/>
        protected override Size MeasureOverride(Size constraint)
        {
            var flexDirection = Direction;
            var flexWrap = Wrap;
            var crossAlignment = CrossAlignment;
            var mainSpacing = MainSpacing;
            var crossSpacing = CrossSpacing;

            var curLineSize = new UVSize(flexDirection);
            var panelSize = new UVSize(flexDirection);
            _uvConstraint = new UVSize(flexDirection, constraint);
            var childConstraint = new Size(constraint.Width, constraint.Height);
            _lineCount = 1;
            var children = InternalChildren;

            _orderList.Clear();
            for (var i = 0; i < children.Count; i++)
            {
                var child = children[i];
                if (child == null)
                    continue;

                _orderList.Add(new FlexItemInfo(i, GetOrder(child)));
            }

            _orderList.Sort();

            int lineChildIndexStart = 0;
            for (var i = 0; i < children.Count; i++)
            {
                var child = children[_orderList[i].Index];
                if (child == null)
                    continue;

                var flexBasis = GetBasis(child);
                if (!double.IsNaN(flexBasis))
                {
                    child.SetCurrentValue(WidthProperty, flexBasis);
                }
                child.Measure(childConstraint);

                var sz = new UVSize(flexDirection, child.DesiredSize);
                var spacing = mainSpacing * (i - lineChildIndexStart);

                if (flexWrap == FlexWrap.NoWrap) //continue to accumulate a line
                {
                    curLineSize.U += sz.U;
                    curLineSize.V = Math.Max(sz.V, curLineSize.V);
                }
                else
                {
                    if (MathHelper.GreaterThan(curLineSize.U + sz.U + spacing, _uvConstraint.U)) //need to switch to another line
                    {
                        // add spacing to line size
                        curLineSize.U += spacing;

                        panelSize.U = Math.Max(curLineSize.U, panelSize.U);
                        panelSize.V += curLineSize.V;
                        curLineSize = sz;
                        lineChildIndexStart = i;

                        _lineCount++;

                        if (MathHelper.GreaterThan(sz.U, _uvConstraint.U)) //the element is wider then the constrint - give it a separate line
                        {
                            panelSize.U = Math.Max(sz.U, panelSize.U);
                            panelSize.V += sz.V;
                            curLineSize = new UVSize(flexDirection);
                            lineChildIndexStart = i;

                            _lineCount++;
                        }
                    }
                    else //continue to accumulate a line
                    {
                        curLineSize.U += sz.U;
                        curLineSize.V = Math.Max(sz.V, curLineSize.V);
                    }
                }
            }

            //the last line size, if any should be added
            curLineSize.U += mainSpacing * (children.Count - lineChildIndexStart - 1);
            panelSize.U = Math.Max(curLineSize.U, panelSize.U);
            panelSize.V += curLineSize.V;

            // add cross spacing
            panelSize.V += crossSpacing * (_lineCount - 1);

            if (_lineCount > 1)
            {
                if (crossAlignment is FlexCrossAlignment.SpaceAround)
                {
                    panelSize.V += crossSpacing;
                }
                else if (crossAlignment is FlexCrossAlignment.SpaceEvenly)
                {
                    panelSize.V += crossSpacing * 2;
                }
            }

            var finalSize = new Size(panelSize.Width, panelSize.Height);

            // layout again, make child not greater than it's box area
            Layout(finalSize, false);

            if (finalSize.Width > constraint.Width)
            {
                finalSize.Width = constraint.Width;
            }

            if (finalSize.Height > constraint.Height)
            {
                finalSize.Height = constraint.Height;
            }

            //go from UV space to W/H space
            return finalSize;
        }

        /// <inheritdoc/>
        protected override Size ArrangeOverride(Size arrangeSize)
        {
            Layout(arrangeSize, true);

            return arrangeSize;
        }

        private void Layout(Size size, bool arrange)
        {
            var flexDirection = Direction;
            var flexWrap = Wrap;
            var alignContent = CrossAlignment;
            var mainSpacing = MainSpacing;
            var crossSpacing = CrossSpacing;

            var uvFinalSize = new UVSize(flexDirection, size);
            if (MathHelper.IsZero(uvFinalSize.U) || MathHelper.IsZero(uvFinalSize.V))
                return;

            // init status
            var children = InternalChildren;
            var lineIndex = 0;

            var curLineSizeArr = new UVSize[_lineCount];
            curLineSizeArr[0] = new UVSize(flexDirection);

            var lastInLineArr = new int[_lineCount];
            for (var i = 0; i < _lineCount; i++)
            {
                lastInLineArr[i] = int.MaxValue;
            }

            // calculate line max U space
            int lineChildIndexStart = 0;
            for (var i = 0; i < children.Count; i++)
            {
                var child = children[_orderList[i].Index];
                if (child == null)
                    continue;

                var sz = new UVSize(flexDirection, child.DesiredSize);
                var spacing = mainSpacing * (i - lineChildIndexStart);

                if (flexWrap == FlexWrap.NoWrap)
                {
                    curLineSizeArr[lineIndex].U += sz.U;
                    curLineSizeArr[lineIndex].V = Math.Max(sz.V, curLineSizeArr[lineIndex].V);
                }
                else
                {
                    if (MathHelper.GreaterThan(curLineSizeArr[lineIndex].U + sz.U + spacing, uvFinalSize.U)) //need to switch to another line
                    {
                        var spacingBefore = mainSpacing * (i - lineChildIndexStart - 1);

                        var curLineSize = curLineSizeArr[lineIndex];
                        curLineSize.U += spacingBefore;

                        lastInLineArr[lineIndex] = i;
                        curLineSizeArr[lineIndex] = curLineSize;
                        lineIndex++;
                        curLineSizeArr[lineIndex] = sz;
                        lineChildIndexStart = i;

                        if (MathHelper.GreaterThan(sz.U, uvFinalSize.U)) //the element is wider then the constraint - give it a separate line
                        {
                            //switch to next line which only contain one element
                            curLineSize = curLineSizeArr[lineIndex];

                            lastInLineArr[lineIndex] = i;
                            curLineSizeArr[lineIndex] = curLineSize;
                            lineIndex++;
                            curLineSizeArr[lineIndex] = new UVSize(flexDirection);
                            lineChildIndexStart = i;
                        }
                    }
                    else //continue to accumulate a line
                    {
                        curLineSizeArr[lineIndex].U += sz.U;
                        curLineSizeArr[lineIndex].V = Math.Max(sz.V, curLineSizeArr[lineIndex].V);
                    }
                }
            }

            // add spacing to last line

            // init status
            var firstInLine = 0;
            var wrapReverseAdd = 0;
            var wrapReverseFlag = flexWrap == FlexWrap.WrapReverse ? -1 : 1;
            var accumulatedFlag = flexWrap == FlexWrap.WrapReverse ? 1 : 0;
            var itemsU = .0;
            var accumulatedV = .0;
            var freeV = uvFinalSize.V;
            foreach (var flexSize in curLineSizeArr)
            {
                freeV -= flexSize.V;
            }

            var freeItemV = freeV;

            // calculate status
            var lineFreeVArr = new double[_lineCount];
            switch (alignContent)
            {
                case FlexCrossAlignment.Stretch:
                    if (flexWrap != FlexWrap.NoWrap)
                    {
                        for (var i = 0; i < _lineCount; i++)
                        {
                            lineFreeVArr[i] = crossSpacing;
                        }

                        var totalLineSizeV = curLineSizeArr.Select(size => size.V).Sum();
                        var totalLineSpacingV = crossSpacing * (_lineCount - 1);
                        var remainSizeV = uvFinalSize.V - totalLineSizeV - totalLineSpacingV;
                        for (var i = 0; i < _lineCount; i++)
                        {
                            var curLineSize = curLineSizeArr[i];
                            curLineSize.V += remainSizeV * (curLineSize.V / totalLineSizeV);

                            curLineSizeArr[i] = curLineSize;
                        }

                        accumulatedV = flexWrap == FlexWrap.WrapReverse ? uvFinalSize.V - curLineSizeArr[0].V - lineFreeVArr[0] : 0;
                    }

                    break;
                case FlexCrossAlignment.Start:
                    if (flexWrap != FlexWrap.NoWrap)
                    {
                        for (var i = 0; i < _lineCount - 1; i++)
                        {
                            lineFreeVArr[i] = crossSpacing;
                        }

                        accumulatedV = flexWrap == FlexWrap.WrapReverse ? uvFinalSize.V - curLineSizeArr[0].V : 0;
                    }

                    break;
                case FlexCrossAlignment.End:
                    wrapReverseAdd = flexWrap == FlexWrap.WrapReverse ? 1 : 0;
                    if (flexWrap != FlexWrap.NoWrap)
                    {
                        for (var i = 0; i < _lineCount - 1; i++)
                        {
                            lineFreeVArr[i] = crossSpacing;
                        }

                        var totalCrosSpacing = crossSpacing * (_lineCount - 1);
                        accumulatedV = flexWrap == FlexWrap.WrapReverse ? uvFinalSize.V - curLineSizeArr[0].V - freeV - freeV + totalCrosSpacing : freeV - totalCrosSpacing;
                    }
                    else
                    {
                        wrapReverseAdd = 0;
                    }

                    break;
                case FlexCrossAlignment.Center:
                    if (flexWrap != FlexWrap.NoWrap)
                    {
                        for (var i = 0; i < _lineCount - 1; i++)
                        {
                            lineFreeVArr[i] = crossSpacing;
                        }

                        var totalCrosSpacing = crossSpacing * (_lineCount - 1);
                        accumulatedV = flexWrap == FlexWrap.WrapReverse ? uvFinalSize.V - curLineSizeArr[0].V - freeV * 0.5 + totalCrosSpacing / 2 : freeV * 0.5 - totalCrosSpacing / 2;
                    }

                    break;
                case FlexCrossAlignment.SpaceBetween:
                    if (flexWrap != FlexWrap.NoWrap)
                    {
                        freeItemV = freeV / (_lineCount - 1);
                        for (var i = 0; i < _lineCount - 1; i++)
                        {
                            lineFreeVArr[i] = freeItemV;
                        }

                        accumulatedV = flexWrap == FlexWrap.WrapReverse ? uvFinalSize.V - curLineSizeArr[0].V : 0;
                    }

                    break;
                case FlexCrossAlignment.SpaceAround:
                    if (flexWrap != FlexWrap.NoWrap)
                    {
                        freeItemV = freeV / _lineCount * 0.5;
                        for (var i = 0; i < _lineCount - 1; i++)
                        {
                            lineFreeVArr[i] = freeItemV * 2;
                        }

                        accumulatedV = flexWrap == FlexWrap.WrapReverse ? uvFinalSize.V - curLineSizeArr[0].V - freeItemV : freeItemV;
                    }

                    break;
                case FlexCrossAlignment.SpaceEvenly:
                    freeItemV = freeV / (_lineCount + 1);
                    for (var i = 0; i < _lineCount; i++)
                    {
                        lineFreeVArr[i] = freeItemV;
                    }

                    accumulatedV = flexWrap == FlexWrap.WrapReverse ? uvFinalSize.V - curLineSizeArr[0].V - freeItemV : freeItemV;

                    break;

            }

            // clear status
            lineIndex = 0;

            // arrange line
            for (var i = 0; i < children.Count; i++)
            {
                var child = children[_orderList[i].Index];
                if (child == null)
                    continue;

                var sz = new UVSize(flexDirection, child.DesiredSize);

                if (flexWrap != FlexWrap.NoWrap)
                {
                    if (i >= lastInLineArr[lineIndex]) //need to switch to another line
                    {
                        LayoutLine(new FlexLineInfo
                        {
                            ItemsU = itemsU,
                            OffsetV = accumulatedV + freeItemV * wrapReverseAdd,
                            LineV = curLineSizeArr[lineIndex].V,
                            LineFreeV = freeItemV,
                            LineU = uvFinalSize.U,
                            ItemStartIndex = firstInLine,
                            ItemEndIndex = i,
                        }, arrange);

                        accumulatedV += (lineFreeVArr[lineIndex] + curLineSizeArr[lineIndex + accumulatedFlag].V) * wrapReverseFlag;
                        lineIndex++;
                        itemsU = 0;

                        if (i >= lastInLineArr[lineIndex]) //the element is wider then the constraint - give it a separate line
                        {
                            //switch to next line which only contain one element
                            LayoutLine(new FlexLineInfo
                            {
                                ItemsU = itemsU,
                                OffsetV = accumulatedV + freeItemV * wrapReverseAdd,
                                LineV = curLineSizeArr[lineIndex].V,
                                LineFreeV = freeItemV,
                                LineU = uvFinalSize.U,
                                ItemStartIndex = i,
                                ItemEndIndex = ++i,
                            }, arrange);

                            accumulatedV += (lineFreeVArr[lineIndex] + curLineSizeArr[lineIndex + accumulatedFlag].V) * wrapReverseFlag;
                            lineIndex++;
                            itemsU = 0;
                        }

                        firstInLine = i;
                    }
                }

                itemsU += sz.U;
            }

            // arrange the last line, if any
            if (firstInLine < children.Count)
            {
                LayoutLine(new FlexLineInfo
                {
                    ItemsU = itemsU,
                    OffsetV = accumulatedV + freeItemV * wrapReverseAdd,
                    LineV = curLineSizeArr[lineIndex].V,
                    LineFreeV = freeItemV,
                    LineU = uvFinalSize.U,
                    ItemStartIndex = firstInLine,
                    ItemEndIndex = children.Count,
                }, arrange);
            }
        }

        private void LayoutLine(FlexLineInfo lineInfo, bool arrange)
        {
            var flexDirection = Direction;
            var flexWrap = Wrap;
            var justifyContent = MainAlignment;
            var alignItems = ItemsAlignment;
            var uniformGrow = UniformGrow;
            var uniformShrink = UniformShrink;
            var mainSpacing = MainSpacing;
            var crossSpacing = CrossSpacing;

            var isHorizontal = flexDirection == FlexDirection.Row || flexDirection == FlexDirection.RowReverse;
            var isReverse = flexDirection == FlexDirection.RowReverse || flexDirection == FlexDirection.ColumnReverse;
            var itemCount = lineInfo.ItemEndIndex - lineInfo.ItemStartIndex;
            var children = InternalChildren;
            var lineSpacingU = mainSpacing * (lineInfo.ItemEndIndex - lineInfo.ItemStartIndex - 1);
            var lineFreeU = lineInfo.LineU - lineInfo.ItemsU - lineSpacingU;
            var constraintFreeU = _uvConstraint.U - lineInfo.ItemsU - lineSpacingU;

            // calculate initial u
            var u = .0;
            if (isReverse)
            {
                u = justifyContent switch
                {
                    FlexMainAlignment.Start => lineInfo.LineU,
                    FlexMainAlignment.SpaceBetween => lineInfo.LineU,
                    FlexMainAlignment.SpaceAround => lineInfo.LineU,
                    FlexMainAlignment.SpaceEvenly => lineInfo.LineU,
                    FlexMainAlignment.End => lineInfo.ItemsU + lineSpacingU,
                    FlexMainAlignment.Center => (lineInfo.LineU + lineInfo.ItemsU + lineSpacingU) / 2,
                    _ => u
                };
            }
            else
            {
                u = justifyContent switch
                {
                    FlexMainAlignment.End => lineFreeU,
                    FlexMainAlignment.Center => lineFreeU / 2,
                    _ => u
                };
            }

            // apply FlexGrow
            var flexGrowUArr = new double[itemCount];
            if (constraintFreeU > 0)
            {
                var ignoreFlexGrow = true;
                var flexGrowSum = .0;

                for (var i = 0; i < itemCount; i++)
                {
                    var flexGrow = GetGrow(children[_orderList[i].Index]);
                    if (double.IsNaN(flexGrow))
                        flexGrow = uniformGrow;
                    ignoreFlexGrow &= MathHelper.IsVerySmall(flexGrow);
                    flexGrowUArr[i] = flexGrow;
                    flexGrowSum += flexGrow;
                }

                if (!ignoreFlexGrow)
                {
                    var flexGrowItem = .0;
                    if (flexGrowSum > 0)
                    {
                        flexGrowItem = constraintFreeU / flexGrowSum;
                        lineFreeU = 0; //line free U was used up
                    }

                    for (var i = 0; i < itemCount; i++)
                    {
                        flexGrowUArr[i] *= flexGrowItem;
                    }
                }
                else
                {
                    flexGrowUArr = new double[itemCount];
                }
            }

            // apply FlexShrink
            var flexShrinkUArr = new double[itemCount];
            if (constraintFreeU < 0)
            {
                var ignoreFlexShrink = true;
                var flexShrinkSum = .0;

                for (var i = 0; i < itemCount; i++)
                {
                    var flexShrink = GetShrink(children[_orderList[i].Index]);
                    if (double.IsNaN(flexShrink))
                        flexShrink = uniformShrink;
                    ignoreFlexShrink &= MathHelper.IsVerySmall(flexShrink);
                    flexShrinkUArr[i] = flexShrink;
                    flexShrinkSum += flexShrink;
                }

                if (!ignoreFlexShrink)
                {
                    var flexShrinkItem = .0;
                    if (flexShrinkSum > 0)
                    {
                        flexShrinkItem = constraintFreeU / flexShrinkSum;
                        lineFreeU = 0; //line free U was used up
                    }

                    for (var i = 0; i < itemCount; i++)
                    {
                        flexShrinkUArr[i] *= flexShrinkItem;
                    }
                }
                else
                {
                    flexShrinkUArr = new double[itemCount];
                }
            }

            // calculate offset u
            var offsetUArr = new double[itemCount];
            if (lineFreeU >= 0)
            {
                if (justifyContent == FlexMainAlignment.SpaceBetween)
                {
                    var freeItemU = lineFreeU / (itemCount - 1);
                    for (var i = 1; i < itemCount; i++)
                    {
                        offsetUArr[i] = freeItemU;
                    }
                }
                else if (justifyContent == FlexMainAlignment.SpaceAround)
                {
                    var freeItemU = lineFreeU / itemCount * 0.5;
                    offsetUArr[0] = freeItemU;
                    for (var i = 1; i < itemCount; i++)
                    {
                        offsetUArr[i] = freeItemU * 2;
                    }
                }
                else if (justifyContent == FlexMainAlignment.SpaceEvenly)
                {
                    var freeItemU = lineFreeU / (itemCount + 1);
                    for (var i = 0; i < itemCount; i++)
                    {
                        offsetUArr[i] = freeItemU;
                    }
                }

            }

            // arrange item
            for (int i = lineInfo.ItemStartIndex, j = 0; i < lineInfo.ItemEndIndex; i++, j++)
            {
                var child = children[_orderList[i].Index];
                if (child == null)
                    continue;

                var childSize = new UVSize(flexDirection, isHorizontal
                    ? new Size(child.DesiredSize.Width, child.DesiredSize.Height)
                    : new Size(child.DesiredSize.Width, child.DesiredSize.Height));

                childSize.U += flexGrowUArr[j] + flexShrinkUArr[j];
                if (childSize.U < 0)
                {
                    childSize.U = 0;
                }

                if (isReverse)
                {
                    u -= childSize.U;
                    u -= offsetUArr[j];
                }
                else
                {
                    u += offsetUArr[j];
                }

                var v = lineInfo.OffsetV;
                var alignSelf = GetAlignment(child);
                var alignment = alignSelf == FlexItemAlignment.Auto ? alignItems : (FlexItemsAlignment) alignSelf;

                switch (alignment)
                {
                    case FlexItemsAlignment.Stretch:
                        if (_lineCount == 1 && flexWrap == FlexWrap.NoWrap)
                        {
                            childSize.V = lineInfo.LineV + lineInfo.LineFreeV;
                        }
                        else
                        {
                            childSize.V = lineInfo.LineV;
                        }

                        break;
                    case FlexItemsAlignment.End:
                        v += lineInfo.LineV - childSize.V;
                        break;
                    case FlexItemsAlignment.Center:
                        v += (lineInfo.LineV - childSize.V) * 0.5;
                        break;
                }

                var childFinalRect = isHorizontal ? new Rect(u, v, childSize.U, childSize.V) : new Rect(v, u, childSize.V, childSize.U);

                if (arrange)
                {
                    child.Arrange(childFinalRect);
                }
                else
                {
                    child.Measure(childFinalRect.Size);
                }

                if (isReverse)
                {
                    u -= mainSpacing;
                }
                else
                {
                    u += childSize.U;
                    u += mainSpacing;
                }
            }
        }

        private readonly struct FlexItemInfo : IComparable<FlexItemInfo>
        {
            public FlexItemInfo(int index, int order)
            {
                Index = index;
                Order = order;
            }

            private int Order { get; }

            public int Index { get; }

            public int CompareTo(FlexItemInfo other)
            {
                var orderCompare = Order.CompareTo(other.Order);
                if (orderCompare != 0)
                    return orderCompare;
                return Index.CompareTo(other.Index);
            }
        }

        private struct FlexLineInfo
        {
            public double ItemsU { get; set; }

            public double OffsetV { get; set; }

            public double LineU { get; set; }

            public double LineV { get; set; }

            public double LineFreeV { get; set; }

            public int ItemStartIndex { get; set; }

            public int ItemEndIndex { get; set; }
        }

        private struct UVSize
        {
            public UVSize(FlexDirection direction, Size size)
            {
                U = V = 0d;
                FlexDirection = direction;
                Width = size.Width;
                Height = size.Height;
            }

            public UVSize(FlexDirection direction)
            {
                U = V = 0d;
                FlexDirection = direction;
            }

            public double U { get; set; }

            public double V { get; set; }

            private FlexDirection FlexDirection { get; }

            public double Width
            {
                get => FlexDirection == FlexDirection.Row || FlexDirection == FlexDirection.RowReverse ? U : V;
                private set
                {
                    if (FlexDirection == FlexDirection.Row || FlexDirection == FlexDirection.RowReverse)
                    {
                        U = value;
                    }
                    else
                    {
                        V = value;
                    }
                }
            }

            public double Height
            {
                get => FlexDirection == FlexDirection.Row || FlexDirection == FlexDirection.RowReverse ? V : U;
                private set
                {
                    if (FlexDirection == FlexDirection.Row || FlexDirection == FlexDirection.RowReverse)
                    {
                        V = value;
                    }
                    else
                    {
                        U = value;
                    }
                }
            }
        }
    }
}
