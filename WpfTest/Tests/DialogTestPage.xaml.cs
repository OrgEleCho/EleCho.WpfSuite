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
using CommunityToolkit.Mvvm.ComponentModel;
using EleCho.WpfSuite.Controls;

namespace WpfTest.Tests
{
    /// <summary>
    /// Interaction logic for DialogTestPage.xaml
    /// </summary>
    [ObservableObject]
    public partial class DialogTestPage : Page
    {
        [ObservableProperty]
        private bool _isDialogOpen;


        public DialogTestPage()
        {
            DataContext = this;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IsDialogOpen = false;
        }
    }
}
