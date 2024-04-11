using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EleCho.WpfSuite
{

    public class FlexPanel : Panel, IFlexPanel
    {
        static FlexPanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlexPanel), new FrameworkPropertyMetadata(typeof(FlexPanel)));
        }

        #region Properties

        public FlexDirection Direction
        {
            get { return (FlexDirection)GetValue(DirectionProperty); }
            set { SetValue(DirectionProperty, value); }
        }

        public FlexWrap Wrap
        {
            get { return (FlexWrap)GetValue(WrapProperty); }
            set { SetValue(WrapProperty, value); }
        }

        public FlexMainAlignment MainAlignment
        {
            get { return (FlexMainAlignment)GetValue(MainAlignmentProperty); }
            set { SetValue(MainAlignmentProperty, value); }
        }

        public FlexCrossAlignment CrossAlignment
        {
            get { return (FlexCrossAlignment)GetValue(CrossAlignmentProperty); }
            set { SetValue(CrossAlignmentProperty, value); }
        }

        public FlexItemAlignment ItemsAlignment
        {
            get { return (FlexItemAlignment)GetValue(ItemsAlignmentProperty); }
            set { SetValue(ItemsAlignmentProperty, value); }
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





        public static readonly DependencyProperty MainAlignmentProperty =
            DependencyProperty.Register(nameof(MainAlignment), typeof(FlexMainAlignment), typeof(FlexPanel),
                new FrameworkPropertyMetadata(FlexMainAlignment.Start, FrameworkPropertyMetadataOptions.AffectsArrange));

        public static readonly DependencyProperty CrossAlignmentProperty =
            DependencyProperty.Register(nameof(CrossAlignment), typeof(FlexCrossAlignment), typeof(FlexPanel),
                new FrameworkPropertyMetadata(FlexCrossAlignment.Start, FrameworkPropertyMetadataOptions.AffectsArrange));

        public static readonly DependencyProperty ItemsAlignmentProperty =
            DependencyProperty.Register(nameof(ItemsAlignment), typeof(FlexItemAlignment), typeof(FlexPanel),
                new FrameworkPropertyMetadata(FlexItemAlignment.Start, FrameworkPropertyMetadataOptions.AffectsArrange));

        public static readonly DependencyProperty DirectionProperty =
            DependencyProperty.Register(nameof(Direction), typeof(FlexDirection), typeof(FlexPanel),
                new FrameworkPropertyMetadata(FlexDirection.Row, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));

        public static readonly DependencyProperty WrapProperty =
            DependencyProperty.Register(nameof(Wrap), typeof(FlexWrap), typeof(FlexPanel),
                new FrameworkPropertyMetadata(FlexWrap.NoWrap, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));

        public static readonly DependencyProperty UniformGrowProperty =
            DependencyProperty.Register(nameof(UniformGrow), typeof(double), typeof(FlexPanel),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsArrange));

        public static readonly DependencyProperty UniformShrinkProperty =
            DependencyProperty.Register(nameof(UniformShrink), typeof(double), typeof(FlexPanel),
                new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.AffectsArrange));

        public static readonly DependencyProperty GrowProperty =
            DependencyProperty.RegisterAttached("Grow", typeof(double?), typeof(FlexPanel),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsParentArrange));

        public static readonly DependencyProperty ShrinkProperty =
            DependencyProperty.RegisterAttached("Shrink", typeof(double?), typeof(FlexPanel),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsParentArrange));

        public static readonly DependencyProperty SelfAlignmentProperty =
            DependencyProperty.RegisterAttached("SelfAlignment", typeof(FlexItemAlignment?), typeof(FlexPanel),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsParentArrange));






        public static double? GetGrow(DependencyObject obj)
        {
            return (double?)obj.GetValue(GrowProperty);
        }

        public static void SetGrow(DependencyObject obj, double? value)
        {
            obj.SetValue(GrowProperty, value);
        }

        public static double? GetShrink(DependencyObject obj)
        {
            return (double?)obj.GetValue(ShrinkProperty);
        }

        public static void SetShrink(DependencyObject obj, double? value)
        {
            obj.SetValue(ShrinkProperty, value);
        }

        public static FlexItemAlignment? GetSelfAlignment(DependencyObject obj)
        {
            return (FlexItemAlignment?)obj.GetValue(SelfAlignmentProperty);
        }

        public static void SetSelfAlignment(DependencyObject obj, FlexItemAlignment? value)
        {
            obj.SetValue(SelfAlignmentProperty, value);
        }








        UIElementCollection IFlexPanel.InternalChildren => InternalChildren;

        private static readonly FlexPanelAttachedProperties StaticAttachedProperties = new FlexPanelAttachedProperties(GrowProperty, ShrinkProperty, SelfAlignmentProperty);

        FlexPanelAttachedProperties IFlexPanel.AttachedProperties { get; } = StaticAttachedProperties;


        #endregion

        protected override Size MeasureOverride(Size availableSize)
        {
            return FlexPanelCore.MeasureOverride(this, availableSize);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            return FlexPanelCore.ArrangeOverride(this, finalSize);
        }
    }
}
