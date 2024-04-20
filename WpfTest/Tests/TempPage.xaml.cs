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

namespace WpfTest.Tests
{
    /// <summary>
    /// Interaction logic for TempPage.xaml
    /// </summary>
    public partial class TempPage : Page
    {
        public TempPage()
        {
            DataContext = this;
            InitializeComponent();
        }

        bool flag = false;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var targetState = flag ? "Normal" : "TestState";
            flag ^= true;

            VisualStateManager.GoToElementState(testBorder, targetState, true);
        }
    }
}
