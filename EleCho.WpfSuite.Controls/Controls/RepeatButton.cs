using System.Windows;
using System.Windows.Media;
using EleCho.WpfSuite.Controls.SourceGeneration;
using EleCho.WpfSuite.Controls.States;

namespace EleCho.WpfSuite.Controls
{
    [GenerateStates]
    [GenerateStatesState(State.Hover)]
    [GenerateStatesState(State.Pressed)]
    [GenerateStatesState(State.Highlighted)]
    [GenerateStatesState(State.Disabled)]
    [GenerateStatesProperty(StateProperty.Background)]
    [GenerateStatesProperty(StateProperty.Foreground)]
    [GenerateStatesProperty(StateProperty.BorderBrush)]
    [GenerateStatesProperty(StateProperty.Padding)]
    [GenerateStatesProperty(StateProperty.BorderThickness)]
    [GenerateStatesProperty(StateProperty.CornerRadius)]
    [GenerateCornerRadiusProperty]
    public partial class RepeatButton : System.Windows.Controls.Primitives.RepeatButton
    {
        static RepeatButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RepeatButton), new FrameworkPropertyMetadata(typeof(RepeatButton)));
        }
    }
}
