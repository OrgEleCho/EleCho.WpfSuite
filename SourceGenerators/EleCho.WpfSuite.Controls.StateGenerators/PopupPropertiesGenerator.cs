﻿using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EleCho.WpfSuite.Controls.StateGenerators
{
    [Generator]
    public class PopupPropertiesGenerator : IIncrementalGenerator
    {
        const string TagAttributeFullName = "EleCho.WpfSuite.Controls.SourceGeneration.GeneratePopupPropertiesAttribute";

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
                var source =
                    $$"""
                    // <auto-generated/>
                    #pragma warning disable
                    #nullable enable

                    using System;
                    using System.Windows;
                    using System.Windows.Media;
                    using EleCho.WpfSuite.Media.Transition;

                    namespace {{typeNamespace}}
                    {
                        partial class {{typeName}}
                        {
                            public Brush PopupBackground
                            {
                                get { return (Brush)GetValue(PopupBackgroundProperty); }
                                set { SetValue(PopupBackgroundProperty, value); }
                            }
                            
                            public Brush PopupBorderBrush
                            {
                                get { return (Brush)GetValue(PopupBorderBrushProperty); }
                                set { SetValue(PopupBorderBrushProperty, value); }
                            }
                            
                            public Thickness PopupPadding
                            {
                                get { return (Thickness)GetValue(PopupPaddingProperty); }
                                set { SetValue(PopupPaddingProperty, value); }
                            }
                            
                            public Thickness PopupBorderThickness
                            {
                                get { return (Thickness)GetValue(PopupBorderThicknessProperty); }
                                set { SetValue(PopupBorderThicknessProperty, value); }
                            }
                            
                            public CornerRadius PopupCornerRadius
                            {
                                get { return (CornerRadius)GetValue(PopupCornerRadiusProperty); }
                                set { SetValue(PopupCornerRadiusProperty, value); }
                            }
                            
                            public IContentTransition PopupContentTransition
                            {
                                get { return (IContentTransition)GetValue(PopupContentTransitionProperty); }
                                set { SetValue(PopupContentTransitionProperty, value); }
                            }
                            public ContentTransitionMode PopupContentTransitionMode
                            {
                                get { return (ContentTransitionMode)GetValue(PopupContentTransitionModeProperty); }
                                set { SetValue(PopupContentTransitionModeProperty, value); }
                            }



                            public static readonly DependencyProperty PopupBackgroundProperty =
                                DependencyProperty.Register(nameof(PopupBackground), typeof(Brush), typeof({{typeName}}), new FrameworkPropertyMetadata(SystemColors.WindowBrush));
                            
                            public static readonly DependencyProperty PopupBorderBrushProperty =
                                DependencyProperty.Register(nameof(PopupBorderBrush), typeof(Brush), typeof({{typeName}}), new FrameworkPropertyMetadata(null));
                            
                            public static readonly DependencyProperty PopupPaddingProperty =
                                DependencyProperty.Register(nameof(PopupPadding), typeof(Thickness), typeof({{typeName}}), new FrameworkPropertyMetadata(default(Thickness)));
                            
                            public static readonly DependencyProperty PopupBorderThicknessProperty =
                                DependencyProperty.Register(nameof(PopupBorderThickness), typeof(Thickness), typeof({{typeName}}), new FrameworkPropertyMetadata(default(Thickness)));
                            
                            public static readonly DependencyProperty PopupCornerRadiusProperty =
                                DependencyProperty.Register(nameof(PopupCornerRadius), typeof(CornerRadius), typeof({{typeName}}), new FrameworkPropertyMetadata(default(CornerRadius)));
                            
                            public static readonly DependencyProperty PopupContentTransitionProperty =
                                DependencyProperty.Register(nameof(PopupContentTransition), typeof(IContentTransition), typeof({{typeName}}), new FrameworkPropertyMetadata(null));
                            
                            public static readonly DependencyProperty PopupContentTransitionModeProperty =
                                DependencyProperty.Register(nameof(PopupContentTransitionMode), typeof(ContentTransitionMode), typeof({{typeName}}), new FrameworkPropertyMetadata(ContentTransitionMode.ChangedOrLoaded));
                        }
                    }
                    """;
                context.AddSource($"{typeNamespace}.{typeName}.PopupProperties.g.cs", source);
            });
        }
    }
}
