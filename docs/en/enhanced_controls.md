---
title: Enhanced Controls
layout: default
nav_order: 3
permalink: /en/enhanced-controls
parent: Documentation
---

# Enhanced Controls
{: .fs-9 }

WPF Suite has property extensions for most controls, including state properties, corner rounding, and some controls have functionality extensions.
{: .fs-6 .fw-300 }

---

## State Properties

You will see in the enhanced control, some additional properties, such as `HoverBackground`, `HoverBorderBrush`, which,
as the name suggests, indicate the value of the corresponding property in the corresponding state.

If these properties are set to null, they will also fallback correctly to the set `Background` and `BorderBrush` property values.

This means that you don't need to change the template to set the control to look a certain way in a certain state.

Example
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

Appearence, non-hovering, hovering and pressed states:

<p>
    <img src="/images/stateproperty-example-state1.png" title="状态属性示例,状态1" />
    <img src="/images/stateproperty-example-state2.png" title="状态属性示例,状态2" />
    <img src="/images/stateproperty-example-state3.png" title="状态属性示例,状态3" />
</p>

## Rounding Corners

Button, TextBox, PasswordBox and most controls now support CornerRadius for rounding corners.

Example
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

![CornerRadius Example](/images/cornerradiusproperty-example.png)

## Placeholder

TextBox and PasswordBox can now set placeholders via the Placeholder property. The placeholder is displayed when the textbox is empty.


Example
{: .fw-300}

```xml
<ws:StackPanel Spacing="4">
    <ws:TextBox Placeholder="This is a text box"/>
    <ws:PasswordBox Placeholder="This is a password box"/>
</ws:StackPanel>
```

![Placeholder Example](/images/placeholderproperty-example.png)

## Rounding Corners Clipping

By binding the Clip property to the Border's ContentClip property, you can automatically clip the portion that extends beyond the rounded corners.

Example:
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

![Rounding corners clipping example](/images/borderclip-example.png)

## Smooth Scrolling

If you use the ScrollViewer provided by the WPF Suite, it will have a smoother effect on content scrolling with the mouse.

| Property | Type | Description |
| --- | --- | --- |
| ScrollWithWheelDelta | bool | Determines the scroll distance by the delta value of Wheel event. This option is used to optimize the scroll effect. The default value is true |
| EnableScrollingAnimation | bool | Enable scrolling animation, which determines whether to use easing when scrolling with the mouse. The default value is true and this option requires ScrollWithWheelDelta to be set to true. Otherwise it doesn't work |
| ScrollingAnimationDuration | Duration | The duration of scrolling animation, the default value is 250 ms |
| MouseScrollDeltaFactor | double | When scrolling with the mouse, the multiplier of the scrolling Delta value. Change this value to change the scroll speed and direction. The default is 1 |
| TouchpadScrollDeltaFactor | double | When scrolling with the touchpad, the multiplier of the scrolling Delta value. Change this value to change the scroll speed and direction. The default is 1 |
| AlwaysHandleMouseWheelScrolling | bool | Always handle the mouse Wheel event for scrolling, ScrollViewer content is scrolled when the mouse wheel scrolls. The default is true |


{: .tip }
> Will AlwaysHandleMouseWheelScrolling set to false, the ScrollViewer smooth scrolling effect will not be applied in TextBox

Example:
{: .fw-300}

```xml
<ws:ScrollViewer>
    Some content...
</ws:ScrollViewer>
```

![ScrollViewer Example](/images/scrollviewer-example.webp)

## Touch Simulation

Native WPF does not support scrolling with a stylus on a ScrollViewer. However, if you want to enable scrolling with a stylus, you can use "touch device simulation". By simulating a touch device with the stylus, you can achieve scrolling functionality using the stylus.

| AttachedProperty | Type | Description |
| --- | --- | --- |
| StylusTouchDevice.Simulate | bool | Whether to simulate the pen as a touch device, default is false |
| StylusTouchDevice.MoveThreshold | double | Move threshold, the pen will report as touch move only after moving beyond the specified distance, default is 3 |
| MouseTouchDevice.Simulate | bool | Whether to simulate the mouse as a touch device, default is false |
| MouseTouchDevice.MoveThreshold | double | Move threshold, the mouse will report as touch move only after moving beyond the specified distance, default is 3 |

{: .tip }
> If you set the move threshold to 0, you will not be able to click on controls inside the ScrollViewer using the pen, because the pen always has some jitter and touch move will trigger the scrolling of the ScrollViewer, interrupting the click action.
>
> So, adjust the move threshold according to your desired "precision".
> (Many programs have a larger threshold for this, such as 5px, or even 10px or more)

Example:
{: .fw-300}

```xml
<ws:ScrollViewer PanningMode="Both"
                 ws:MouseTouchDevice.Simulate="True">
    SomeContent...
</ws:ScrollViewer>
```

![Touch simulation example](/images/simulatetouch-example.webp)