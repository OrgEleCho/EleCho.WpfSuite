using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;

namespace EleCho.WpfSuite.Layouts
{
    public partial class RelativePanel
    {
        private sealed class GraphNode
        {
            private readonly UIElement _element;

            private State _state;
            private Constraints _constraints;

            internal bool _isHorizontalLeaf;
            internal bool _isVerticalLeaf;
            internal GraphNode? _leftOfNode;
            internal GraphNode? _aboveNode;
            internal GraphNode? _rightOfNode;
            internal GraphNode? _belowNode;
            internal GraphNode? _alignHorizontalCenterWithNode;
            internal GraphNode? _alignVerticalCenterWithNode;
            internal GraphNode? _alignLeftWithNode;
            internal GraphNode? _alignTopWithNode;
            internal GraphNode? _alignRightWithNode;
            internal GraphNode? _alignBottomWithNode;
            internal UnsafeRect _measureRect;
            internal UnsafeRect _arrangeRect;

            public UIElement Element => _element;

            public GraphNode(UIElement element)
            {
                _element = element;
                _state = State.Unresolved;
                _isHorizontalLeaf = true;
                _isVerticalLeaf = true;
                _constraints = Constraints.None;
                _leftOfNode = default(GraphNode);
                _aboveNode = default(GraphNode);
                _rightOfNode = default(GraphNode);
                _belowNode = default(GraphNode);
                _alignHorizontalCenterWithNode = default(GraphNode);
                _alignVerticalCenterWithNode = default(GraphNode);
                _alignLeftWithNode = default(GraphNode);
                _alignTopWithNode = default(GraphNode);
                _alignRightWithNode = default(GraphNode);
                _alignBottomWithNode = default(GraphNode);
            }


            // The node is said to be anchored when its ArrangeRect is expected to
            // align with its MeasureRect on one or more sides. For example, if the 
            // node is right-anchored, the right side of the ArrangeRect should overlap
            // with the right side of the MeasureRect. Anchoring is determined by
            // specific combinations of dependencies.
            public bool IsLeftAnchored
                => (IsAlignLeftWithPanel() || IsAlignLeftWith() || (IsRightOf() && !IsAlignHorizontalCenterWith()));

            public bool IsTopAnchored
                => (IsAlignTopWithPanel() || IsAlignTopWith() || (IsBelow() && !IsAlignVerticalCenterWith()));

            public bool IsRightAnchored
                => (IsAlignRightWithPanel() || IsAlignRightWith() || (IsLeftOf() && !IsAlignHorizontalCenterWith()));

            public bool IsBottomAnchored
                => (IsAlignBottomWithPanel() || IsAlignBottomWith() || (IsAbove() && !IsAlignVerticalCenterWith()));

            public bool IsHorizontalCenterAnchored
                => ((IsAlignHorizontalCenterWithPanel() && !IsAlignLeftWithPanel() && !IsAlignRightWithPanel() && !IsAlignLeftWith() && !IsAlignRightWith() && !IsLeftOf() && !IsRightOf())
                    || (IsAlignHorizontalCenterWith() && !IsAlignLeftWithPanel() && !IsAlignRightWithPanel() && !IsAlignLeftWith() && !IsAlignRightWith()));

            public bool IsVerticalCenterAnchored
                => ((IsAlignVerticalCenterWithPanel() && !IsAlignTopWithPanel() && !IsAlignBottomWithPanel() && !IsAlignTopWith() && !IsAlignBottomWith() && !IsAbove() && !IsBelow())
                    || (IsAlignVerticalCenterWith() && !IsAlignTopWithPanel() && !IsAlignBottomWithPanel() && !IsAlignTopWith() && !IsAlignBottomWith()));

            // RPState flag checks.
            public bool IsUnresolved() { return _state == State.Unresolved; }
            public bool IsPending() { return (_state & State.Pending) == State.Pending; }
            public bool IsMeasured() { return (_state & State.Measured) == State.Measured; }
            public bool IsArrangedHorizontally() { return (_state & State.ArrangedHorizontally) == State.ArrangedHorizontally; }
            public bool IsArrangedVertically() { return (_state & State.ArrangedVertically) == State.ArrangedVertically; }
            public bool IsArranged() { return (_state & State.Arranged) == State.Arranged; }

            public void SetPending(bool value)
            {
                if (value)
                {
                    _state |= State.Pending;
                }
                else
                {
                    _state &= ~State.Pending;
                }
            }
            public void SetMeasured(bool value)
            {
                if (value)
                {
                    _state |= State.Measured;
                }
                else
                {
                    _state &= ~State.Measured;
                }
            }
            public void SetArrangedHorizontally(bool value)
            {
                if (value)
                {
                    _state |= State.ArrangedHorizontally;
                }
                else
                {
                    _state &= ~State.ArrangedHorizontally;
                }
            }
            public void SetArrangedVertically(bool value)
            {
                if (value)
                {
                    _state |= State.ArrangedVertically;
                }
                else
                {
                    _state &= ~State.ArrangedVertically;
                }
            }

            // RPEdge flag checks.
            public bool IsLeftOf() { return (_constraints & Constraints.LeftOf) == Constraints.LeftOf; }
            public bool IsAbove() { return (_constraints & Constraints.Above) == Constraints.Above; }
            public bool IsRightOf() { return (_constraints & Constraints.RightOf) == Constraints.RightOf; }
            public bool IsBelow() { return (_constraints & Constraints.Below) == Constraints.Below; }
            public bool IsAlignHorizontalCenterWith() { return (_constraints & Constraints.AlignHorizontalCenterWith) == Constraints.AlignHorizontalCenterWith; }
            public bool IsAlignVerticalCenterWith() { return (_constraints & Constraints.AlignVerticalCenterWith) == Constraints.AlignVerticalCenterWith; }
            public bool IsAlignLeftWith() { return (_constraints & Constraints.AlignLeftWith) == Constraints.AlignLeftWith; }
            public bool IsAlignTopWith() { return (_constraints & Constraints.AlignTopWith) == Constraints.AlignTopWith; }
            public bool IsAlignRightWith() { return (_constraints & Constraints.AlignRightWith) == Constraints.AlignRightWith; }
            public bool IsAlignBottomWith() { return (_constraints & Constraints.AlignBottomWith) == Constraints.AlignBottomWith; }
            public bool IsAlignLeftWithPanel() { return (_constraints & Constraints.AlignLeftWithPanel) == Constraints.AlignLeftWithPanel; }
            public bool IsAlignTopWithPanel() { return (_constraints & Constraints.AlignTopWithPanel) == Constraints.AlignTopWithPanel; }
            public bool IsAlignRightWithPanel() { return (_constraints & Constraints.AlignRightWithPanel) == Constraints.AlignRightWithPanel; }
            public bool IsAlignBottomWithPanel() { return (_constraints & Constraints.AlignBottomWithPanel) == Constraints.AlignBottomWithPanel; }
            public bool IsAlignHorizontalCenterWithPanel() { return (_constraints & Constraints.AlignHorizontalCenterWithPanel) == Constraints.AlignHorizontalCenterWithPanel; }
            public bool IsAlignVerticalCenterWithPanel() { return (_constraints & Constraints.AlignVerticalCenterWithPanel) == Constraints.AlignVerticalCenterWithPanel; }

            public void SetLeftOfConstraint(GraphNode? neighbor)
            {
                if (neighbor is not null)
                {
                    _leftOfNode = neighbor;
                    _constraints |= Constraints.LeftOf;
                }
                else
                {
                    _leftOfNode = null;
                    _constraints &= ~Constraints.LeftOf;
                }
            }
            public void SetAboveConstraint(GraphNode? neighbor)
            {
                if (neighbor is not null)
                {
                    _aboveNode = neighbor;
                    _constraints |= Constraints.Above;
                }
                else
                {
                    _aboveNode = null;
                    _constraints &= ~Constraints.Above;
                }
            }
            public void SetRightOfConstraint(GraphNode? neighbor)
            {
                if (neighbor is not null)
                {
                    _rightOfNode = neighbor;
                    _constraints |= Constraints.RightOf;
                }
                else
                {
                    _rightOfNode = null;
                    _constraints &= ~Constraints.RightOf;
                }
            }
            public void SetBelowConstraint(GraphNode? neighbor)
            {
                if (neighbor is not null)
                {
                    _belowNode = neighbor;
                    _constraints |= Constraints.Below;
                }
                else
                {
                    _belowNode = null;
                    _constraints &= ~Constraints.Below;
                }
            }
            public void SetAlignHorizontalCenterWithConstraint(GraphNode? neighbor)
            {
                if (neighbor is not null)
                {
                    _alignHorizontalCenterWithNode = neighbor;
                    _constraints |= Constraints.AlignHorizontalCenterWith;
                }
                else
                {
                    _alignHorizontalCenterWithNode = null;
                    _constraints &= ~Constraints.AlignHorizontalCenterWith;
                }
            }
            public void SetAlignVerticalCenterWithConstraint(GraphNode? neighbor)
            {
                if (neighbor is not null)
                {
                    _alignVerticalCenterWithNode = neighbor;
                    _constraints |= Constraints.AlignVerticalCenterWith;
                }
                else
                {
                    _alignVerticalCenterWithNode = null;
                    _constraints &= ~Constraints.AlignVerticalCenterWith;
                }
            }
            public void SetAlignLeftWithConstraint(GraphNode? neighbor)
            {
                if (neighbor is not null)
                {
                    _alignLeftWithNode = neighbor;
                    _constraints |= Constraints.AlignLeftWith;
                }
                else
                {
                    _alignLeftWithNode = null;
                    _constraints &= ~Constraints.AlignLeftWith;
                }
            }
            public void SetAlignTopWithConstraint(GraphNode? neighbor)
            {
                if (neighbor is not null)
                {
                    _alignTopWithNode = neighbor;
                    _constraints |= Constraints.AlignTopWith;
                }
                else
                {
                    _alignTopWithNode = null;
                    _constraints &= ~Constraints.AlignTopWith;
                }
            }
            public void SetAlignRightWithConstraint(GraphNode? neighbor)
            {
                if (neighbor is not null)
                {
                    _alignRightWithNode = neighbor;
                    _constraints |= Constraints.AlignRightWith;
                }
                else
                {
                    _alignRightWithNode = null;
                    _constraints &= ~Constraints.AlignRightWith;
                }
            }
            public void SetAlignBottomWithConstraint(GraphNode? neighbor)
            {
                if (neighbor is not null)
                {
                    _alignBottomWithNode = neighbor;
                    _constraints |= Constraints.AlignBottomWith;
                }
                else
                {
                    _alignBottomWithNode = null;
                    _constraints &= ~Constraints.AlignBottomWith;
                }
            }
            public void SetAlignLeftWithPanelConstraint(bool value)
            {
                if (value)
                {
                    _constraints |= Constraints.AlignLeftWithPanel;
                }
                else
                {
                    _constraints &= ~Constraints.AlignLeftWithPanel;
                }
            }
            public void SetAlignTopWithPanelConstraint(bool value)
            {
                if (value)
                {
                    _constraints |= Constraints.AlignTopWithPanel;
                }
                else
                {
                    _constraints &= ~Constraints.AlignTopWithPanel;
                }
            }
            public void SetAlignRightWithPanelConstraint(bool value)
            {
                if (value)
                {
                    _constraints |= Constraints.AlignRightWithPanel;
                }
                else
                {
                    _constraints &= ~Constraints.AlignRightWithPanel;
                }
            }
            public void SetAlignBottomWithPanelConstraint(bool value)
            {
                if (value)
                {
                    _constraints |= Constraints.AlignBottomWithPanel;
                }
                else
                {
                    _constraints &= ~Constraints.AlignBottomWithPanel;
                }
            }
            public void SetAlignHorizontalCenterWithPanelConstraint(bool value)
            {
                if (value)
                {
                    _constraints |= Constraints.AlignHorizontalCenterWithPanel;
                }
                else
                {
                    _constraints &= ~Constraints.AlignHorizontalCenterWithPanel;
                }
            }
            public void SetAlignVerticalCenterWithPanelConstraint(bool value)
            {
                if (value)
                {
                    _constraints |= Constraints.AlignVerticalCenterWithPanel;
                }
                else
                {
                    _constraints &= ~Constraints.AlignVerticalCenterWithPanel;
                }
            }

            public void UnmarkNeighborsAsHorizontalOrVerticalLeaves()
            {
                bool isHorizontallyCenteredFromLeft = false;
                bool isHorizontallyCenteredFromRight = false;
                bool isVerticallyCenteredFromTop = false;
                bool isVerticallyCenteredFromBottom = false;

                if (!IsAlignLeftWithPanel())
                {
                    if (IsAlignLeftWith())
                    {
                        _alignLeftWithNode!._isHorizontalLeaf = false;
                    }
                    else if (IsAlignHorizontalCenterWith())
                    {
                        isHorizontallyCenteredFromLeft = true;
                    }
                    else if (IsRightOf())
                    {
                        _rightOfNode!._isHorizontalLeaf = false;
                    }
                }

                if (!IsAlignTopWithPanel())
                {
                    if (IsAlignTopWith())
                    {
                        _alignTopWithNode!._isVerticalLeaf = false;
                    }
                    else if (IsAlignVerticalCenterWith())
                    {
                        isVerticallyCenteredFromTop = true;
                    }
                    else if (IsBelow())
                    {
                        _belowNode!._isVerticalLeaf = false;
                    }
                }

                if (!IsAlignRightWithPanel())
                {
                    if (IsAlignRightWith())
                    {
                        _alignRightWithNode!._isHorizontalLeaf = false;
                    }
                    else if (IsAlignHorizontalCenterWith())
                    {
                        isHorizontallyCenteredFromRight = true;
                    }
                    else if (IsLeftOf())
                    {
                        _leftOfNode!._isHorizontalLeaf = false;
                    }
                }

                if (!IsAlignBottomWithPanel())
                {
                    if (IsAlignBottomWith())
                    {
                        _alignBottomWithNode!._isVerticalLeaf = false;
                    }
                    else if (IsAlignVerticalCenterWith())
                    {
                        isVerticallyCenteredFromBottom = true;
                    }
                    else if (IsAbove())
                    {
                        _aboveNode!._isVerticalLeaf = false;
                    }
                }

                if (isHorizontallyCenteredFromLeft && isHorizontallyCenteredFromRight)
                {
                    _alignHorizontalCenterWithNode!._isHorizontalLeaf = false;
                }

                if (isVerticallyCenteredFromTop && isVerticallyCenteredFromBottom)
                {
                    _alignVerticalCenterWithNode!._isVerticalLeaf = false;
                }
            }

        }
    }
}
