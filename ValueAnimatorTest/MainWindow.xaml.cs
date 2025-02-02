using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EleCho.WpfSuite.Media.Animation;

namespace ValueAnimatorTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
        }

        public ValueAnimator<double> ItemWidth { get; } = new ValueAnimator<double>(200)
        {
            Duration = new Duration(TimeSpan.FromMilliseconds(350)),
            EasingFunction = new SineEase()
            {
                EasingMode = EasingMode.EaseOut,
            }
        };

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ItemWidth.Value > 0)
            {
                ItemWidth.Value = 0;
            }
            else
            {
                ItemWidth.Value = 200;
            }
        }
    }
}