using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EleCho.WpfSuite.Controls.StateGenerators
{
    [Generator]
    public class StatePropertyGenerator : IIncrementalGenerator
    {
        const string TagAttributeFullName = "EleCho.WpfSuite.Controls.SourceGeneration.GenerateStatesAttribute";
        const string TagStateAttributeFullName = "EleCho.WpfSuite.Controls.SourceGeneration.GenerateStatesStateAttribute";
        const string TagStatePropertyAttributeFullName = "EleCho.WpfSuite.Controls.SourceGeneration.GenerateStatesPropertyAttribute";
        const string BaseType = "DependencyObject";

        const string TypeStateManagerFullName = "global::EleCho.WpfSuite.Controls.States.StateManager";
        const string TypeDependencyPropertyFullName = "global::System.Windows.DependencyProperty";

        [Flags]
        private enum StateFlags
        {
            None                     = 0,
            Focused                  = 1 << 0,
            Hover                    = 1 << 1,
            Pressed                  = 1 << 2,
            Dragging                 = 1 << 3,
            Checked                  = 1 << 4,
        
            Selected                 = 1 << 5,
            SelectedActive           = 1 << 6,
            SelectedFocused          = 1 << 7,

            FocusedHover             = 1 << 8,
            CheckedHover             = 1 << 9,
            SelectedHover            = 1 << 10,
            SelectedActiveHover      = 1 << 11,
            SelectedFocusedHover     = 1 << 12,

            Highlighted              = 1 << 13,
            Disabled                 = 1 << 14,
            HighlightedDisabled      = 1 << 15,
        }

        [Flags]
        private enum StatePropertyFlags
        {
            None                  = 0,
            Background            = 1 << 0,
            Foreground            = 1 << 1,
            BorderBrush           = 1 << 2,
            Padding               = 1 << 3,
            BorderThickness       = 1 << 4,
            CornerRadius          = 1 << 5,

            CheckerPadding        = 1 << 6,
            GlyphBrush            = 1 << 7,
            PlaceholderBrush      = 1 << 8,
        }

        private record struct ComponentGenerationInfo(string ComponentName, StateFlags StateFlags, StatePropertyFlags StatePropertyFlags);
        private record struct GenerationInfo(INamedTypeSymbol NamedTypeSymbol, StateFlags StateFlags, StatePropertyFlags StatePropertyFlags, IEnumerable<ComponentGenerationInfo> ComponentGenerationInfos);

        private readonly StatePropertyFlags[] DefaultStateNotBuiltInProperties = new[]
        {
            StatePropertyFlags.GlyphBrush,
            StatePropertyFlags.CheckerPadding,
            StatePropertyFlags.PlaceholderBrush,
        };

        private static string GetTypeNameOfStateProperty(StatePropertyFlags stateProperty, out bool isValueType, out bool isBrushType)
        {
            const string TypeBrushFullName = "Brush";
            const string TypeThicknessFullName = "Thickness";
            const string TypeCornerRadiusFullName = "CornerRadius";

            isValueType = stateProperty switch
            {
                StatePropertyFlags.Background => false,
                StatePropertyFlags.Foreground => false,
                StatePropertyFlags.BorderBrush => false,
                StatePropertyFlags.Padding => true,
                StatePropertyFlags.BorderThickness => true,
                StatePropertyFlags.CornerRadius => true,

                StatePropertyFlags.CheckerPadding => true,
                StatePropertyFlags.GlyphBrush => false,
                StatePropertyFlags.PlaceholderBrush => false,

                _ => throw new NotImplementedException()
            };

            var propertyTypeName = stateProperty switch
            {
                StatePropertyFlags.Background => TypeBrushFullName,
                StatePropertyFlags.Foreground => TypeBrushFullName,
                StatePropertyFlags.BorderBrush => TypeBrushFullName,
                StatePropertyFlags.Padding => TypeThicknessFullName,
                StatePropertyFlags.BorderThickness => TypeThicknessFullName,
                StatePropertyFlags.CornerRadius => TypeCornerRadiusFullName,

                StatePropertyFlags.CheckerPadding => TypeThicknessFullName,
                StatePropertyFlags.GlyphBrush => TypeBrushFullName,
                StatePropertyFlags.PlaceholderBrush => TypeBrushFullName,

                _ => throw new NotImplementedException()
            };

            isBrushType = propertyTypeName == TypeBrushFullName;

            return propertyTypeName;
        }

        private static string GetDefaultValueOfStateProperty(StatePropertyFlags stateProperty, bool nullable)
        {
            const string DefaultValueBrushFullName = "default(Brush)";
            const string DefaultValueThicknessFullName = "default(Thickness)";
            const string DefaultValueCornerRadiusFullName = "default(CornerRadius)";

            const string DefaultValueNullableThicknessFullName = "default(Thickness?)";
            const string DefaultValueNullableCornerRadiusFullName = "default(CornerRadius?)";

            if (!nullable)
            {
                return stateProperty switch
                {
                    StatePropertyFlags.Background => DefaultValueBrushFullName,
                    StatePropertyFlags.Foreground => DefaultValueBrushFullName,
                    StatePropertyFlags.BorderBrush => DefaultValueBrushFullName,
                    StatePropertyFlags.Padding => DefaultValueThicknessFullName,
                    StatePropertyFlags.BorderThickness => DefaultValueThicknessFullName,
                    StatePropertyFlags.CornerRadius => DefaultValueCornerRadiusFullName,

                    StatePropertyFlags.CheckerPadding => DefaultValueThicknessFullName,
                    StatePropertyFlags.GlyphBrush => DefaultValueBrushFullName,
                    StatePropertyFlags.PlaceholderBrush => DefaultValueBrushFullName,

                    _ => throw new NotImplementedException()
                };
            }
            else
            {
                return stateProperty switch
                {
                    StatePropertyFlags.Background => DefaultValueBrushFullName,
                    StatePropertyFlags.Foreground => DefaultValueBrushFullName,
                    StatePropertyFlags.BorderBrush => DefaultValueBrushFullName,
                    StatePropertyFlags.Padding => DefaultValueNullableThicknessFullName,
                    StatePropertyFlags.BorderThickness => DefaultValueNullableThicknessFullName,
                    StatePropertyFlags.CornerRadius => DefaultValueNullableCornerRadiusFullName,

                    StatePropertyFlags.CheckerPadding => DefaultValueNullableThicknessFullName,
                    StatePropertyFlags.GlyphBrush => DefaultValueBrushFullName,
                    StatePropertyFlags.PlaceholderBrush => DefaultValueBrushFullName,

                    _ => throw new NotImplementedException()
                };
            }
        }

        private static void AddDependencyPropertyDefinition(
            StringBuilder sb, string propertyName, string propertyTypeName, string ownerTypeName, string defaultValue, string? coerceValueCallback, string? propertyChangedCallback, bool isValueType, bool addNullableTag, int indent, string metaDataTypeName = "PropertyMetadata")
        {
            var getSetTypeTag = addNullableTag ? $"{propertyTypeName}?" : propertyTypeName;
            var typeOfTypeTag = (isValueType && addNullableTag) ? $"{propertyTypeName}?" : propertyTypeName;
            var indentText = new string(' ', indent);

            sb.AppendLine(
                $$"""
                {{indentText}}public {{getSetTypeTag}} {{propertyName}}
                {{indentText}}{
                {{indentText}}    get => ({{getSetTypeTag}})GetValue({{propertyName}}Property);
                {{indentText}}    set => SetValue({{propertyName}}Property, value);
                {{indentText}}}
                {{indentText}}
                {{indentText}}public static readonly DependencyProperty {{propertyName}}Property =
                {{indentText}}    DependencyProperty.Register("{{propertyName}}", typeof({{typeOfTypeTag}}), typeof({{ownerTypeName}}), new {{metaDataTypeName}}({{defaultValue}}, propertyChangedCallback: {{propertyChangedCallback ?? "null"}}, coerceValueCallback: {{coerceValueCallback ?? "null"}}));
                {{indentText}}
                """);
        }

        private void AddDependencyPropertyFromStateManager(StringBuilder sb, string propertyName, string propertyTypeName, string ownerTypeName, bool addNullableTag, int indent)
        {
            string indentText = new string(' ', indent);
            string nullableTagPlaceholder = addNullableTag ? "?" : string.Empty;

            sb.AppendLine(
                $$"""
                {{indentText}}public {{propertyTypeName}}{{nullableTagPlaceholder}} {{propertyName}} 
                {{indentText}}{
                {{indentText}}    get => ({{propertyTypeName}}{{nullableTagPlaceholder}})GetValue({{propertyName}}Property);
                {{indentText}}    set => SetValue({{propertyName}}Property, value);
                {{indentText}}}
                {{indentText}}
                {{indentText}}public static readonly {{TypeDependencyPropertyFullName}} {{propertyName}}Property
                {{indentText}}    = {{TypeStateManagerFullName}}.{{propertyName}}Property.AddOwner(typeof({{ownerTypeName}}));
                {{indentText}}
                """);
        }

        private void AddStatePropertyDefinition(StringBuilder sb, StateFlags state, StatePropertyFlags stateProperty, string propertyTypeName, string ownerTypeName, bool addNullableTag, int indent)
        {
            var propertyPrefix = string.Empty;

            if (state != StateFlags.None)
            {
                propertyPrefix = state.ToString();
            }

            AddDependencyPropertyFromStateManager(sb, $"{propertyPrefix}{stateProperty}", propertyTypeName, ownerTypeName, addNullableTag, indent);
        }

        private void AddStatePropertyTransitionDurationDefinition(StringBuilder sb, StateFlags state, StatePropertyFlags stateProperty, string ownerTypeName, bool addNullableTag, int indent)
        {
            if (state == StateFlags.None)
            {
                throw new ArgumentOutOfRangeException(nameof(state));
            }

            const string TypeDurationFullName = "global::System.Windows.Duration";
            var propertyPrefix = state.ToString();

            AddDependencyPropertyFromStateManager(sb, $"{propertyPrefix}{stateProperty}TransitionDuration", TypeDurationFullName, ownerTypeName, addNullableTag, indent);
        }

        private void AddStatePropertyEasingFunctionDefinition(StringBuilder sb, StateFlags state, StatePropertyFlags stateProperty, string ownerTypeName, bool addNullableTag, int indent)
        {
            if (state == StateFlags.None)
            {
                throw new ArgumentOutOfRangeException(nameof(state));
            }

            const string TypeIEasingFunctionFullName = "global::System.Windows.Media.Animation.IEasingFunction";
            var propertyPrefix = state.ToString();

            AddDependencyPropertyFromStateManager(sb, $"{propertyPrefix}{stateProperty}EasingFunction", TypeIEasingFunctionFullName, ownerTypeName, addNullableTag, indent);
        }

        private void AddStateDefaultProperties(StringBuilder sb, StateFlags state, string ownerTypeName, int indent)
        {
            const string TypeDurationFullName = "global::System.Windows.Duration";
            const string TypeIEasingFunctionFullName = "global::System.Windows.Media.Animation.IEasingFunction";

            if (state == StateFlags.None)
            {
                AddDependencyPropertyFromStateManager(sb, $"DefaultTransitionDuration", TypeDurationFullName, ownerTypeName, false, indent);
                AddDependencyPropertyFromStateManager(sb, $"DefaultEasingFunction", TypeIEasingFunctionFullName, ownerTypeName, true, indent);
            }
            else
            {
                AddDependencyPropertyFromStateManager(sb, $"{state}TransitionDuration", TypeDurationFullName, ownerTypeName, true, indent);
                AddDependencyPropertyFromStateManager(sb, $"{state}EasingFunction", TypeIEasingFunctionFullName, ownerTypeName, true, indent);
            }
        }

        private string GenerateForType(string typeName, string typeNamespace, StateFlags stateFlags, StatePropertyFlags statePropertyFlags, IEnumerable<ComponentGenerationInfo> componentGenerationInfos)
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

                namespace {{typeNamespace}}
                {
                    partial class {{typeName}}
                    {
                """);

            AddStateDefaultProperties(sb, StateFlags.None, typeName, 8);

            foreach (var property in DefaultStateNotBuiltInProperties)
            {
                if (!statePropertyFlags.HasFlag(property))
                {
                    continue;
                }

                var propertyTypeName = GetTypeNameOfStateProperty(property, out _, out _);
                AddStatePropertyDefinition(sb, StateFlags.None, property, propertyTypeName, typeName, false, 8);
            }

            foreach (var stateFlag in (StateFlags[])Enum.GetValues(typeof(StateFlags)))
            {
                if (stateFlag == StateFlags.None ||
                    !stateFlags.HasFlag(stateFlag))
                {
                    continue;
                }

                AddStateDefaultProperties(sb, stateFlag, typeName, 8);

                foreach (var statePropertyFlag in (StatePropertyFlags[])Enum.GetValues(typeof(StatePropertyFlags)))
                {
                    if (statePropertyFlag == StatePropertyFlags.None ||
                        !statePropertyFlags.HasFlag(statePropertyFlag))
                    {
                        continue;
                    }

                    var propertyTypeName = GetTypeNameOfStateProperty(statePropertyFlag, out _, out _);

                    AddStatePropertyTransitionDurationDefinition(sb, stateFlag, statePropertyFlag, typeName, true, 8);
                    AddStatePropertyEasingFunctionDefinition(sb, stateFlag, statePropertyFlag, typeName, true, 8);
                    AddStatePropertyDefinition(sb, stateFlag, statePropertyFlag, propertyTypeName, typeName, true, 8);
                }
            }

            foreach (var componentGenerationInfo in componentGenerationInfos)
            {
                foreach (var stateFlag in (StateFlags[])Enum.GetValues(typeof(StateFlags)))
                {
                    if (!componentGenerationInfo.StateFlags.HasFlag(stateFlag))
                    {
                        continue;
                    }

                    foreach (var statePropertyFlag in (StatePropertyFlags[])Enum.GetValues(typeof(StatePropertyFlags)))
                    {
                        if (statePropertyFlag == StatePropertyFlags.None ||
                            !componentGenerationInfo.StatePropertyFlags.HasFlag(statePropertyFlag))
                        {
                            continue;
                        }

                        var propertyTypeName = GetTypeNameOfStateProperty(statePropertyFlag, out var isValueType, out var isBrushType);
                        var propertyDefaultValue = GetDefaultValueOfStateProperty(statePropertyFlag, isValueType && stateFlag != StateFlags.None);
                        var propertyPrefix = stateFlag != StateFlags.None ? stateFlag.ToString() : string.Empty;

                        AddDependencyPropertyDefinition(sb, $"{componentGenerationInfo.ComponentName}{propertyPrefix}{statePropertyFlag}", propertyTypeName, typeName, propertyDefaultValue, null, null, isValueType, stateFlag != StateFlags.None, 8);
                    }
                }
            }

            sb.AppendLine(
                $$"""
                    }
                }
                """);

            return sb.ToString();
        }

        private static void GetFlagsFromAttribute(AttributeSyntax syntaxNode, out StateFlags stateFlag, out StatePropertyFlags statePropertyFlag)
        {
            stateFlag = StateFlags.None;
            statePropertyFlag = StatePropertyFlags.None;

            if (syntaxNode.ArgumentList is not null)
            {
                if (syntaxNode.Name is not SimpleNameSyntax attributeNameSyntax)
                {
                    return;
                }

                var attributeNameText = attributeNameSyntax.Identifier.ValueText;

                if (attributeNameText == "GenerateStatesState")
                {
                    foreach (var argument in syntaxNode.ArgumentList.Arguments)
                    {
                        if (argument.Expression is not MemberAccessExpressionSyntax memberAccessExpression)
                        {
                            continue;
                        }

                        var name = memberAccessExpression.Name.Identifier.ValueText;
                        Enum.TryParse<StateFlags>(name, out stateFlag);
                    }
                }
                else if (attributeNameText == "GenerateStatesProperty")
                {
                    foreach (var argument in syntaxNode.ArgumentList.Arguments)
                    {
                        if (argument.Expression is not MemberAccessExpressionSyntax memberAccessExpression)
                        {
                            continue;
                        }

                        var name = memberAccessExpression.Name.Identifier.ValueText;
                        Enum.TryParse<StatePropertyFlags>(name, out statePropertyFlag);
                    }
                }
            }
        }

        private static void GetComponentFlagsFromAttribute(AttributeSyntax syntaxNode, out string? componentName, out StateFlags stateFlag, out StatePropertyFlags statePropertyFlag)
        {
            stateFlag = StateFlags.None;
            statePropertyFlag = StatePropertyFlags.None;

            componentName = null;

            if (syntaxNode.ArgumentList is not null)
            {
                if (syntaxNode.Name is not SimpleNameSyntax attributeNameSyntax)
                {
                    return;
                }

                var attributeNameText = attributeNameSyntax.Identifier.ValueText;

                if (attributeNameText == "GenerateComponentStatesState")
                {
                    foreach (var argument in syntaxNode.ArgumentList.Arguments)
                    {
                        if (componentName is null)
                        {
                            if (argument.Expression is not LiteralExpressionSyntax literalExpressionSyntax)
                            {
                                continue;
                            }

                            componentName = literalExpressionSyntax.Token.ValueText;
                        }
                        else
                        {
                            if (argument.Expression is not MemberAccessExpressionSyntax memberAccessExpression)
                            {
                                continue;
                            }

                            var name = memberAccessExpression.Name.Identifier.ValueText;
                            Enum.TryParse<StateFlags>(name, out stateFlag);
                        }
                    }
                }
                else if (attributeNameText == "GenerateComponentStateProperty")
                {
                    foreach (var argument in syntaxNode.ArgumentList.Arguments)
                    {
                        if (componentName is null)
                        {
                            if (argument.Expression is not LiteralExpressionSyntax literalExpressionSyntax)
                            {
                                continue;
                            }

                            componentName = literalExpressionSyntax.Token.ValueText;
                        }
                        else
                        {
                            if (argument.Expression is not MemberAccessExpressionSyntax memberAccessExpression)
                            {
                                continue;
                            }

                            var name = memberAccessExpression.Name.Identifier.ValueText;
                            Enum.TryParse<StatePropertyFlags>(name, out statePropertyFlag);
                        }
                    }
                }
            }
        }

        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var generationInfos = context.SyntaxProvider.ForAttributeWithMetadataName(
                TagAttributeFullName,
                static (node, _) => node is ClassDeclarationSyntax,
                static (context, token) =>
                {
                    INamedTypeSymbol typeSymbol = (INamedTypeSymbol)context.TargetSymbol;

                    var stateFlags = StateFlags.None;
                    var statePropertiesFlags = StatePropertyFlags.None;
                    var classDeclaration = (ClassDeclarationSyntax)context.TargetNode;
                    var componentGenerationInfos = new Dictionary<string, ComponentGenerationInfo>();

                    foreach (var attributeList in classDeclaration.AttributeLists)
                    {
                        foreach (var attribute in attributeList.ChildNodes())
                        {
                            GetFlagsFromAttribute((AttributeSyntax)attribute, out var stateFlag, out var statePropertyFlag);
                            GetComponentFlagsFromAttribute((AttributeSyntax)attribute, out var componentName, out var componentStateFlag, out var componentStatePropertyFlags);

                            stateFlags |= stateFlag;
                            statePropertiesFlags |= statePropertyFlag;

                            if (componentName is not null)
                            {
                                if (!componentGenerationInfos.TryGetValue(componentName, out var componentGenerationInfo))
                                {
                                    componentGenerationInfo = new ComponentGenerationInfo(componentName, StateFlags.None, StatePropertyFlags.None);
                                }

                                componentGenerationInfo.StateFlags |= componentStateFlag;
                                componentGenerationInfo.StatePropertyFlags |= componentStatePropertyFlags;

                                componentGenerationInfos[componentName] = componentGenerationInfo;
                            }
                        }
                    }

                    return new GenerationInfo(typeSymbol, stateFlags, statePropertiesFlags, componentGenerationInfos.Values);
                });

            context.RegisterSourceOutput(generationInfos, (context, generationInfo) =>
            {
                var typeName = generationInfo.NamedTypeSymbol.Name;
                var typeNamespace = generationInfo.NamedTypeSymbol.ContainingNamespace.ToString();
                var sourceText = GenerateForType(typeName, typeNamespace, generationInfo.StateFlags, generationInfo.StatePropertyFlags, generationInfo.ComponentGenerationInfos);

                context.AddSource($"{typeNamespace}.{typeName}.StateProperties.g.cs", sourceText);
            });
        }
    }
}
