---
title: 布局面板
layout: default
nav_order: 2
permalink: /zh/layout-panels
parent: 中文文档
---

# 布局面板
{: .fs-9 }

WPF Suite 中的布局面板, 如与 WPF 原有面板名称一致, 则使用方式相同. 拓展的两个布局面板的使用方式也是较符合直觉的.
{: .fs-6 .fw-300 }

---

下面是 WPF Suite 中布局面板的使用说明

## StackPanel

堆叠面板, 将子元素排列成垂直或水平的一行.

| 属性 | 类型 | 说明 |
| --- | --- | --- |
| Orientaion | Orientation | 元素的布局方向, 默认为垂直方向 |
| Spacing | double | 面板中各个元素之间的间距, 默认为 0 |


使用示例
{: .fw-300}

```xml
<ws:StackPanel Orientation="Horizontal" Spacing="4">
    <ws:Button Content="Test"/>
    <ws:Button Content="Test"/>
    <ws:Button Content="Test"/>
</ws:StackPanel>
```

![StackPanel 示例效果](/images/stackpanel-example.png)


## WrapPanel

换行面板, 按指定的方向将子元素排列成水平或垂直的一行, 并且在超出面板的边缘处自动换行.

| 属性 | 类型 | 说明 |
| --- | --- | --- |
| Orientation | Orientation | 元素的布局方向, 默认为水平方向 |
| ItemWidth | double | 元素的宽度, 如果值为 NaN, 则是自动宽度, 默认为 NaN |
| ItemHeight | double | 元素的高度, 如果值为 NaN, 则是自动高度, 默认为 NaN |
| HorizontalSpacing | double | 面板中各个元素之间水平方向上的间距, 默认为 0 |
| VerticalSpacing | double | 面板中各个元素之间垂直方向上的间距, 默认为 0 |


使用示例
{: .fw-300}

```xml
<ws:WrapPanel Orientation="Horizontal"
              HorizontalSpacing="8"
              VerticalSpacing="4">
    <ws:Button Content="Test"/>
    <ws:Button Content="Test"/>
    <ws:Button Content="Test"/>
    <ws:Button Content="Test"/>
    <ws:Button Content="Test"/>
    <ws:Button Content="Test"/>
    <ws:Button Content="Test"/>
    <ws:Button Content="Test"/>
    <ws:Button Content="Test"/>
    <ws:Button Content="Test"/>
    <ws:Button Content="Test"/>
    <ws:Button Content="Test"/>
    <ws:Button Content="Test"/>
</ws:WrapPanel>
```

![WrapPanel 示例效果](/images/wrappanel-example.png)

## UniformGrid

提供一种在网格(网格中的所有单元格都具有相同的大小)中排列内容的方法.

| 属性 | 类型 | 说明 |
| --- | --- | --- |
| Columns | int | 网格中的列数, 默认为 0, 即自适应 |
| Rows | int | 网格中的行数, 默认为 0 , 即自适应 |
| FirstColumn | int | 网格在第一行中最开始空出来的单元格数, 默认为 0 |
| HorizontalSpacing | double | 面板中各个元素之间水平方向上的间距, 默认为 0 |
| VerticalSpacing | double | 面板中各个元素之间垂直方向上的间距, 默认为 0 |


使用示例
{: .fw-300}

```xml
<ws:UniformGrid HorizontalSpacing="8"
                VerticalSpacing="4">
    <ws:Button Content="Test"/>
    <ws:Button Content="Test"/>
    <ws:Button Content="Test"/>
    <ws:Button Content="Test"/>
    <ws:Button Content="Test"/>
    <ws:Button Content="Test"/>
    <ws:Button Content="Test"/>
    <ws:Button Content="Test"/>
    <ws:Button Content="Test"/>
    <ws:Button Content="Test"/>
</ws:UniformGrid>
```

![UniformGrid 示例效果](/images/uniformgrid-example.png)

## FlexPanel

弹性面板, 可以理解为增强的 WrapPanel, 可以指定元素的空间分布与对齐方式.

要理解弹性面板, 需要知道在弹性面板中的两个方向 "主轴" 与 "交叉轴"(或"侧轴"), 当设置面板的方向为行, 即水平时,
主轴就是水平方向, 交叉轴就是另一个方向, 即垂直方向.

{: .tip }
> 关于弹性布局, 请参考: [flex 布局的基本概念 - CSS 层叠样式表 MDN](https://developer.mozilla.org/zh-CN/docs/Web/CSS/CSS_flexible_box_layout/Basic_concepts_of_flexbox)

| 属性 | 类型 | 说明 |
| --- | --- | --- |
| Direction | FlexDirection | 布局方向, 默认为行, 也就是水平方向 |
| Wrap | FlexWrap | 换行行为, 默认为不换行 |
| MainAlignment | FlexMainAlignment | 元素在主轴上的对齐方式, 默认是对齐到开头 |
| CrossAlignment | FlexCrossAlignment | 内容在交叉轴上的对其方式, 默认是对齐到开头 |
| ItemsAlignment | FlexItemsAlignment | 元素自身在主轴上的对齐方式, 默认是对齐到开头 |
| UniformGrow | double | 统一的大小增长系数 |
| UniformShrink | double | 统一的大小收缩系数 |
| MainSpacing | double | 元素在主轴上的间距 |
| CrossSpacing | double | 元素在交叉轴上的间距 |

附加属性
{: .fw-300}

| 属性 | 类型 | 说明 |
| Order | int | 元素的排序位置 |
| Grow | double | 元素自身的大小增长系数 |
| Shrink | double | 元素自身的大小收缩系数 |
| Basis | double | 指定自身在主轴上的初始大小 |

FlexDirection 定义
{: .fw-300}

| 值 | 描述 |
| --- | --- |
| Row | 在行上布局, 也就是水平方向 |
| RowReversed | 在行上布局, 但一行内的元素顺序会反过来 |
| Column | 在列上布局, 也就是垂直方向 |
| ColumnReversed | 在列上布局, 但一行内的元素顺序会反过来 |

FlexWrap 定义
{: .fw-300}

| 值 | 描述 |
| --- | --- |
| NoWrap | 不换行 |
| Wrap | 换行 |
| WrapReverse | 换行, 但是行与行之间的顺序会反过来 |

FlexMainAlignment 定义
{: .fw-300}

| 值 | 描述 |
| --- | --- |
| Start | 对齐元素到行的开头 |
| End | 对齐元素到行的结尾 |
| Center | 对齐元素到行的中间 |
| SpaceBetween | 对齐元素到行的两边 |
| SpaceAround | 环绕对齐元素, 行的两侧会留有间距 |
| SpaceEvenly | 平分对齐元素, 每个元素两边都会有相同的外间距 |

FlexCrossAlignment 定义
{: .fw-300}

| 值 | 描述 |
| --- | --- |
| Start | 对齐行到开头 |
| End | 对齐行到结尾 |
| Center | 对齐行到中间 |
| Stretch | 拉伸行的高度, 以填充满整个面板 |
| SpaceBetween | 对齐行到两边 |
| SpaceAround | 环绕对齐行, 行的两边会留有间距 |
| SpaceEvenly | 平分对齐行, 每个元素两边都会有相同的外间距 |

FlexItemsAlignment 定义
{: .fw-300}

| 值 | 描述 |
| --- | --- |
| Start | 对齐元素到开头 |
| End | 对齐元素到结尾 |
| Center | 对齐元素到中间 |
| Baseline | 对齐元素到基线 (此值无效) |
| Stretch | 拉伸元素的高度, 以填充满整个面板 |

使用示例, 居中对齐
{: .fw-300}

```xml
<Border BorderThickness="1"
        BorderBrush="Gray">
    <ws:FlexPanel MainAlignment="Center"
                  MainSpacing="4">
        <ws:Button Content="Test"/>
        <ws:Button Content="Test"/>
        <ws:Button Content="Test"/>
    </ws:FlexPanel>
</Border>
```

![FlexPanel 示例效果1](/images/flexpanel-example1.png)


使用示例, 两端对齐
{: .fw-300}

```xml
<Border BorderThickness="1"
        BorderBrush="Gray">
    <ws:FlexPanel MainAlignment="SpaceBetween"
                  MainSpacing="4">
        <ws:Button Content="Test"/>
        <ws:Button Content="Test"/>
        <ws:Button Content="Test"/>
        <ws:Button Content="Test"/>
    </ws:FlexPanel>
</Border>
```

![FlexPanel 示例效果2](/images/flexpanel-example2.png)

使用示例, 两端对齐, 并使第一个元素占满剩余空间
{: .fw-300}

```xml
<Border BorderThickness="1"
        BorderBrush="Gray">
    <ws:FlexPanel MainAlignment="SpaceBetween"
                  MainSpacing="4">
        <ws:Button ws:FlexPanel.Grow="1" Content="Test"/>
        <ws:Button Content="Test"/>
        <ws:Button Content="Test"/>
        <ws:Button Content="Test"/>
    </ws:FlexPanel>
</Border>
```

![FlexPanel 示例效果3](/images/flexpanel-example3.png)

使用示例, 自动换行, 并使每一个元素都尽可能的占满剩余空间
{: .fw-300}

```xml
<Border BorderThickness="1"
        BorderBrush="Gray">
    <ws:FlexPanel MainAlignment="SpaceBetween"
                  MainSpacing="4"
                  CrossSpacing="4"
                  UniformGrow="1"
                  Wrap="Wrap">
        <ws:Button Content="Test"/>
        <ws:Button Content="Test"/>
        <ws:Button Content="Test"/>
        <ws:Button Content="Test"/>
        <ws:Button Content="Test"/>
        <ws:Button Content="Test"/>
        <ws:Button Content="Test"/>
        <ws:Button Content="Test"/>
        <ws:Button Content="Test"/>
        <ws:Button Content="Test"/>
        <ws:Button Content="Test"/>
    </ws:FlexPanel>
</Border>
```

![FlexPanel 示例效果4](/images/flexpanel-example4.png)

## MasonryPanel

砌筑面板, 简单的瀑布流布局实现.

| 属性 | 类型 | 说明 |
| --- | --- | --- |
| Orientation | Orientation | 布局方向, 也就是元素堆叠方向, 默认为垂直 |
| Flows | int | 流的数量, 默认为 1, 当值为 1 时, 行为与 StackPanel 相同 |
| FlowSpacing | 流与流之间的间距, 默认为 0 |
| ItemSpacing | 元素与元素之间的间距, 默认为 0 |

使用示例
{: .fw-300}

```xml
<Border BorderThickness="1"
        BorderBrush="Gray">
    <ws:MasonryPanel Flows="3"
                     FlowSpacing="4"
                     ItemSpacing="4">
        <Border Background="RosyBrown" Height="60"/>
        <Border Background="AntiqueWhite" Height="50"/>
        <Border Background="Pink" Height="40"/>
        <Border Background="RoyalBlue" Height="65"/>
        <Border Background="CadetBlue" Height="74"/>
        <Border Background="Beige" Height="42"/>
        <Border Background="NavajoWhite" Height="67"/>
        <Border Background="DarkTurquoise" Height="89"/>
        <Border Background="Violet" Height="42"/>
        <Border Background="PaleGoldenrod" Height="54"/>
        <Border Background="Lavender" Height="66"/>
        <Border Background="NavajoWhite" Height="38"/>
    </ws:MasonryPanel>
</Border>
```

![MasonryPanel 示例效果](/images/masonrypanel-example.png)

## BoxPanel

盒子面板, 每个成员都占用整个容器的空间.

{: .tip }
> 该面板常用于替换掉 Grid, 如果你只想要多个元素重合在同一个空间中, 用 BoxPanel 可以提升些许性能

使用示例
{: .fw-300}

```xml
<Border BorderThickness="1"
        BorderBrush="Gray"
        Height="100">
    <ws:BoxPanel>
        <TextBlock Text="Hello" VerticalAlignment="Top"/>
        <TextBlock Text="World" VerticalAlignment="Bottom"/>
    </ws:BoxPanel>
</Border>
```

![BoxPanel 示例效果](/images/boxpanel-example.png)
