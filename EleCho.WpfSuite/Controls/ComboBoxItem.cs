using System.Windows;

namespace EleCho.WpfSuite
{
    public class ComboBoxItem : System.Windows.Controls.ComboBox
    {
        static ComboBoxItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ComboBoxItem), new FrameworkPropertyMetadata(typeof(ComboBoxItem)));
        }
    }
}
