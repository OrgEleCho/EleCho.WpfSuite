using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace EleCho.WpfSuite.Layouts
{
    /// <summary>
    /// Enhanced WrapPanel
    /// </summary>
    public class WrapPanel : Panel
    {
        private List<UIElement> _layoutHelperList = new();

        /// <summary>
        /// Layout orientation
        /// </summary>
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        /// <summary>
        /// Item width. double.NaN for auto width.
        /// </summary>
        public double ItemWidth
        {
            get { return (double)GetValue(ItemWidthProperty); }
            set { SetValue(ItemWidthProperty, value); }
        }

        /// <summary>
        /// Item height. double.NaN for auto height.
        /// </summary>
        public double ItemHeight
        {
            get { return (double)GetValue(ItemHeightProperty); }
            set { SetValue(ItemHeightProperty, value); }
        }

        /// <summary>
        /// Vertical spacing between items
        /// </summary>
        public double VerticalSpacing
        {
            get { return (double)GetValue(VerticalSpacingProperty); }
            set { SetValue(VerticalSpacingProperty, value); }
        }

        /// <summary>
        /// Horizontal spacing between items
        /// </summary>
        public double HorizontalSpacing
        {
            get { return (double)GetValue(HorizontalSpacingProperty); }
            set { SetValue(HorizontalSpacingProperty, value); }
        }

        /// <inheritdoc/>
        protected override Size MeasureOverride(Size availableSize)
        {
            var itemWidth = ItemWidth;
            var itemHeight = ItemHeight;
            var verticalSpacing = VerticalSpacing;
            var horizontalSpacing = HorizontalSpacing;

            var offsetX = 0.0;
            var offsetY = 0.0;
            var maxLineLength = 0.0;
            var currentLineSize = 0.0;
            var currentLineLength = 0.0;

            Func<Size, double> childWidthGetter = double.IsNaN(itemWidth) ? size => size.Width : size => itemWidth;
            Func<Size, double> childHeightGetter = double.IsNaN(itemHeight) ? size => size.Height : size => itemHeight;

            var internalChildren = InternalChildren;
            if (Orientation == Orientation.Horizontal)
            {
                availableSize.Height = double.PositiveInfinity;
                for (int i = 0; i < internalChildren.Count; i++)
                {
                    var child = internalChildren[i];

                    child.Measure(availableSize);
                    var childDesiredSize = child.DesiredSize;
                    var childWidth = childWidthGetter.Invoke(childDesiredSize);
                    var childHeight = childHeightGetter.Invoke(childDesiredSize);

                    offsetX += childWidth;

                    if (child.Visibility != Visibility.Collapsed)
                        offsetX += horizontalSpacing;

                    if (offsetX - horizontalSpacing > availableSize.Width)
                    {
                        currentLineLength = offsetX - horizontalSpacing - childWidth - horizontalSpacing;
                        if (currentLineLength > maxLineLength)
                            maxLineLength = currentLineLength;

                        offsetX = childWidth + horizontalSpacing;
                        offsetY += currentLineSize;
                        offsetY += verticalSpacing;
                        currentLineSize = 0;
                    }

                    if (childHeight > currentLineSize)
                        currentLineSize = childHeight;
                }

                currentLineLength = offsetX - horizontalSpacing;
                if (currentLineLength > maxLineLength)
                    maxLineLength = currentLineLength;

                offsetX = 0;
                offsetY += currentLineSize;
                offsetY += verticalSpacing;

                currentLineSize = 0;

                return new Size(maxLineLength, offsetY - verticalSpacing);
            }
            else
            {
                availableSize.Width = double.PositiveInfinity;
                for (int i = 0; i < internalChildren.Count; i++)
                {
                    var child = internalChildren[i];

                    child.Measure(availableSize);
                    var childDesiredSize = child.DesiredSize;
                    var childWidth = childWidthGetter.Invoke(childDesiredSize);
                    var childHeight = childHeightGetter.Invoke(childDesiredSize);

                    offsetY += childHeight;

                    if (child.Visibility != Visibility.Collapsed)
                        offsetY += verticalSpacing;

                    if (offsetY - verticalSpacing > availableSize.Height)
                    {
                        currentLineLength = offsetY - horizontalSpacing - childHeight - verticalSpacing;
                        if (currentLineLength > maxLineLength)
                            maxLineLength = currentLineLength;

                        offsetY = childHeight + verticalSpacing;
                        offsetX += currentLineSize;
                        offsetX += horizontalSpacing;
                        currentLineSize = 0;
                    }

                    if (childWidth > currentLineSize)
                        currentLineSize = childWidth;
                }

                currentLineLength = offsetY - verticalSpacing;
                if (currentLineLength > maxLineLength)
                    maxLineLength = currentLineLength;

                offsetY = 0;
                offsetX += currentLineSize;
                offsetX += horizontalSpacing;
                currentLineSize = 0;

                return new Size(offsetX - horizontalSpacing, maxLineLength);
            }
        }

        /// <inheritdoc/>
        protected override Size ArrangeOverride(Size finalSize)
        {
            var itemWidth = ItemWidth;
            var itemHeight = ItemHeight;
            var verticalSpacing = VerticalSpacing;
            var horizontalSpacing = HorizontalSpacing;

            var tempOffset = 0.0;

            Func<Size, double> childWidthGetter = double.IsNaN(itemWidth) ? size => size.Width : size => itemWidth;
            Func<Size, double> childHeightGetter = double.IsNaN(itemHeight) ? size => size.Height : size => itemHeight;

            var internalChildren = InternalChildren;
            var currentLineSize = 0.0;
            var currentLineOffsetX = 0.0;
            var currentLineOffsetY = 0.0;
            var currentLineIndexStart = 0;
            if (Orientation == Orientation.Horizontal)
            {
                for (int i = 0; i < internalChildren.Count; i++)
                {
                    var child = internalChildren[i];
                    var childDesiredSize = child.DesiredSize;
                    var childWidth = childWidthGetter.Invoke(childDesiredSize);
                    var childHeight = childHeightGetter.Invoke(childDesiredSize);

                    tempOffset += childWidth;

                    if (child.Visibility != Visibility.Collapsed)
                        tempOffset += horizontalSpacing;

                    if (tempOffset - horizontalSpacing > finalSize.Width)
                    {
                        // 布局
                        ArrangeLineHorizontal(
                            internalChildren,
                            currentLineIndexStart,
                            i,
                            currentLineOffsetX,
                            currentLineOffsetY,
                            currentLineSize,
                            horizontalSpacing,
                            childWidthGetter,
                            childHeightGetter);

                        // 累计
                        currentLineOffsetX = 0;
                        currentLineOffsetY += currentLineSize;
                        currentLineOffsetY += verticalSpacing;
                        currentLineIndexStart = i;

                        // 重置
                        currentLineSize = 0;
                        tempOffset = childWidth + horizontalSpacing;
                    }

                    if (childHeight > currentLineSize)
                        currentLineSize = childHeight;
                }

                // 布局
                ArrangeLineHorizontal(
                    internalChildren,
                    currentLineIndexStart,
                    internalChildren.Count,
                    currentLineOffsetX,
                    currentLineOffsetY,
                    currentLineSize,
                    horizontalSpacing,
                    childWidthGetter,
                    childHeightGetter);
            }
            else
            {
                for (int i = 0; i < internalChildren.Count; i++)
                {
                    var child = internalChildren[i];
                    var childDesiredSize = child.DesiredSize;
                    var childWidth = childWidthGetter.Invoke(childDesiredSize);
                    var childHeight = childHeightGetter.Invoke(childDesiredSize);

                    tempOffset += childHeight;

                    if (child.Visibility != Visibility.Collapsed)
                        tempOffset += verticalSpacing;

                    if (tempOffset - verticalSpacing > finalSize.Height)
                    {
                        // 布局
                        ArrangeLineVertical(
                            internalChildren,
                            currentLineIndexStart,
                            i,
                            currentLineOffsetX,
                            currentLineOffsetY,
                            currentLineSize,
                            verticalSpacing,
                            childWidthGetter,
                            childHeightGetter);

                        // 累计
                        currentLineOffsetY = 0;
                        currentLineOffsetX += currentLineSize;
                        currentLineOffsetX += horizontalSpacing;
                        currentLineIndexStart = i;

                        // 重置
                        currentLineSize = 0;
                        tempOffset = childHeight + verticalSpacing;
                    }

                    if (childWidth > currentLineSize)
                        currentLineSize = childWidth;
                }

                // 布局
                ArrangeLineVertical(
                    internalChildren,
                    currentLineIndexStart,
                    internalChildren.Count,
                    currentLineOffsetX,
                    currentLineOffsetY,
                    currentLineSize,
                    verticalSpacing,
                    childWidthGetter,
                    childHeightGetter);
            }

            return finalSize;

            static void ArrangeLineHorizontal(
                UIElementCollection children,
                int childIndexStart,
                int childIndexEnd,
                double currentLineOffsetX,
                double currentLineOffsetY,
                double currentLineSize,
                double spacing,
                Func<Size, double> widthGetter,
                Func<Size, double> heightGetter)
            {
                // 布局
                var lineChildOffset = 0.0;
                for (int j = childIndexStart; j < childIndexEnd; j++)
                {
                    var lineChild = children[j];
                    var lineChildDesiredSize = lineChild.DesiredSize;
                    var lineChildWidth = widthGetter.Invoke(lineChildDesiredSize);
                    var lineChildHeight = heightGetter.Invoke(lineChildDesiredSize);

                    lineChild.Arrange(new Rect(currentLineOffsetX + lineChildOffset, currentLineOffsetY, lineChildWidth, currentLineSize));

                    lineChildOffset += lineChildWidth;

                    if (lineChild.Visibility != Visibility.Collapsed)
                        lineChildOffset += spacing;
                }
            }

            static void ArrangeLineVertical(
                UIElementCollection children,
                int childIndexStart,
                int childIndexEnd,
                double currentLineOffsetX,
                double currentLineOffsetY,
                double currentLineSize,
                double spacing,
                Func<Size, double> widthGetter,
                Func<Size, double> heightGetter)
            {
                // 布局
                var lineChildOffset = 0.0;
                for (int j = childIndexStart; j < childIndexEnd; j++)
                {
                    var lineChild = children[j];
                    var lineChildDesiredSize = lineChild.DesiredSize;
                    var lineChildWidth = widthGetter.Invoke(lineChildDesiredSize);
                    var lineChildHeight = heightGetter.Invoke(lineChildDesiredSize);

                    lineChild.Arrange(new Rect(currentLineOffsetX, currentLineOffsetY + lineChildOffset, currentLineSize, lineChildHeight));

                    lineChildOffset += lineChildHeight;

                    if (lineChild.Visibility != Visibility.Collapsed)
                        lineChildOffset += spacing;
                }
            }
        }


        /// <summary>
        /// Dependency property of Orientation property
        /// </summary>
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register(nameof(Orientation), typeof(Orientation), typeof(WrapPanel), new FrameworkPropertyMetadata(Orientation.Horizontal, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Dependency property of ItemHeight property
        /// </summary>
        public static readonly DependencyProperty ItemHeightProperty =
            DependencyProperty.Register(nameof(ItemHeight), typeof(double), typeof(WrapPanel), new FrameworkPropertyMetadata(double.NaN, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Dependency property of ItemWidth property
        /// </summary>
        public static readonly DependencyProperty ItemWidthProperty =
            DependencyProperty.Register(nameof(ItemWidth), typeof(double), typeof(WrapPanel), new FrameworkPropertyMetadata(double.NaN, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Dependency property of VerticalSpacing property
        /// </summary>
        public static readonly DependencyProperty VerticalSpacingProperty =
            DependencyProperty.Register(nameof(VerticalSpacing), typeof(double), typeof(WrapPanel), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Dependency property of HorizontalSpacing property
        /// </summary>
        public static readonly DependencyProperty HorizontalSpacingProperty =
            DependencyProperty.Register(nameof(HorizontalSpacing), typeof(double), typeof(WrapPanel), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure));


    }
}
