using System;
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
            None           = 0,
            Hover          = 1 << 0,
            Pressed        = 1 << 1,
            Checked        = 1 << 2,
            Selected       = 1 << 3,
            SelectedActive = 1 << 4,
            Focused        = 1 << 5,
            Highlighted    = 1 << 6,
            Disabled       = 1 << 7,
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

            EditableBackground        = 1 << 9,
            EditableForeground        = 1 << 10,
            EditableBorderBrush       = 1 << 11,
            EditablePadding           = 1 << 12,
            EditableBorderThickness   = 1 << 13,
            EditableCornerRadius      = 1 << 14,

            EditableButtonBackground        = 1 << 15,
            EditableButtonForeground        = 1 << 16,
            EditableButtonBorderBrush       = 1 << 17,
            EditableButtonPadding           = 1 << 18,
            EditableButtonBorderThickness   = 1 << 19,
            EditableButtonCornerRadius      = 1 << 20,
        }

        private record struct GenerationInfo(INamedTypeSymbol NamedTypeSymbol, StateFlags StateFlags, StatePropertyFlags StatePropertyFlags);

        private readonly StatePropertyFlags[] DefaultStateNotBuiltInProperties = new[]
        {
            StatePropertyFlags.GlyphBrush,
            StatePropertyFlags.CheckerPadding,
            StatePropertyFlags.PlaceholderBrush,

            StatePropertyFlags.EditableBackground,
            StatePropertyFlags.EditableForeground,
            StatePropertyFlags.EditableBorderBrush,
            StatePropertyFlags.EditablePadding,
            StatePropertyFlags.EditableBorderThickness,
            StatePropertyFlags.EditableCornerRadius,

            StatePropertyFlags.EditableButtonBackground,
            StatePropertyFlags.EditableButtonForeground,
            StatePropertyFlags.EditableButtonBorderBrush,
            StatePropertyFlags.EditableButtonPadding,
            StatePropertyFlags.EditableButtonBorderThickness,
            StatePropertyFlags.EditableButtonCornerRadius,
        };

        private string GetTypeNameForStateProperty(StatePropertyFlags flag)
        {
            const string TypeBrushFullName = "global::System.Windows.Media.Brush";
            const string TypeThicknessFullName = "global::System.Windows.Thickness";
            const string TypeCornerRadiusFullName = "global::System.Windows.CornerRadius";

            return flag switch
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

                StatePropertyFlags.EditableBackground => TypeBrushFullName,
                StatePropertyFlags.EditableForeground => TypeBrushFullName,
                StatePropertyFlags.EditableBorderBrush => TypeBrushFullName,
                StatePropertyFlags.EditablePadding => TypeThicknessFullName,
                StatePropertyFlags.EditableBorderThickness => TypeThicknessFullName,
                StatePropertyFlags.EditableCornerRadius => TypeCornerRadiusFullName,

                StatePropertyFlags.EditableButtonBackground => TypeBrushFullName,
                StatePropertyFlags.EditableButtonForeground => TypeBrushFullName,
                StatePropertyFlags.EditableButtonBorderBrush => TypeBrushFullName,
                StatePropertyFlags.EditableButtonPadding => TypeThicknessFullName,
                StatePropertyFlags.EditableButtonBorderThickness => TypeThicknessFullName,
                StatePropertyFlags.EditableButtonCornerRadius => TypeCornerRadiusFullName,

                _ => throw new ArgumentException(),
            };
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

        private string GenerateForType(string typeName, string typeNamespace, StateFlags stateFlags, StatePropertyFlags statePropertyFlags)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(
                $$"""
                // <auto-generated/>
                #pragma warning disable
                #nullable enable

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

                var propertyTypeName = GetTypeNameForStateProperty(property);
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

                    var propertyTypeName = GetTypeNameForStateProperty(statePropertyFlag);

                    AddStatePropertyTransitionDurationDefinition(sb, stateFlag, statePropertyFlag, typeName, true, 8);
                    AddStatePropertyEasingFunctionDefinition(sb, stateFlag, statePropertyFlag, typeName, true, 8);
                    AddStatePropertyDefinition(sb, stateFlag, statePropertyFlag, propertyTypeName, typeName, true, 8);
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

            if (syntaxNode.ArgumentList is not null)
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

                    foreach (var attributeList in classDeclaration.AttributeLists)
                    {
                        foreach (var attribute in attributeList.ChildNodes())
                        {
                            GetFlagsFromAttribute((AttributeSyntax)attribute, out var stateFlag, out var statePropertyFlag);

                            stateFlags |= stateFlag;
                            statePropertiesFlags |= statePropertyFlag;
                        }
                    }

                    return new GenerationInfo(typeSymbol, stateFlags, statePropertiesFlags);
                });

            context.RegisterSourceOutput(generationInfos, (context, generationInfo) =>
            {
                var typeName = generationInfo.NamedTypeSymbol.Name;
                var typeNamespace = generationInfo.NamedTypeSymbol.ContainingNamespace.ToString();
                var sourceText = GenerateForType(typeName, typeNamespace, generationInfo.StateFlags, generationInfo.StatePropertyFlags);

                context.AddSource($"{typeNamespace}.{typeName}.StateProperties.g.cs", sourceText);
            });
        }
    }
}
