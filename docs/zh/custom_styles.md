---
title: 自定义样式
layout: default
nav_order: 1
permalink: /zh/custom-styles
parent: 中文文档
---

# 自定义样式
{: .fs-9 }

在接下来的文档中, 会使用类似于 FluentDesign 的样式来进行演示
{: .fs-6 .fw-300 }

---

将下面这个 ResourceDictionary 添加到 App.xaml 中以应用到全局

```xml
<ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                    xmlns:ws="clr-namespace:EleCho.WpfSuite;assembly=EleCho.WpfSuite"
                    xmlns:sys="clr-namespace:System;assembly=mscorlib">

    <sys:String x:Key="WpfSuiteDesignName">SimpleDesign</sys:String>

    <Color x:Key="PrimaryColor">#0067c0</Color>
    <SolidColorBrush x:Key="PrimaryBrush" Color="{DynamicResource PrimaryColor}"/>

    <LinearGradientBrush x:Key="ControlBorder" StartPoint="0,0" EndPoint="0,1">
        <GradientStop Offset="0" Color="#18000000"/>
        <GradientStop Offset=".9" Color="#18000000"/>
        <GradientStop Offset="1" Color="#33000000"/>
    </LinearGradientBrush>

    <LinearGradientBrush x:Key="TextBoxBorder" StartPoint="0,0" EndPoint="0,1">
        <GradientStop Offset="0" Color="#18000000"/>
        <GradientStop Offset=".9" Color="#18000000"/>
        <GradientStop Offset="1" Color="#88000000"/>
    </LinearGradientBrush>

    <LinearGradientBrush x:Key="FocusedTextBoxBorder" StartPoint="0,0" EndPoint="0,1">
        <GradientStop Offset="0" Color="#18000000"/>
        <GradientStop Offset=".9" Color="#18000000"/>
        <GradientStop Offset="1" Color="{DynamicResource PrimaryColor}"/>
    </LinearGradientBrush>

    <Style TargetType="{x:Type ws:TextBox}"
           BasedOn="{StaticResource {x:Type ws:TextBox}}">
        <Setter Property="CornerRadius" Value="4"/>
        <Setter Property="Padding" Value="4 6"/>
        <Setter Property="VerticalContentAlignment" Value="Center"/>
        <Setter Property="BorderBrush" Value="{StaticResource TextBoxBorder}"/>
        <Setter Property="HoverBorderBrush" Value="{x:Null}"/>
        <Setter Property="FocusedBorderBrush" Value="{StaticResource FocusedTextBoxBorder}"/>
        <Setter Property="Background" Value="#EEFFFFFF"/>
        <Setter Property="HoverBackground" Value="#88FFFFFF"/>
    </Style>

    <Style TargetType="{x:Type ws:PasswordBox}"
           BasedOn="{StaticResource {x:Type ws:PasswordBox}}">
        <Setter Property="CornerRadius" Value="4"/>
        <Setter Property="Padding" Value="4 6"/>
        <Setter Property="VerticalContentAlignment" Value="Center"/>
        <Setter Property="BorderBrush" Value="{StaticResource TextBoxBorder}"/>
        <Setter Property="HoverBorderBrush" Value="{x:Null}"/>
        <Setter Property="FocusedBorderBrush" Value="{StaticResource FocusedTextBoxBorder}"/>
        <Setter Property="Background" Value="#EEFFFFFF"/>
        <Setter Property="HoverBackground" Value="#88FFFFFF"/>
    </Style>

    <Style TargetType="{x:Type ws:Button}"
           BasedOn="{StaticResource {x:Type ws:Button}}">
        <Setter Property="CornerRadius" Value="4"/>
        <Setter Property="Padding" Value="8 6"/>
        <Setter Property="BorderBrush" Value="{StaticResource ControlBorder}"/>
        <Setter Property="HoverBorderBrush" Value="{x:Null}"/>
        <Setter Property="PressedBorderBrush" Value="{StaticResource ControlBorder}"/>
        <Setter Property="Background" Value="#EEFFFFFF"/>
        <Setter Property="HoverBackground" Value="#88FFFFFF"/>
        <Setter Property="PressedBackground" Value="#44FFFFFF"/>
    </Style>

    <Style TargetType="{x:Type ws:ToggleButton}"
           BasedOn="{StaticResource {x:Type ws:ToggleButton}}">
        <Setter Property="CornerRadius" Value="4"/>
        <Setter Property="Padding" Value="8 6"/>
        <Setter Property="BorderBrush" Value="{StaticResource ControlBorder}"/>
        <Setter Property="HoverBorderBrush" Value="{x:Null}"/>
        <Setter Property="PressedBorderBrush" Value="{StaticResource ControlBorder}"/>
        <Setter Property="Background" Value="#EEFFFFFF"/>
        <Setter Property="HoverBackground" Value="#88FFFFFF"/>
        <Setter Property="PressedBackground" Value="#44FFFFFF"/>
    </Style>

    <Style TargetType="{x:Type ws:ComboBox}"
           BasedOn="{StaticResource {x:Type ws:ComboBox}}">
        <Setter Property="Padding" Value="8 6"/>
        <Setter Property="CornerRadius" Value="4"/>
        <Setter Property="PopupCornerRadius" Value="4"/>
        <Setter Property="ClipToBounds" Value="True"/>
        <Setter Property="BorderBrush" Value="{StaticResource ControlBorder}"/>
        <Setter Property="HoverBorderBrush" Value="{StaticResource ControlBorder}"/>
        <Setter Property="PressedBorderBrush" Value="{StaticResource ControlBorder}"/>
        <Setter Property="Background" Value="#EEFFFFFF"/>
        <Setter Property="HoverBackground" Value="#88FFFFFF"/>
        <Setter Property="PressedBackground" Value="#44FFFFFF"/>
        
        <!--Popup-->
        <Setter Property="PopupBorderBrush" Value="{DynamicResource ControlBorder}"/>
    </Style>

    <Style TargetType="{x:Type ws:ComboBoxItem}"
           BasedOn="{StaticResource {x:Type ws:ComboBoxItem}}">
        <Setter Property="Margin" Value="2 1"/>
        <Setter Property="Padding" Value="8 6"/>
        <Setter Property="CornerRadius" Value="2"/>
        <Setter Property="BorderThickness" Value="2 0 0 0"/>
        <Setter Property="HoverBackground" Value="#11000000"/>
        <Setter Property="HoverBorderBrush" Value="#11000000"/>

        <Setter Property="SelectedBackground" Value="#11000000"/>
        <Setter Property="SelectedBorderBrush" Value="{DynamicResource PrimaryBrush}"/>

        <Setter Property="HoverFocusedBackground" Value="#11000000"/>
        <Setter Property="HoverFocusedBorderBrush" Value="#11000000"/>
        
        <Setter Property="SelectedFocusedBackground" Value="#11000000"/>
        <Setter Property="HoverSelectedBackground" Value="#11000000"/>
        <Setter Property="HoverSelectedBorderBrush" Value="{DynamicResource PrimaryBrush}"/>
        <Setter Property="FocusedBorderBrush" Value="Transparent"/>
    </Style>

    <Style TargetType="{x:Type ws:CheckBox}"
           BasedOn="{StaticResource {x:Type ws:CheckBox}}">
        <Setter Property="CornerRadius" Value="2"/>
    </Style>

    <Style TargetType="{x:Type ws:ListBox}"
           BasedOn="{StaticResource {x:Type ws:ListBox}}">
        <Setter Property="CornerRadius" Value="4"/>
    </Style>

    <Style TargetType="{x:Type ws:ListBoxItem}"
           BasedOn="{StaticResource {x:Type ws:ListBoxItem}}">
        <Setter Property="CornerRadius" Value="4"/>
    </Style>

    <Style TargetType="{x:Type ws:TabControl}"
           BasedOn="{StaticResource {x:Type ws:TabControl}}">
        <Setter Property="CornerRadius" Value="4"/>
    </Style>

    <Style TargetType="{x:Type ws:TabItem}"
           BasedOn="{StaticResource {x:Type ws:TabItem}}">
        <Setter Property="CornerRadius" Value="2"/>
    </Style>

    <Style TargetType="{x:Type ws:ContextMenu}"
           BasedOn="{StaticResource {x:Type ws:ContextMenu}}">
        <Setter Property="CornerRadius" Value="4"/>
    </Style>

    <Style TargetType="{x:Type ws:Menu}"
           BasedOn="{StaticResource {x:Type ws:Menu}}">
        <Setter Property="CornerRadius" Value="4"/>
    </Style>

    <Style TargetType="{x:Type ws:MenuItem}"
           BasedOn="{StaticResource {x:Type ws:MenuItem}}">
        <Setter Property="CornerRadius" Value="2"/>
    </Style>

    <Style TargetType="{x:Type ws:ScrollBar}"
           BasedOn="{StaticResource {x:Type ws:ScrollBar}}">
        <Setter Property="ThumbCornerRadius" Value="3"/>
        <Setter Property="ButtonCornerRadius" Value="3"/>
        <Style.Triggers>
            <Trigger Property="Orientation" Value="Vertical">
                <Setter Property="Width" Value="12"/>
                <Setter Property="MinWidth" Value="12"/>
                <Setter Property="GlyphMargin" Value="1 2"/>
            </Trigger>
            <Trigger Property="Orientation" Value="Horizontal">
                <Setter Property="Height" Value="12"/>
                <Setter Property="MinHeight" Value="12"/>
                <Setter Property="GlyphMargin" Value="1 2"/>
            </Trigger>
        </Style.Triggers>
    </Style>

    <Style TargetType="{x:Type ws:Tooltip}"
           BasedOn="{StaticResource {x:Type ws:Tooltip}}">
        <!--nothing-->
    </Style>

</ResourceDictionary>
```