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
using EleCho.WpfSuite.Controls.States;
using EleCho.WpfSuite.Controls.SourceGeneration;

namespace StateMachineControlTest.Controls
{
    [GenerateCornerRadiusProperty]
    [GenerateStateProperties(State.Hover)]
    [GenerateStateProperties(State.Pressed)]
    [GenerateStateProperties(State.Disabled)]
    public partial class TestControl : Button
    {
        static TestControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TestControl), new FrameworkPropertyMetadata(typeof(TestControl)));
        }
    }
}
