---
title: 值转换器
layout: default
nav_order: 6
permalink: /zh/value-converters
parent: 中文文档
---

# 值转换器
{: .fs-9 }

WPF Suite 提供了各种你会经常用到的值转换器, 例如数学计算, 空值, 空字符串以及空集合验证, 类型转换等
{: .fs-6 .fw-300 }

---

下面是 WPF Suite 中值转换器的使用说明.

对于可单例访问的值转换器, 你可以通过 `{x:Static ws:转换器类型名.Instance}` 来访问.


## AddNumberConverter

进行数字加法的转换器.

| 属性 | 类型 | 说明 |
| --- | --- | --- |
| Other | double | 要减去的值, 默认为 0 |

转换器会将源值加上指定的 Other 值, 如果转换器接受的参数也是数字, 那么也加上参数值

转换器的目标类型是 IConvertible 的实现, 那么结果会使用 System.Convert.ChangeType 尝试转换为目标类型, 否则直接返回 double 类型的计算结果.
如果转换失败, 则返回 DependencyProperty.UnsetValue


## SubtractNumberConverter

进行数字减法的转换器.

| 属性 | 类型 | 说明 |
| --- | --- | --- |
| Other | double | 要减去的值, 默认为 0 |

转换器会将源值减去指定的 Other 值, 如果转换器接受的参数也是数字, 那么也减去参数值

转换器的目标类型是 IConvertible 的实现, 那么结果会使用 System.Convert.ChangeType 尝试转换为目标类型, 否则直接返回 double 类型的计算结果.
如果转换失败, 则返回 DependencyProperty.UnsetValue


## MultiplyNumberConverter

进行数字乘法的转换器.

| 属性 | 类型 | 说明 |
| --- | --- | --- |
| By | double | 要乘以的值, 默认为 1 |

转换器会将源值乘以指定的 By 值, 如果转换器接受的参数也是数字, 那么也乘以参数值

转换器的目标类型是 IConvertible 的实现, 那么结果会使用 System.Convert.ChangeType 尝试转换为目标类型, 否则直接返回 double 类型的计算结果.
如果转换失败, 则返回 DependencyProperty.UnsetValue


## DivideNumberConverter

进行数字除法的转换器.

| 属性 | 类型 | 说明 |
| --- | --- | --- |
| By | double | 要除以的值, 默认为 1 |

转换器会将源值除以指定的 By 值, 如果转换器接受的参数也是数字, 那么也除以参数值

转换器的目标类型是 IConvertible 的实现, 那么结果会使用 System.Convert.ChangeType 尝试转换为目标类型, 否则直接返回 double 类型的计算结果.
如果转换失败, 则返回 DependencyProperty.UnsetValue


## NumberCompareConverter

用于比较数字, 并返回布尔值的转换器.

| 属性 | 类型 | 说明 |
| --- | --- | --- |
| TargetValue | double | 要进行比较的目标值 |
| Comparison | NumberComparison | 数字的比较方法, 可以是等于, 不等于, 大于, 小于, 大于等于, 小于等于 |

转换器会将源值与目标值使用指定的方法进行比较, 并返回比较结果. 如果转换器参数也是可识别的数字, 那么 "TargetValue" 属性会被忽略, 而是使用转换器参数作为要比较的目标值.

转换器会忽略目标类型, 始终返回布尔. 如果其中任何一个过程出问题, 则返回 DependencyProperty.UnsetValue


## ClampNumberConverter

将数字限制在指定最小值与最大值之间的转换器.

| 属性 | 类型 | 说明 |
| --- | --- | --- |
| Minimum | double | 最小值, 默认为负无限 |
| Maximum | double | 最大值, 默认为正无限 |

转换器会将源值限制在指定的最小到最大值区间, 使其不小于最小值, 不大于最大值. (可以等于)

转换器的目标类型是 IConvertible 的实现, 那么结果会使用 System.Convert.ChangeType 尝试转换为目标类型, 否则直接返回 double 类型的结果.
如果转换失败, 则返回 DependencyProperty.UnsetValue


## NumberConverter

将数字转换为指定目标数字类型的转换器.

| 属性 | 类型 | 说明 |
| --- | --- | --- |
| TargetType | Type | 目标类型, 默认为 null |

转换器会将源值转换为所需的目标类型. 由于目标类型会自动传入到转换器逻辑, 所以用户一般不需要手动指定 "TargetType".

除非绑定的目标类型不明确, 或者该转换器位于转换器组中且不是最后一个时, 用户才需要强制指定目标类型.
如果转换存在异常, 则返回 DependencyProperty.UnsetValue


## NumberToThicknessConverter

将数字转换为 Thickness 类型的转换器.

此转换器可单例访问


## NumberToCornerRadiusConverter

将数字转换为 CornerRadius 类型的转换器.

此转换器可单例访问


## ValueIsNullConverter

判断值是否为空的转换器. 如果为空, 则返回 true, 否则返回 false

此转换器可单例访问


## ValueIsNotNullConverter

判断值是否不为空的转换器. 如果不为空, 则返回 true, 否则返回 false

此转换器可单例访问


## EqualConverter

判断值是否相等的转换器.

| 属性 | 类型 | 说明 |
| --- | --- | --- |
| TargetValue | object | 要比较的目标值, 默认为 null |

转换器会比较源值以及目标值是否相等, 如果相等, 则返回 true, 否则返回 false. 如果转换器参数不为空, 则忽略 "TargetValue" 属性, 转而使用转换器参数为目标值.

此转换器可单例访问


## NotEqualConverter

判断值是否不相等的转换器.

| 属性 | 类型 | 说明 |
| --- | --- | --- |
| TargetValue | object | 要比较的目标值, 默认为 null |

转换器会比较源值以及目标值是否不相等, 如果不相等, 则返回 true, 否则返回 false. 如果转换器参数不为空, 则忽略 "TargetValue" 属性, 转而使用转换器参数为目标值.

此转换器可单例访问


## BooleanToVisibilityConverter

将布尔值转换为 Visibility 的转换器.

| 属性 | 类型 | 说明 |
| --- | --- | --- |
| ValueWhenTrue | Visibility | 当布尔值为 true 时, 返回的目标值, 默认为 Visibility.Visible |
| ValueWhenFalse | Visibility | 当布尔值为 false 时, 返回的目标值, 默认为 Visibility.Collapsed |

当布尔值为 true 时, 返回 ValueWhenTrue 的值, 否则返回 ValueWhenFalse 的值.

此转换器可单例访问


## InvertBooleanConverter

用于反转布尔值的转换器.

当源值为 true 时, 返回 false, 否则返回 true. 如果出现错误, 则返回 DependencyProperty.UnsetValue

此转换器可单例访问


## InvertThicknessConverter

用于反转 Thickness 值的转换器.

转换器会将源 Thickness 的值乘以负一, 然后返回. 如果原值不是 Thickness, 则返回 DependencyProperty.UnsetValue

此转换器可单例访问


## StringIsNullOrEmptyConverter

用于判断字符串是否为 null 或空字符串的转换器.

如果是 null 或空字符串, 则返回 true, 否则返回 false. 如果源值不是字符串, 也返回 false

此转换器可单例访问


## StringIsNullOrWhitespaceConverter

用于判断字符串是否为 null 或空白字符串的转换器.

如果是 null 或空白字符串, 则返回 true, 否则返回 false. 如果源值不是字符串, 也返回 false

此转换器可单例访问


## StringIsNotNullOrEmptyConverter

用于判断字符串是否为 null 或空字符串的转换器.

如果不是 null 或空字符串, 则返回 true, 否则返回 false. 如果源值不是字符串, 也返回 false

此转换器可单例访问


## StringIsNotNullOrWhitespaceConverter

用于判断字符串是否为 null 或空白字符串的转换器.

如果不是 null 或空白字符串, 则返回 true, 否则返回 false. 如果源值不是字符串, 也返回 false

此转换器可单例访问


## StringToImageSourceConverter

用于将字符串转换为 ImageSource 的转换器.

如果字符串是有效的 Uri, 则通过 Uri 创建一个 BitmapImage, 否则返回 DependencyProperty.UnsetValue

此转换器可单例访问


## CollectionIsNullOrEmptyConverter

判断集合是否为 null 或空集合的转换器.

如果值为 null 或空集合, 则返回 true. 如果存在任何错误, 则返回 DependencyProperty.UnsetValue

此转换器可单例访问


## CollectionIsNotNullOrEmptyConverter

判断集合是否不为 null 或空集合的转换器.

如果值不为 null, 且不是空集合, 则返回 true. 如果存在任何错误, 则返回 DependencyProperty.UnsetValue

此转换器可单例访问


## PrimitiveTypeConverter

基元类型转换器.

将源值, 使用 System.Convert.ChangeType 转换为目标类型.

{: .warning }
> 注意, 当使用该转换器将浮点类型转换为整数型时, 其逻辑与 Math.Round 一致, 如果你需要舍去小数部分, 你应该使用 NumberConverter

## FallbackConverter

提供 Fallback 功能的多值转换器.

返回所有值中第一个不为 null 且不为 DependencyProperty.UnsetValue 的值.

此转换器可单例访问