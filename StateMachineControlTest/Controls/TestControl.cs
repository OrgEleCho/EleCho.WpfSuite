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

namespace StateMachineControlTest.Controls
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:StateMachineControlTest.Controls"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:StateMachineControlTest.Controls;assembly=StateMachineControlTest.Controls"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:TestControl/>
    ///
    /// </summary>
    public class TestControl : Button
    {
        static TestControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TestControl), new FrameworkPropertyMetadata(typeof(TestControl)));
        }

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public Brush HoverBackground
        {
            get { return (Brush)GetValue(HoverBackgroundProperty); }
            set { SetValue(HoverBackgroundProperty, value); }
        }

        public Brush HoverForeground
        {
            get { return (Brush)GetValue(HoverForegroundProperty); }
            set { SetValue(HoverForegroundProperty, value); }
        }

        public Brush HoverBorderBrush
        {
            get { return (Brush)GetValue(HoverBorderBrushProperty); }
            set { SetValue(HoverBorderBrushProperty, value); }
        }

        public Thickness HoverPadding
        {
            get { return (Thickness)GetValue(HoverPaddingProperty); }
            set { SetValue(HoverPaddingProperty, value); }
        }

        public Thickness HoverBorderThickness
        {
            get { return (Thickness)GetValue(HoverBorderThicknessProperty); }
            set { SetValue(HoverBorderThicknessProperty, value); }
        }

        public CornerRadius HoverCornerRadius
        {
            get { return (CornerRadius)GetValue(HoverCornerRadiusProperty); }
            set { SetValue(HoverCornerRadiusProperty, value); }
        }



        public Brush PressedBackground
        {
            get { return (Brush)GetValue(PressedBackgroundProperty); }
            set { SetValue(PressedBackgroundProperty, value); }
        }

        public Brush PressedForeground
        {
            get { return (Brush)GetValue(PressedForegroundProperty); }
            set { SetValue(PressedForegroundProperty, value); }
        }

        public Brush PressedBorderBrush
        {
            get { return (Brush)GetValue(PressedBorderBrushProperty); }
            set { SetValue(PressedBorderBrushProperty, value); }
        }

        public Thickness PressedPadding
        {
            get { return (Thickness)GetValue(PressedPaddingProperty); }
            set { SetValue(PressedPaddingProperty, value); }
        }

        public Thickness PressedBorderThickness
        {
            get { return (Thickness)GetValue(PressedBorderThicknessProperty); }
            set { SetValue(PressedBorderThicknessProperty, value); }
        }

        public CornerRadius PressedCornerRadius
        {
            get { return (CornerRadius)GetValue(PressedCornerRadiusProperty); }
            set { SetValue(PressedCornerRadiusProperty, value); }
        }


        public static readonly DependencyProperty CornerRadiusProperty = Border.CornerRadiusProperty.AddOwner(typeof(TestControl));


        public static readonly DependencyProperty HoverBackgroundProperty =
            DependencyProperty.Register("HoverBackground", typeof(Brush), typeof(TestControl), new PropertyMetadata(null));

        public static readonly DependencyProperty HoverForegroundProperty =
            DependencyProperty.Register("HoverForeground", typeof(Brush), typeof(TestControl), new PropertyMetadata(null));

        public static readonly DependencyProperty HoverBorderBrushProperty =
            DependencyProperty.Register("HoverBorderBrush", typeof(Brush), typeof(TestControl), new PropertyMetadata(null));

        public static readonly DependencyProperty HoverPaddingProperty =
            DependencyProperty.Register("HoverPadding", typeof(Thickness), typeof(TestControl), new PropertyMetadata(null));

        public static readonly DependencyProperty HoverBorderThicknessProperty =
            DependencyProperty.Register("HoverBorderThickness", typeof(Thickness), typeof(TestControl), new PropertyMetadata(null));

        public static readonly DependencyProperty HoverCornerRadiusProperty =
            DependencyProperty.Register("HoverCornerRadius", typeof(CornerRadius), typeof(TestControl), new PropertyMetadata(null));


        public static readonly DependencyProperty PressedBackgroundProperty =
            DependencyProperty.Register("PressedBackground", typeof(Brush), typeof(TestControl), new PropertyMetadata(null));

        public static readonly DependencyProperty PressedForegroundProperty =
            DependencyProperty.Register("PressedForeground", typeof(Brush), typeof(TestControl), new PropertyMetadata(null));

        public static readonly DependencyProperty PressedBorderBrushProperty =
            DependencyProperty.Register("PressedBorderBrush", typeof(Brush), typeof(TestControl), new PropertyMetadata(null));

        public static readonly DependencyProperty PressedPaddingProperty =
            DependencyProperty.Register("PressedPadding", typeof(Thickness), typeof(TestControl), new PropertyMetadata(null));

        public static readonly DependencyProperty PressedBorderThicknessProperty =
            DependencyProperty.Register("PressedBorderThickness", typeof(Thickness), typeof(TestControl), new PropertyMetadata(null));

        public static readonly DependencyProperty PressedCornerRadiusProperty =
            DependencyProperty.Register("PressedCornerRadius", typeof(CornerRadius), typeof(TestControl), new PropertyMetadata(null));


    }
}
