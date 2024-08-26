using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Emit;
using System.Windows;
using System.Windows.Media.Media3D;
using System.Xml.Linq;

namespace EleCho.WpfSuite
{
    public partial class RelativePanel
    {
        private sealed class Graph
        {
            private double _minX;
            private double _maxX;
            private double _minY;
            private double _maxY;
            private bool _isMinCapped;
            private bool _isMaxCapped;
            private bool _knownErrorPending;
            private bool _agErrorCode;
            private Size _availableSizeForNodeResolution;

            public LinkedList<GraphNode> Nodes { get; }

            public Graph()
            {
                _minX = 0.0f;
                _maxX = 0.0f;
                _minY = 0.0f;
                _maxY = 0.0f;
                _isMinCapped = false;
                _isMaxCapped = false;
                _knownErrorPending = false;
                _agErrorCode = false;

                Nodes = new();
            }

            private GraphNode? GetNodeByValue(UIElement? uiElement)
            {
                if (uiElement is null)
                {
                    return null;
                }

                foreach (var node in Nodes)
                {
                    if (node.Element == uiElement)
                    {
                        return node;
                    }
                }

                return null;
            }

            // Starting off with the space that is available to the entire panel
            // (a.k.a. available size), we will constrain this space little by 
            // little based on the ArrangeRects of the dependencies that the
            // node has. The end result corresponds to the MeasureRect of this node. 
            // Consider the following example: if an element is to the left of a 
            // sibling, that means that the space that is available to this element
            // in particular is now the available size minus the Width of the 
            // ArrangeRect associated with this sibling.
            private void CalculateMeasureRectHorizontally(GraphNode node, Size availableSize, out double x, out double width)
            {
                bool isHorizontallyCenteredFromLeft = false;
                bool isHorizontallyCenteredFromRight = false;

                // The initial values correspond to the entire available space. In
                // other words, the edges of the element are aligned to the edges
                // of the panel by default. We will now constrain each side of this
                // space as necessary.
                x = 0.0f;
                width = availableSize.Width;

                // If we have infinite available width, then the Width of the
                // MeasureRect is also infinite; we do not have to constrain it.
                if (availableSize.Width != double.PositiveInfinity)
                {
                    // Constrain the left side of the available space, i.e.
                    // a) The child has its left edge aligned with the panel (default),
                    // b) The child has its left edge aligned with the left edge of a sibling,
                    // or c) The child is positioned to the right of a sibling.
                    //
                    //  |;;                 |               |                                                   
                    //  |;;                 |               |                
                    //  |;;                 |:::::::::::::::|                       ;;:::::::::::::;; 
                    //  |;;                 |;             ;|       .               ;;             ;;
                    //  |;;                 |;             ;|     .;;............   ;;             ;;
                    //  |;;                 |;             ;|   .;;;;::::::::::::   ;;             ;;
                    //  |;;                 |;             ;|    ':;;::::::::::::   ;;             ;;
                    //  |;;                 |;             ;|      ':               ;;             ;;       
                    //  |;;                 |:::::::::::::::|                       :::::::::::::::::
                    //  |;;                 |               |               
                    //  |;;                 |               |
                    //  AlignLeftWithPanel  AlignLeftWith   RightOf
                    //
                    if (!node.IsAlignLeftWithPanel())
                    {
                        if (node.IsAlignLeftWith())
                        {
                            GraphNode alignLeftWithNeighbor = node._alignLeftWithNode!;
                            double restrictedHorizontalSpace = alignLeftWithNeighbor._arrangeRect.X;

                            x = restrictedHorizontalSpace;
                            width -= restrictedHorizontalSpace;
                        }
                        else if (node.IsAlignHorizontalCenterWith())
                        {
                            isHorizontallyCenteredFromLeft = true;
                        }
                        else if (node.IsRightOf())
                        {
                            GraphNode rightOfNeighbor = node._rightOfNode!;
                            double  restrictedHorizontalSpace = rightOfNeighbor._arrangeRect.X + rightOfNeighbor._arrangeRect.Width;

                            x = restrictedHorizontalSpace;
                            width -= restrictedHorizontalSpace;
                        }
                    }

                    // Constrain the right side of the available space, i.e.
                    // a) The child has its right edge aligned with the panel (default),
                    // b) The child has its right edge aligned with the right edge of a sibling,
                    // or c) The child is positioned to the left of a sibling.
                    //  
                    //                                          |               |                   ;;|
                    //                                          |               |                   ;;|
                    //  ;;:::::::::::::;;                       |;:::::::::::::;|                   ;;|
                    //  ;;             ;;               .       |;             ;|                   ;;|
                    //  ;;             ;;   ............;;.     |;             ;|                   ;;|
                    //  ;;             ;;   ::::::::::::;;;;.   |;             ;|                   ;;|
                    //  ;;             ;;   ::::::::::::;;:'    |;             ;|                   ;;|
                    //  ;;             ;;               :'      |;             ;|                   ;;|
                    //  :::::::::::::::::                       |:::::::::::::::|                   ;;|
                    //                                          |               |                   ;;|
                    //                                          |               |                   ;;|
                    //                                          LeftOf          AlignRightWith      AlignRightWithPanel
                    //
                    if (!node.IsAlignRightWithPanel())
                    {
                        if (node.IsAlignRightWith())
                        {
                            GraphNode alignRightWithNeighbor = node._alignRightWithNode!;

                            width -= availableSize.Width - (alignRightWithNeighbor._arrangeRect.X + alignRightWithNeighbor._arrangeRect.Width);
                        }
                        else if (node.IsAlignHorizontalCenterWith())
                        {
                            isHorizontallyCenteredFromRight = true;
                        }
                        else if (node.IsLeftOf())
                        {
                            GraphNode leftOfNeighbor = node._leftOfNode!;

                            width -= availableSize.Width - leftOfNeighbor._arrangeRect.X;
                        }
                    }

                    if (isHorizontallyCenteredFromLeft && isHorizontallyCenteredFromRight)
                    {
                        GraphNode alignHorizontalCenterWithNeighbor = node._alignHorizontalCenterWithNode!;
                        double centerOfNeighbor = alignHorizontalCenterWithNeighbor._arrangeRect.X + (alignHorizontalCenterWithNeighbor._arrangeRect.Width / 2.0f);
                        width = Math.Min(centerOfNeighbor, availableSize.Width - centerOfNeighbor) * 2.0f;
                        x = centerOfNeighbor - (width / 2.0f);
                    }
                }
            }
            private void CalculateMeasureRectVertically(GraphNode node, Size availableSize, out double y, out double height)
            {
                bool isVerticallyCenteredFromTop = false;
                bool isVerticallyCenteredFromBottom = false;

                // The initial values correspond to the entire available space. In
                // other words, the edges of the element are aligned to the edges
                // of the panel by default. We will now constrain each side of this
                // space as necessary.
                y = 0.0f;
                height = availableSize.Height;

                // If we have infinite available height, then the Height of the
                // MeasureRect is also infinite; we do not have to constrain it.
                if (availableSize.Height != double.PositiveInfinity)
                {
                    // Constrain the top of the available space, i.e.
                    // a) The child has its top edge aligned with the panel (default),
                    // b) The child has its top edge aligned with the top edge of a sibling,
                    // or c) The child is positioned to the below a sibling.
                    //
                    //  ================================== AlignTopWithPanel
                    //  ::::::::::::::::::::::::::::::::::
                    //
                    //
                    //
                    //  --------;;=============;;--------- AlignTopWith
                    //          ;;             ;;
                    //          ;;             ;;
                    //          ;;             ;;
                    //          ;;             ;;
                    //          ;;             ;;
                    //  --------::=============::--------- Below 
                    //                  .
                    //                .:;:.
                    //              .:;;;;;:.
                    //                ;;;;;
                    //                ;;;;;
                    //                ;;;;;
                    //                ;;;;;
                    //                ;;;;;
                    //
                    //          ;;:::::::::::::;; 
                    //          ;;             ;;
                    //          ;;             ;;
                    //          ;;             ;;
                    //          ;;             ;;
                    //          ;;             ;;
                    //          :::::::::::::::::
                    //
                    if (!node.IsAlignTopWithPanel())
                    {
                        if (node.IsAlignTopWith())
                        {
                            GraphNode alignTopWithNeighbor = node._alignTopWithNode!;
                            double restrictedVerticalSpace = alignTopWithNeighbor._arrangeRect.Y;

                            y = restrictedVerticalSpace;
                            height -= restrictedVerticalSpace;
                        }
                        else if (node.IsAlignVerticalCenterWith())
                        {
                            isVerticallyCenteredFromTop = true;
                        }
                        else if (node.IsBelow())
                        {
                            GraphNode belowNeighbor = node._belowNode!;
                            double restrictedVerticalSpace = belowNeighbor._arrangeRect.Y + belowNeighbor._arrangeRect.Height;

                            y = restrictedVerticalSpace;
                            height -= restrictedVerticalSpace;
                        }
                    }

                    // Constrain the bottom of the available space, i.e.
                    // a) The child has its bottom edge aligned with the panel (default),
                    // b) The child has its bottom edge aligned with the bottom edge of a sibling,
                    // or c) The child is positioned to the above a sibling.
                    //
                    //          ;;:::::::::::::;; 
                    //          ;;             ;;
                    //          ;;             ;;
                    //          ;;             ;;
                    //          ;;             ;;
                    //          ;;             ;;
                    //          :::::::::::::::::
                    //
                    //                ;;;;;
                    //                ;;;;;
                    //                ;;;;;
                    //                ;;;;;
                    //                ;;;;;
                    //              ..;;;;;..
                    //               ':::::'
                    //                 ':`
                    //                  
                    //  --------;;=============;;--------- Above 
                    //          ;;             ;;
                    //          ;;             ;;
                    //          ;;             ;;
                    //          ;;             ;;
                    //          ;;             ;;
                    //  --------::=============::--------- AlignBottomWith
                    //
                    // 
                    //
                    //  ::::::::::::::::::::::::::::::::::
                    //  ================================== AlignBottomWithPanel
                    //
                    if (!node.IsAlignBottomWithPanel())
                    {
                        if (node.IsAlignBottomWith())
                        {
                            GraphNode alignBottomWithNeighbor = node._alignBottomWithNode!;

                            height -= availableSize.Height - (alignBottomWithNeighbor._arrangeRect.Y + alignBottomWithNeighbor._arrangeRect.Height);
                        }
                        else if (node.IsAlignVerticalCenterWith())
                        {
                            isVerticallyCenteredFromBottom = true;
                        }
                        else if (node.IsAbove())
                        {
                            GraphNode aboveNeighbor = node._aboveNode!;

                            height -= availableSize.Height - aboveNeighbor._arrangeRect.Y;
                        }
                    }

                    if (isVerticallyCenteredFromTop && isVerticallyCenteredFromBottom)
                    {
                        GraphNode alignVerticalCenterWithNeighbor = node._alignVerticalCenterWithNode!;
                        double centerOfNeighbor = alignVerticalCenterWithNeighbor._arrangeRect.Y + (alignVerticalCenterWithNeighbor._arrangeRect.Height / 2.0f);
                        height = Math.Min(centerOfNeighbor, availableSize.Height - centerOfNeighbor) * 2.0f;
                        y = centerOfNeighbor - (height / 2.0f);
                    }
                }
            }

            // The ArrageRect (a.k.a. layout slot) corresponds to the specific rect 
            // within the MeasureRect that will be given to an element for it to
            // position itself. The exact rect is calculated based on anchoring
            // and, unless anchoring dictates otherwise, the size of the
            // ArrangeRect is equal to the desired size of the element itself. 
            // Consider the following example: if the node is right-anchored, the 
            // right side of the ArrangeRect should overlap with the right side
            // of the MeasureRect; this same logic is applied to the other three
            // sides of the rect.
            private void CalculateArrangeRectHorizontally(GraphNode node, out double x, out double width)
            {
                UnsafeRect measureRect = node._measureRect;
                double desiredWidth = Math.Min(measureRect.Width, node.Element.DesiredSize.Width);

                //Debug.Assert(node.IsMeasured() && (measureRect.Width != double.PositiveInfinity));

                // The initial values correspond to the left corner, using the 
                // desired size of element. If no attached properties were set, 
                // this means that the element will default to the left corner of
                // the panel.
                x = measureRect.X;
                width = desiredWidth;

                if (node.IsLeftAnchored)
                {
                    if (node.IsRightAnchored)
                    {
                        x = measureRect.X;
                        width = measureRect.Width;
                    }
                    else
                    {
                        x = measureRect.X;
                        width = desiredWidth;
                    }
                }
                else if (node.IsRightAnchored)
                {
                    x = measureRect.X + measureRect.Width - desiredWidth;
                    width = desiredWidth;
                }
                else if (node.IsHorizontalCenterAnchored)
                {
                    x = measureRect.X + (measureRect.Width / 2.0f) - (desiredWidth / 2.0f);
                    width = desiredWidth;
                }
            }
            private void CalculateArrangeRectVertically(GraphNode node, out double y, out double height)
            {
                UnsafeRect measureRect = node._measureRect;
                double desiredHeight = Math.Min(measureRect.Height, node.Element.DesiredSize.Height);

                //Debug.Assert(node.IsMeasured() && (measureRect.Height != double.PositiveInfinity));

                // The initial values correspond to the top corner, using the 
                // desired size of element. If no attached properties were set, 
                // this means that the element will default to the top corner of
                // the panel.
                y = measureRect.Y;
                height = desiredHeight;

                if (node.IsTopAnchored)
                {
                    if (node.IsBottomAnchored)
                    {
                        y = measureRect.Y;
                        height = measureRect.Height;
                    }
                    else
                    {
                        y = measureRect.Y;
                        height = desiredHeight;
                    }
                }
                else if (node.IsBottomAnchored)
                {
                    y = measureRect.Y + measureRect.Height - desiredHeight;
                    height = desiredHeight;
                }
                else if (node.IsVerticalCenterAnchored)
                {
                    y = measureRect.Y + (measureRect.Height / 2.0f) - (desiredHeight / 2.0f);
                    height = desiredHeight;
                }
            }
            private void MarkHorizontalAndVerticalLeaves()
            {
                foreach (var node in Nodes)
                {
                    node._isHorizontalLeaf = true;
                    node._isVerticalLeaf = true;
                }

                foreach (var node in Nodes)
                {
                    node.UnmarkNeighborsAsHorizontalOrVerticalLeaves();
                }
            }

            private void AccumulatePositiveDesiredWidth(GraphNode node, double x)
            {
                double initialX = x;
                bool isHorizontallyCenteredFromLeft = false;
                bool isHorizontallyCenteredFromRight = false;

                Debug.Assert(node.IsMeasured());

                // If we are going in the positive direction, move the cursor
                // right by the desired width of the node with which we are 
                // currently working and refresh the maximum positive value.
                x += node.Element.DesiredSize.Width;
                _maxX = Math.Max(_maxX, x);

                if (node.IsAlignLeftWithPanel())
                {
                    if (!_isMaxCapped)
                    {
                        _maxX = x;
                        _isMaxCapped = true;
                    }
                }
                else if (node.IsAlignLeftWith())
                {
                    // If the AlignLeftWithNode and AlignRightWithNode are the
                    // same element, we can skip the former, since we will move 
                    // through the latter later.
                    if (node._alignLeftWithNode != node._alignRightWithNode)
                    {
                        AccumulateNegativeDesiredWidth(node._alignLeftWithNode!, x);
                    }
                }
                else if (node.IsAlignHorizontalCenterWith())
                {
                    isHorizontallyCenteredFromLeft = true;
                }
                else if (node.IsRightOf())
                {
                    AccumulatePositiveDesiredWidth(node._rightOfNode!, x);
                }

                if (node.IsAlignRightWithPanel())
                {
                    if (_isMinCapped)
                    {
                        _minX = Math.Min(_minX, initialX);
                    }
                    else
                    {
                        _minX = initialX;
                        _isMinCapped = true;
                    }
                }
                else if (node.IsAlignRightWith())
                {
                    // If this element's right is aligned to some other 
                    // element's right, now we will be going in the positive
                    // direction to that other element in order to continue the
                    // traversal of the dependency chain. But first, since we 
                    // arrived to the node where we currently are by going in
                    // the positive direction, that means that we have already 
                    // moved the cursor right to calculate the maximum positive 
                    // value, so we will use the initial value of Y.
                    AccumulatePositiveDesiredWidth(node._alignRightWithNode!, initialX);
                }
                else if (node.IsAlignHorizontalCenterWith())
                {
                    isHorizontallyCenteredFromRight = true;
                }
                else if (node.IsLeftOf())
                {
                    // If this element is to the left of some other element,
                    // now we will be going in the negative direction to that
                    // other element in order to continue the traversal of the
                    // dependency chain. But first, since we arrived to the
                    // node where we currently are by going in the positive
                    // direction, that means that we have already moved the 
                    // cursor right to calculate the maximum positive value, so
                    // we will use the initial value of X.
                    AccumulateNegativeDesiredWidth(node._leftOfNode!, initialX);
                }

                if (isHorizontallyCenteredFromLeft && isHorizontallyCenteredFromRight)
                {
                    double centerX = x - (node.Element.DesiredSize.Width / 2.0f);
                    double edgeX = centerX - (node._alignHorizontalCenterWithNode!.Element.DesiredSize.Width / 2.0f);
                    _minX = Math.Min(_minX, edgeX);
                    AccumulatePositiveDesiredWidth(node._alignHorizontalCenterWithNode, edgeX);
                }
                else if (node.IsHorizontalCenterAnchored)
                {
                    // If this node is horizontally anchored to the center, then it
                    // means that it is the root of this dependency chain based on
                    // the current definition of precedence for constraints: 
                    // e.g. AlignLeftWithPanel 
                    // > AlignLeftWith 
                    // > RightOf
                    // > AlignHorizontalCenterWithPanel    
                    // Thus, we can report its width as twice the width of 
                    // either the difference from center to left or the difference
                    // from center to right, whichever is the greatest.
                    double centerX = x - (node.Element.DesiredSize.Width / 2.0f);
                    double upper = _maxX - centerX;
                    double lower = centerX - _minX;
                    _maxX = Math.Max(upper, lower) * 2.0f;
                    _minX = 0.0f;
                }
            }
            private void AccumulateNegativeDesiredWidth(GraphNode node, double x)
            {
                double initialX = x;
                bool isHorizontallyCenteredFromLeft = false;
                bool isHorizontallyCenteredFromRight = false;

                Debug.Assert(node.IsMeasured());

                // If we are going in the negative direction, move the cursor
                // left by the desired width of the node with which we are 
                // currently working and refresh the minimum negative value.
                x -= node.Element.DesiredSize.Width;
                _minX = Math.Min(_minX, x);

                if (node.IsAlignRightWithPanel())
                {
                    if (!_isMinCapped)
                    {
                        _minX = x;
                        _isMinCapped = true;
                    }
                }
                else if (node.IsAlignRightWith())
                {
                    // If the AlignRightWithNode and AlignLeftWithNode are the
                    // same element, we can skip the former, since we will move 
                    // through the latter later.
                    if (node._alignRightWithNode != node._alignLeftWithNode)
                    {
                        AccumulatePositiveDesiredWidth(node._alignRightWithNode!, x);
                    }
                }
                else if (node.IsAlignHorizontalCenterWith())
                {
                    isHorizontallyCenteredFromRight = true;
                }
                else if (node.IsLeftOf())
                {
                    AccumulateNegativeDesiredWidth(node._leftOfNode!, x);
                }

                if (node.IsAlignLeftWithPanel())
                {
                    if (_isMaxCapped)
                    {
                        _maxX = Math.Max(_maxX, initialX);
                    }
                    else
                    {
                        _maxX = initialX;
                        _isMaxCapped = true;
                    }
                }
                else if (node.IsAlignLeftWith())
                {
                    // If this element's left is aligned to some other element's
                    // left, now we will be going in the negative direction to 
                    // that other element in order to continue the traversal of
                    // the dependency chain. But first, since we arrived to the
                    // node where we currently are by going in the negative 
                    // direction, that means that we have already moved the 
                    // cursor left to calculate the minimum negative value,
                    // so we will use the initial value of X.
                    AccumulateNegativeDesiredWidth(node._alignLeftWithNode!, initialX);
                }
                else if (node.IsAlignHorizontalCenterWith())
                {
                    isHorizontallyCenteredFromLeft = true;
                }
                else if (node.IsRightOf())
                {
                    // If this element is to the right of some other element,
                    // now we will be going in the positive direction to that
                    // other element in order to continue the traversal of the
                    // dependency chain. But first, since we arrived to the
                    // node where we currently are by going in the negative
                    // direction, that means that we have already moved the 
                    // cursor left to calculate the minimum negative value, so
                    // we will use the initial value of X.
                    AccumulatePositiveDesiredWidth(node._rightOfNode!, initialX);
                }

                if (isHorizontallyCenteredFromLeft && isHorizontallyCenteredFromRight)
                {
                    double centerX = x + (node.Element.DesiredSize.Width / 2.0f);
                    double edgeX = centerX + (node._alignHorizontalCenterWithNode!.Element.DesiredSize.Width / 2.0f);
                    _maxX = Math.Max(_maxX, edgeX);
                    AccumulateNegativeDesiredWidth(node._alignHorizontalCenterWithNode, edgeX);
                }
                else if (node.IsHorizontalCenterAnchored)
                {
                    // If this node is horizontally anchored to the center, then it
                    // means that it is the root of this dependency chain based on
                    // the current definition of precedence for constraints: 
                    // e.g. AlignLeftWithPanel 
                    // > AlignLeftWith 
                    // > RightOf
                    // > AlignHorizontalCenterWithPanel    
                    // Thus, we can report its width as twice the width of 
                    // either the difference from center to left or the difference
                    // from center to right, whichever is the greatest.
                    double centerX = x + (node.Element.DesiredSize.Width / 2.0f);
                    double upper = _maxX - centerX;
                    double lower = centerX - _minX;
                    _maxX = Math.Max(upper, lower) * 2.0f;
                    _minX = 0.0f;
                }
            }
            private void AccumulatePositiveDesiredHeight(GraphNode node, double y)
            {
                double initialY = y;
                bool isVerticallyCenteredFromTop = false;
                bool isVerticallyCenteredFromBottom = false;

                Debug.Assert(node.IsMeasured());

                // If we are going in the positive direction, move the cursor
                // up by the desired height of the node with which we are 
                // currently working and refresh the maximum positive value.
                y += node.Element.DesiredSize.Height;
                _maxY = Math.Max(_maxY, y);

                if (node.IsAlignTopWithPanel())
                {
                    if (!_isMaxCapped)
                    {
                        _maxY = y;
                        _isMaxCapped = true;
                    }
                }
                else if (node.IsAlignTopWith())
                {
                    // If the AlignTopWithNode and AlignBottomWithNode are the
                    // same element, we can skip the former, since we will move 
                    // through the latter later.
                    if (node._alignTopWithNode != node._alignBottomWithNode)
                    {
                        AccumulateNegativeDesiredHeight(node._alignTopWithNode!, y);
                    }
                }
                else if (node.IsAlignVerticalCenterWith())
                {
                    isVerticallyCenteredFromTop = true;
                }
                else if (node.IsBelow())
                {
                    AccumulatePositiveDesiredHeight(node._belowNode!, y);
                }

                if (node.IsAlignBottomWithPanel())
                {
                    if (_isMinCapped)
                    {
                        _minY = Math.Min(_minY, initialY);
                    }
                    else
                    {
                        _minY = initialY;
                        _isMinCapped = true;
                    }
                }
                else if (node.IsAlignBottomWith())
                {
                    // If this element's bottom is aligned to some other 
                    // element's bottom, now we will be going in the positive
                    // direction to that other element in order to continue the
                    // traversal of the dependency chain. But first, since we 
                    // arrived to the node where we currently are by going in
                    // the positive direction, that means that we have already 
                    // moved the cursor up to calculate the maximum positive 
                    // value, so we will use the initial value of Y.
                    AccumulatePositiveDesiredHeight(node._alignBottomWithNode!, initialY);
                }
                else if (node.IsAlignVerticalCenterWith())
                {
                    isVerticallyCenteredFromBottom = true;
                }
                else if (node.IsAbove())
                {
                    // If this element is above some other element, now we will 
                    // be going in the negative direction to that other element
                    // in order to continue the traversal of the dependency  
                    // chain. But first, since we arrived to the node where we 
                    // currently are by going in the positive direction, that
                    // means that we have already moved the cursor up to 
                    // calculate the maximum positive value, so we will use
                    // the initial value of Y.
                    AccumulateNegativeDesiredHeight(node._aboveNode!, initialY);
                }

                if (isVerticallyCenteredFromTop && isVerticallyCenteredFromBottom)
                {
                    double centerY = y - (node.Element.DesiredSize.Height / 2.0f);
                    double edgeY = centerY - (node._alignVerticalCenterWithNode!.Element.DesiredSize.Height / 2.0f);
                    _minY = Math.Min(_minY, edgeY);
                    AccumulatePositiveDesiredHeight(node._alignVerticalCenterWithNode, edgeY);
                }
                else if (node.IsVerticalCenterAnchored)
                {
                    // If this node is vertically anchored to the center, then it
                    // means that it is the root of this dependency chain based on
                    // the current definition of precedence for constraints: 
                    // e.g. AlignTopWithPanel 
                    // > AlignTopWith
                    // > Below
                    // > AlignVerticalCenterWithPanel 
                    // Thus, we can report its height as twice the height of 
                    // either the difference from center to top or the difference
                    // from center to bottom, whichever is the greatest.
                    double centerY = y - (node.Element.DesiredSize.Height / 2.0f);
                    double upper = _maxY - centerY;
                    double lower = centerY - _minY;
                    _maxY = Math.Max(upper, lower) * 2.0f;
                    _minY = 0.0f;
                }
            }
            private void AccumulateNegativeDesiredHeight(GraphNode node, double y)
            {
                double initialY = y;
                bool isVerticallyCenteredFromTop = false;
                bool isVerticallyCenteredFromBottom = false;

                Debug.Assert(node.IsMeasured());

                // If we are going in the negative direction, move the cursor
                // down by the desired height of the node with which we are 
                // currently working and refresh the minimum negative value.
                y -= node.Element.DesiredSize.Height;
                _minY = Math.Min(_minY, y);

                if (node.IsAlignBottomWithPanel())
                {
                    if (!_isMinCapped)
                    {
                        _minY = y;
                        _isMinCapped = true;
                    }
                }
                else if (node.IsAlignBottomWith())
                {
                    // If the AlignBottomWithNode and AlignTopWithNode are the
                    // same element, we can skip the former, since we will move 
                    // through the latter later.
                    if (node._alignBottomWithNode != node._alignTopWithNode)
                    {
                        AccumulatePositiveDesiredHeight(node._alignBottomWithNode!, y);
                    }
                }
                else if (node.IsAlignVerticalCenterWith())
                {
                    isVerticallyCenteredFromBottom = true;
                }
                else if (node.IsAbove())
                {
                    AccumulateNegativeDesiredHeight(node._aboveNode!, y);
                }

                if (node.IsAlignTopWithPanel())
                {
                    if (_isMaxCapped)
                    {
                        _maxY = Math.Max(_maxY, initialY);
                    }
                    else
                    {
                        _maxY = initialY;
                        _isMaxCapped = true;
                    }
                }
                else if (node.IsAlignTopWith())
                {
                    // If this element's top is aligned to some other element's
                    // top, now we will be going in the negative direction to 
                    // that other element in order to continue the traversal of
                    // the dependency chain. But first, since we arrived to the
                    // node where we currently are by going in the negative 
                    // direction, that means that we have already moved the 
                    // cursor down to calculate the minimum negative value,
                    // so we will use the initial value of Y.
                    AccumulateNegativeDesiredHeight(node._alignTopWithNode!, initialY);
                }
                else if (node.IsAlignVerticalCenterWith())
                {
                    isVerticallyCenteredFromTop = true;
                }
                else if (node.IsBelow())
                {
                    // If this element is below some other element, now we'll
                    // be going in the positive direction to that other element  
                    // in order to continue the traversal of the dependency
                    // chain. But first, since we arrived to the node where we
                    // currently are by going in the negative direction, that
                    // means that we have already moved the cursor down to
                    // calculate the minimum negative value, so we will use
                    // the initial value of Y.
                    AccumulatePositiveDesiredHeight(node._belowNode!, initialY);
                }

                if (isVerticallyCenteredFromTop && isVerticallyCenteredFromBottom)
                {
                    double centerY = y + (node.Element.DesiredSize.Height / 2.0f);
                    double edgeY = centerY + (node._alignVerticalCenterWithNode!.Element.DesiredSize.Height / 2.0f);
                    _maxY = Math.Max(_maxY, edgeY);
                    AccumulateNegativeDesiredHeight(node._alignVerticalCenterWithNode, edgeY);
                }
                else if (node.IsVerticalCenterAnchored)
                {
                    // If this node is vertically anchored to the center, then it
                    // means that it is the root of this dependency chain based on
                    // the current definition of precedence for constraints: 
                    // e.g. AlignTopWithPanel 
                    // > AlignTopWith
                    // > Below
                    // > AlignVerticalCenterWithPanel 
                    // Thus, we can report its height as twice the height of 
                    // either the difference from center to top or the difference
                    // from center to bottom, whichever is the greatest.
                    double centerY = y + (node.Element.DesiredSize.Height / 2.0f);
                    double upper = _maxY - centerY;
                    double lower = centerY - _minY;
                    _maxY = Math.Max(upper, lower) * 2.0f;
                    _minY = 0.0f;
                }
            }

            // Calculates the MeasureRect of a node and then calls Measure on the
            // corresponding element by passing the Width and Height of this rect.
            // Given that the calculation of the MeasureRect requires the 
            // ArrangeRects of the dependencies, we call this method recursively on
            // said dependencies first and calculate both rects as we go. In other
            // words, this method is figuratively a combination of a measure pass 
            // plus a pseudo-arrange pass.
            private void MeasureNode(GraphNode? node, Size availableSize)
            {
                if (node is null)
                {
                    return;
                }

                if (node.IsPending())
                {
                    // If the node is already in the process of being resolved
                    // but we tried to resolve it again, that means we are in the
                    // middle of circular dependency and we must throw an 
                    // InvalidOperationException. We will fail fast here and let
                    // the CRelativePanel handle the rest.
                    _knownErrorPending = true;
                    //m_agErrorCode = AG_E_RELATIVEPANEL_CIRCULAR_DEP;
                    //E_FAIL;

                    throw new InvalidOperationException("Circular dependency detected");
                }
                else if (node.IsUnresolved())
                {
                    Size constrainedAvailableSize = new();

                    // We must resolve the dependencies of this node first.
                    // In the meantime, we will mark the state as pending.
                    node.SetPending(true);

                    MeasureNode(node._leftOfNode, availableSize);
                    MeasureNode(node._aboveNode, availableSize);
                    MeasureNode(node._rightOfNode, availableSize);
                    MeasureNode(node._belowNode, availableSize);
                    MeasureNode(node._alignLeftWithNode, availableSize);
                    MeasureNode(node._alignTopWithNode, availableSize);
                    MeasureNode(node._alignRightWithNode, availableSize);
                    MeasureNode(node._alignBottomWithNode, availableSize);
                    MeasureNode(node._alignHorizontalCenterWithNode, availableSize);
                    MeasureNode(node._alignVerticalCenterWithNode, availableSize);

                    node.SetPending(false);

                    CalculateMeasureRectHorizontally(node, availableSize, out node._measureRect.X, out node._measureRect.Width);
                    CalculateMeasureRectVertically(node, availableSize, out node._measureRect.Y, out node._measureRect.Height);

                    constrainedAvailableSize.Width = Math.Max(node._measureRect.Width, 0.0f);
                    constrainedAvailableSize.Height = Math.Max(node._measureRect.Height, 0.0f);
                    node.Element.Measure(constrainedAvailableSize);
                    node.SetMeasured(true);

                    // (Pseudo-) Arranging against infinity does not make sense, so 
                    // we will skip the calculations of the ArrangeRects if 
                    // necessary. During the true arrange pass, we will be given a
                    // non-infinite final size; we will do the necessary
                    // calculations until then.
                    if (availableSize.Width != double.PositiveInfinity)
                    {
                        CalculateArrangeRectHorizontally(node, out node._arrangeRect.X, out node._arrangeRect.Width);
                        node.SetArrangedHorizontally(true);
                    }

                    if (availableSize.Height != double.PositiveInfinity)
                    {
                        CalculateArrangeRectVertically(node, out node._arrangeRect.Y, out node._arrangeRect.Height);
                        node.SetArrangedVertically(true);
                    }
                }
            }

            // Calculates the X and Width properties of the ArrangeRect of a node
            // as well as the X and Width properties of the MeasureRect (which is
            // necessary in order to calculate the former correctly). Given that 
            // the calculation of the MeasureRect requires the ArrangeRects of the
            // dependencies, we call this method recursively on said dependencies
            // first.
            private void ArrangeNodeHorizontally(GraphNode? node, Size finalSize)
            {
                if (node is null)
                {
                    return;
                }

                if (!node.IsArrangedHorizontally())
                {
                    // We must resolve dependencies first.
                    ArrangeNodeHorizontally(node._leftOfNode, finalSize);
                    ArrangeNodeHorizontally(node._aboveNode, finalSize);
                    ArrangeNodeHorizontally(node._rightOfNode, finalSize);
                    ArrangeNodeHorizontally(node._belowNode, finalSize);
                    ArrangeNodeHorizontally(node._alignLeftWithNode, finalSize);
                    ArrangeNodeHorizontally(node._alignTopWithNode, finalSize);
                    ArrangeNodeHorizontally(node._alignRightWithNode, finalSize);
                    ArrangeNodeHorizontally(node._alignBottomWithNode, finalSize);
                    ArrangeNodeHorizontally(node._alignHorizontalCenterWithNode, finalSize);
                    ArrangeNodeHorizontally(node._alignVerticalCenterWithNode, finalSize);

                    CalculateMeasureRectHorizontally(node, finalSize, out node._measureRect.X, out node._measureRect.Width);
                    CalculateArrangeRectHorizontally(node, out node._arrangeRect.X, out node._arrangeRect.Width);

                    node.SetArrangedHorizontally(true);
                }
            }

            // Calculates the Y and Height properties of the ArrangeRect of a node
            // as well as the Y and Height properties of the MeasureRect (which is
            // necessary in order to calculate the former correctly).Given that 
            // the calculation of the MeasureRect requires the ArrangeRects of the
            // dependencies, we call this method recursively on said dependencies
            // first.
            private void ArrangeNodeVertically(GraphNode? node, Size finalSize)
            {
                if (node is null)
                {
                    return;
                }

                if (!node.IsArrangedVertically())
                {
                    // We must resolve dependencies first.
                    ArrangeNodeVertically(node._leftOfNode, finalSize);
                    ArrangeNodeVertically(node._aboveNode, finalSize);
                    ArrangeNodeVertically(node._rightOfNode, finalSize);
                    ArrangeNodeVertically(node._belowNode, finalSize);
                    ArrangeNodeVertically(node._alignLeftWithNode, finalSize);
                    ArrangeNodeVertically(node._alignTopWithNode, finalSize);
                    ArrangeNodeVertically(node._alignRightWithNode, finalSize);
                    ArrangeNodeVertically(node._alignBottomWithNode, finalSize);
                    ArrangeNodeVertically(node._alignHorizontalCenterWithNode, finalSize);
                    ArrangeNodeVertically(node._alignVerticalCenterWithNode, finalSize);

                    CalculateMeasureRectVertically(node, finalSize, out node._measureRect.Y, out node._measureRect.Height);
                    CalculateArrangeRectVertically(node, out node._arrangeRect.Y, out node._arrangeRect.Height);

                    node.SetArrangedVertically(true);
                }
            }

            private void ResolveConstraint(GraphNode node)
            {
                if (RelativePanel.GetLeftOf(node.Element) is UIElement leftOf)
                {
                    var leftOfNode = GetNodeByValue(leftOf);
                    node.SetLeftOfConstraint(leftOfNode);
                }

                if (RelativePanel.GetAbove(node.Element) is UIElement above)
                {
                    var aboveNode = GetNodeByValue(above);
                    node.SetAboveConstraint(aboveNode);
                }

                if (RelativePanel.GetRightOf(node.Element) is UIElement rightOf)
                {
                    var rightOfNode = GetNodeByValue(rightOf);
                    node.SetRightOfConstraint(rightOfNode);
                }

                if (RelativePanel.GetBelow(node.Element) is UIElement below)
                {
                    var belowNode = GetNodeByValue(below);
                    node.SetBelowConstraint(belowNode);
                }

                if (RelativePanel.GetAlignHorizontalCenterWith(node.Element) is UIElement alignHorizontalCenterWith)
                {
                    var alignHorizontalCenterWithNode = GetNodeByValue(alignHorizontalCenterWith);
                    node.SetAlignHorizontalCenterWithConstraint(alignHorizontalCenterWithNode);
                }

                if (RelativePanel.GetAlignVerticalCenterWith(node.Element) is UIElement alignVerticalCenterWith)
                {
                    var alignVerticalCenterWithNode = GetNodeByValue(alignVerticalCenterWith);
                    node.SetAlignVerticalCenterWithConstraint(alignVerticalCenterWithNode);
                }

                if (RelativePanel.GetAlignLeftWith(node.Element) is UIElement alignLeftWith)
                {
                    var alignLeftWithNode = GetNodeByValue(alignLeftWith);
                    node.SetAlignLeftWithConstraint(alignLeftWithNode);
                }

                if (RelativePanel.GetAlignTopWith(node.Element) is UIElement alignTopWith)
                {
                    var alignTopWithNode = GetNodeByValue(alignTopWith);
                    node.SetAlignTopWithConstraint(alignTopWithNode);
                }

                if (RelativePanel.GetAlignRightWith(node.Element) is UIElement alignRightWith)
                {
                    var alignRightWithNode = GetNodeByValue(alignRightWith);
                    node.SetAlignRightWithConstraint(alignRightWithNode);
                }

                if (RelativePanel.GetAlignBottomWith(node.Element) is UIElement alignBottomWith)
                {
                    var alignBottomWithNode = GetNodeByValue(alignBottomWith);
                    node.SetAlignBottomWithConstraint(alignBottomWithNode);
                }

                node.SetAlignLeftWithPanelConstraint(GetAlignLeftWithPanel(node.Element));
                node.SetAlignTopWithPanelConstraint(GetAlignTopWithPanel(node.Element));
                node.SetAlignRightWithPanelConstraint(GetAlignRightWithPanel(node.Element));
                node.SetAlignBottomWithPanelConstraint(GetAlignBottomWithPanel(node.Element));
                node.SetAlignHorizontalCenterWithPanelConstraint(GetAlignHorizontalCenterWithPanel(node.Element));
                node.SetAlignVerticalCenterWithPanelConstraint(GetAlignVerticalCenterWithPanel(node.Element));
            }

            public void ResolveConstraints()
            {
                foreach (var node in Nodes)
                {
                    ResolveConstraint(node);
                }
            }

            public void MeasureNodes(Size availableSize)
            {
                foreach (var node in Nodes)
                {
                    MeasureNode(node, availableSize);
                }

                _availableSizeForNodeResolution = availableSize;
            }

            public void ArrangeNodes(Rect finalRect)
            {
                Size finalSize = new Size(finalRect.Width, finalRect.Height);

                // If the final size is the same as the available size that we used
                // to measure the nodes, this means that the pseudo-arrange pass  
                // that we did during the measure pass is, in fact, valid and the 
                // ArrangeRects that were calculated for each node are correct. In 
                // other words, we can just go ahead and call arrange on each
                // element. However, if the width and/or height of the final size
                // differs (e.g. when the element's HorizontalAlignment and/or
                // VerticalAlignment is something other than Stretch and thus the final
                // size corresponds to the desired size of the panel), we must first
                // recalculate the horizontal and/or vertical values of the ArrangeRects,
                // respectively.
                if (_availableSizeForNodeResolution.Width != finalSize.Width)
                {
                    foreach (GraphNode node in Nodes)
                    {
                        node.SetArrangedHorizontally(false);
                    }

                    foreach (GraphNode node in Nodes)
                    {
                        ArrangeNodeHorizontally(node, finalSize);
                    }
                }

                if (_availableSizeForNodeResolution.Height != finalSize.Height)
                {
                    foreach (GraphNode node in Nodes)
                    {
                        node.SetArrangedVertically(false);
                    }

                    foreach (GraphNode node in Nodes)
                    {
                        ArrangeNodeVertically(node, finalSize);
                    }
                }

                _availableSizeForNodeResolution = finalSize;

                foreach (GraphNode node in Nodes)
                {
                    Debug.Assert(node.IsArranged());

                    Rect layoutSlot = new Rect(
                        Math.Max(node._arrangeRect.X + finalRect.X, 0.0f),
                        Math.Max(node._arrangeRect.Y + finalRect.Y, 0.0f),
                        Math.Max(node._arrangeRect.Width, 0.0f),
                        Math.Max(node._arrangeRect.Height, 0.0f));

                    node.Element.Arrange(layoutSlot);
                }

            }

            public Size CalculateDesiredSize()
            {
                Size maxDesiredSize = new(0.0, 0.0);

                MarkHorizontalAndVerticalLeaves();

                foreach (var node in Nodes)
                {
                    if (node._isHorizontalLeaf)
                    {
                        _minX = 0.0f;
                        _maxX = 0.0f;
                        _isMinCapped = false;
                        _isMaxCapped = false;

                        AccumulatePositiveDesiredWidth(node, 0.0f);
                        maxDesiredSize.Width = Math.Max(maxDesiredSize.Width, _maxX - _minX);
                    }

                    if (node._isVerticalLeaf)
                    {
                        _minY = 0.0f;
                        _maxY = 0.0f;
                        _isMinCapped = false;
                        _isMaxCapped = false;

                        AccumulatePositiveDesiredHeight(node, 0.0f);
                        maxDesiredSize.Height = Math.Max(maxDesiredSize.Height, _maxY - _minY);
                    }
                }

                return maxDesiredSize;
            }
        }
    }
}
