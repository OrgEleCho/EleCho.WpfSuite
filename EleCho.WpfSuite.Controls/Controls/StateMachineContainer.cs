using EleCho.WpfSuite.Controls.StateMachines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EleCho.WpfSuite.Controls
{
    public class StateMachineContainer : System.Windows.Controls.ContentControl
    {
        public StateCollection? States
        {
            get => (StateCollection?)GetValue(StatesProperty);
            set => SetValue(StatesProperty, value);
        }

        public string? ActiveState
        {
            get => (string?)GetValue(ActiveStateProperty);
            set => SetValue(ActiveStateProperty, value);
        }

        public IState? Presentation
        {
            get => (IState?)GetValue(PresentatinProperty);
        }

        static StateMachineContainer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StateMachineContainer), new FrameworkPropertyMetadata(typeof(StateMachineContainer)));
        }




        public static readonly DependencyProperty StatesProperty
            = StateMachine.StatesProperty.AddOwner(typeof(StateMachineContainer));

        public static readonly DependencyProperty ActiveStateProperty
            = StateMachine.ActiveStateProperty.AddOwner(typeof(StateMachineContainer));

        public static readonly DependencyProperty PresentatinProperty
            = StateMachine.PresentationProperty.AddOwner(typeof(StateMachineContainer));

    }
}
