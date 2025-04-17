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
using CommunityToolkit.Mvvm.Input;
using EleCho.WpfSuite.Controls;
using WpfTest.Controls;

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

        [ObservableProperty]
        private string _customMessage = string.Empty;


        public DialogTestPage()
        {
            DataContext = this;
            InitializeComponent();
        }

        [RelayCommand]
        public void ShowCustomMessage()
        {
            var dialogLayer = DialogLayer.GetDialogLayer(this);

            if (dialogLayer is null)
            {
                MessageBox.Show("Can not find dialog layer");
                return;
            }

            dialogLayer.Push(new Dialog()
            {
                Content = new SimpleMessageToast()
                {
                    Title = "Some Titme",
                    Message = CustomMessage,
                }
            });
        }

        [RelayCommand]
        public void CloseDialog()
        {
            IsDialogOpen = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IsDialogOpen = false;
        }
    }
}
