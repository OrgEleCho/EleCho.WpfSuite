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
using EleCho.WpfSuite.Controls.SourceGeneration;
using EleCho.WpfSuite.Controls.States;

namespace EleCho.WpfSuite.Controls
{
    [GenerateStates]
    [GenerateStatesState(State.Hover)]
    [GenerateStatesState(State.Focused)]
    [GenerateStatesState(State.Disabled)]
    [GenerateStatesProperty(StateProperty.Background)]
    [GenerateStatesProperty(StateProperty.Foreground)]
    [GenerateStatesProperty(StateProperty.BorderBrush)]
    [GenerateStatesProperty(StateProperty.Padding)]
    [GenerateStatesProperty(StateProperty.BorderThickness)]
    [GenerateStatesProperty(StateProperty.CornerRadius)]
    [GenerateStatesProperty(StateProperty.PlaceholderBrush)]
    [GenerateCornerRadiusProperty]
    public partial class TextBox : System.Windows.Controls.TextBox
    {
        static TextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextBox), new FrameworkPropertyMetadata(typeof(TextBox)));
            TextProperty.OverrideMetadata(typeof(System.Windows.Controls.PasswordBox), new FrameworkPropertyMetadata(string.Empty));
        }

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public double DisabledTextOpacity
        {
            get => (double)GetValue(DisabledTextOpacityProperty);
            set => SetValue(DisabledTextOpacityProperty, value);
        }

        public static readonly DependencyProperty DisabledTextOpacityProperty =
            DependencyProperty.Register(nameof(DisabledTextOpacity), typeof(double), typeof(TextBox), new FrameworkPropertyMetadata(0.56));

        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(TextBox), new FrameworkPropertyMetadata(string.Empty));
    }
}
