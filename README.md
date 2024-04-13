# EleCho.WpfSuite

Layout panels:

- StackPanel: Origin stack panel with `Spacing` property
- WrapPanel: Origin wrap panel with `HorizontalSpacing` and `VerticalSpacing` property
- FlexPanel: Flex layout implementation with `HorizontalSpacing` and `VerticalSpacing` property

Controls:

- Image: Simple image control with `CornerRadius` property
- ScrollViewer: Origin scroll viewer with 'MouseWheelDelta' based scrolling
- ConditionalControl: Display the control based on the condition

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