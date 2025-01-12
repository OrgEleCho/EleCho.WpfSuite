using System.Windows;
using System.Windows.Controls;
using EleCho.WpfSuite.Controls.SourceGeneration;

namespace EleCho.WpfSuite.Controls
{
    [GenerateCornerRadiusProperty]
    public partial class ProgressBar : System.Windows.Controls.ProgressBar
    {
        static ProgressBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ProgressBar), new FrameworkPropertyMetadata(typeof(ProgressBar)));
        }
    }
}
