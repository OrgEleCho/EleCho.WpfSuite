﻿using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EleCho.WpfSuite.Controls.StateGenerators
{
    [Generator]
    public class CornerRadiusPropertyGenerator : IIncrementalGenerator
    {
        const string TagAttributeFullName = "EleCho.WpfSuite.Controls.SourceGeneration.GenerateCornerRadiusPropertyAttribute";

        const string TypeDependencyPropertyFullName = "global::System.Windows.DependencyProperty";
        const string TypeBorderFullName = "global::System.Windows.Controls.Border";
        const string TypeCornerRadiusFullName = "global::System.Windows.CornerRadius";

        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var source = context.SyntaxProvider
                .ForAttributeWithMetadataName(
                TagAttributeFullName,
                static (node, _) => node is ClassDeclarationSyntax,
                static (context, _) => (INamedTypeSymbol)context.TargetSymbol);

            context.RegisterSourceOutput(source, (context, symbol) =>
            {
                var typeName = symbol.Name;
                var typeNamespace = symbol.ContainingNamespace.ToString();

                context.AddSource($"{typeNamespace}.{typeName}.CornerRadius.g.cs",
                    $$"""
                    // <auto-generated/>
                    #pragma warning disable
                    #nullable enable

                    namespace {{typeNamespace}}
                    {
                        partial class {{typeName}}
                        {
                            public {{TypeCornerRadiusFullName}} CornerRadius 
                            {
                                get => ({{TypeCornerRadiusFullName}})GetValue(CornerRadiusProperty);
                                set => SetValue(CornerRadiusProperty, value);
                            }

                            public static readonly {{TypeDependencyPropertyFullName}} CornerRadiusProperty = 
                                {{TypeBorderFullName}}.CornerRadiusProperty.AddOwner(typeof({{typeName}}));
                        }
                    }
                    """);
            });
        }
    }
}
