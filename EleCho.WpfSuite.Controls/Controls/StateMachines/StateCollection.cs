using System;
using System.Collections.ObjectModel;
using System.Windows;

#pragma warning disable CS1591

namespace EleCho.WpfSuite.Controls.StateMachines
{
    public class StateCollection : FreezableCollection<State>
    {
        protected override Freezable CreateInstanceCore()
        {
            return new StateCollection();
        }
    }
}
