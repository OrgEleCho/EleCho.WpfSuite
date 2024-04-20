# EleCho.WpfSuite

WPF layout panels, controls, value converters and utilities

## Usage

Add XML namespace to your XAML file:

```xml
<Window x:Class="WpfTest.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:WpfTest"
        xmlns:ws="https://github.com/OrgEleCho/EleCho.WpfSuite"
        mc:Ignorable="d"
        Title="MainWindow" Height="450" Width="800">

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
        xmlns:ws="https://github.com/OrgEleCho/EleCho.WpfSuite"
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


Layout panels:

- StackPanel: Origin stack panel with `Spacing` property
- WrapPanel: Origin wrap panel with `HorizontalSpacing` and `VerticalSpacing` property
- FlexPanel: Flex layout implementation with `HorizontalSpacing` and `VerticalSpacing` property

Controls:

- Image: Simple image control with `CornerRadius` property
- SlicedImage: Simple image control for drawing '9SliceSprite'
- Button: Origin button with `CornerRadius` property
- ToggleButton: Origin toggle button with `CornerRadius` property
- ScrollViewer: Origin scroll viewer with 'MouseWheelDelta' based scrolling and scroll smoothing feature
- ConditionalControl: Display the control based on the condition
- TextBox: Origin text box with `Placeholder` and `CornerRadius` property
- ListBox: Origin list box with `CornerRadius` property
- ListBoxItem: Origin list box item with `CornerRadius` property
- TransitioningContentControl: ContentControl that allows you to set transitions
- Frame: Frame that allows you to set transitions
- ProgressBar: Origin progress bar with `CornerRadius` property
- Border: Origin border with `ContentClip` property for binding

Transitions:

- SlideTransition: Moves the old page out of view, and the new page into view
- FadeTransition: Fades out the current page and fades in the new page by animating the opacity
- SlideFadeTransition: SlideTransition and FadeTransition
- ScaleTransition: Scale the old page to smaller and fade out, and scale the new page from a larger zoom to 1 and fade in

Value converters:

- AddNumberConverter: Mathematical calculations, addition
- SubstractNumberConverter: Mathematical calculations, subtraction
- MultiplyNumberConverter: Mathematical calculations, multiplication
- DivideNumberConverter: Mathematical calculations, division
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

Animations:

- CornerRadiusAnimation: Animates the value of a CornerRadius property between two target values using linear interpolation over a specified Duration.

Utilities:

- BindingProxy: A utility class for binding, commonly used when a collection element has property to be bound to a page DataContext
- ScrollViewerHelper: Helper for set vertical offset and horizontal offset of scroll viewer and scroll content presenter
- ValidateHelper: DependencyProperty validation helper