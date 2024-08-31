# EleCho.WpfSuite

WPF layout panels, controls, value converters, markup extensions, transitions and utilities

> For full documentation, see [wpfsuite.elecho.dev](https://wpfsuite.elecho.dev)

## Usage

Add XML namespace to your XAML file:

```xml
<Window xmlns:ws="https://schemas.elecho.dev/wpfsuite">
    ...     
</Window>
```

Enjoy!

```xml
<Window x:Class="WpfTest.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:WpfTest"
        xmlns:ws="https://schemas.elecho.dev/wpfsuite"
        mc:Ignorable="d"
        Title="MainWindow" Height="450" Width="800">

    <ws:ScrollViewer Margin="12"
                     ScrollWithWheelDelta="True">
        <ws:StackPanel Spacing="8">
            <ws:TextBox Width="200"
                        Placeholder="This is a text box with placeholder"/>
            <ws:Image Source="/Assets/TestAvatar.jpg"
                      CornerRadius="10"/>

            <StackPanel>
                <TextBlock Text="WrapPanel with spacing"/>
                <ItemsControl ItemsSource="{Binding TestItems}">
                    <ItemsControl.ItemsPanel>
                        <ItemsPanelTemplate>
                            <ws:WrapPanel HorizontalSpacing="8"
                                          VerticalSpacing="8"/>
                        </ItemsPanelTemplate>
                    </ItemsControl.ItemsPanel>
                </ItemsControl>
            </StackPanel>
        </ws:StackPanel>
    </ws:ScrollViewer>

</Window>
```


## Features

The following is a summary of the features of WPF Suite.


### Layout panels:

- StackPanel: Origin stack panel with `Spacing` property
- WrapPanel: Origin wrap panel with `HorizontalSpacing` and `VerticalSpacing` property
- FlexPanel: Flex layout implementation with `HorizontalSpacing` and `VerticalSpacing` property
- MasonryPanel: Masonry layout implementation with `FlowSpacing` and `ItemSpacing` property

### Controls:

The WPF Suite has overridden many native controls, added some properties, or optimized their logic.
You can directly access the corresponding controls via the WPF Suite namespace.

Almost all controls have added the CornerRadius property, so now if you need to adjust the corner radius of a certain control,
you just need to set this property instead of changing the control template.

```xml
<ws:Button Content="Hello world"
           CornerRadius="3">
```

If a corresponding control has multiple states, such as "mouse hover" and "mouse press",
WPF Suite also exposes certain values corresponding to these states directly through properties,
such as "HoverBackground" which represents the background color used by the control in the hover state.
Therefore, if you need to change the style of certain controls in different states,
you just need to set the corresponding properties.


```xml
<ws:Button Content="Hello world"
           HoverBackground="Pink">
```

When using the default Border in WPF, it allows the content to exceed the bounds of the Border. 
Even if you set a CornerRadius and ClipToBounds, it won't be able to clip the content to fit within the rounded corners. 
However, when using the Border provided by the WPF Suite, 
you can achieve automatic clipping by binding the content's Clip property to the Border's ContentClip property.

```xml
<ws:Border CornerRadius="5" Width="50" Height="50">
    <Rectangle Fill="Pink"
               Clip="{Binding RelativeSource={RelativeSource AncestorType=ws:Border},Path=ContentClip}"/>
</ws:Border>

<ws:Border x:Name="NamedBorder" CornerRadius="5" Width="50" Height="50">
    <Rectangle Fill="Pink"
               Clip="{Binding ElementName=NamedBorder,Path=ContentClip}"/>
</ws:Border>
```

ContentControl in WPF suite can be understood as a ContentControl with added transition effects. 
After setting the transition effects, the specified transition effect will be executed when the content changes.

```xml
<ws:ContentControl Content="Some content">
    <ws:ContentControl.Transition>
        <ws:SlideFadeTransition Duration="0:0:0.200"/>
    </ws:ContentControl.Transition>
</ws:ContentControl>
```

WPF Suite has also made several optimizations for ScrollViewer.
For example, it now supports smooth scrolling with the mouse and allows for finer scrolling using touchpad,
which was not available in the original ScrollViewer.

```xml
<ws:ScrollViewer>
    <ws:StackPanel Spacing="8">
        <TextBlock Text="Some content"/>
        <!--And more-->
    </ws:StackPanel>
</ws:ScrollViewer>
```

> The enhanced features of ScrollViewer can be disabled via its properties.


In WPF, by default you cannot scroll the content of a ScrollViewer through a pen device.
However, WPF Suite provides a solution to simulate the pen as a touch device. Through this method, you can use the pen to scroll the content of the ScrollViewer.

```xml
<ws:ScrollViewer ws:StylusTouchDevice.Simulate="True">
    <ws:StackPanel Spacing="8">
        <TextBlock Text="Some content"/>
        <!--And more-->
    </ws:StackPanel>
</ws:ScrollViewer>
```

### Transitions:

- FadeTransition: Fades out the current content and fades in the new content by animating the opacity
- SlideTransition: Moves the old content out of view, and the new content into view
- SlideFadeTransition: Composite of SlideTransition and FadeTransition
- ScaleTransition: Scale the old content to smaller and fade out, and scale the new content from a larger zoom to 1
- ScaleFadeTransition: Composite of ScaleTransition and FadeTransition
- RotateTransition: Rotate the old content and new content by specified angle
- RotateFadeTransition: Composite of RotateTransition and FadeTransition

### Value converters:

- AddNumberConverter: Mathematical calculations, addition
- SubtractNumberConverter: Mathematical calculations, subtraction
- MultiplyNumberConverter: Mathematical calculations, multiplication
- DivideNumberConverter: Mathematical calculations, division
- ClampNumberConverter: Clamps the given value between the given minimum float and maximum values
- NumberCompareConverter: Compare between numbers, support `equal`, `not equal`, `greator than`, `greator or equal`, `less than`, `less or equal`
- EqualConverter: Check if the value equals converter parameter
- NotEqualConverter: Check if the value not equal to converter parameter
- ValueIsNullConverter: Determines whether the object is empty
- ValueIsNotNullConverter: Determines whether the object is not empty
- InvertBooleanConverter: Inverts the boolean value
- ValueConverterGroup: Converts the value using the specified multiple converters
- StringIsNullOrEmptyConverter: Check if string is null or empty, returns a boolean value
- StringIsNotNullOrEmptyConverter: Check if string is not null or empty, returns a boolean value
- StringIsNullOrWhiteSpaceConverter: Check if  string is null or white space, returns a boolean value
- StringIsNotNullOrWhiteSpaceConverter: Check if string is not null or white space, returns a boolean value
- CollectionIsNullOrEmptyConverter: Check if collection is null or empty, returns a boolean value
- CollectionIsNotNullOrEmptyConverter: Check if collection is not null or empty, returns a boolean value
- StringToImageSourceConverter: Convert any valid URI string to image source
- NumberToThicknessConverter: Convert number to uniform thickness
- NumberToCornerRadiusConverter: Convert number to uniform corner radius
- InvertThicknessConverter: Invert the given thickness value

### Markup extensions:

- String: Represents text as a sequence of UTF-16 code units. (returns System.String)
- Char: Represents a character as a UTF-16 code unit. (returns System.Char)
- Boolean: Represents a Boolean (true or false) value. (returns System.Boolean)
- Byte: Represents an 8-bit unsigned integer. (returns System.Byte)
- Int16: Represents a 16-bit signed integer. (returns System.Int16)
- Int32: Represents a 32-bit signed integer. (returns System.Int32)
- Int64: Represents a 64-bit signed integer. (returns System.Int64)
- Single: Represents a single-precision floating-point number. (returns System.Single)
- Double: Represents a double-precision floating-point number. (returns System.Double)
- Decimal: Represents a decimal floating-point number. (returns System.Decimal)
- HsvColor: Represents a color from HSV color space values (returns System.Windows.Media.Color)

### Animations:

- CornerRadiusAnimation: Animates the value of a CornerRadius property between two target values using linear interpolation over a specified Duration.
- QuadraticBezierEase: Quadratic bezier curve easing function
- CubicBezierEase: Cubic bezier curve easing function

### Utilities:

You can easily create windows with Mica or Acrylic materials through WPF Suite, just like this:

```xml
<Window ... 
        xmlns:ws="https://schemas.elecho.dev/wpfsuite"
        Background="Transparent"
        ws:WindowOption.Backdrop="Mica">
    ...
</Window>
```

You can also set the color mode of the current window. When IsDarkMode is set to True, Mica and Acrylic materials will also change accordingly.

```xml
<Window ...
        xmlns:ws="https://schemas.elecho.dev/wpfsuite"
        Background="Transparent"
        ws:WindowOption.Backdrop="Mica"
        ws.WindowOption.IsDarkMode="True">
    ...
</Window>
```



WindowOption.Backdrop can be used only on Windows11, if you want use acrylic on Windows10, you should use WindowOption.AccentState:

```xml
<Window ...
        xmlns:ws="https://schemas.elecho.dev/wpfsuite"
        Background="Transparent"
        ws.WindowOption.AccentState="AcrylicBlurBehind">
    ...
</Window>
```

If you want to use an acrylic material mixed with a certain color, there are two options.

Use ordinary acrylic material and set the form color to a translucent corresponding color. 

```xml
<Window ...
        xmlns:ws="https://schemas.elecho.dev/wpfsuite"
        Background="#11FF0000"
        ws.WindowOption.AccentState="AcrylicBlurBehind">
    ...
</Window>
```


Or use accent gradient color property.

```xml
<Window ...
        xmlns:ws="https://schemas.elecho.dev/wpfsuite"
        Background="Transparent"
        ws.WindowOption.AccentState="AcrylicBlurBehind"
        ws.WindowOption.AccentGradientColor="#33FF8888">
    ...
</Window>
```


- BindingProxy: A utility class for binding, commonly used when a collection element has property to be bound to a page DataContext
- ScrollViewerUtils: Utilities for set vertical offset and horizontal offset of scroll viewer and scroll content presenter
- ItemsControlUtils: Utilities for removing item from ItemsControl with transitions
- ColorUtils: Utilities for color calculation
- BooleanValues: Static value of boolean 'true' and 'false'
- ValidationUtils: DependencyProperty validation utilities