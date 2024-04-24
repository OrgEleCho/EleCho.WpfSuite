using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;
using CommunityToolkit.Mvvm.ComponentModel;

namespace WpfTest.Tests
{
    [ObservableObject]
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

        [ObservableProperty]
        private Brush? _currentBrush;

        partial void OnCurrentBrushChanged(Brush? value)
        {
            Debug.WriteLine(value);
        }
    }
}
