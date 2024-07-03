using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace EleCho.WpfSuite
{
    /// <summary>
    /// Masonry layout panel
    /// </summary>
    public class MasonryPanel : System.Windows.Controls.Panel
    {
        private readonly List<double> _flowOffsets = new();
        private readonly List<double> _flowMaxUSizes = new();
        private readonly List<int> _flowSortedOffsetIndexes = new();

        /// <summary>
        /// Layout orientation
        /// </summary>
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        /// <summary>
        /// Flow count
        /// </summary>
        public int Flows
        {
            get { return (int)GetValue(FlowsProperty); }
            set { SetValue(FlowsProperty, value); }
        }

        /// <summary>
        /// Spacing between flows
        /// </summary>
        public double FlowSpacing
        {
            get { return (double)GetValue(FlowSpacingProperty); }
            set { SetValue(FlowSpacingProperty, value); }
        }

        /// <summary>
        /// Spacing between items in a flow
        /// </summary>
        public double ItemSpacing
        {
            get { return (double)GetValue(ItemSpacingProperty); }
            set { SetValue(ItemSpacingProperty, value); }
        }

        private static void EnsureListCount<T>(List<T?> list, int count)
        {
            while (count > list.Count)
                list.Add(default);
            while (count < list.Count)
                list.RemoveAt(list.Count - 1);
        }

        //protected void MeasureChildren(Size constrain)
        //{
        //    var orientation = Orientation;

        //    if (orientation == Orientation.Vertical)
        //        constrain.Height = double.PositiveInfinity;
        //    else if (orientation == Orientation.Horizontal)
        //        constrain.Width = double.PositiveInfinity;

        //    var internalChildren = InternalChildren;
        //    for (int i = 0; i < internalChildren.Count; i++)
        //    {
        //        var child = internalChildren[i];
        //        child.Measure(constrain);
        //    }
        //}

        /// <inheritdoc/>
        protected Size Layout(Size size, bool arrange)
        {
            var isHorizontal = Orientation == Orientation.Horizontal;
            var flows = Flows;
            var containerU = isHorizontal ? size.Height : size.Width;
            var maxFlowSpacing = containerU / flows;
            var flowSpacing = Math.Min(FlowSpacing, maxFlowSpacing);
            var flowTotalSpacing = flowSpacing * (flows - 1);
            var itemSpacing = ItemSpacing;
            var itemU = (containerU - flowTotalSpacing) / flows;
            var flowStep = itemU + flowSpacing;
            var childConstraint = isHorizontal ? new Size(double.PositiveInfinity, itemU) : new Size(itemU, double.PositiveInfinity);

            EnsureListCount(_flowOffsets, flows);
            EnsureListCount(_flowMaxUSizes, flows);
            EnsureListCount(_flowSortedOffsetIndexes, flows);

            for (int i = 0; i < flows; i++)
            {
                _flowOffsets[i] = 0;
                _flowMaxUSizes[i] = 0;
            }

            for (int i = 0; i < flows; i++)
            {
                _flowSortedOffsetIndexes[i] = i;
            }

            var internalChildren = InternalChildren;
            for (int i = 0; i < internalChildren.Count; i++)
            {
                var child = internalChildren[i];

                if (!arrange)
                {
                    child.Measure(childConstraint);
                }

                var flowIndex = _flowSortedOffsetIndexes[0];
                var flowOffset = _flowOffsets[flowIndex];

                var childDesiredSize = child.DesiredSize;
                var childU = isHorizontal ? child.DesiredSize.Height : child.DesiredSize.Width;

                if (childU > _flowMaxUSizes[flowIndex])
                {
                    _flowMaxUSizes[flowIndex] = childU;
                }

                if (arrange)
                {
                    var childRect = isHorizontal ?
                        new Rect(flowOffset, flowStep * flowIndex, childDesiredSize.Width, itemU) :
                        new Rect(flowStep * flowIndex, flowOffset, itemU, childDesiredSize.Height);

                    child.Arrange(childRect);
                }

                flowOffset += isHorizontal ? childDesiredSize.Width : childDesiredSize.Height;
                flowOffset += itemSpacing;

                _flowOffsets[flowIndex] = flowOffset;

                for (int j = 1; j < flows; j++)
                {
                    var prevIndex = _flowSortedOffsetIndexes[j - 1];
                    var currIndex = _flowSortedOffsetIndexes[j];

                    if (_flowOffsets[prevIndex] > _flowOffsets[currIndex])
                    {
                        _flowSortedOffsetIndexes[j - 1] = currIndex;
                        _flowSortedOffsetIndexes[j] = prevIndex;
                    }
                }
            }

            var end = _flowOffsets.Max();

            if (end != 0)
            {
                end -= itemSpacing;
            }

            if (isHorizontal)
            {
                size.Width = end;

                if (double.IsPositiveInfinity(size.Height))
                {
                    size.Height = _flowMaxUSizes.Sum() + (flowSpacing * (flows - 1));
                }
            }
            else
            {
                size.Height = end;

                if (double.IsPositiveInfinity(size.Width))
                {
                    size.Width = _flowMaxUSizes.Sum() + (flowSpacing * (flows - 1));
                }
            }

            return size;
        }

        /// <inheritdoc/>
        protected override Size MeasureOverride(Size availableSize)
        {
            return Layout(availableSize, false);
        }

        /// <inheritdoc/>
        protected override Size ArrangeOverride(Size finalSize)
        {
            Layout(finalSize, true);

            return finalSize;
        }




        /// <summary>
        /// The DependencyProperty of <see cref="Orientation"/> property
        /// </summary>
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register(nameof(Orientation), typeof(Orientation), typeof(MasonryPanel),
                new FrameworkPropertyMetadata(Orientation.Vertical, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// The DependencyProperty of <see cref="Flows"/> property
        /// </summary>
        public static readonly DependencyProperty FlowsProperty =
            DependencyProperty.Register(nameof(Flows), typeof(int), typeof(MasonryPanel),
                new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.AffectsMeasure), ValidationUtils.IsInRangeOfPosInt);

        /// <summary>
        /// The DependencyProperty of <see cref="FlowSpacing"/> property
        /// </summary>
        public static readonly DependencyProperty FlowSpacingProperty =
            DependencyProperty.Register(nameof(FlowSpacing), typeof(double), typeof(MasonryPanel),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure), ValidationUtils.IsInRangeOfPosDoubleIncludeZero);

        /// <summary>
        /// The DependencyProperty of <see cref="ItemSpacing"/> property
        /// </summary>
        public static readonly DependencyProperty ItemSpacingProperty =
            DependencyProperty.Register(nameof(ItemSpacing), typeof(double), typeof(MasonryPanel),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure), ValidationUtils.IsInRangeOfPosDoubleIncludeZero);


    }
}
