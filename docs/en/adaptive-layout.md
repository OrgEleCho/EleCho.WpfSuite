---
title: Adaptive Layout
layout: default
nav_order: 7
permalink: /zh/adaptive-layout
parent: Documentation
---

# Adaptive Layout
{: .fs-9 }

Using the value converters and layout panels provided by WPF Suite, you can easily create an adaptive grid layout that fills the width and automatically adjusts the number of columns.
{: .fs-6 .fw-300 }

---

The following is the effect:

![adaptive-layout](/images/adaptive-layout1.png)

And here is the result when the width expands, automatically adjusting the number of columns:

![adaptive-layout](/images/adaptive-layout2.png)

The core principle is:

1. Use ActualWidth divided by each item's width to determine the appropriate number of columns.
2. Use the UniformGrid provided by WPF Suite to set the column spacing.

```xml
<ws:GroupBox Header="UniformGrid (HorizontalSpacing=8,VerticalSpacing=8,ColumnsBinding)"
             Padding="8"
             xmlns:sys="clr-namespace:System;assembly=mscorlib">
    <ws:GroupBox.Resources>
        <ws:ValueConverterGroup x:Key="NumberDivideBy150Converter">
            <ws:DivideNumberConverter By="150"/>
            <ws:NumberConverter/>
        </ws:ValueConverterGroup>
    </ws:GroupBox.Resources>
    <ws:UniformGrid HorizontalSpacing="8"
                    VerticalSpacing="8"
                    Columns="{Binding RelativeSource={RelativeSource Mode=Self},Path=ActualWidth,Converter={StaticResource NumberDivideBy150Converter}}">
        <ws:Button Content="QWQ"/>
        <ws:Button Content="QWQ"/>
        <ws:Button Content="QWQ"/>
        <ws:Button Content="QWQ"/>
        <ws:Button Content="QWQ"/>
        <ws:Button Content="QWQ"/>
        <ws:Button Content="QWQ"/>
        <ws:Button Content="QWQ"/>
        <ws:Button Content="QWQ"/>
        <ws:Button Content="QWQ"/>
        <ws:Button Content="QWQ"/>
        <ws:Button Content="QWQ"/>
        <ws:Button Content="QWQ"/>
        <ws:Button Content="QWQ"/>
        <ws:Button Content="QWQ"/>
        <ws:Button Content="QWQ"/>
        <ws:Button Content="QWQ"/>
        <ws:Button Content="QWQ"/>
    </ws:UniformGrid>
</ws:GroupBox>
```

This ensures that elements fully occupy the width while also allowing the number of columns and the width of the elements to automatically adapt based on the container's width.