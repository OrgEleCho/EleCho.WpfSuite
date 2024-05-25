using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EleCho.WpfSuite
{
    public class ComboBox : System.Windows.Controls.ComboBox
    {
        static ComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ComboBox), new FrameworkPropertyMetadata(typeof(ComboBox)));
        }

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public CornerRadius PopupCornerRadius
        {
            get { return (CornerRadius)GetValue(PopupCornerRadiusProperty); }
            set { SetValue(PopupCornerRadiusProperty, value); }
        }

        // extra properties

        public Brush GlyphBrush
        {
            get { return (Brush)GetValue(GlyphBrushProperty); }
            set { SetValue(GlyphBrushProperty, value); }
        }

        public Brush TextBoxBackground
        {
            get { return (Brush)GetValue(TextBoxBackgroundProperty); }
            set { SetValue(TextBoxBackgroundProperty, value); }
        }

        public Brush EditableBackground
        {
            get { return (Brush)GetValue(EditableBackgroundProperty); }
            set { SetValue(EditableBackgroundProperty, value); }
        }

        public Brush EditableBorderBrush
        {
            get { return (Brush)GetValue(EditableBorderBrushProperty); }
            set { SetValue(EditableBorderBrushProperty, value); }
        }

        public Brush EditableButtonBackground
        {
            get { return (Brush)GetValue(EditableButtonBackgroundProperty); }
            set { SetValue(EditableButtonBackgroundProperty, value); }
        }

        public Brush EditableButtonBorderBrush
        {
            get { return (Brush)GetValue(EditableButtonBorderBrushProperty); }
            set { SetValue(EditableButtonBorderBrushProperty, value); }
        }







        // hover state


        public Brush HoverForeground
        {
            get { return (Brush)GetValue(HoverForegroundProperty); }
            set { SetValue(HoverForegroundProperty, value); }
        }

        public Brush HoverBackground
        {
            get { return (Brush)GetValue(HoverBackgroundProperty); }
            set { SetValue(HoverBackgroundProperty, value); }
        }

        public Brush HoverBorderBrush
        {
            get { return (Brush)GetValue(HoverBorderBrushProperty); }
            set { SetValue(HoverBorderBrushProperty, value); }
        }

        public Brush HoverGlyphBrush
        {
            get { return (Brush)GetValue(HoverGlyphBrushProperty); }
            set { SetValue(HoverGlyphBrushProperty, value); }
        }

        public Brush EditableHoverBackground
        {
            get { return (Brush)GetValue(EditableHoverBackgroundProperty); }
            set { SetValue(EditableHoverBackgroundProperty, value); }
        }

        public Brush EditableHoverBorderBrush
        {
            get { return (Brush)GetValue(EditableHoverBorderBrushProperty); }
            set { SetValue(EditableHoverBorderBrushProperty, value); }
        }

        public Brush EditableButtonHoverBackground
        {
            get { return (Brush)GetValue(EditableButtonHoverBackgroundProperty); }
            set { SetValue(EditableButtonHoverBackgroundProperty, value); }
        }

        public Brush EditableButtonHoverBorderBrush
        {
            get { return (Brush)GetValue(EditableButtonHoverBorderBrushProperty); }
            set { SetValue(EditableButtonHoverBorderBrushProperty, value); }
        }




        // pressed state


        public Brush PressedForeground
        {
            get { return (Brush)GetValue(PressedForegroundProperty); }
            set { SetValue(PressedForegroundProperty, value); }
        }

        public Brush PressedBackground
        {
            get { return (Brush)GetValue(PressedBackgroundProperty); }
            set { SetValue(PressedBackgroundProperty, value); }
        }

        public Brush PressedBorderBrush
        {
            get { return (Brush)GetValue(PressedBorderBrushProperty); }
            set { SetValue(PressedBorderBrushProperty, value); }
        }

        public Brush PressedGlyphBrush
        {
            get { return (Brush)GetValue(PressedGlyphBrushProperty); }
            set { SetValue(PressedGlyphBrushProperty, value); }
        }

        public Brush EditablePressedBackground
        {
            get { return (Brush)GetValue(EditablePressedBackgroundProperty); }
            set { SetValue(EditablePressedBackgroundProperty, value); }
        }

        public Brush EditablePressedBorderBrush
        {
            get { return (Brush)GetValue(EditablePressedBorderBrushProperty); }
            set { SetValue(EditablePressedBorderBrushProperty, value); }
        }

        public Brush EditableButtonPressedBackground
        {
            get { return (Brush)GetValue(EditableButtonPressedBackgroundProperty); }
            set { SetValue(EditableButtonPressedBackgroundProperty, value); }
        }

        public Brush EditableButtonPressedBorderBrush
        {
            get { return (Brush)GetValue(EditableButtonPressedBorderBrushProperty); }
            set { SetValue(EditableButtonPressedBorderBrushProperty, value); }
        }




        // pressed state


        public Brush DisabledForeground
        {
            get { return (Brush)GetValue(DisabledForegroundProperty); }
            set { SetValue(DisabledForegroundProperty, value); }
        }

        public Brush DisabledBackground
        {
            get { return (Brush)GetValue(DisabledBackgroundProperty); }
            set { SetValue(DisabledBackgroundProperty, value); }
        }

        public Brush DisabledBorderBrush
        {
            get { return (Brush)GetValue(DisabledBorderBrushProperty); }
            set { SetValue(DisabledBorderBrushProperty, value); }
        }

        public Brush DisabledGlyphBrush
        {
            get { return (Brush)GetValue(DisabledGlyphBrushProperty); }
            set { SetValue(DisabledGlyphBrushProperty, value); }
        }

        public Brush EditableDisabledBackground
        {
            get { return (Brush)GetValue(EditableDisabledBackgroundProperty); }
            set { SetValue(EditableDisabledBackgroundProperty, value); }
        }

        public Brush EditableDisabledBorderBrush
        {
            get { return (Brush)GetValue(EditableDisabledBorderBrushProperty); }
            set { SetValue(EditableDisabledBorderBrushProperty, value); }
        }

        public Brush EditableButtonDisabledBackground
        {
            get { return (Brush)GetValue(EditableButtonDisabledBackgroundProperty); }
            set { SetValue(EditableButtonDisabledBackgroundProperty, value); }
        }

        public Brush EditableButtonDisabledBorderBrush
        {
            get { return (Brush)GetValue(EditableButtonDisabledBorderBrushProperty); }
            set { SetValue(EditableButtonDisabledBorderBrushProperty, value); }
        }













        #region DependencyProperty Declaration

        // extra properties

        public static readonly DependencyProperty GlyphBrushProperty =
            DependencyProperty.Register(nameof(GlyphBrush), typeof(Brush), typeof(ComboBox), new PropertyMetadata(null));

        public static readonly DependencyProperty TextBoxBackgroundProperty =
            DependencyProperty.Register(nameof(TextBoxBackground), typeof(Brush), typeof(ComboBox), new PropertyMetadata(null));

        public static readonly DependencyProperty EditableBackgroundProperty =
            DependencyProperty.Register(nameof(EditableBackground), typeof(Brush), typeof(ComboBox), new PropertyMetadata(null));

        public static readonly DependencyProperty EditableBorderBrushProperty =
            DependencyProperty.Register(nameof(EditableBorderBrush), typeof(Brush), typeof(ComboBox), new PropertyMetadata(null));

        public static readonly DependencyProperty EditableButtonBackgroundProperty =
            DependencyProperty.Register(nameof(EditableButtonBackground), typeof(Brush), typeof(ComboBox), new PropertyMetadata(null));

        public static readonly DependencyProperty EditableButtonBorderBrushProperty =
            DependencyProperty.Register(nameof(EditableButtonBorderBrush), typeof(Brush), typeof(ComboBox), new PropertyMetadata(null));



        // hover state

        public static readonly DependencyProperty HoverForegroundProperty =
            DependencyProperty.Register(nameof(HoverForeground), typeof(Brush), typeof(ComboBox), new PropertyMetadata(null));

        public static readonly DependencyProperty HoverBackgroundProperty =
            DependencyProperty.Register(nameof(HoverBackground), typeof(Brush), typeof(ComboBox), new PropertyMetadata(null));

        public static readonly DependencyProperty HoverBorderBrushProperty =
            DependencyProperty.Register(nameof(HoverBorderBrush), typeof(Brush), typeof(ComboBox), new PropertyMetadata(null));

        public static readonly DependencyProperty HoverGlyphBrushProperty =
            DependencyProperty.Register(nameof(HoverGlyphBrush), typeof(Brush), typeof(ComboBox), new PropertyMetadata(null));

        public static readonly DependencyProperty EditableHoverBorderBrushProperty =
            DependencyProperty.Register(nameof(EditableHoverBorderBrush), typeof(Brush), typeof(ComboBox), new PropertyMetadata(null));

        public static readonly DependencyProperty EditableHoverBackgroundProperty =
            DependencyProperty.Register(nameof(EditableHoverBackground), typeof(Brush), typeof(ComboBox), new PropertyMetadata(null));

        public static readonly DependencyProperty EditableButtonHoverBackgroundProperty =
            DependencyProperty.Register(nameof(EditableButtonHoverBackground), typeof(Brush), typeof(ComboBox), new PropertyMetadata(null));

        public static readonly DependencyProperty EditableButtonHoverBorderBrushProperty =
            DependencyProperty.Register(nameof(EditableButtonHoverBorderBrush), typeof(Brush), typeof(ComboBox), new PropertyMetadata(null));


        // pressed state

        public static readonly DependencyProperty PressedForegroundProperty =
            DependencyProperty.Register(nameof(PressedForeground), typeof(Brush), typeof(ComboBox), new PropertyMetadata(null));

        public static readonly DependencyProperty PressedBackgroundProperty =
            DependencyProperty.Register(nameof(PressedBackground), typeof(Brush), typeof(ComboBox), new PropertyMetadata(null));

        public static readonly DependencyProperty PressedBorderBrushProperty =
            DependencyProperty.Register(nameof(PressedBorderBrush), typeof(Brush), typeof(ComboBox), new PropertyMetadata(null));

        public static readonly DependencyProperty PressedGlyphBrushProperty =
            DependencyProperty.Register(nameof(PressedGlyphBrush), typeof(Brush), typeof(ComboBox), new PropertyMetadata(null));

        public static readonly DependencyProperty EditablePressedBorderBrushProperty =
            DependencyProperty.Register(nameof(EditablePressedBorderBrush), typeof(Brush), typeof(ComboBox), new PropertyMetadata(null));

        public static readonly DependencyProperty EditablePressedBackgroundProperty =
            DependencyProperty.Register(nameof(EditablePressedBackground), typeof(Brush), typeof(ComboBox), new PropertyMetadata(null));

        public static readonly DependencyProperty EditableButtonPressedBackgroundProperty =
            DependencyProperty.Register(nameof(EditableButtonPressedBackground), typeof(Brush), typeof(ComboBox), new PropertyMetadata(null));

        public static readonly DependencyProperty EditableButtonPressedBorderBrushProperty =
            DependencyProperty.Register(nameof(EditableButtonPressedBorderBrush), typeof(Brush), typeof(ComboBox), new PropertyMetadata(null));


        // disabled state

        public static readonly DependencyProperty DisabledForegroundProperty =
            DependencyProperty.Register(nameof(DisabledForeground), typeof(Brush), typeof(ComboBox), new PropertyMetadata(null));

        public static readonly DependencyProperty DisabledBackgroundProperty =
            DependencyProperty.Register(nameof(DisabledBackground), typeof(Brush), typeof(ComboBox), new PropertyMetadata(null));

        public static readonly DependencyProperty DisabledBorderBrushProperty =
            DependencyProperty.Register(nameof(DisabledBorderBrush), typeof(Brush), typeof(ComboBox), new PropertyMetadata(null));

        public static readonly DependencyProperty DisabledGlyphBrushProperty =
            DependencyProperty.Register(nameof(DisabledGlyphBrush), typeof(Brush), typeof(ComboBox), new PropertyMetadata(null));

        public static readonly DependencyProperty EditableDisabledBorderBrushProperty =
            DependencyProperty.Register(nameof(EditableDisabledBorderBrush), typeof(Brush), typeof(ComboBox), new PropertyMetadata(null));

        public static readonly DependencyProperty EditableDisabledBackgroundProperty =
            DependencyProperty.Register(nameof(EditableDisabledBackground), typeof(Brush), typeof(ComboBox), new PropertyMetadata(null));

        public static readonly DependencyProperty EditableButtonDisabledBackgroundProperty =
            DependencyProperty.Register(nameof(EditableButtonDisabledBackground), typeof(Brush), typeof(ComboBox), new PropertyMetadata(null));

        public static readonly DependencyProperty EditableButtonDisabledBorderBrushProperty =
            DependencyProperty.Register(nameof(EditableButtonDisabledBorderBrush), typeof(Brush), typeof(ComboBox), new PropertyMetadata(null));




        public static readonly DependencyProperty CornerRadiusProperty =
            Border.CornerRadiusProperty.AddOwner(typeof(ComboBox));

        public static readonly DependencyProperty PopupCornerRadiusProperty =
            DependencyProperty.Register(nameof(PopupCornerRadius), typeof(CornerRadius), typeof(ComboBox), new PropertyMetadata(default(CornerRadius)));

        #endregion
    }
}
