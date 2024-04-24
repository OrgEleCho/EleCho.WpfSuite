using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace EleCho.WpfSuite
{
    public static class ScrollViewerUtils
    {
        public static double GetVerticalOffset(DependencyObject obj)
        {
            return (double)obj.GetValue(VerticalOffsetProperty);
        }

        public static void SetVerticalOffset(DependencyObject obj, double value)
        {
            obj.SetValue(VerticalOffsetProperty, value);
        }

        public static readonly DependencyProperty VerticalOffsetProperty =
            DependencyProperty.RegisterAttached("VerticalOffset", typeof(double), typeof(ScrollViewerUtils), new PropertyMetadata(0.0, VerticalOffsetChangedCallback));

        public static double GetHorizontalOffset(DependencyObject obj)
        {
            return (double)obj.GetValue(HorizontalOffsetProperty);
        }

        public static void SetHorizontalOffset(DependencyObject obj, double value)
        {
            obj.SetValue(HorizontalOffsetProperty, value);
        }


        public static readonly DependencyProperty HorizontalOffsetProperty =
            DependencyProperty.RegisterAttached("HorizontalOffset", typeof(double), typeof(ScrollViewerUtils), new PropertyMetadata(0.0, HorizontalOffsetChangedCallback));


        private static void VerticalOffsetChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is not double offset)
            {
                return;
            }

            if (d is ScrollViewer sv)
            {
                sv.ScrollToVerticalOffset(offset);
            }
            else if (d is ScrollContentPresenter scp)
            {
                scp.SetVerticalOffset(offset);
            }
        }

        private static void HorizontalOffsetChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is not double offset)
            {
                return;
            }

            if (d is ScrollViewer sv)
            {
                sv.ScrollToHorizontalOffset(offset);
            }
            else if (d is ScrollContentPresenter scp)
            {
                scp.SetHorizontalOffset(offset);
            }
        }
    }
}
