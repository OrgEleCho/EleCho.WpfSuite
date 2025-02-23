using System;
using System.Collections.Generic;
using System.Text;

namespace EleCho.WpfSuite.Controls.StateManagerGenerator
{
    partial class Generator
    {
        private static void AddAttachedDependencyPropertyDefinition(
            StringBuilder sb, string propertyName, string propertyTypeName, string ownerTypeName, string defaultValue, string? coerceValueCallback, string? propertyChangedCallback, bool isValueType, bool addNullableTag, int indent, string metaDataTypeName = "PropertyMetadata")
        {
            var getSetTypeTag = addNullableTag ? $"{propertyTypeName}?" : propertyTypeName;
            var typeOfTypeTag = (isValueType && addNullableTag) ? $"{propertyTypeName}?" : propertyTypeName;
            var indentText = new string(' ', indent);

            sb.AppendLine(
                $$"""
                {{indentText}}public static {{getSetTypeTag}} Get{{propertyName}}(DependencyObject obj)
                {{indentText}}{
                {{indentText}}    return ({{getSetTypeTag}})obj.GetValue({{propertyName}}Property);
                {{indentText}}}
                {{indentText}}
                {{indentText}}public static void Set{{propertyName}}(DependencyObject obj, {{getSetTypeTag}} value)
                {{indentText}}{
                {{indentText}}    obj.SetValue({{propertyName}}Property, value);
                {{indentText}}}
                {{indentText}}
                {{indentText}}public static readonly DependencyProperty {{propertyName}}Property =
                {{indentText}}    DependencyProperty.RegisterAttached("{{propertyName}}", typeof({{typeOfTypeTag}}), typeof({{ownerTypeName}}), new {{metaDataTypeName}}({{defaultValue}}, propertyChangedCallback: {{propertyChangedCallback ?? "null"}}, coerceValueCallback: {{coerceValueCallback ?? "null"}}));
                {{indentText}}
                """);
        }

        private static void AddAttachedReadOnlyDependencyPropertyDefinition(
            StringBuilder sb, string propertyName, string propertyTypeName, string ownerTypeName, string defaultValue, string? coerceValueCallback, string? propertyChangedCallback, bool isValueType, bool addNullableTag, int indent, string metaDataTypeName = "PropertyMetadata")
        {
            var getSetTypeTag = addNullableTag ? $"{propertyTypeName}?" : propertyTypeName;
            var typeOfTypeTag = (isValueType && addNullableTag) ? $"{propertyTypeName}?" : propertyTypeName;
            var indentText = new string(' ', indent);

            sb.AppendLine(
                $$"""
                {{indentText}}public static {{getSetTypeTag}} Get{{propertyName}}(DependencyObject obj)
                {{indentText}}{
                {{indentText}}    return ({{getSetTypeTag}})obj.GetValue({{propertyName}}Property);
                {{indentText}}}
                {{indentText}}
                {{indentText}}public static readonly DependencyPropertyKey {{propertyName}}PropertyKey =
                {{indentText}}    DependencyProperty.RegisterAttachedReadOnly("{{propertyName}}", typeof({{typeOfTypeTag}}), typeof({{ownerTypeName}}), new {{metaDataTypeName}}({{defaultValue}}, propertyChangedCallback: {{propertyChangedCallback ?? "null"}}, coerceValueCallback: {{coerceValueCallback ?? "null"}}));
                {{indentText}}
                {{indentText}}public static readonly DependencyProperty {{propertyName}}Property = {{propertyName}}PropertyKey.DependencyProperty;
                """);
        }

        private static void AddActiveStateDefinition(
            StringBuilder sb, string ownerTypeName, int indent)
        {
            var indentText = new string(' ', indent);

            AddAttachedDependencyPropertyDefinition(sb, "ActiveState", "State", ownerTypeName, "State.Normal", null, "OnActiveStateChanged", true, false, 8);

            sb.AppendLine(
                $$"""
                {{indentText}}private static void OnActiveStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
                {{indentText}}{
                {{indentText}}    var newState = (State)e.NewValue;
                {{indentText}}
                """);

            foreach (var stateProperty in (StateProperty[])Enum.GetValues(typeof(StateProperty)))
            {
                sb.AppendLine($"{indentText}    ActiveState{stateProperty}(d, newState);");
            }

            sb.AppendLine(
                $$"""
                {{indentText}}}
                """);
        }

        private static void AddActiveStatePropertyMethod(
            StringBuilder sb, StateProperty stateProperty, string propertyTypeName, string ownerTypeName, bool propertyTypeIsBrush, int indent)
        {
            var indentText = new string(' ', indent);

            if (propertyTypeIsBrush)
            {
                sb.AppendLine(
                    $$"""
                    {{indentText}}private static void ActiveState{{stateProperty}}(DependencyObject d, State targetState)
                    {{indentText}}{
                    {{indentText}}    var nowValue = GetShowing{{stateProperty}}(d);
                    {{indentText}}    var storyboardKey = new DependencyObjectAndStateProperty(d, StateProperty.{{stateProperty}});
                    {{indentText}}
                    {{indentText}}    if (_runningStoryboards is not null &&
                    {{indentText}}        _runningStoryboards.ContainsKey(storyboardKey))
                    {{indentText}}    {
                    {{indentText}}        _runningStoryboards[storyboardKey].Stop();
                    {{indentText}}        _runningStoryboards.Remove(storyboardKey);
                    {{indentText}}    }
                    {{indentText}}
                    {{indentText}}    var targetValue = GetStatePropertyClassValue<Brush>(d, targetState, StateProperty.{{stateProperty}});
                    {{indentText}}
                    {{indentText}}    if (d is not FrameworkElement animatable ||
                    {{indentText}}        d.ReadLocalValue(Showing{{stateProperty}}Property) == DependencyProperty.UnsetValue)
                    {{indentText}}    {
                    {{indentText}}        d.SetValue(Showing{{stateProperty}}PropertyKey, targetValue);
                    {{indentText}}        return;
                    {{indentText}}    }
                    {{indentText}}
                    {{indentText}}    animatable.BeginAnimation(Showing{{stateProperty}}ProxyProperty, null);
                    {{indentText}}
                    {{indentText}}    var targetTransitionDuration = GetStatePropertyTransitionDuration(d, targetState, StateProperty.{{stateProperty}});
                    {{indentText}}
                    {{indentText}}    if (targetTransitionDuration.TimeSpan == TimeSpan.Zero)
                    {{indentText}}    {
                    {{indentText}}        d.SetValue(Showing{{stateProperty}}PropertyKey, targetValue);
                    {{indentText}}        return;
                    {{indentText}}    }
                    {{indentText}}
                    {{indentText}}    var targetEasingFunction = GetStatePropertyEasingFunction(d, targetState, StateProperty.{{stateProperty}});
                    {{indentText}}
                    {{indentText}}    if ((nowValue is null || nowValue.CanFreeze) &&
                    {{indentText}}        (targetValue is null || targetValue.CanFreeze))
                    {{indentText}}    {
                    {{indentText}}        BrushAnimation brushAnimation = new BrushAnimation()
                    {{indentText}}        {
                    {{indentText}}            From = nowValue,
                    {{indentText}}            To = targetValue,
                    {{indentText}}            Duration = targetTransitionDuration,
                    {{indentText}}            EasingFunction = targetEasingFunction,
                    {{indentText}}            FillBehavior = FillBehavior.HoldEnd,
                    {{indentText}}        };
                    {{indentText}}
                    {{indentText}}        d.SetValue(Showing{{stateProperty}}PropertyKey, nowValue);
                    {{indentText}}        animatable.BeginAnimation(Showing{{stateProperty}}ProxyProperty, brushAnimation, HandoffBehavior.SnapshotAndReplace);
                    {{indentText}}    }
                    {{indentText}}    else
                    {{indentText}}    {
                    {{indentText}}        _runningStoryboards ??= new();
                    {{indentText}}
                    {{indentText}}        var brushTransitionHelper = new BrushTransitionHelper(nowValue, targetValue, d, Showing{{stateProperty}}ProxyProperty);
                    {{indentText}}
                    {{indentText}}        DoubleAnimation doubleAnimation = new DoubleAnimation()
                    {{indentText}}        {
                    {{indentText}}            From = 0,
                    {{indentText}}            To = 1,
                    {{indentText}}            Duration = targetTransitionDuration,
                    {{indentText}}            EasingFunction = targetEasingFunction,
                    {{indentText}}            FillBehavior = FillBehavior.HoldEnd
                    {{indentText}}        };
                    {{indentText}}
                    {{indentText}}        var storyboard = new Storyboard();
                    {{indentText}}        Storyboard.SetTarget(doubleAnimation, brushTransitionHelper);
                    {{indentText}}        Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(BrushTransitionHelper.ProgressProperty));
                    {{indentText}}        storyboard.Children.Add(doubleAnimation);
                    {{indentText}}        storyboard.Completed += (s, e) =>
                    {{indentText}}        {
                    {{indentText}}            _runningStoryboards.Remove(storyboardKey);
                    {{indentText}}        };
                    {{indentText}}
                    {{indentText}}        _runningStoryboards[storyboardKey] = storyboard;
                    {{indentText}}        d.SetValue(Showing{{stateProperty}}PropertyKey, nowValue);
                    {{indentText}}        animatable.BeginStoryboard(storyboard, HandoffBehavior.SnapshotAndReplace, true);
                    {{indentText}}    }
                    {{indentText}}}
                    """);
            }
            else
            {
                sb.AppendLine(
                    $$"""
                    {{indentText}}private static void ActiveState{{stateProperty}}(DependencyObject d, State targetState)
                    {{indentText}}{
                    {{indentText}}    var targetValue = GetStatePropertyStructValue<{{propertyTypeName}}>(d, targetState, StateProperty.{{stateProperty}});
                    {{indentText}}
                    {{indentText}}    if (!targetValue.HasValue)
                    {{indentText}}    {
                    {{indentText}}        return;
                    {{indentText}}    }
                    {{indentText}}
                    {{indentText}}    if (d is not FrameworkElement animatable ||
                    {{indentText}}        d.ReadLocalValue(Showing{{stateProperty}}Property) == DependencyProperty.UnsetValue)
                    {{indentText}}    {
                    {{indentText}}        d.SetValue(Showing{{stateProperty}}PropertyKey, targetValue.Value);
                    {{indentText}}        return;
                    {{indentText}}    }
                    {{indentText}}
                    {{indentText}}    var nowValue = GetShowing{{stateProperty}}(d);
                    {{indentText}}    animatable.BeginAnimation(Showing{{stateProperty}}ProxyProperty, null);
                    {{indentText}}
                    {{indentText}}    var targetTransitionDuration = GetStatePropertyTransitionDuration(d, targetState, StateProperty.{{stateProperty}});
                    {{indentText}}
                    {{indentText}}    if (targetTransitionDuration.TimeSpan == TimeSpan.Zero)
                    {{indentText}}    {
                    {{indentText}}        d.SetValue(Showing{{stateProperty}}PropertyKey, targetValue.Value);
                    {{indentText}}        return;
                    {{indentText}}    }
                    {{indentText}}
                    {{indentText}}    var targetEasingFunction = GetStatePropertyEasingFunction(d, targetState, StateProperty.{{stateProperty}});
                    {{indentText}}
                    {{indentText}}    {{propertyTypeName}}Animation animation = new {{propertyTypeName}}Animation()
                    {{indentText}}    {
                    {{indentText}}        From = nowValue,
                    {{indentText}}        To = targetValue.Value,
                    {{indentText}}        Duration = targetTransitionDuration,
                    {{indentText}}        EasingFunction = targetEasingFunction,
                    {{indentText}}        FillBehavior = FillBehavior.HoldEnd,
                    {{indentText}}    };
                    {{indentText}}
                    {{indentText}}    d.SetValue(Showing{{stateProperty}}PropertyKey, nowValue);
                    {{indentText}}    animatable.BeginAnimation(Showing{{stateProperty}}ProxyProperty, animation, HandoffBehavior.SnapshotAndReplace);
                    {{indentText}}}
                    """);
            }
        }

        private static void AddStateFallback(
            StringBuilder sb, State state, string ownerTypeName, int indent)
        {
            var fallbackState = GetFallbackState(state);
            var defaultValue = $"State.{fallbackState}";

            AddAttachedDependencyPropertyDefinition(sb, $"State{state}Fallback", "State", ownerTypeName, defaultValue, null, null, true, true, indent);
        }

        private static void AddStateProperty(
            StringBuilder sb, State state, StateProperty stateProperty, string ownerTypeName, int indent)
        {
            var notDefaultState = state != State.Normal;
            var propertyPrefix = notDefaultState ? state.ToString() : string.Empty;

            var propertyName = $"{propertyPrefix}{stateProperty}";
            var propertyTypeName = GetTypeNameOfStateProperty(stateProperty, out bool isValueType, out _);
            var defaultValue = GetDefaultValueOfStateProperty(stateProperty, notDefaultState);

            var coerceValueCallback = default(string);
            var propertyChangedCallback = $"OnAnyState{stateProperty}Changed";

            AddAttachedDependencyPropertyDefinition(sb, propertyName, propertyTypeName, ownerTypeName, defaultValue, coerceValueCallback, propertyChangedCallback, isValueType, notDefaultState || !isValueType, indent);
        }

        private static void AddStateDefaultProperties(
            StringBuilder sb, State state, string ownerTypeName, int indent)
        {
            const string TypeDurationFullName = "Duration";
            const string TypeIEasingFunctionFullName = "IEasingFunction";

            if (state == State.Normal)
            {
                AddAttachedDependencyPropertyDefinition(sb, $"DefaultTransitionDuration", TypeDurationFullName, ownerTypeName, $"new {TypeDurationFullName}(TimeSpan.Zero)", null, null, true, false, indent);
                AddAttachedDependencyPropertyDefinition(sb, $"DefaultEasingFunction", TypeIEasingFunctionFullName, ownerTypeName, $"default({TypeIEasingFunctionFullName})", null, null, false, true, indent);
            }
            else
            {
                AddAttachedDependencyPropertyDefinition(sb, $"{state}TransitionDuration", TypeDurationFullName, ownerTypeName, $"default({TypeDurationFullName}?)", null, null, true, true, indent);
                AddAttachedDependencyPropertyDefinition(sb, $"{state}EasingFunction", TypeIEasingFunctionFullName, ownerTypeName, $"default({TypeIEasingFunctionFullName})", null, null, false, true, indent);
            }
        }

        private static void AddStatePropertyTransitionDuration(
            StringBuilder sb, State state, StateProperty stateProperty, string ownerTypeName, int indent)
        {
            const string TypeDurationFullName = "Duration";

            var notDefaultState = state != State.Normal;
            var propertyPrefix = notDefaultState ? state.ToString() : string.Empty;

            var propertyName = $"{propertyPrefix}{stateProperty}TransitionDuration";
            var propertyTypeName = TypeDurationFullName;
            var defaultValue = "default(Duration?)";

            var coerceValueCallback = GetTypeCoerceValueCallbackMethodName(propertyTypeName, true, true);
            var propertyChangedCallback = default(string);

            AddAttachedDependencyPropertyDefinition(sb, propertyName, propertyTypeName, ownerTypeName, defaultValue, coerceValueCallback, propertyChangedCallback, true, true, indent);
        }

        private static void AddStatePropertyEasingFunction(
            StringBuilder sb, State state, StateProperty stateProperty, string ownerTypeName, int indent)
        {
            const string TypeIEasingFunctionFullName = "IEasingFunction";

            var notDefaultState = state != State.Normal;
            var propertyPrefix = notDefaultState ? state.ToString() : string.Empty;

            var propertyName = $"{propertyPrefix}{stateProperty}EasingFunction";
            var propertyTypeName = TypeIEasingFunctionFullName;
            var defaultValue = "default(IEasingFunction)";

            var coerceValueCallback = default(string);
            var propertyChangedCallback = default(string);

            AddAttachedDependencyPropertyDefinition(sb, propertyName, propertyTypeName, ownerTypeName, defaultValue, coerceValueCallback, propertyChangedCallback, false, true, indent);
        }

        private static void AddCoerceDurationMethod(StringBuilder sb, int indent)
        {
            var indentText = new string(' ', indent);

            sb.AppendLine(
                $$"""
                {{indentText}}private static object CoerceDuration(DependencyObject d, object baseValue)
                {{indentText}}{
                {{indentText}}    if (baseValue is not Duration duration ||
                {{indentText}}        !duration.HasTimeSpan)
                {{indentText}}    {
                {{indentText}}        throw new ArgumentException();
                {{indentText}}    }
                {{indentText}}
                {{indentText}}    return baseValue;
                {{indentText}}}
                """);
        }

        private static void AddCoerceNullableDurationMethod(StringBuilder sb, int indent)
        {
            var indentText = new string(' ', indent);

            sb.AppendLine(
                $$"""
                {{indentText}}private static object CoerceNullableDuration(DependencyObject d, object baseValue)
                {{indentText}}{
                {{indentText}}    if (baseValue is Duration duration &&
                {{indentText}}        !duration.HasTimeSpan)
                {{indentText}}    {
                {{indentText}}        throw new ArgumentException();
                {{indentText}}    }
                {{indentText}}
                {{indentText}}    return baseValue;
                {{indentText}}}
                """);
        }

        private static void AddAnyStatePropertyChangedCallbackMethod(StringBuilder sb, StateProperty stateProperty, int indent)
        {
            var indentText = new string(' ', indent);

            sb.AppendLine(
                $$"""
                {{indentText}}private static void OnAnyState{{stateProperty}}Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
                {{indentText}}{
                {{indentText}}    ActiveState{{stateProperty}}(d, GetActiveState(d));
                {{indentText}}}
                """);
        }

        private static void AddShowingPropertyDefinitions(StringBuilder sb, StateProperty stateProperty, string ownerTypeName, int indent)
        {
            var indentText = new string(' ', indent);
            var propertyTypeName = GetTypeNameOfStateProperty(stateProperty, out var isValueType, out _);
            var defaultValue = GetDefaultValueOfStateProperty(stateProperty, false);

            AddAttachedReadOnlyDependencyPropertyDefinition(sb, $"Showing{stateProperty}", propertyTypeName, ownerTypeName, defaultValue, null, null, isValueType, !isValueType, indent);
            AddAttachedDependencyPropertyDefinition(sb, $"Showing{stateProperty}Proxy", propertyTypeName, ownerTypeName, defaultValue, null, $"OnShowing{stateProperty}ProxyChanged", isValueType, !isValueType, indent);

            sb.AppendLine(
                $$"""
                {{indentText}}private static void OnShowing{{stateProperty}}ProxyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
                {{indentText}}{
                {{indentText}}    d.SetValue(Showing{{stateProperty}}PropertyKey, e.NewValue);
                {{indentText}}}
                """);
        }

        private static void AddGetStatePropertyClassValueMethod(StringBuilder sb, string ownerTypeName, int indent)
        {
            var indentText = new string(' ', indent);

            sb.AppendLine(
                $$"""
                {{indentText}}private static T? GetStatePropertyClassValue<T>(DependencyObject d, State state, StateProperty property)
                {{indentText}}    where T : class
                {{indentText}}{
                {{indentText}}    var targetValue = state switch
                {{indentText}}    {
                """);

            foreach (var state in (State[])Enum.GetValues(typeof(State)))
            {
                var propertyPrefix = state != State.Normal ? state.ToString() : string.Empty;

                sb.AppendLine(
                    $$"""
                    {{indentText}}    State.{{state}} => property switch
                    {{indentText}}    {
                    """);
                foreach (var stateProperty in (StateProperty[])Enum.GetValues(typeof(StateProperty)))
                {
                    sb.AppendLine(
                        $$"""
                        {{indentText}}        StateProperty.{{stateProperty}} => (T?)d.GetValue({{propertyPrefix}}{{stateProperty}}Property),
                        """);
                }
                sb.AppendLine(
                    $$"""
                    {{indentText}}        _ => throw new ArgumentException("Invalid property", nameof(property)),
                    {{indentText}}    },
                    """);
            }

            sb.AppendLine(
                $$"""
                {{indentText}}
                {{indentText}}        _ => throw new ArgumentException("Invalid state", nameof(state))
                {{indentText}}    };
                {{indentText}}
                {{indentText}}    if (targetValue is null)
                {{indentText}}    {
                {{indentText}}        var fallbackState = state switch
                {{indentText}}        {
                """);
            foreach (var state in (State[])Enum.GetValues(typeof(State)))
            {
                if (state == State.Normal)
                {
                    continue;
                }

                sb.AppendLine(
                    $$"""
                    {{indentText}}            State.{{state}} => GetState{{state}}Fallback(d),
                    """);
            }
            sb.AppendLine(
                $$"""
                {{indentText}}            _ => null,
                {{indentText}}        };
                {{indentText}}
                {{indentText}}        if (fallbackState is not null)
                {{indentText}}        {
                {{indentText}}            return GetStatePropertyClassValue<T>(d, fallbackState.Value, property);
                {{indentText}}        }
                {{indentText}}    }
                {{indentText}}
                {{indentText}}    return targetValue;
                {{indentText}}}
                """);
        }

        private static void AddGetStatePropertyStructValueMethod(StringBuilder sb, string ownerTypeName, int indent)
        {
            var indentText = new string(' ', indent);

            sb.AppendLine(
                $$"""
                {{indentText}}private static T? GetStatePropertyStructValue<T>(DependencyObject d, State state, StateProperty property)
                {{indentText}}    where T : struct
                {{indentText}}{
                {{indentText}}    var targetValue = state switch
                {{indentText}}    {
                """);

            foreach (var state in (State[])Enum.GetValues(typeof(State)))
            {
                var propertyPrefix = state != State.Normal ? state.ToString() : string.Empty;

                sb.AppendLine(
                    $$"""
                    {{indentText}}    State.{{state}} => property switch
                    {{indentText}}    {
                    """);
                foreach (var stateProperty in (StateProperty[])Enum.GetValues(typeof(StateProperty)))
                {
                    sb.AppendLine(
                        $$"""
                        {{indentText}}        StateProperty.{{stateProperty}} => (T?)d.GetValue({{propertyPrefix}}{{stateProperty}}Property),
                        """);
                }
                sb.AppendLine(
                    $$"""
                    {{indentText}}        _ => throw new ArgumentException("Invalid property", nameof(property)),
                    {{indentText}}    },
                    """);
            }

            sb.AppendLine(
                $$"""
                {{indentText}}
                {{indentText}}        _ => throw new ArgumentException("Invalid state", nameof(state))
                {{indentText}}    };
                {{indentText}}
                {{indentText}}    if (targetValue is null)
                {{indentText}}    {
                {{indentText}}        var fallbackState = state switch
                {{indentText}}        {
                """);

            foreach (var state in (State[])Enum.GetValues(typeof(State)))
            {
                if (state == State.Normal)
                {
                    continue;
                }

                sb.AppendLine(
                    $$"""
                    {{indentText}}            State.{{state}} => GetState{{state}}Fallback(d),
                    """);
            }

            sb.AppendLine(
                $$"""
                {{indentText}}            _ => null,
                {{indentText}}        };
                {{indentText}}        
                {{indentText}}        if (fallbackState is not null)
                {{indentText}}        {
                {{indentText}}            return GetStatePropertyStructValue<T>(d, fallbackState.Value, property);
                {{indentText}}        }
                {{indentText}}    }
                {{indentText}}
                {{indentText}}    return targetValue;
                {{indentText}}}
                """);
        }

        private static void AddGetStatePropertyEasingFunctionMethod(StringBuilder sb, string ownerTypeName, int indent)
        {
            var indentText = new string(' ', indent);

            sb.AppendLine(
                $$"""
                {{indentText}}private static IEasingFunction? GetStatePropertyEasingFunction(DependencyObject d, State state, StateProperty property)
                {{indentText}}{
                {{indentText}}    return state switch
                {{indentText}}    {
                """);

            foreach (var state in (State[])Enum.GetValues(typeof(State)))
            {
                var propertyPrefix = state != State.Normal ? state.ToString() : null;

                sb.AppendLine(
                    $$"""
                    {{indentText}}        State.{{state}} => property switch
                    {{indentText}}        {
                    """);
                foreach (var stateProperty in (StateProperty[])Enum.GetValues(typeof(StateProperty)))
                {
                    sb.AppendLine(
                        $$"""
                        {{indentText}}            StateProperty.{{stateProperty}} => Get{{propertyPrefix}}{{stateProperty}}EasingFunction(d),
                        """);

                }

                if (state != State.Normal)
                {
                    sb.AppendLine(
                        $$"""
                        {{indentText}}            _ => throw new ArgumentException("Invalid property", nameof(property)),
                        {{indentText}}        } ?? Get{{propertyPrefix ?? "Default"}}EasingFunction(d),
                        """);
                }
                else
                {
                    sb.AppendLine(
                        $$"""
                        {{indentText}}            _ => throw new ArgumentException("Invalid property", nameof(property)),
                        {{indentText}}        },
                        """);
                }
            }

            sb.AppendLine(
                $$""" 
                {{indentText}}        _ => throw new ArgumentException("Invalid state", nameof(state))
                {{indentText}}
                {{indentText}}    } ?? GetDefaultEasingFunction(d);
                {{indentText}}}
                """);
        }

        private static void AddGetStatePropertyTransitionDurationMethod(StringBuilder sb, string ownerTypeName, int indent)
        {
            var indentText = new string(' ', indent);

            sb.AppendLine(
                $$"""
                {{indentText}}private static Duration GetStatePropertyTransitionDuration(DependencyObject d, State state, StateProperty property)
                {{indentText}}{
                {{indentText}}    return state switch
                {{indentText}}    {
                """);

            foreach (var state in (State[])Enum.GetValues(typeof(State)))
            {
                var propertyPrefix = state != State.Normal ? state.ToString() : null;

                sb.AppendLine(
                    $$"""
                    {{indentText}}        State.{{state}} => property switch
                    {{indentText}}        {
                    """);
                foreach (var stateProperty in (StateProperty[])Enum.GetValues(typeof(StateProperty)))
                {
                    sb.AppendLine(
                        $$"""
                        {{indentText}}            StateProperty.{{stateProperty}} => Get{{propertyPrefix}}{{stateProperty}}TransitionDuration(d),
                        """);

                }

                if (state != State.Normal)
                {
                    sb.AppendLine(
                        $$"""
                        {{indentText}}           _ => throw new ArgumentException("Invalid property", nameof(property)),
                        {{indentText}}       } ?? Get{{propertyPrefix ?? "Default"}}TransitionDuration(d),
                        """);
                }
                else
                {
                    sb.AppendLine(
                        $$"""
                        {{indentText}}            _ => throw new ArgumentException("Invalid property", nameof(property)),
                        {{indentText}}        },
                        """);
                }
            }

            sb.AppendLine(
                $$""" 
                {{indentText}}        _ => throw new ArgumentException("Invalid state", nameof(state))
                {{indentText}}
                {{indentText}}    } ?? GetDefaultTransitionDuration(d);
                {{indentText}}}
                """);
        }

        private static string GenerateStateManagerSource(string typeName, string typeNamespace)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(
                $$"""
                // <auto-generated/>
                #pragma warning disable
                #nullable enable

                using System;
                using System.Collections.Generic;
                using System.Windows;
                using System.Windows.Media;
                using System.Windows.Media.Animation;
                using EleCho.WpfSuite.Media.Animation;
                using EleCho.WpfSuite.Controls.States.Internal;

                namespace {{typeNamespace}}
                {
                    partial class {{typeName}}
                    {
                        private record struct DependencyObjectAndStateProperty(DependencyObject DependencyObject, StateProperty StateProperty);

                        private static Dictionary<DependencyObjectAndStateProperty, Storyboard>? _runningStoryboards;


                """);

            // ActiveState
            AddActiveStateDefinition(sb, typeName, 8);

            foreach (var state in (State[])Enum.GetValues(typeof(State)))
            {
                foreach (var stateProperty in (StateProperty[])Enum.GetValues(typeof(StateProperty)))
                {
                    AddStateProperty(sb, state, stateProperty, typeName, 8);
                    AddStatePropertyTransitionDuration(sb, state, stateProperty, typeName, 8);
                    AddStatePropertyEasingFunction(sb, state, stateProperty, typeName, 8);
                }

                AddStateDefaultProperties(sb, state, typeName, 8);

                if (state != State.Normal)
                {
                    AddStateFallback(sb, state, typeName, 8);
                }
            }

            foreach (var stateProperty in (StateProperty[])Enum.GetValues(typeof(StateProperty)))
            {
                var propertyTypeName = GetTypeNameOfStateProperty(stateProperty, out _, out var isBrushType);

                AddAnyStatePropertyChangedCallbackMethod(sb, stateProperty, 8);
                AddShowingPropertyDefinitions(sb, stateProperty, typeName, 8);

                AddActiveStatePropertyMethod(sb, stateProperty, propertyTypeName, typeName, isBrushType, 8);
            }

            AddGetStatePropertyClassValueMethod(sb, typeName, 8);
            AddGetStatePropertyStructValueMethod(sb, typeName, 8);
            AddGetStatePropertyTransitionDurationMethod(sb, typeName, 8);
            AddGetStatePropertyEasingFunctionMethod(sb, typeName, 8);

            AddCoerceDurationMethod(sb, 8);
            AddCoerceNullableDurationMethod(sb, 8);

            sb.AppendLine(
                """
                    }
                }
                """);


            return sb.ToString();
        }
    }
}
