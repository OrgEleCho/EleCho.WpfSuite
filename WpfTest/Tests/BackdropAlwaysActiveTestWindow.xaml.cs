using System.Windows;

namespace WpfTest.Tests
{
    /// <summary>
    /// Interaction logic for BackdropAlwaysActiveTestWindow.xaml
    /// </summary>
    public partial class BackdropAlwaysActiveTestWindow : Window
    {
        public BackdropAlwaysActiveTestWindow()
        {
            InitializeComponent();
        }

        private void OpenNormalWindow_Click(object sender, RoutedEventArgs e)
        {
            var normalWindow = new BackdropNormalTestWindow();
            normalWindow.Show();
        }
    }
}
