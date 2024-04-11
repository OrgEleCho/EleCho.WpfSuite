using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace EleCho.WpfSuite
{
    internal static class FlexPanelCore
    {
        #region MeasureOverride

        static Size MeasureOverrideOfRowDirection(IFlexPanel self, Size availableSize)
        {
            Size panelDesiredSize = new Size();

            if (self.Wrap == FlexWrap.NoWrap)
            {
                foreach (UIElement child in self.InternalChildren)
                {
                    child.Measure(availableSize);

                    Size childDesiredSize = child.DesiredSize;
                    panelDesiredSize = new Size()
                    {
                        Width = panelDesiredSize.Width + childDesiredSize.Width,
                        Height = Math.Max(panelDesiredSize.Height, childDesiredSize.Height)
                    };
                }

                if (panelDesiredSize.Width > availableSize.Width)
                    panelDesiredSize.Width = availableSize.Width;

                return panelDesiredSize;
            }
            else
            {
                double aggregateHeight = 0;

                foreach (UIElement child in self.InternalChildren)
                {
                    child.Measure(availableSize);

                    Size childDesiredSize = child.DesiredSize;

                    if (panelDesiredSize.Width + childDesiredSize.Width > availableSize.Width)
                    {
                        aggregateHeight += panelDesiredSize.Height;
                        panelDesiredSize.Width = 0;
                        panelDesiredSize.Height = 0;
                    }

                    panelDesiredSize = new Size()
                    {
                        Width = panelDesiredSize.Width + childDesiredSize.Width,
                        Height = Math.Max(panelDesiredSize.Height, childDesiredSize.Height)
                    };
                }

                panelDesiredSize.Height += aggregateHeight;

                return panelDesiredSize;
            }
        }

        static Size MeasureOverrideOfColumnDirection(IFlexPanel self, Size availableSize)
        {
            Size panelDesiredSize = new Size();

            if (self.Wrap == FlexWrap.NoWrap)
            {
                foreach (UIElement child in self.InternalChildren)
                {
                    child.Measure(availableSize);

                    Size childDesiredSize = child.DesiredSize;
                    panelDesiredSize = new Size()
                    {
                        Width = Math.Max(panelDesiredSize.Width, childDesiredSize.Width),
                        Height = panelDesiredSize.Height + childDesiredSize.Height
                    };
                }

                if (panelDesiredSize.Height > availableSize.Height)
                    panelDesiredSize.Height = availableSize.Height;

                return panelDesiredSize;
            }
            else
            {
                double aggregateWidth = 0;

                foreach (UIElement child in self.InternalChildren)
                {
                    child.Measure(availableSize);

                    Size childDesiredSize = child.DesiredSize;

                    if (panelDesiredSize.Height + childDesiredSize.Height > availableSize.Height)
                    {
                        aggregateWidth += panelDesiredSize.Width;
                        panelDesiredSize.Width = 0;
                        panelDesiredSize.Height = 0;
                    }

                    panelDesiredSize = new Size()
                    {
                        Width = Math.Max(panelDesiredSize.Width, childDesiredSize.Width),
                        Height = panelDesiredSize.Height + childDesiredSize.Height
                    };
                }

                panelDesiredSize.Width += aggregateWidth;

                return panelDesiredSize;
            }
        }

        public static Size MeasureOverride(IFlexPanel self, Size availableSize)
        {
            return self.Direction switch
            {
                FlexDirection.Row or FlexDirection.RowReverse => MeasureOverrideOfRowDirection(self, availableSize),
                FlexDirection.Column or FlexDirection.ColumnReverse => MeasureOverrideOfColumnDirection(self,availableSize),
                _ => throw new InvalidOperationException("Invalid Direction")
            };
        }

        #endregion


        #region ArrangeOverride

        private record ChildrenLine
        {
            public ChildrenLine(UIElement[] elements, Size[] elementOffsets, Size[] elementSizes, double selfOffset, double selfSize)
            {
                Elements = elements;
                ElementOffsets = elementOffsets;
                ElementSizes = elementSizes;
                SelfOffset = selfOffset;
                SelfSize = selfSize;
            }

            public UIElement[] Elements { get; }
            public Size[] ElementOffsets { get; }
            public Size[] ElementSizes { get; }
            public double SelfOffset { get; set; }
            public double SelfSize { get; set; }
        }

        /// <summary>
        /// 横向的将元素分割成不同行
        /// </summary>
        /// <param name="children"></param>
        /// <param name="mainAxisPanelSize"></param>
        /// <returns></returns>
        static ChildrenLine[] SplitChildrenWithRowDirection(IFlexPanel self, UIElementCollection children, double mainAxisPanelSize)
        {
            Size currentLineSize = new Size();

            if (self.Wrap == FlexWrap.NoWrap)
            {
                UIElement[] lineChildren = new UIElement[children.Count];
                Size[] lineChildrenOffsets = new Size[children.Count];
                Size[] lineChildrenSizes = new Size[children.Count];

                for (int i = 0; i < children.Count; i++)
                {
                    var child = children[i];
                    var childDesiredSize = child.DesiredSize;

                    currentLineSize = new Size()
                    {
                        Width = currentLineSize.Width + childDesiredSize.Width,
                        Height = Math.Max(currentLineSize.Height, childDesiredSize.Height)
                    };

                    lineChildren[i] = child;
                    lineChildrenSizes[i] = childDesiredSize;
                }

                if (self.Direction == FlexDirection.RowReverse)
                {
                    Array.Reverse(lineChildren);
                    Array.Reverse(lineChildrenOffsets);
                    Array.Reverse(lineChildrenSizes);
                }

                return new ChildrenLine[]
                {
                    new ChildrenLine(lineChildren, lineChildrenOffsets, lineChildrenSizes, 0, currentLineSize.Height)
                };
            }
            else
            {
                bool isWrapReverse = self.Wrap == FlexWrap.WrapReverse;

                List<UIElement> currentLineChildren = new List<UIElement>();
                List<Size> currentLineChildrenSizes = new List<Size>();
                List<ChildrenLine> result = new List<ChildrenLine>();
                for (int i = 0; i < children.Count; i++)
                {
                    var child = children[i];
                    var childDesiredSize = child.DesiredSize;

                    // 长度上限, 该换行了
                    if (currentLineSize.Width + childDesiredSize.Width > mainAxisPanelSize && currentLineChildren.Count != 0)
                    {
                        if (self.Direction == FlexDirection.RowReverse)
                        {
                            currentLineChildren.Reverse();
                            currentLineChildrenSizes.Reverse();
                        }

                        result.Add(
                            new ChildrenLine(
                                currentLineChildren.ToArray(),
                                new Size[currentLineChildren.Count],
                                currentLineChildrenSizes.ToArray(),
                                0,
                                currentLineSize.Height));

                        // reset
                        currentLineChildren.Clear();
                        currentLineChildrenSizes.Clear();
                        currentLineSize = new Size();
                    }

                    // 增长大小
                    currentLineSize = new Size()
                    {
                        Width = currentLineSize.Width + childDesiredSize.Width,
                        Height = Math.Max(currentLineSize.Height, childDesiredSize.Height)
                    };

                    currentLineChildren.Add(child);
                    currentLineChildrenSizes.Add(childDesiredSize);
                }

                if (currentLineChildren.Count != 0)
                {
                    if (self.Direction == FlexDirection.RowReverse)
                    {
                        currentLineChildren.Reverse();
                        currentLineChildrenSizes.Reverse();
                    }

                    result.Add(
                        new ChildrenLine(
                            currentLineChildren.ToArray(),
                            new Size[currentLineChildren.Count],
                            currentLineChildrenSizes.ToArray(),
                            0,
                            currentLineSize.Height));
                }


                if (isWrapReverse)
                    result.Reverse();

                return result.ToArray();
            }
        }

        /// <summary>
        /// 纵向的将元素分割成不同行
        /// </summary>
        /// <param name="children"></param>
        /// <returns></returns>
        static ChildrenLine[] SplitChildrenWithColumnDirection(IFlexPanel self, UIElementCollection children, double mainAxisPanelSize)
        {
            Size currentLineSize = new Size();

            if (self.Wrap == FlexWrap.NoWrap)
            {
                UIElement[] lineChildren = new UIElement[children.Count];
                Size[] lineChildrenOffsets = new Size[children.Count];
                Size[] lineChildrenSizes = new Size[children.Count];

                for (int i = 0; i < children.Count; i++)
                {
                    var child = children[i];
                    var childDesiredSize = child.DesiredSize;

                    currentLineSize = new Size()
                    {
                        Width = Math.Max(currentLineSize.Width, childDesiredSize.Width),
                        Height = currentLineSize.Height + childDesiredSize.Height
                    };

                    lineChildren[i] = child;
                    lineChildrenSizes[i] = childDesiredSize;
                }

                return new ChildrenLine[]
                {
                    new ChildrenLine(lineChildren, lineChildrenOffsets, lineChildrenSizes, 0, currentLineSize.Width)
                };
            }
            else
            {
                bool isWrapReverse = self.Wrap == FlexWrap.WrapReverse;

                List<UIElement> currentLineChildren = new List<UIElement>();
                List<Size> currentLineChildrenSizes = new List<Size>();
                List<ChildrenLine> result = new List<ChildrenLine>();
                for (int i = 0; i < children.Count; i++)
                {
                    var child = children[i];
                    var childDesiredSize = child.DesiredSize;

                    // 长度上限, 该换行了
                    if (currentLineSize.Height + childDesiredSize.Height > mainAxisPanelSize && currentLineChildren.Count != 0)
                    {
                        result.Add(
                            new ChildrenLine(
                                currentLineChildren.ToArray(),
                                new Size[currentLineChildren.Count],
                                currentLineChildrenSizes.ToArray(),
                                0,
                                currentLineSize.Width));

                        // reset
                        currentLineChildren.Clear();
                        currentLineChildrenSizes.Clear();
                        currentLineSize = new Size();
                    }

                    // 增长大小
                    currentLineSize = new Size()
                    {
                        Width = Math.Max(currentLineSize.Width, childDesiredSize.Width),
                        Height = currentLineSize.Height + childDesiredSize.Height,
                    };

                    currentLineChildren.Add(child);
                    currentLineChildrenSizes.Add(childDesiredSize);
                }

                if (currentLineChildren.Count != 0)
                {
                    result.Add(
                        new ChildrenLine(
                            currentLineChildren.ToArray(),
                            new Size[currentLineChildren.Count],
                            currentLineChildrenSizes.ToArray(),
                            0,
                            currentLineSize.Width));
                }


                if (isWrapReverse)
                    result.Reverse();

                return result.ToArray();
            }
        }

        static void ApplyGrowWithRowDirection(IFlexPanel self, ChildrenLine childrenLine, double lineSize)
        {
            double remainingSize = lineSize - childrenLine.ElementSizes.Sum(size => size.Width);

            if (remainingSize < 0)
                return;

            double weightTotal = childrenLine.Elements.Sum(ele => self.AttachedProperties.GetGrow(ele) ?? self.UniformGrow);

            if (weightTotal == 0)
                return;

            for (int i = 0; i < childrenLine.ElementSizes.Length; i++)
            {
                UIElement currentElement = childrenLine.Elements[i];
                Size currentElementSize = childrenLine.ElementSizes[i];
                childrenLine.ElementSizes[i] = currentElementSize with
                {
                    Width = currentElementSize.Width + (self.AttachedProperties.GetGrow(currentElement) ?? self.UniformGrow) / weightTotal * remainingSize
                };
            }
        }

        static void ApplyGrowWithColumnDirection(IFlexPanel self, ChildrenLine childrenLine, double lineSize)
        {
            double remainingSize = lineSize - childrenLine.ElementSizes.Sum(size => size.Height);

            if (remainingSize < 0)
                return;

            double weightTotal = childrenLine.Elements.Sum(ele => self.AttachedProperties.GetGrow(ele) ?? self.UniformGrow);

            if (weightTotal == 0)
                return;

            for (int i = 0; i < childrenLine.ElementSizes.Length; i++)
            {
                UIElement currentElement = childrenLine.Elements[i];
                Size currentElementSize = childrenLine.ElementSizes[i];
                childrenLine.ElementSizes[i] = currentElementSize with
                {
                    Height = currentElementSize.Height + (self.AttachedProperties.GetGrow(currentElement) ?? self.UniformGrow) / weightTotal * remainingSize
                };
            }
        }

        static void ApplyShrinkWithRowDirection(IFlexPanel self, ChildrenLine childrenLine, double lineSize)
        {
            double sizeToShrink = childrenLine.ElementSizes.Sum(size => size.Width) - lineSize;

            if (sizeToShrink < 0)
                return;

            double weightTotal = Enumerable.Range(0, childrenLine.Elements.Length).Sum(index => (self.AttachedProperties.GetGrow(childrenLine.Elements[index]) ?? self.UniformGrow) * childrenLine.ElementSizes[index].Width);

            if (weightTotal == 0)
                return;

            for (int i = 0; i < childrenLine.ElementSizes.Length; i++)
            {
                UIElement currentElement = childrenLine.Elements[i];
                Size currentElementSize = childrenLine.ElementSizes[i];
                childrenLine.ElementSizes[i] = currentElementSize with
                {
                    Width = currentElementSize.Width - (self.AttachedProperties.GetGrow(currentElement) ?? self.UniformGrow) * currentElementSize.Width / weightTotal * sizeToShrink
                };
            }
        }

        static void ApplyShrinkWithColumnDirection(IFlexPanel self, ChildrenLine childrenLine, double lineSize)
        {
            double sizeToShrink = childrenLine.ElementSizes.Sum(size => size.Height) - lineSize;

            if (sizeToShrink < 0)
                return;

            double weightTotal = Enumerable.Range(0, childrenLine.Elements.Length).Sum(index => (self.AttachedProperties.GetGrow(childrenLine.Elements[index]) ?? self.UniformGrow) * childrenLine.ElementSizes[index].Height);

            if (weightTotal == 0)
                return;

            for (int i = 0; i < childrenLine.ElementSizes.Length; i++)
            {
                UIElement currentElement = childrenLine.Elements[i];
                Size currentElementSize = childrenLine.ElementSizes[i];
                childrenLine.ElementSizes[i] = currentElementSize with
                {
                    Height = currentElementSize.Height - (self.AttachedProperties.GetGrow(currentElement) ?? self.UniformGrow) * currentElementSize.Height / weightTotal * sizeToShrink
                };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="childrenLine"></param>
        /// <param name="mainAxisPanelSize"></param>
        /// <returns>在主轴上的位置 (偏移量)</returns>
        static void ApplyMainAlignmentWithRowDirection(IFlexPanel self, ChildrenLine childrenLine, double mainAxisPanelSize)
        {
            double current = 0;
            for (int i = 0; i < childrenLine.ElementSizes.Length; i++)
            {
                childrenLine.ElementOffsets[i].Width = current;
                current += childrenLine.ElementSizes[i].Width;
            }

            if (self.MainAlignment == FlexMainAlignment.Start)
                return;

            double remainingSize = mainAxisPanelSize - childrenLine.ElementSizes.Sum(size => size.Width);

            if (self.MainAlignment == FlexMainAlignment.Center)
            {
                for (int i = 0; i < childrenLine.ElementOffsets.Length; i++)
                    childrenLine.ElementOffsets[i].Width += remainingSize / 2;
            }
            else if (self.MainAlignment == FlexMainAlignment.End)
            {
                for (int i = 0; i < childrenLine.ElementOffsets.Length; i++)
                    childrenLine.ElementOffsets[i].Width += remainingSize;
            }
            else if (self.MainAlignment == FlexMainAlignment.SpaceBetween)
            {
                if (remainingSize < 0 || childrenLine.Elements.Length <= 1)
                    return;

                double every = remainingSize / (childrenLine.ElementSizes.Length - 1);
                for (int i = 0; i < childrenLine.ElementOffsets.Length; i++)
                    childrenLine.ElementOffsets[i].Width += every * i;
            }
            else if (self.MainAlignment == FlexMainAlignment.SpaceAround)
            {
                if (remainingSize < 0)
                    return;

                double every = remainingSize / childrenLine.ElementSizes.Length;
                for (int i = 0; i < childrenLine.ElementOffsets.Length; i++)
                    childrenLine.ElementOffsets[i].Width += every * i + every / 2;
            }
            else
            {
                if (remainingSize < 0)
                    return;

                double every = remainingSize / (childrenLine.ElementSizes.Length + 1);
                for (int i = 0; i < childrenLine.ElementOffsets.Length; i++)
                    childrenLine.ElementOffsets[i].Width += every * i + every;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="childrenLine"></param>
        /// <param name="mainAxisPanelSize"></param>
        /// <returns>在主轴上的位置 (偏移量)</returns>
        static void ApplyMainAlignmentWithColumnDirection(IFlexPanel self, ChildrenLine childrenLine, double mainAxisPanelSize)
        {
            double current = 0;
            for (int i = 0; i < childrenLine.ElementSizes.Length; i++)
            {
                childrenLine.ElementOffsets[i].Height = current;
                current += childrenLine.ElementSizes[i].Height;
            }

            if (self.MainAlignment == FlexMainAlignment.Start)
                return;

            double remainingSize = mainAxisPanelSize - childrenLine.ElementSizes.Sum(size => size.Height);

            if (self.MainAlignment == FlexMainAlignment.Center)
            {
                for (int i = 0; i < childrenLine.ElementOffsets.Length; i++)
                    childrenLine.ElementOffsets[i].Height += remainingSize / 2;
            }
            else if (self.MainAlignment == FlexMainAlignment.End)
            {
                for (int i = 0; i < childrenLine.ElementOffsets.Length; i++)
                    childrenLine.ElementOffsets[i].Height += remainingSize;
            }
            else if (self.MainAlignment == FlexMainAlignment.SpaceBetween)
            {
                if (remainingSize < 0 || childrenLine.Elements.Length <= 1)
                    return;

                double every = remainingSize / (childrenLine.ElementSizes.Length - 1);
                for (int i = 0; i < childrenLine.ElementOffsets.Length; i++)
                    childrenLine.ElementOffsets[i].Height += every * i;
            }
            else if (self.MainAlignment == FlexMainAlignment.SpaceAround)
            {
                if (remainingSize < 0)
                    return;

                double every = remainingSize / childrenLine.ElementSizes.Length;
                for (int i = 0; i < childrenLine.ElementOffsets.Length; i++)
                    childrenLine.ElementOffsets[i].Height += every * i + every / 2;
            }
            else
            {
                if (remainingSize < 0)
                    return;

                double every = remainingSize / (childrenLine.ElementSizes.Length + 1);
                for (int i = 0; i < childrenLine.ElementOffsets.Length; i++)
                    childrenLine.ElementOffsets[i].Height += every * i + every;
            }
        }

        static void ApplyCrossAlignment(IFlexPanel self, ChildrenLine[] childrenLines, double crossAxisPanelSize)
        {
            double current = 0;
            for (int i = 0; i < childrenLines.Length; i++)
            {
                childrenLines[i].SelfOffset = current;
                current += childrenLines[i].SelfSize;
            }

            if (self.CrossAlignment == FlexCrossAlignment.Start)
                return;

            double remainingSize = crossAxisPanelSize - childrenLines.Sum(line => line.SelfSize);

            if (self.CrossAlignment == FlexCrossAlignment.Center)
            {
                for (int i = 0; i < childrenLines.Length; i++)
                    childrenLines[i].SelfOffset += remainingSize / 2;
            }
            else if (self.CrossAlignment == FlexCrossAlignment.End)
            {
                for (int i = 0; i < childrenLines.Length; i++)
                    childrenLines[i].SelfOffset += remainingSize;
            }
            else if (self.CrossAlignment == FlexCrossAlignment.SpaceBetween)
            {
                if (remainingSize < 0)
                    return;

                double every = remainingSize / (childrenLines.Length - 1);
                for (int i = 0; i < childrenLines.Length; i++)
                    childrenLines[i].SelfOffset += every * i;
            }
            else if (self.CrossAlignment == FlexCrossAlignment.SpaceAround)
            {
                if (remainingSize < 0)
                    return;

                double every = remainingSize / childrenLines.Length;
                for (int i = 0; i < childrenLines.Length; i++)
                    childrenLines[i].SelfOffset += every * i + every / 2;
            }
            else if (self.CrossAlignment == FlexCrossAlignment.SpaceEvently)
            {
                if (remainingSize < 0)
                    return;

                double every = remainingSize / (childrenLines.Length + 1);
                for (int i = 0; i < childrenLines.Length; i++)
                    childrenLines[i].SelfOffset += every * i + every;
            }
            else
            {
                double totalWeight = childrenLines.Sum(line => line.SelfSize);
                double lineOffsetCurrent = 0;
                for (int i = 0; i < childrenLines.Length; i++)
                {
                    double growSize = remainingSize * (childrenLines[i].SelfSize / totalWeight);

                    childrenLines[i].SelfOffset += lineOffsetCurrent;
                    childrenLines[i].SelfSize += growSize;
                    lineOffsetCurrent += growSize;
                }
            }
        }

        static void ApplyItemsAlignmentWithRowDirection(IFlexPanel self, ChildrenLine childrenLine)
        {
            for (int i = 0; i < childrenLine.Elements.Length; i++)
            {
                var align = self.AttachedProperties.GetSelfAlignment(childrenLine.Elements[i]) ?? self.ItemsAlignment;
                if (align == FlexItemAlignment.Start)
                    continue;

                if (align == FlexItemAlignment.Stretch)
                {
                    childrenLine.ElementSizes[i].Height = childrenLine.SelfSize;
                    continue;
                }

                double remainingSpace = childrenLine.SelfSize - childrenLine.ElementSizes[i].Height;
                if (align == FlexItemAlignment.Center)
                    childrenLine.ElementOffsets[i].Height += remainingSpace / 2;
                else
                    childrenLine.ElementOffsets[i].Height += remainingSpace;
            }
        }

        static void ApplyItemsAlignmentWithColumnDirection(IFlexPanel self, ChildrenLine childrenLine)
        {
            for (int i = 0; i < childrenLine.Elements.Length; i++)
            {
                var align = self.AttachedProperties.GetSelfAlignment(childrenLine.Elements[i]) ?? self.ItemsAlignment;
                if (align == FlexItemAlignment.Start)
                    continue;

                if (align == FlexItemAlignment.Stretch)
                {
                    childrenLine.ElementSizes[i].Width = childrenLine.SelfSize;
                    continue;
                }

                double remainingSpace = childrenLine.SelfSize - childrenLine.ElementSizes[i].Width;
                if (align == FlexItemAlignment.Center)
                    childrenLine.ElementOffsets[i].Width += remainingSpace / 2;
                else
                    childrenLine.ElementOffsets[i].Width += remainingSpace;
            }
        }

        public static Size ArrangeOverride(IFlexPanel self, Size finalSize)
        {
            if (self.Direction is FlexDirection.Row or FlexDirection.RowReverse)
            {
                // 分割
                ChildrenLine[] lines = SplitChildrenWithRowDirection(self, self.InternalChildren, finalSize.Width);

                // 对每一行进行 grow, shrink, 以及主轴对齐计算
                for (int i = 0; i < lines.Length; i++)
                {
                    ApplyGrowWithRowDirection(self, lines[i], finalSize.Width);
                    ApplyShrinkWithRowDirection(self, lines[i], finalSize.Width);
                    ApplyMainAlignmentWithRowDirection(self, lines[i], finalSize.Width);
                }

                // 交叉轴对齐计算
                ApplyCrossAlignment(self, lines, finalSize.Height);

                // Items 对齐计算
                for (int i = 0; i < lines.Length; i++)
                    ApplyItemsAlignmentWithRowDirection(self, lines[i]);

                for (int i = 0; i < lines.Length; i++)
                {
                    ChildrenLine currentLine = lines[i];

                    for (int j = 0; j < currentLine.Elements.Length; j++)
                    {
                        currentLine.Elements[j].Arrange(
                            new Rect()
                            {
                                X = currentLine.ElementOffsets[j].Width,
                                Y = currentLine.SelfOffset + currentLine.ElementOffsets[j].Height,
                                Size = currentLine.ElementSizes[j]
                            });
                    }
                }
            }
            else
            {
                ChildrenLine[] lines = SplitChildrenWithColumnDirection(self, self.InternalChildren, finalSize.Height);

                for (int i = 0; i < lines.Length; i++)
                {
                    ApplyGrowWithColumnDirection(self, lines[i], finalSize.Height);
                    ApplyShrinkWithColumnDirection(self, lines[i], finalSize.Height);
                    ApplyMainAlignmentWithColumnDirection(self, lines[i], finalSize.Height);
                }

                ApplyCrossAlignment(self, lines, finalSize.Width);

                for (int i = 0; i < lines.Length; i++)
                    ApplyItemsAlignmentWithColumnDirection(self, lines[i]);

                for (int i = 0; i < lines.Length; i++)
                {
                    ChildrenLine currentLine = lines[i];

                    for (int j = 0; j < currentLine.Elements.Length; j++)
                    {
                        currentLine.Elements[j].Arrange(
                            new Rect()
                            {
                                X = currentLine.SelfOffset + currentLine.ElementOffsets[j].Width,
                                Y = currentLine.ElementOffsets[j].Height,
                                Size = currentLine.ElementSizes[j]
                            });
                    }
                }
            }

            return finalSize;
        }

        #endregion
    }
}
