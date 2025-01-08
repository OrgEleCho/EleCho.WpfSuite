using System.Windows.Controls;
using EleCho.WpfSuite.Controls.States;
using EleCho.WpfSuite.Controls.SourceGeneration;
using System.Windows.Media.Animation;

namespace DebugConsole
{
    [GenerateCornerRadiusProperty]
    [GenerateStates]
    [GenerateStatesState(State.Hover)]
    [GenerateStatesState(State.Highlighted)]
    [GenerateStatesProperty(StateProperty.Background)]
    [GenerateStatesProperty(StateProperty.Foreground)]
    [GenerateStatesProperty(StateProperty.BorderBrush)]
    [GenerateStatesProperty(StateProperty.GlyphBrush)]
    [GenerateStatesProperty(StateProperty.Padding)]
    [GenerateStatesProperty(StateProperty.BorderThickness)]
    [GenerateStatesProperty(StateProperty.CornerRadius)]
    public partial class TestControl : Control
    {

    }
}
