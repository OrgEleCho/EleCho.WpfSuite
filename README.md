# EleCho.WpfSuite

Layout panels:

- StackPanel: Origin stack panel with "Spacing" property
- WrapPanel: Origin wrap panel with "HorizontalSpacing" and "VerticalSpacing" property
- FlexPanel: Flex layout implementation with `HorizontalSpacing` and `VerticalSpacing`

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

Utilities:

- BindingProxy: A utility class for binding, commonly used when a collection element has property to be bound to a page DataContext