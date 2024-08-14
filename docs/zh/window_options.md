---
title: 窗口选项
layout: default
nav_order: 5
permalink: /zh/window-options
parent: 中文文档
---

# 窗口选项
{: .fs-9 }

通过窗口选项, 你可以设置窗口的背景类型, 亮暗色主题, 标题栏可见性, 标题栏菜单按钮可见性等
{: .fs-6 .fw-300 }

---

下面是关于窗口选项的介绍

## WindowOption

用于设置窗口选项的工具类

| 附加属性 | 类型 | 描述 |
| --- | --- | --- |
| Backdrop | WindowBackdrop | 背景类型, 可以是自动(Auto), 无(None), 云母(Mica), 亚克力(Acrylic)或备选云母(MicaAlt). 默认为 "自动" |
| Corner | WindowCorner | 窗口边缘类型, 可以是默认(Default), 无圆角(DoNotRound), 圆角(Round), 小圆角(RoundSmall), 默认为 "默认" |
| CaptionColor | WindowOptionColor | 窗体标题栏颜色, 可以是默认(Default), 无(None) 或任意自定义 RGB 颜色, 默认为 "默认" |
| TextColor | WindowOptionColor | 文字颜色, 即标题栏文字颜色, 可以是默认(Default), 无(None) 或任意自定义 RGB 颜色, 默认为 "默认" |
| BorderColor | WindowOptionColor | 边框颜色, 可以是默认(Default), 无(None) 或任意自定义 RGB 颜色, 默认为 "默认" |
| IsDarkMode | bool | 表示窗口是否是暗色模式, 默认为 false |
| AccentState | WindowAccentState | 窗口风格状态, 可以是无(None), 渐变(Gradient), 透明(Transparent), 模糊背景(BlurBehind), 亚克力模糊背景(AcrylicBlurBehind), 以及 HostBackdrop, 默认为 "无" |
| AccentBorder | bool | 表示当应用窗口风格(Accent)时, 是否带有边框, 默认为 true |
| AccentGradientColor | Color | 窗体风格的渐变色, 默认为 ScRGB(0.25, 1, 1, 1) |
| IsCaptionVisible | bool | 是否显示标题栏, 默认为 true |
| IsCaptionMenuVisible | bool | 是否显示标题栏的菜单, 其中包含应用图标, 以及最小化, 最大化和关闭按钮, 默认为 true |
| IsMinimumButton | bool | 表示一个 Visual 是否是窗体的最小化按钮, 用于自定义最小化按钮, 默认为 false |
| IsMaximumButton | bool | 表示一个 Visual 是否是窗体的最大化按钮, 用于自定义最大化按钮, 默认为 false |
| IsCloseButton | bool | 表示一个 Visual 是否是窗体的关闭按钮, 用于自定义关闭按钮, 默认为 false |

## 在 Windows11 上设置窗口背景

如果系统版本大于等于 22621, 那么是可以通过 WindowOption.Backdrop 来设置窗口背景的. 
注意别忘了将窗口背景设为 Transparent, 否则默认窗口的白色背景会遮挡你设置的背景.

使用示例
{: .fw-300}

```xml
<Window x:Class="TestWpf.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:ws="https://schemas.elecho.dev/wpfsuite"
        xmlns:local="clr-namespace:TestWpf"
        ws:WindowOption.Backdrop="Mica"
        Background="Transparent"
        Title="MainWindow" Height="300" Width="450">
        
</Window>
```

![窗口选项示例1](/images/windowoption-example1.png)

再将 `ws:WindowOption.Backdrop` 的值改为 `Acrylic`, 效果如下:

![窗口选项示例2](/images/windowoption-example2.png)

注意, 如果你需要使用 WindowChrome, 那么 GlassFrameThickness 表示的是背景效果所应用的厚度

由于 GlassFrameThickness 默认为 0, 所以你会发现背景效果没有了, 将它设置为 -1 (无限大)就可以重新看到背景了

```xml
<Window x:Class="TestWpf.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:ws="https://schemas.elecho.dev/wpfsuite"
        xmlns:local="clr-namespace:TestWpf"
        ws:WindowOption.Backdrop="Mica"
        Background="Transparent"
        Title="MainWindow" Height="300" Width="450">
    <WindowChrome.WindowChrome>
        <WindowChrome GlassFrameThickness="-1"/>
    </WindowChrome.WindowChrome>
</Window>
```

![窗口选项示例3](/images/windowoption-example3.png)

{: .warning }
> 其实 Backdrop 还要求窗口 HwndSource 的 CompositionTarget.BackgroundColor 值为 Transparent, 所以如果你将它设为其他值, Backdrop 同样会失效.

## 在 Windows11 上为窗口启用暗色模式

在系统版本 22000 上, 你可以为窗口启用暗色模式, 这样, 标题栏以及设置的窗口背景都会变成暗色.

使用示例
{: .fw-300}

```xml
<Window x:Class="TestWpf.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:ws="https://schemas.elecho.dev/wpfsuite"
        xmlns:local="clr-namespace:TestWpf"
        ws:WindowOption.Backdrop="Mica"
        ws:WindowOption.IsDarkMode="True"
        Background="Transparent"
        Title="MainWindow" Height="300" Width="450">
    
</Window>
```

![窗口选项示例4](/images/windowoption-example4.png)

在亚克力背景窗口下也是如此

![窗口选项示例5](/images/windowoption-example5.png)



## 在 Windows11 上设置颜色, 边框

设置标题栏颜色, 文字颜色, 窗口边缘样式和边框颜色可以在系统版本高于 22000 时使用.

关闭圆角
{: .fw-300}

```xml
<Window x:Class="TestWpf.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:ws="https://schemas.elecho.dev/wpfsuite"
        xmlns:local="clr-namespace:TestWpf"
        ws:WindowOption.Backdrop="Mica"
        ws:WindowOption.Corner="DoNotRound"
        Background="Transparent"
        Title="MainWindow" Height="300" Width="450">
    <WindowChrome.WindowChrome>
        <WindowChrome GlassFrameThickness="-1"/>
    </WindowChrome.WindowChrome>
</Window>
```

![窗口选项示例6](/images/windowoption-example6.png)

设置标题栏颜色, 文字颜色, 以及边框颜色
{: .fw-300}

```xml
<Window x:Class="TestWpf.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:ws="https://schemas.elecho.dev/wpfsuite"
        xmlns:local="clr-namespace:TestWpf"
        ws:WindowOption.Backdrop="Mica"
        ws:WindowOption.CaptionColor="Azure"
        ws:WindowOption.TextColor="Purple"
        ws:WindowOption.BorderColor="#74b874"
        Background="Transparent"
        Title="MainWindow" Height="300" Width="450">

</Window>
```

![窗口选项示例7](/images/windowoption-example7.png)

## 在 Windows10 中使用亚克力

早在 Windows10, 就已经可以通过一个隐藏的 API "SetWindowCompositionAttribute" 来设置背景了.
唯一的缺点就是无法将亚克力拓展到标题栏区域.

使用 AccentState 设置窗口背景为亚克力
{: .fw-300}

```xml
<Window x:Class="TestWpf.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:ws="https://schemas.elecho.dev/wpfsuite"
        xmlns:local="clr-namespace:TestWpf"
        ws:WindowOption.AccentState="AcrylicBlurBehind"
        ws:WindowOption.AccentGradientColor="Transparent"
        Background="Transparent"
        Title="MainWindow" Height="300" Width="450">

</Window>
```

![窗口选项示例8](/images/windowoption-example8.png)

{: .warning }
> 注意, 如果你要使用 WindowChrome, 与 Backdrop 不同的是, AccentState 要求 GlassFrameThickness 必须为 0
> 否则亚克力效果不会显示出来

当然, 通过任意方式去除标题栏, 你就可以得到一个干净的亚克力窗口了. 例如下面设置 `WindowStyle` 为 `None`, 设置 `ResizeMode` 为 `NoResize`.

为亚克力去除标题栏
{: .fw-300}

```xml
<Window x:Class="TestWpf.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:ws="https://schemas.elecho.dev/wpfsuite"
        xmlns:local="clr-namespace:TestWpf"
        ws:WindowOption.AccentState="AcrylicBlurBehind"
        WindowStyle="None"
        ResizeMode="NoResize"
        Background="Transparent"
        Title="MainWindow" Height="300" Width="450">

</Window>
```

![窗口选项示例9](/images/windowoption-example9.png)

{: .tip }
> 之所以窗口设置了 `WindowStyle` 为 `None` 却仍然有一个边框, 是因为 `AccentBorder` 默认为 true.
> 在底层使用 API 应用 Accent 时, 还是会为窗口添加一个边框的

当然, 使用 WindowChrome 去除标题栏也可以得到一个纯净的亚克力窗口, 而且还能保留窗体拖拽边框调整大小的功能.
只不过窗口的最小化, 最大化以及关闭按钮会消失.

```xml
<Window x:Class="TestWpf.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:ws="https://schemas.elecho.dev/wpfsuite"
        xmlns:local="clr-namespace:TestWpf"
        ws:WindowOption.AccentState="AcrylicBlurBehind"
        Background="Transparent"
        Title="MainWindow" Height="300" Width="450">
    <WindowChrome.WindowChrome>
        <WindowChrome GlassFrameThickness="0"/>
    </WindowChrome.WindowChrome>
</Window>
```

![窗口选项示例10](/images/windowoption-example10.png)

## 自定义窗口菜单按钮

如果你希望去除窗口的三个按钮, 并换成自定义的实现, 可以执行以下操作:

1. 对 Window 设置 ws:WindowOption.IsCaptionMenuVisible 为 false, 以去除系统的标题菜单
2. 为自定义的最大化按钮设置 ws:WindowOption.IsMaximumButton 为 true, 以正确响应系统行为

使用示例:
{: .fw-300}

```xml
<Window x:Class="TestWpf.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:ws="https://schemas.elecho.dev/wpfsuite"
        xmlns:local="clr-namespace:TestWpf"
        ws:WindowOption.Backdrop="Mica"
        ws:WindowOption.IsCaptionMenuVisible="False"
        Background="Transparent"
        Title="MainWindow" Height="300" Width="450">
    <WindowChrome.WindowChrome>
        <WindowChrome GlassFrameThickness="-1"/>
    </WindowChrome.WindowChrome>
    <DockPanel>
        <Grid Height="25"
              DockPanel.Dock="Top">
            <StackPanel HorizontalAlignment="Left"
                        Orientation="Horizontal"
                        Margin="4 4 0 0">
                <Button Padding="8 0"
                        Background="Transparent"
                        BorderThickness="0"
                        ws:WindowOption.IsCloseButton="True">
                    <Ellipse Width="10" Height="10" Fill="#f9605a"/>
                </Button>
                <Button Padding="8 0"
                        Background="Transparent"
                        BorderThickness="0"
                        ws:WindowOption.IsMinimumButton="True">
                    <Ellipse Width="10" Height="10" Fill="#febc3a"/>
                </Button>
                <Button Padding="8 0"
                        Background="Transparent"
                        BorderThickness="0"
                        ws:WindowOption.IsMaximumButton="True">
                    <Ellipse Width="10" Height="10" Fill="#00cd4d"/>
                </Button>
            </StackPanel>
        </Grid>

        <Grid>
            <TextBlock Text="Window Content"
                       VerticalAlignment="Center"
                       HorizontalAlignment="Center"/>
        </Grid>
    </DockPanel>
</Window>
```

![窗口选项示例11](/images/windowoption-example11.png)

如果你使用的是 Windows11 操作系统, 鼠标悬停在最大化按钮上, 也会正确显示出布局选项

![窗口选项示例12](/images/windowoption-example12.png)