using System.Windows;
using EleCho.WpfSuite.Controls.SourceGeneration;

namespace EleCho.WpfSuite.Controls
{
    [GenerateCornerRadiusProperty]
    public partial class GroupBox : System.Windows.Controls.GroupBox
    {
        static GroupBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GroupBox), new FrameworkPropertyMetadata(typeof(GroupBox)));
        }
    }
}
