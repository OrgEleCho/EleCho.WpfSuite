using System.Windows.Controls;
using EleCho.WpfSuite.Controls.States;
using EleCho.WpfSuite.Controls.SourceGeneration;

namespace DebugConsole
{
    [GenerateCornerRadiusProperty]
    [GenerateStateProperties(State.Hover)]
    [GenerateStateProperties(State.Highlighted)]
    public partial class TestControl : Control
    {

    }
}
