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
- ScrollViewer: Origin scroll viewer with 'MouseWheelDelta' based scrolling
- ConditionalControl: Display the control based on the condition
- TextBox: Origin text box with `Placeholder` property

Value converters:

- AddNumberConverter: Mathematical calculations, addition
- SubstractNumberConverter: Mathematical calculations, subtraction
- MultiplyNumberConverter: Mathematical calculations, multiplication
- DivideNumberConverter: Mathematical calculations, division
- NumberCompareConverter: Compare between numbers, support `equal`, `not equal`, `greator than`, `greator or equal`, `less than`, `less or equal`
- ObjectCompareConverter: Compare between objects, support `equal`, `not equal`
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

Utilities:

- BindingProxy: A utility class for binding, commonly used when a collection element has property to be bound to a page DataContext