using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using EleCho.WpfSuite.Controls.SourceGeneration;

namespace EleCho.WpfSuite.Controls
{
    [GenerateCornerRadiusProperty]
    public partial class Tooltip : System.Windows.Controls.ToolTip
    {
        static Tooltip()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Tooltip), new FrameworkPropertyMetadata(typeof(Tooltip)));
        }
    }
}
