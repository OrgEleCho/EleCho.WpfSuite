using System.Windows;
using System.Windows.Media;
using EleCho.WpfSuite.Controls.SourceGeneration;
using EleCho.WpfSuite.Controls.States;

namespace EleCho.WpfSuite.Controls
{
    [GenerateStates]
    [GenerateStatesState(State.Hover)]
    [GenerateStatesState(State.Selected)]
    [GenerateStatesState(State.Disabled)]
    [GenerateStatesProperty(StateProperty.Background)]
    [GenerateStatesProperty(StateProperty.Foreground)]
    [GenerateStatesProperty(StateProperty.BorderBrush)]
    [GenerateStatesProperty(StateProperty.Padding)]
    [GenerateStatesProperty(StateProperty.CornerRadius)]
    [GenerateCornerRadiusProperty]
    public partial class TabItem : System.Windows.Controls.TabItem
    {
        static TabItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TabItem), new FrameworkPropertyMetadata(typeof(TabItem)));
        }

        public double DisabledOpacity
        {
            get { return (double)GetValue(DisabledOpacityProperty); }
            set { SetValue(DisabledOpacityProperty, value); }
        }

        public static readonly DependencyProperty DisabledOpacityProperty =
            DependencyProperty.Register(nameof(DisabledOpacity), typeof(double), typeof(TabItem), new FrameworkPropertyMetadata(0.56));
    }
}
