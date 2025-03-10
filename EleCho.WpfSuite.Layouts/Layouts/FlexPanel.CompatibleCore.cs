﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EleCho.WpfSuite.Layouts;

#if !NET5_0_OR_GREATER
public partial class FlexPanel
{
    private static void FillItemsAndLines(
        UIElementCollection elements,
        List<FlexElement> items,
        List<FlexElementSpan> lines,
        Orientation orientation,
        FlexItemAlignment itemAlignment,
        Size availableSize,
        double availableU,
        double availableV,
        double mainSpacing,
        double crossSpacing,
        bool wrap,
        double uniformGrow,
        double uniformShrink,
        out double lineMaxU,
        out double linesTotalV)
    {
        items.Clear();
        lines.Clear();

        // add all child to flex item list
        foreach (UIElement child in elements)
        {
            var order = GetOrder(child);
            var grow = GetGrow(child);
            var shrink = GetShrink(child);
            var alignment = GetAlignment(child);

            var desiredSize = child.DesiredSize;
            var childU = GetU(desiredSize, orientation);
            var childV = GetV(desiredSize, orientation);

            if (double.IsNaN(order))
            {
                order = 0;
            }

            if (double.IsNaN(grow))
            {
                grow = uniformGrow;
            }

            if (double.IsNaN(shrink))
            {
                shrink = uniformShrink;
            }

            if (alignment == FlexItemAlignment.Auto)
            {
                alignment = itemAlignment;

                if (alignment == FlexItemAlignment.Auto)
                {
                    alignment = FlexItemAlignment.Stretch;
                }
            }

            items.Add(new FlexElement(child, 0, 0, childU, childV, order, grow, shrink, alignment));
        }

        items.Sort(CompareFlexItem);

        double currentSizeU = 0;
        double currentSizeV = 0;

        lineMaxU = 0;
        linesTotalV = 0;

        int lineStart = 0;
        for (int i = 0; i < items.Count; i++)
        {
            var item = items[i];

            if (wrap && currentSizeU + mainSpacing + item.SizeU > availableU)
            {
                AddLine(lines, ref lineStart, i, crossSpacing, ref lineMaxU, ref linesTotalV, ref currentSizeU, ref currentSizeV);
            }

            if (item.SizeV > currentSizeV)
            {
                currentSizeV = item.SizeV;
            }

            if (currentSizeU != 0)
            {
                item.OffsetU += mainSpacing;
                currentSizeU += mainSpacing;
            }

            currentSizeU += item.SizeU;
            items[i] = item;
        }

        if (items.Count - lineStart > 0)
        {
            AddLine(lines, ref lineStart, items.Count, crossSpacing, ref lineMaxU, ref linesTotalV, ref currentSizeU, ref currentSizeV);
        }

        static int CompareFlexItem(FlexElement item1, FlexElement item2)
        {
            return item1.Order - item2.Order;
        }

        static void AddLine(
            List<FlexElementSpan> lines,
            ref int lineStart, int currentIndex, double crossSpacing,
            ref double lineMaxU, ref double linesTotalV, ref double currentSizeU, ref double currentSizeV)
        {
            var line = new FlexElementSpan(lineStart, currentIndex - lineStart, 0, 0, currentSizeU, currentSizeV);

            if (line.SizeU > lineMaxU)
            {
                lineMaxU = line.SizeU;
            }

            if (linesTotalV != 0)
            {
                line.OffsetV += crossSpacing;
                linesTotalV += crossSpacing;
            }

            linesTotalV += line.SizeV;

            lines.Add(line);

            currentSizeU = 0;
            currentSizeV = 0;
            lineStart = currentIndex;
        }
    }

    private static void GrowAndShrinkItems(
        List<FlexElement> items,
        List<FlexElementSpan> lines,
        double availableU,
        double availableV)
    {
        for (int i = 0; i < lines.Count; i++)
        {
            var line = lines[i];
            var otherSpace = availableU - line.SizeU;
            var totalGrow = 0.0;
            var totalShrink = 0.0;

            for (int j = 0; j < line.Length; j++)
            {
                var childIndex = line.Start + j;
                var child = items[childIndex];
                totalGrow += child.Grow;
                totalGrow += child.Shrink;
            }

            if (otherSpace > 0 && totalGrow != 0)
            {
                for (int j = 0; j < line.Length; j++)
                {
                    var childIndex = line.Start + j;
                    var child = items[childIndex];
                    child.SizeU += otherSpace * child.Grow / totalGrow;

                    items[childIndex] = child;
                }

                line.SizeU = availableU;
            }
            else if (otherSpace < 0 && totalShrink != 0)
            {
                for (int j = 0; j < line.Length; j++)
                {
                    var childIndex = line.Start + j;
                    var child = items[childIndex];
                    child.SizeU += otherSpace * child.Shrink / totalShrink;

                    items[childIndex] = child;
                }

                line.SizeU = availableU;
            }

            lines[i] = line;
        }
    }

    private static void LayoutLines(
        List<FlexElement> items,
        List<FlexElementSpan> lines,
        double availableU,
        double availableV,
        double linesTotalV,
        FlexMainAlignment mainAlignment,
        FlexCrossAlignment crossAlignment)
    {
        switch (mainAlignment)
        {
            case FlexMainAlignment.Center:
            {
                for (int lineIndex = 0; lineIndex < lines.Count; lineIndex++)
                {
                    var line = lines[lineIndex];
                    var otherSpace = availableU - line.SizeU;
                    line.OffsetU += otherSpace / 2;

                    lines[lineIndex] = line;
                }
                break;
            }
            case FlexMainAlignment.End:
            {
                for (int lineIndex = 0; lineIndex < lines.Count; lineIndex++)
                {
                    var line = lines[lineIndex];
                    var otherSpace = availableU - line.SizeU;
                    line.OffsetU += otherSpace;

                    lines[lineIndex] = line;
                }
                break;
            }
            case FlexMainAlignment.SpaceBetween:
            {
                for (int lineIndex = 0; lineIndex < lines.Count; lineIndex++)
                {
                    var line = lines[lineIndex];
                    var otherSpace = availableU - line.SizeU;
                    var spaceBetweenItems = otherSpace / (line.Length - 1);

                    for (int i = 1; i < line.Length; i++)
                    {
                        var itemIndex = line.Start + i;
                        var item = items[itemIndex];
                        item.OffsetU += spaceBetweenItems;
                        items[itemIndex] = item;
                    }

                    lines[lineIndex] = line;
                }
                break;
            }
            case FlexMainAlignment.SpaceAround:
            {
                for (int lineIndex = 0; lineIndex < lines.Count; lineIndex++)
                {
                    var line = lines[lineIndex];
                    var otherSpace = availableU - line.SizeU;
                    var spaceBetweenItems = otherSpace / line.Length;

                    var firstItem = items[line.Start];
                    firstItem.OffsetU += spaceBetweenItems / 2;
                    for (int i = 1; i < line.Length; i++)
                    {
                        var itemIndex = line.Start + i;
                        var item = items[itemIndex];
                        item.OffsetU += spaceBetweenItems;
                        items[itemIndex] = item;
                    }

                    lines[lineIndex] = line;
                }
                break;
            }
            case FlexMainAlignment.SpaceEvenly:
            {
                for (int lineIndex = 0; lineIndex < lines.Count; lineIndex++)
                {
                    var line = lines[lineIndex];
                    var otherSpace = availableU - line.SizeU;
                    var spaceBetweenItems = otherSpace / (line.Length + 1);

                    for (int i = 0; i < line.Length; i++)
                    {
                        var itemIndex = line.Start + i;
                        var item = items[itemIndex];
                        item.OffsetU += spaceBetweenItems;
                        items[itemIndex] = item;
                    }

                    lines[lineIndex] = line;
                }
                break;
            }
        }

        switch (crossAlignment)
        {
            case FlexCrossAlignment.Center:
            {
                var otherSpace = availableV - linesTotalV;
                var firstLine = lines[0];
                firstLine.OffsetV += otherSpace / 2;
                lines[0] = firstLine;
                break;
            }
            case FlexCrossAlignment.End:
            {
                var otherSpace = availableV - linesTotalV;
                var firstLine = lines[0];
                firstLine.OffsetV += otherSpace;
                lines[0] = firstLine;
                break;
            }
            case FlexCrossAlignment.Stretch:
            {
                var otherSpace = availableV - linesTotalV;
                var expand = otherSpace / lines.Count;
                for (int lineIndex = 0; lineIndex < lines.Count; lineIndex++)
                {
                    var line = lines[lineIndex];
                    line.SizeV += expand;

                    lines[lineIndex] = line;
                }
                break;
            }
            case FlexCrossAlignment.SpaceBetween:
            {
                var otherSpace = availableV - linesTotalV;
                var spaceBetweenItems = otherSpace / (lines.Count - 1);

                for (int i = 1; i < lines.Count; i++)
                {
                    var line = lines[i];
                    line.OffsetV += spaceBetweenItems;
                    lines[i] = line;
                }
                break;
            }
            case FlexCrossAlignment.SpaceAround:
            {
                var otherSpace = availableV - linesTotalV;
                var spaceBetweenItems = otherSpace / lines.Count;

                var firstLine = lines[0];
                firstLine.OffsetV += spaceBetweenItems / 2;
                lines[0] = firstLine;
                for (int i = 1; i < lines.Count; i++)
                {
                    var line = lines[i];
                    line.OffsetV += spaceBetweenItems;
                    lines[i] = line;
                }
                break;
            }
            case FlexCrossAlignment.SpaceEvenly:
            {
                var otherSpace = availableV - linesTotalV;
                var spaceBetweenItems = otherSpace / (lines.Count + 1);

                for (int i = 0; i < lines.Count; i++)
                {
                    var line = lines[i];
                    line.OffsetV += spaceBetweenItems;
                    lines[i] = line;
                }
                break;
            }
        }
    }

    private static void LayoutItems(
        List<FlexElement> items,
        List<FlexElementSpan> lines)
    {
        foreach (var line in lines)
        {
            for(int lineItemIndexOffset = 0; lineItemIndexOffset < line.Length; lineItemIndexOffset++)
            {
                var lineItemIndex = line.Start + lineItemIndexOffset;
                var lineItem = items[lineItemIndex];

                switch (lineItem.Alignment)
                {
                    case FlexItemAlignment.Stretch:
                    {
                        lineItem.SizeV = line.SizeV;
                        break;
                    }
                    case FlexItemAlignment.Center:
                    {
                        lineItem.OffsetV = (line.SizeV - lineItem.SizeV) / 2;
                        break;
                    }
                    case FlexItemAlignment.End:
                    {
                        lineItem.OffsetV = line.SizeV - lineItem.SizeV;
                        break;
                    }
                }

                items[lineItemIndex] = lineItem;
            }
        }
    }

    private static void ArrangeFlex(
        List<FlexElement> items,
        List<FlexElementSpan> lines,
        Orientation orientation,
        double availableU,
        double availableV,
        bool reverseLines,
        bool reverseItems,
        FlexMainAlignment mainAlignment, FlexCrossAlignment crossAlignment)
    {

        double currentPositionV = 0;

        if (reverseLines)
        {
            currentPositionV = availableV;
        }

        foreach (var line in lines)
        {
            double currentPositionU = 0;

            if (reverseItems)
            {
                currentPositionU = availableU;
            }

            if (!reverseLines)
            {
                currentPositionV += line.OffsetV;
            }
            else
            {
                currentPositionV -= line.OffsetV;
                currentPositionV -= line.SizeV;
            }

            if (!reverseItems)
            {
                currentPositionU += line.OffsetU;
            }
            else
            {
                currentPositionU -= line.OffsetU;
            }

            for (int itemIndexOffset = 0; itemIndexOffset < line.Length; itemIndexOffset++)
            {
                var itemIndex = line.Start + itemIndexOffset;
                var lineItem = items[itemIndex];

                if (!reverseItems)
                {
                    currentPositionU += lineItem.OffsetU;
                    ArrangeItem(lineItem.Element, orientation, currentPositionU, currentPositionV, lineItem.SizeU, lineItem.SizeV);
                    currentPositionU += lineItem.SizeU;
                }
                else
                {
                    currentPositionU -= lineItem.OffsetU;
                    currentPositionU -= lineItem.SizeU;
                    ArrangeItem(lineItem.Element, orientation, currentPositionU, currentPositionV, lineItem.SizeU, lineItem.SizeV);
                }
            }

            if (!reverseLines)
            {
                currentPositionV += line.SizeV;
            }
        }
    }
}

#endif