using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EleCho.WpfSuite.FluentDesign
{
    public class FluentIcon : TextBlock
    {
        static FluentIcon()
        {
            var segoeFont = new FontFamily("Segoe Fluent Icons");
            
            FontFamilyProperty.OverrideMetadata(typeof(FluentIcon), new FrameworkPropertyMetadata(segoeFont));
        }

        public FluentSymbol Symbol
        {
            get { return (FluentSymbol)GetValue(SymbolProperty); }
            set { SetValue(SymbolProperty, value); }
        }

        public static readonly DependencyProperty SymbolProperty =
            DependencyProperty.Register(nameof(Symbol), typeof(FluentSymbol), typeof(FluentIcon), new FrameworkPropertyMetadata(default(FluentSymbol), SymbolChangedCallback));

        private static void SymbolChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not TextBlock textBlock || 
                e.NewValue is not FluentSymbol symbol)
                return;

            textBlock.Text = $"{(char)symbol}";
        }
    }
}
