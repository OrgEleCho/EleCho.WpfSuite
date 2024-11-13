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
using EleCho.WpfSuite.Controls;

namespace WpfTest.Controls
{
    /// <summary>
    /// Interaction logic for SimpleMessageToast.xaml
    /// </summary>
    public partial class SimpleMessageToast : UserControl
    {
        public SimpleMessageToast()
        {
            InitializeComponent();
        }



        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }


        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(string), typeof(SimpleMessageToast), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(SimpleMessageToast), new PropertyMetadata(string.Empty));

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (DialogLayer.GetDialogLayer(this) is DialogLayer layer)
            {
                layer.Pop();
            }
        }
    }
}
