---
title: 增强控件
layout: default
nav_order: 3
permalink: /zh/enhanced-controls
parent: 中文文档
---

# 增强控件
{: .fs-9 }

WPF Suite 对于大部分控件都有进行属性拓展, 包括状态属性, 圆角设置, 部分控件还进行了功能拓展
{: .fs-6 .fw-300 }

---

## 状态属性

你会在增强的控件中, 多了一些属性, 例如 `HoverBackground`, `HoverBorderBrush`, 正如其名, 它们表示在对应状态下对应属性的值.

如果对这些属性设置了 null, 也会正确的 fallback 到设定的 `Background` 和 `BorderBrush` 属性值.

也就是说, 你不需要更改模板, 就可以直接设定控件在某状态下的某种样式.

使用示例
{: .fw-300}

```xml
<ws:Button Content="TextButton" 
           Padding="4"
           BorderBrush="Transparent"
           Background="Transparent"
           HoverBorderBrush="#33000000"
           HoverBackground="#EEFFFFFF"
           PressedBackground="#c8f8ff"/>
```

效果, 非悬停状态, 悬停状态以及按下状态:

<p>
    <img src="/images/stateproperty-example-state1.png" title="状态属性示例,状态1" />
    <img src="/images/stateproperty-example-state2.png" title="状态属性示例,状态2" />
    <img src="/images/stateproperty-example-state3.png" title="状态属性示例,状态3" />
</p>

## 圆角

现在 Button, TextBox, PasswordBox 以及绝大部分控件都支持使用 CornerRadius 来设置圆角

使用示例
{: .fw-300}

```xml
<ws:StackPanel Orientation="Horizontal"
               Spacing="4">
    <ws:Button Content="Button"
               CornerRadius="10"/>
    <ws:TextBox MinWidth="60"
                CornerRadius="10"/>
    <ws:PasswordBox MinWidth="60"
                    CornerRadius="10"/>
</ws:StackPanel>
```

![CornerRadius 示例效果](/images/cornerradiusproperty-example.png)

## 占位符

TextBox 和 PasswordBox 现在可以通过 Placeholder 属性来设置占位符. 占位符会在文本框为空的时候显示.


使用示例
{: .fw-300}

```xml
<ws:StackPanel Spacing="4">
    <ws:TextBox Placeholder="This is a text box"/>
    <ws:PasswordBox Placeholder="This is a password box"/>
</ws:StackPanel>
```

![Placeholder 示例效果](/images/placeholderproperty-example.png)

## 圆角裁剪

通过绑定 Clip 到 Border 的 ContentClip 属性, 可以自动裁剪超出圆角的部分.

使用实例:
{: .fw-300}

```xml
<ws:StackPanel Orientation="Horizontal" 
               Spacing="4">
    <Border Width="100" Height="100"
            BorderThickness="1"
            BorderBrush="Gray"
            CornerRadius="15">
        <TextBlock Text="Hello"/>
    </Border>
    <ws:Border Width="100" Height="100"
               BorderThickness="1"
               BorderBrush="Gray"
               CornerRadius="15">
        <TextBlock Text="Hello"
                   Clip="{Binding RelativeSource={RelativeSource AncestorType=ws:Border},Path=ContentClip}"/>
    </ws:Border>
</ws:StackPanel>
```

![圆角裁剪示例效果](/images/borderclip-example.png)