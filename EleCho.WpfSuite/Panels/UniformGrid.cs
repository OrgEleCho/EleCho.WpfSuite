using System;
using System.Data;
using System.Windows;

namespace EleCho.WpfSuite
{
    /// <summary>
    /// Provides a way to arrange content in a grid where all the cells in the grid have the same size.
    /// </summary>
    public class UniformGrid : System.Windows.Controls.Panel
    {
        private int _rows;
        private int _columns;

#if DEBUG
        void TestMethod()
        {
            System.Windows.Controls.Primitives.UniformGrid qwq;
        }
#endif

        /// <summary>
        /// The number of columns that are in the grid.
        /// </summary>
        public int Columns
        {
            get { return (int)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        /// <summary>
        /// The number of rows that are in the grid.
        /// </summary>
        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        /// <summary>
        /// The number of leading blank cells in the first row of the grid.
        /// </summary>
        public int FirstColumn
        {
            get { return (int)GetValue(FirstColumnProperty); }
            set { SetValue(FirstColumnProperty, value); }
        }

        /// <summary>
        /// Horizontal spacing between items
        /// </summary>
        public double HorizontalSpacing
        {
            get { return (double)GetValue(HorizontalSpacingProperty); }
            set { SetValue(HorizontalSpacingProperty, value); }
        }

        /// <summary>
        /// Vertical spacing between items
        /// </summary>
        public double VerticalSpacing
        {
            get { return (double)GetValue(VerticalSpacingProperty); }
            set { SetValue(VerticalSpacingProperty, value); }
        }



        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.Register(nameof(Columns), typeof(int), typeof(UniformGrid), new PropertyMetadata(0));

        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register(nameof(Rows), typeof(int), typeof(UniformGrid), new PropertyMetadata(0));

        public static readonly DependencyProperty FirstColumnProperty =
            DependencyProperty.Register(nameof(FirstColumn), typeof(int), typeof(UniformGrid), new PropertyMetadata(0));

        public static readonly DependencyProperty HorizontalSpacingProperty =
            DependencyProperty.Register(nameof(HorizontalSpacing), typeof(double), typeof(UniformGrid), new PropertyMetadata(0.0));

        public static readonly DependencyProperty VerticalSpacingProperty =
            DependencyProperty.Register(nameof(VerticalSpacing), typeof(double), typeof(UniformGrid), new PropertyMetadata(0.0));

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override Size MeasureOverride(Size constraint)
        {
            UpdateComputedValues();

            if (_columns == 0 && _rows == 0)
            {
                return new Size(0, 0);
            }

            var maxHorizontalSpacing = constraint.Width / (_columns - 1);
            var maxVerticalSpacing = constraint.Height / (_rows - 1);

            var horizontalSpacing = Math.Min(HorizontalSpacing, maxHorizontalSpacing);
            var verticalSpacing = Math.Min(VerticalSpacing, maxVerticalSpacing);

            double horizontalTotalSpacing = horizontalSpacing * (_columns - 1);
            double verticalTotalSpacing = verticalSpacing * (_rows - 1);

            double childConstraintWidth = (constraint.Width - horizontalTotalSpacing) / _columns;
            double childConstraintHeight = (constraint.Height - verticalTotalSpacing) / _rows;

            if (childConstraintWidth < 0)
            {
                childConstraintWidth = 0;
            }

            if (childConstraintHeight < 0)
            {
                childConstraintHeight = 0;
            }

            Size childConstraint = new Size(childConstraintWidth, childConstraintHeight);
            double maxChildDesiredWidth = 0.0;
            double maxChildDesiredHeight = 0.0;

            //  Measure each child, keeping track of maximum desired width and height.
            for (int i = 0, count = InternalChildren.Count; i < count; ++i)
            {
                UIElement child = InternalChildren[i];

                // Measure the child.
                child.Measure(childConstraint);
                Size childDesiredSize = child.DesiredSize;

                if (maxChildDesiredWidth < childDesiredSize.Width)
                {
                    maxChildDesiredWidth = childDesiredSize.Width;
                }

                if (maxChildDesiredHeight < childDesiredSize.Height)
                {
                    maxChildDesiredHeight = childDesiredSize.Height;
                }
            }

            var requiredSize = new Size((maxChildDesiredWidth * _columns) + horizontalTotalSpacing, (maxChildDesiredHeight * _rows) + verticalTotalSpacing);
            if (requiredSize.Width > constraint.Width)
            {
                requiredSize.Width = constraint.Width;
            }

            if (requiredSize.Height > constraint.Height)
            {
                requiredSize.Height = constraint.Height;
            }

            return requiredSize;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override Size ArrangeOverride(Size arrangeSize)
        {
            if (_rows == 0 && _columns == 0)
            {
                return arrangeSize;
            }

            var maxHorizontalSpacing = arrangeSize.Width / (_columns - 1);
            var maxVerticalSpacing = arrangeSize.Height / (_rows - 1);

            var horizontalSpacing = Math.Min(HorizontalSpacing, maxHorizontalSpacing);
            var verticalSpacing = Math.Min(VerticalSpacing, maxVerticalSpacing);

            double horizontalTotalSpacing = horizontalSpacing * (_columns - 1);
            double verticalTotalSpacing = verticalSpacing * (_rows - 1);

            double childWidth = (arrangeSize.Width - horizontalTotalSpacing) / _columns;
            double childHeight = (arrangeSize.Height - verticalTotalSpacing) / _rows;

            if (childWidth < 0)
            {
                childWidth = 0;
            }

            if (childHeight < 0)
            {
                childHeight = 0;
            }

            Rect childBounds = new Rect(0, 0, childWidth, childHeight);
            double xStep = childBounds.Width + horizontalSpacing;
            double yStep = childBounds.Height + verticalSpacing;
            double xBound = arrangeSize.Width - 1.0;

            childBounds.X += childBounds.Width * FirstColumn;

            // Arrange and Position each child to the same cell size
            foreach (UIElement child in InternalChildren)
            {
                child.Arrange(childBounds);

                // only advance to the next grid cell if the child was not collapsed
                if (child.Visibility != Visibility.Collapsed)
                {
                    childBounds.X += xStep;
                    if (childBounds.X >= xBound)
                    {
                        childBounds.Y += yStep;
                        childBounds.X = 0;
                    }
                }
            }

            return arrangeSize;
        }

        /// <summary>
        /// If either Rows or Columns are set to 0, then dynamically compute these
        /// values based on the actual number of non-collapsed children.
        ///
        /// In the case when both Rows and Columns are set to 0, then make Rows 
        /// and Columns be equal, thus laying out in a square grid.
        /// </summary>
        private void UpdateComputedValues()
        {
            _columns = Columns;
            _rows = Rows;

            //parameter checking. 
            if (FirstColumn >= _columns)
            {
                //NOTE: maybe we shall throw here. But this is somewhat out of 
                //the MCC itself. We need a whole new panel spec.
                FirstColumn = 0;
            }

            if ((_rows == 0) || (_columns == 0))
            {
                int nonCollapsedCount = 0;

                // First compute the actual # of non-collapsed children to be laid out
                for (int i = 0, count = InternalChildren.Count; i < count; ++i)
                {
                    UIElement child = InternalChildren[i];
                    if (child.Visibility != Visibility.Collapsed)
                    {
                        nonCollapsedCount++;
                    }
                }

                // to ensure that we have at leat one row & column, make sure
                // that nonCollapsedCount is at least 1
                if (nonCollapsedCount == 0)
                {
                    nonCollapsedCount = 1;
                }

                if (_rows == 0)
                {
                    if (_columns > 0)
                    {
                        // take FirstColumn into account, because it should really affect the result
                        _rows = (nonCollapsedCount + FirstColumn + (_columns - 1)) / _columns;
                    }
                    else
                    {
                        // both rows and columns are unset -- lay out in a square
                        _rows = (int)Math.Sqrt(nonCollapsedCount);
                        if ((_rows * _rows) < nonCollapsedCount)
                        {
                            _rows++;
                        }
                        _columns = _rows;
                    }
                }
                else if (_columns == 0)
                {
                    // guaranteed that _rows is not 0, because we're in the else clause of the check for _rows == 0
                    _columns = (nonCollapsedCount + (_rows - 1)) / _rows;
                }
            }
        }
    }
}
