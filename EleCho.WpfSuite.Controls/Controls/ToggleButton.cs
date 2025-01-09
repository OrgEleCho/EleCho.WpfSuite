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
    [GenerateStatesState(State.Pressed)]
    [GenerateStatesState(State.Checked)]
    [GenerateStatesState(State.Highlighted)]
    [GenerateStatesState(State.Disabled)]
    [GenerateStatesProperty(StateProperty.Background)]
    [GenerateStatesProperty(StateProperty.Foreground)]
    [GenerateStatesProperty(StateProperty.BorderBrush)]
    [GenerateStatesProperty(StateProperty.Padding)]
    [GenerateStatesProperty(StateProperty.BorderThickness)]
    [GenerateStatesProperty(StateProperty.CornerRadius)]
    [GenerateCornerRadiusProperty]
    public partial class ToggleButton : System.Windows.Controls.Primitives.ToggleButton
    {
        static ToggleButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToggleButton), new FrameworkPropertyMetadata(typeof(ToggleButton)));
        }
    }
}
