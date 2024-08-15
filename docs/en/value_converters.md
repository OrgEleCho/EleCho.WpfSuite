---
title: Value Converters
layout: default
nav_order: 6
permalink: /zh/value-converters
parent: Documentation
---

# ValueConverter
{: .fs-9 }

The WPF Suite provides various value converters that you will frequently use, such as mathematical calculations, null value, empty string, and empty collection validation, type conversion, and more.
{: .fs-6 .fw-300 }

---

Below is the usage guide for value converters in the WPF Suite.

For value converters that can be accessed as singletons, you can access them using `{x:Static ws:ConverterTypeName.Instance}`.


## AddNumberConverter

A converter for performing numerical addition.

| Property | Type | Description |
| --- | --- | --- |
| Other | double | The value to subtract, default is 0 |

The converter adds the specified Other value to the source value. If the converter receives a numeric parameter, it will also add the parameter value.

If the target type of the converter is an implementation of IConvertible, the result will be attempted to be converted to the target type using System.Convert.ChangeType. Otherwise, the calculation result will be returned as a double type.
If the conversion fails, it will return DependencyProperty.UnsetValue.


## SubtractNumberConverter

A converter for performing numerical subtraction.

| Property | Type | Description |
| --- | --- | --- |
| Other | double | The value to subtract, default is 0 |

The converter subtracts the specified Other value from the source value. If the converter receives a numeric parameter, it will also subtract the parameter value.

If the target type of the converter is an implementation of IConvertible, the result will be attempted to be converted to the target type using System.Convert.ChangeType. Otherwise, the calculation result will be returned as a double type.
If the conversion fails, it will return DependencyProperty.UnsetValue.


## MultiplyNumberConverter

A converter for performing numerical multiplication.

| Property | Type | Description |
| --- | --- | --- |
| By | double | The value to multiply by, default is 1 |

The converter multiplies the source value by the specified By value. If the converter receives a numeric parameter, it will also multiply by the parameter value.

If the target type of the converter is an implementation of IConvertible, the result will be attempted to be converted to the target type using System.Convert.ChangeType. Otherwise, the calculation result will be returned as a double type.
If the conversion fails, it will return DependencyProperty.UnsetValue.


## DivideNumberConverter

A converter for performing numerical division.

| Property | Type | Description |
| --- | --- | --- |
| By | double | The value to divide by, default is 1 |

The converter divides the source value by the specified By value. If the converter receives a numeric parameter, it will also divide by the parameter value.

If the target type of the converter is an implementation of IConvertible, the result will be attempted to be converted to the target type using System.Convert.ChangeType. Otherwise, the calculation result will be returned as a double type.
If the conversion fails, it will return DependencyProperty.UnsetValue.


## NumberCompareConverter

A converter for comparing numbers and returning a Boolean value.

| Property | Type | Description |
| --- | --- | --- |
| TargetValue | double | The target value to compare against |
| Comparison | NumberComparison | The method of comparison, which can be Equal, NotEqual, GreaterThan, LessThan, GreaterThanOrEqual, or LessThanOrEqual |

The converter compares the source value with the target value using the specified method and returns the comparison result. If the converter parameter is also a recognizable number, the TargetValue property will be ignored, and the converter parameter will be used as the target value for comparison.

The converter ignores the target type and always returns a Boolean. If any part of the process encounters an issue, it returns DependencyProperty.UnsetValue.


## ClampNumberConverter

A converter that restricts a number within a specified minimum and maximum range.

| Property | Type | Description |
| --- | --- | --- |
| Minimum | double | The minimum value, default is negative infinity |
| Maximum | double | The maximum value, default is positive infinity |

The converter restricts the source value to be within the specified minimum and maximum range, ensuring it is not less than the minimum and not greater than the maximum (it can be equal to either).

If the target type of the converter is an implementation of IConvertible, the result will be attempted to be converted to the target type using System.Convert.ChangeType. Otherwise, the result will be returned as a double type.
If the conversion fails, it will return DependencyProperty.UnsetValue.


## NumberConverter

A converter that converts a number to a specified target numeric type.

| Property | Type | Description |
| --- | --- | --- |
| TargetType | Type | The target type, default is null |

The converter converts the source value to the desired target type. Since the target type is automatically passed into the converter logic, users generally do not need to manually specify the TargetType.

Users only need to explicitly specify the target type if the binding target type is unclear or if the converter is part of a converter group and is not the last one in the group.
If the conversion encounters an issue, it will return DependencyProperty.UnsetValue.


## NumberToThicknessConverter

A converter that converts a number to a Thickness type.

This converter can be accessed as a singleton.


## NumberToCornerRadiusConverter

A converter that converts a number to a CornerRadius type.

This converter can be accessed as a singleton.


## ValueIsNullConverter

A converter that checks if a value is null. If it is null, it returns true; otherwise, it returns false.

This converter can be accessed as a singleton.


## ValueIsNotNullConverter

A converter that checks if a value is not null. If it is not null, it returns true; otherwise, it returns false.

This converter can be accessed as a singleton.


## EqualConverter

A converter that checks if two values are equal.

| Property | Type | Description |
| --- | --- | --- |
| TargetValue | object | The target value to compare against, default is null |

The converter compares the source value with the target value. If they are equal, it returns true; otherwise, it returns false. If the converter parameter is not null, the `TargetValue` property will be ignored, and the converter parameter will be used as the target value.

This converter can be accessed as a singleton.


## NotEqualConverter

A converter that checks if two values are not equal.

| Property | Type | Description |
| --- | --- | --- |
| TargetValue | object | The target value to compare against, default is null |

The converter compares the source value with the target value. If they are not equal, it returns true; otherwise, it returns false. If the converter parameter is not null, the `TargetValue` property will be ignored, and the converter parameter will be used as the target value.

This converter can be accessed as a singleton.


## BooleanToVisibilityConverter

A converter that converts a Boolean value to Visibility.

| Property | Type | Description |
| --- | --- | --- |
| ValueWhenTrue | Visibility | The target value returned when the Boolean value is true, default is Visibility.Visible |
| ValueWhenFalse | Visibility | The target value returned when the Boolean value is false, default is Visibility.Collapsed |

When the Boolean value is true, the converter returns the value of ValueWhenTrue; otherwise, it returns the value of ValueWhenFalse.

This converter can be accessed as a singleton.


## InvertBooleanConverter

A converter for inverting Boolean values.

When the source value is true, it returns false; otherwise, it returns true. If an error occurs, it returns DependencyProperty.UnsetValue.

This converter can be accessed as a singleton.


## InvertThicknessConverter

A converter for inverting Thickness values.

The converter multiplies the source Thickness value by negative one and returns the result. If the original value is not a Thickness, it returns DependencyProperty.UnsetValue.

This converter can be accessed as a singleton.


## StringIsNullOrEmptyConverter

A converter that checks if a string is null or an empty string.

If the string is null or empty, it returns true; otherwise, it returns false. If the source value is not a string, it also returns false.

This converter can be accessed as a singleton.


## StringIsNullOrWhitespaceConverter

A converter that checks if a string is null or consists only of whitespace characters.

If the string is null or a whitespace string, it returns true; otherwise, it returns false. If the source value is not a string, it also returns false.

This converter can be accessed as a singleton.


## StringIsNotNullOrEmptyConverter

A converter that checks if a string is not null or an empty string.

If the string is neither null nor empty, it returns true; otherwise, it returns false. If the source value is not a string, it also returns false.

This converter can be accessed as a singleton.


## StringIsNotNullOrWhitespaceConverter

A converter that checks if a string is not null and not a whitespace string.

If the string is neither null nor a whitespace string, it returns true; otherwise, it returns false. If the source value is not a string, it also returns false.

This converter can be accessed as a singleton.


## StringToImageSourceConverter

A converter that converts a string to an ImageSource.

If the string is a valid Uri, it creates a BitmapImage from the Uri; otherwise, it returns DependencyProperty.UnsetValue.

This converter can be accessed as a singleton.


## CollectionIsNullOrEmptyConverter

A converter that checks if a collection is null or an empty collection.

If the value is null or an empty collection, it returns true. If any error occurs, it returns DependencyProperty.UnsetValue.

This converter can be accessed as a singleton.


## CollectionIsNotNullOrEmptyConverter

A converter that checks if a collection is neither null nor an empty collection.

If the value is not null and is not an empty collection, it returns true. If any error occurs, it returns DependencyProperty.UnsetValue.

This converter can be accessed as a singleton.


## PrimitiveTypeConverter

Primitive type converter.

It converts the source value to the target type using System.Convert.ChangeType.

{: .warning }
> Note: When using this converter to convert from floating-point types to integer types, the logic is consistent with Math.Round. If you need to truncate the decimal part, you should use NumberConverter.

## FallbackConverter

A multi-value converter that provides fallback functionality.

It returns the first value among all values that is not null and not DependencyProperty.UnsetValue.

This converter can be accessed as a singleton.