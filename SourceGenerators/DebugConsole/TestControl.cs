using System.Windows.Controls;
using EleCho.WpfSuite.Controls.States;
using EleCho.WpfSuite.Controls.SourceGeneration;

namespace DebugConsole
{
    [GenerateCornerRadiusProperty]
    [GenerateStateProperties(State.Hover)]
    public partial class TestControl : Control
    {

    }
}
