using System.Windows;

namespace EleCho.WpfSuite
{
    internal record FlexPanelAttachedProperties
    {
        public DependencyProperty GrowProperty { get; }
        public DependencyProperty ShrinkProperty { get; }
        public DependencyProperty SelfAlignmentProperty { get; }

        public FlexPanelAttachedProperties(
            DependencyProperty growProperty,
            DependencyProperty shrinkProperty,
            DependencyProperty selfAlignmentProperty)
        {
            this.GrowProperty = growProperty;
            this.ShrinkProperty = shrinkProperty;
            this.SelfAlignmentProperty = selfAlignmentProperty;
        }

        public double? GetGrow(DependencyObject obj)
        {
            return (double?)obj.GetValue(GrowProperty);
        }

        public void SetGrow(DependencyObject obj, double? value)
        {
            obj.SetValue(GrowProperty, value);
        }

        public double? GetShrink(DependencyObject obj)
        {
            return (double?)obj.GetValue(ShrinkProperty);
        }

        public void SetShrink(DependencyObject obj, double? value)
        {
            obj.SetValue(ShrinkProperty, value);
        }

        public FlexItemAlignment? GetSelfAlignment(DependencyObject obj)
        {
            return (FlexItemAlignment?)obj.GetValue(SelfAlignmentProperty);
        }

        public void SetSelfAlignment(DependencyObject obj, FlexItemAlignment? value)
        {
            obj.SetValue(SelfAlignmentProperty, value);
        }
    }
}
