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

        public object Placeholder
        {
            get { return (object)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public DataTemplate PlaceholderTemplate
        {
            get { return (DataTemplate)GetValue(PlaceholderTemplateProperty); }
            set { SetValue(PlaceholderTemplateProperty, value); }
        }

        public DataTemplateSelector PlaceholderTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(PlaceholderTemplateSelectorProperty); }
            set { SetValue(PlaceholderTemplateSelectorProperty, value); }
        }




        public double DisabledTextOpacity
        {
            get => (double)GetValue(DisabledTextOpacityProperty);
            set => SetValue(DisabledTextOpacityProperty, value);
        }

        public static readonly DependencyProperty DisabledTextOpacityProperty =
            DependencyProperty.Register(nameof(DisabledTextOpacity), typeof(double), typeof(TextBox), new FrameworkPropertyMetadata(0.56));

        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register(nameof(Placeholder), typeof(object), typeof(TextBox), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty PlaceholderTemplateProperty =
            DependencyProperty.Register(nameof(PlaceholderTemplate), typeof(DataTemplate), typeof(TextBox), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty PlaceholderTemplateSelectorProperty =
            DependencyProperty.Register(nameof(PlaceholderTemplateSelector), typeof(DataTemplateSelector), typeof(TextBox), new FrameworkPropertyMetadata(null));
    }
}
