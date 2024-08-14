---
title: 自适应布局
layout: default
nav_order: 7
permalink: /zh/adaptive-layout
parent: 中文文档
---

# 自适应布局
{: .fs-9 }

使用 WPF Suite 提供的值转换器以及布局面板, 你可以轻松实现一种能够占满宽度且自动调整列数的自适应网格布局
{: .fs-6 .fw-300 }

---

下面是效果:

![adaptive-layout](/images/adaptive-layout1.png)

以及当宽度变宽时, 其自动调整列数的结果:

![adaptive-layout](/images/adaptive-layout2.png)

其核心原理是:

1. 使用 ActualWidth 除以每项的宽度, 并得出应有的列数
2. 使用 WPF Suite 提供的 UniformGrid 设置列间距

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

这样, 就实现了既能保证元素占满宽度, 也能使元素宽度, 布局的列数根据容器的宽度自动适应