using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleCho.WpfSuite.Controls
{
    /// <summary>
    /// The presentation mode of <see cref="DialogLayer"/>
    /// </summary>
    public enum DialogLayerMode
    {
        /// <summary>
        /// Only one dialog will be shown
        /// </summary>
        Switch,

        /// <summary>
        /// All dialogs will be stacked on top of each other
        /// </summary>
        Stack
    }
}
