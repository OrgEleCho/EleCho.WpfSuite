using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace EleCho.WpfSuite.Layouts;


public partial class FlexPanel : Panel
{
    private record struct FlexElement(UIElement Element, double OffsetU, double OffsetV, double SizeU, double SizeV, int Order, double Grow, double Shrink, FlexItemAlignment Alignment);
    private record struct FlexElementSpan(int Start, int Length, double OffsetU, double OffsetV, double SizeU, double SizeV);


    private readonly List<FlexElement> _flexItems = new();
    private readonly List<FlexElementSpan> _flexLines = new();


    public static readonly DependencyProperty OrientationProperty =
        DependencyProperty.Register(nameof(Orientation), typeof(Orientation), typeof(FlexPanel),
            new FrameworkPropertyMetadata(Orientation.Horizontal, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));

    public static readonly DependencyProperty MainAlignmentProperty =
        DependencyProperty.Register(nameof(MainAlignment), typeof(FlexMainAlignment), typeof(FlexPanel),
            new FrameworkPropertyMetadata(default(FlexMainAlignment), FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));

    public static readonly DependencyProperty CrossAlignmentProperty =
        DependencyProperty.Register(nameof(CrossAlignment), typeof(FlexCrossAlignment), typeof(FlexPanel),
            new FrameworkPropertyMetadata(default(FlexCrossAlignment), FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));

    public static readonly DependencyProperty ItemsAlignmentProperty =
        DependencyProperty.Register(nameof(ItemsAlignment), typeof(FlexItemAlignment), typeof(FlexPanel),
            new FrameworkPropertyMetadata(default(FlexItemAlignment), FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));

    public static readonly DependencyProperty MainSpacingProperty =
        DependencyProperty.Register(nameof(MainSpacing), typeof(double), typeof(FlexPanel),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));

    public static readonly DependencyProperty CrossSpacingProperty =
        DependencyProperty.Register(nameof(CrossSpacing), typeof(double), typeof(FlexPanel),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));

    public static readonly DependencyProperty WrapProperty =
        DependencyProperty.Register(nameof(Wrap), typeof(bool), typeof(FlexPanel),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));

    public static readonly DependencyProperty ReverseLinesProperty =
        DependencyProperty.Register(nameof(ReverseLines), typeof(bool), typeof(FlexPanel),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));

    public static readonly DependencyProperty ReverseItemsProperty =
        DependencyProperty.Register(nameof(ReverseItems), typeof(bool), typeof(FlexPanel),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));

    public static readonly DependencyProperty UniformGrowProperty =
        DependencyProperty.Register("UniformGrow", typeof(double), typeof(FlexPanel),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));

    public static readonly DependencyProperty UniformShrinkProperty =
        DependencyProperty.Register("UniformShrink", typeof(double), typeof(FlexPanel),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));

    public static readonly DependencyProperty OrderProperty =
        DependencyProperty.RegisterAttached("Order", typeof(int), typeof(FlexPanel),
            new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsParentArrange));

    public static readonly DependencyProperty GrowProperty =
        DependencyProperty.RegisterAttached("Grow", typeof(double), typeof(FlexPanel),
            new FrameworkPropertyMetadata(double.NaN, FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsParentArrange));

    public static readonly DependencyProperty ShrinkProperty =
        DependencyProperty.RegisterAttached("Shrink", typeof(double), typeof(FlexPanel),
            new FrameworkPropertyMetadata(double.NaN, FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsParentArrange));

    public static readonly DependencyProperty AlignmentProperty =
        DependencyProperty.RegisterAttached("Alignment", typeof(FlexItemAlignment), typeof(FlexPanel),
            new FrameworkPropertyMetadata(FlexItemAlignment.Auto, FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsParentArrange));



    public Orientation Orientation
    {
        get { return (Orientation)GetValue(OrientationProperty); }
        set { SetValue(OrientationProperty, value); }
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

    public double MainSpacing
    {
        get { return (double)GetValue(MainSpacingProperty); }
        set { SetValue(MainSpacingProperty, value); }
    }

    public double CrossSpacing
    {
        get { return (double)GetValue(CrossSpacingProperty); }
        set { SetValue(CrossSpacingProperty, value); }
    }

    public bool Wrap
    {
        get { return (bool)GetValue(WrapProperty); }
        set { SetValue(WrapProperty, value); }
    }

    public bool ReverseLines
    {
        get { return (bool)GetValue(ReverseLinesProperty); }
        set { SetValue(ReverseLinesProperty, value); }
    }

    public bool ReverseItems
    {
        get { return (bool)GetValue(ReverseItemsProperty); }
        set { SetValue(ReverseItemsProperty, value); }
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

    public static int GetOrder(DependencyObject obj)
    {
        return (int)obj.GetValue(OrderProperty);
    }

    public static void SetOrder(DependencyObject obj, int value)
    {
        obj.SetValue(OrderProperty, value);
    }

    public static double GetGrow(DependencyObject obj)
    {
        return (double)obj.GetValue(GrowProperty);
    }

    public static void SetGrow(DependencyObject obj, double value)
    {
        obj.SetValue(GrowProperty, value);
    }

    public static double GetShrink(DependencyObject obj)
    {
        return (double)obj.GetValue(ShrinkProperty);
    }

    public static void SetShrink(DependencyObject obj, double value)
    {
        obj.SetValue(ShrinkProperty, value);
    }

    public static FlexItemAlignment GetAlignment(DependencyObject obj)
    {
        return (FlexItemAlignment)obj.GetValue(AlignmentProperty);
    }

    public static void SetAlignment(DependencyObject obj, FlexItemAlignment value)
    {
        obj.SetValue(AlignmentProperty, value);
    }




    private static double GetU(Size size, Orientation orientation)
    {
        return orientation switch
        {
            Orientation.Horizontal => size.Width,
            Orientation.Vertical => size.Height,
            _ => default,
        };
    }

    private static double GetV(Size size, Orientation orientation)
    {
        return orientation switch
        {
            Orientation.Horizontal => size.Height,
            Orientation.Vertical => size.Width,
            _ => default,
        };
    }

    private static Size GetSize(double u, double v, Orientation orientation)
    {
        if (orientation == Orientation.Horizontal)
        {
            return new Size(u, v);
        }
        else
        {
            return new Size(v, u);
        }
    }

    private static void ArrangeItem(
        UIElement item,
        Orientation orientation,
        double u, double v,
        double sizeU, double sizeV)
    {
        if (orientation == Orientation.Horizontal)
        {
            item.Arrange(new Rect(u, v, sizeU, sizeV));
        }
        else
        {
            item.Arrange(new Rect(v, u, sizeV, sizeU));
        }
    }


    protected override Size MeasureOverride(Size availableSize)
    {
        var orientation = Orientation;
        var uniformGrow = UniformGrow;
        var uniformShrink = UniformShrink;
        var mainSpacing = MainSpacing;
        var crossSpacing = CrossSpacing;
        var mainAlignment = MainAlignment;
        var crossAlignment = CrossAlignment;
        var itemAlignment = ItemsAlignment;
        var wrap = Wrap;

        double availableU = GetU(availableSize, orientation);
        double availableV = GetV(availableSize, orientation);

        foreach (UIElement child in InternalChildren)
        {
            child.Measure(availableSize);
        }

        FillItemsAndLines(InternalChildren, _flexItems, _flexLines, orientation, itemAlignment, availableSize, availableU, availableV, mainSpacing, crossSpacing, wrap, uniformGrow, uniformShrink, out var lineMaxU, out var linesTotalV);
        return GetSize(lineMaxU, linesTotalV, orientation);
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        var orientation = Orientation;
        var uniformGrow = UniformGrow;
        var uniformShrink = UniformShrink;
        var mainSpacing = MainSpacing;
        var crossSpacing = CrossSpacing;
        var mainAlignment = MainAlignment;
        var crossAlignment = CrossAlignment;
        var itemAlignment = ItemsAlignment;
        var reverseLines = ReverseLines;
        var reverseItems = ReverseItems;
        var wrap = Wrap;

        double availableU = GetU(finalSize, orientation);
        double availableV = GetV(finalSize, orientation);

        FillItemsAndLines(InternalChildren, _flexItems, _flexLines, orientation, itemAlignment, finalSize, availableU, availableV, mainSpacing, crossSpacing, wrap, uniformGrow, uniformShrink, out var lineMaxU, out var linesTotalV);
        GrowAndShrinkItems(_flexItems, _flexLines, availableU, availableV);
        LayoutLines(_flexItems, _flexLines, availableU, availableV, linesTotalV, mainAlignment, crossAlignment);
        LayoutItems(_flexItems, _flexLines);
        ArrangeFlex(_flexItems, _flexLines, orientation, availableU, availableV, reverseLines, reverseItems, mainAlignment, crossAlignment);

        var resultSize = GetSize(lineMaxU, linesTotalV, orientation);
        resultSize.Width = Math.Max(resultSize.Width, finalSize.Width);
        resultSize.Height = Math.Max(resultSize.Height, finalSize.Height);

        return resultSize;
    }
}
