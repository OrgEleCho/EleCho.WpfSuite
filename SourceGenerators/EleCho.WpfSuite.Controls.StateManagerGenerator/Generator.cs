using Microsoft.CodeAnalysis;
using System;
using System.Text;

namespace EleCho.WpfSuite.Controls.StateManagerGenerator
{
    [Generator]
    public partial class Generator : ISourceGenerator
    {
        private enum State
        {
            Normal,

            // control
            Hover,

            // button
            Pressed,

            // checkbox / toggle button
            Checked,

            // selection
            Selected,
            SelectedActive,

            // Focused
            Focused,

            // highlighted
            Highlighted,

            // disabled
            Disabled
        }

        private enum StateProperty
        {
            Background,
            Foreground,
            BorderBrush,
            Padding,
            BorderThickness,
            CornerRadius,

            CheckerPadding,
            GlyphBrush,
            PlaceholderBrush,

            EditableBackground,
            EditableForeground,
            EditableBorderBrush,
            EditablePadding,
            EditableBorderThickness,
            EditableCornerRadius,

            EditableButtonBackground,
            EditableButtonForeground,
            EditableButtonBorderBrush,
            EditableButtonPadding,
            EditableButtonBorderThickness,
            EditableButtonCornerRadius,
        }

        private static State GetFallbackState(State state)
        {
            return state switch
            {
                State.Hover => State.Normal,
                State.Pressed => State.Hover,
                State.Checked => State.Pressed,
                State.Selected => State.Pressed,
                State.SelectedActive => State.Selected,
                State.Focused => State.Normal,
                State.Highlighted => State.Normal,
                State.Disabled => State.Normal,

                _ => throw new ArgumentException()
            };
        }

        private static string GetTypeNameOfStatePropert(StateProperty stateProperty, out bool isValueType, out bool isBrushType)
        {
            const string TypeBrushFullName = "Brush";
            const string TypeThicknessFullName = "Thickness";
            const string TypeCornerRadiusFullName = "CornerRadius";

            isValueType = stateProperty switch
            {
                StateProperty.Background => false,
                StateProperty.Foreground => false,
                StateProperty.BorderBrush => false,
                StateProperty.Padding => true,
                StateProperty.BorderThickness => true,
                StateProperty.CornerRadius => true,

                StateProperty.CheckerPadding => true,
                StateProperty.GlyphBrush => false,
                StateProperty.PlaceholderBrush => false,

                StateProperty.EditableBackground => false,
                StateProperty.EditableForeground => false,
                StateProperty.EditableBorderBrush => false,
                StateProperty.EditablePadding => true,
                StateProperty.EditableBorderThickness => true,
                StateProperty.EditableCornerRadius => true,

                StateProperty.EditableButtonBackground => false,
                StateProperty.EditableButtonForeground => false,
                StateProperty.EditableButtonBorderBrush => false,
                StateProperty.EditableButtonPadding => true,
                StateProperty.EditableButtonBorderThickness => true,
                StateProperty.EditableButtonCornerRadius => true,

                _ => throw new NotImplementedException()
            };

            var propertyTypeName = stateProperty switch
            {
                StateProperty.Background => TypeBrushFullName,
                StateProperty.Foreground => TypeBrushFullName,
                StateProperty.BorderBrush => TypeBrushFullName,
                StateProperty.Padding => TypeThicknessFullName,
                StateProperty.BorderThickness => TypeThicknessFullName,
                StateProperty.CornerRadius => TypeCornerRadiusFullName,

                StateProperty.CheckerPadding => TypeThicknessFullName,
                StateProperty.GlyphBrush => TypeBrushFullName,
                StateProperty.PlaceholderBrush => TypeBrushFullName,

                StateProperty.EditableBackground => TypeBrushFullName,
                StateProperty.EditableForeground => TypeBrushFullName,
                StateProperty.EditableBorderBrush => TypeBrushFullName,
                StateProperty.EditablePadding => TypeThicknessFullName,
                StateProperty.EditableBorderThickness => TypeThicknessFullName,
                StateProperty.EditableCornerRadius => TypeCornerRadiusFullName,

                StateProperty.EditableButtonBackground => TypeBrushFullName,
                StateProperty.EditableButtonForeground => TypeBrushFullName,
                StateProperty.EditableButtonBorderBrush => TypeBrushFullName,
                StateProperty.EditableButtonPadding => TypeThicknessFullName,
                StateProperty.EditableButtonBorderThickness => TypeThicknessFullName,
                StateProperty.EditableButtonCornerRadius => TypeCornerRadiusFullName,

                _ => throw new NotImplementedException()
            };

            isBrushType = propertyTypeName == TypeBrushFullName;

            return propertyTypeName;
        }

        private static string GetDefaultValueOfStateProperty(StateProperty stateProperty, bool nullable)
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
                    StateProperty.Background => DefaultValueBrushFullName,
                    StateProperty.Foreground => DefaultValueBrushFullName,
                    StateProperty.BorderBrush => DefaultValueBrushFullName,
                    StateProperty.Padding => DefaultValueThicknessFullName,
                    StateProperty.BorderThickness => DefaultValueThicknessFullName,
                    StateProperty.CornerRadius => DefaultValueCornerRadiusFullName,

                    StateProperty.CheckerPadding => DefaultValueThicknessFullName,
                    StateProperty.GlyphBrush => DefaultValueBrushFullName,
                    StateProperty.PlaceholderBrush => DefaultValueBrushFullName,

                    StateProperty.EditableBackground => DefaultValueBrushFullName,
                    StateProperty.EditableForeground => DefaultValueBrushFullName,
                    StateProperty.EditableBorderBrush => DefaultValueBrushFullName,
                    StateProperty.EditablePadding => DefaultValueThicknessFullName,
                    StateProperty.EditableBorderThickness => DefaultValueThicknessFullName,
                    StateProperty.EditableCornerRadius => DefaultValueCornerRadiusFullName,

                    StateProperty.EditableButtonBackground => DefaultValueBrushFullName,
                    StateProperty.EditableButtonForeground => DefaultValueBrushFullName,
                    StateProperty.EditableButtonBorderBrush => DefaultValueBrushFullName,
                    StateProperty.EditableButtonPadding => DefaultValueThicknessFullName,
                    StateProperty.EditableButtonBorderThickness => DefaultValueThicknessFullName,
                    StateProperty.EditableButtonCornerRadius => DefaultValueCornerRadiusFullName,

                    _ => throw new NotImplementedException()
                };
            }
            else
            {
                return stateProperty switch
                {
                    StateProperty.Background => DefaultValueBrushFullName,
                    StateProperty.Foreground => DefaultValueBrushFullName,
                    StateProperty.BorderBrush => DefaultValueBrushFullName,
                    StateProperty.Padding => DefaultValueNullableThicknessFullName,
                    StateProperty.BorderThickness => DefaultValueNullableThicknessFullName,
                    StateProperty.CornerRadius => DefaultValueNullableCornerRadiusFullName,

                    StateProperty.CheckerPadding => DefaultValueNullableThicknessFullName,
                    StateProperty.GlyphBrush => DefaultValueBrushFullName,
                    StateProperty.PlaceholderBrush => DefaultValueBrushFullName,

                    StateProperty.EditableBackground => DefaultValueBrushFullName,
                    StateProperty.EditableForeground => DefaultValueBrushFullName,
                    StateProperty.EditableBorderBrush => DefaultValueBrushFullName,
                    StateProperty.EditablePadding => DefaultValueNullableThicknessFullName,
                    StateProperty.EditableBorderThickness => DefaultValueNullableThicknessFullName,
                    StateProperty.EditableCornerRadius => DefaultValueNullableCornerRadiusFullName,

                    StateProperty.EditableButtonBackground => DefaultValueBrushFullName,
                    StateProperty.EditableButtonForeground => DefaultValueBrushFullName,
                    StateProperty.EditableButtonBorderBrush => DefaultValueBrushFullName,
                    StateProperty.EditableButtonPadding => DefaultValueNullableThicknessFullName,
                    StateProperty.EditableButtonBorderThickness => DefaultValueNullableThicknessFullName,
                    StateProperty.EditableButtonCornerRadius => DefaultValueNullableCornerRadiusFullName,

                    _ => throw new NotImplementedException()
                };
            }
        }

        private static string? GetTypeCoerceValueCallbackMethodName(string typeName, bool isValueType, bool nullable)
        {
            var segments = typeName.Split('.');
            typeName = segments[segments.Length - 1];

            if (!isValueType)
            {
                return null;
            }

            if (nullable)
            {
                return $"CoerceNullable{typeName}";
            }
            else
            {
                return $"Coerce{typeName}";
            }
        }


        public void Initialize(GeneratorInitializationContext context)
        {

        }

        public void Execute(GeneratorExecutionContext context)
        {
            var typeName = "StateManager";
            var typeNamespace = "EleCho.WpfSuite.Controls.States";
            var sourceText = GenerateStateManagerSource(typeName, typeNamespace);

            context.AddSource($"{typeNamespace}.{typeName}.g.cs", sourceText);
        }


    }
}
