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
    /// <summary>
    /// Represents the enhanced button component that inherently reacts to the Click event.
    /// </summary>
    [GenerateCornerRadiusProperty]
    [GenerateStateProperties(State.Hover)]
    [GenerateStateProperties(State.Pressed)]
    [GenerateStateProperties(State.Highlighted)]
    [GenerateStateProperties(State.Disabled)]
    public partial class Button : System.Windows.Controls.Button
    {
        static Button()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Button), new FrameworkPropertyMetadata(typeof(Button)));
        }
    }
}
