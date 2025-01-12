using System.Windows.Controls;
using EleCho.WpfSuite.Controls.States;
using EleCho.WpfSuite.Controls.SourceGeneration;
using System.Windows.Media.Animation;

namespace DebugConsole
{
    [GenerateCornerRadiusProperty]
    [GenerateStates]
    [GenerateComponentStatesState("Fuck", State.Hover)]
    [GenerateComponentStateProperty("Fuck", StateProperty.Background)]
    public partial class TestControl : Control
    {

    }
}
