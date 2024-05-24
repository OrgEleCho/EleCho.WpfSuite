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

namespace EleCho.WpfSuite
{
    public class ComboBox : System.Windows.Controls.ComboBox
    {
        static ComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ComboBox), new FrameworkPropertyMetadata(typeof(ComboBox)));
        }

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public CornerRadius PopupCornerRadius
        {
            get { return (CornerRadius)GetValue(PopupCornerRadiusProperty); }
            set { SetValue(PopupCornerRadiusProperty, value); }
        }


        public static readonly DependencyProperty CornerRadiusProperty =
            Border.CornerRadiusProperty.AddOwner(typeof(ComboBox));

        public static readonly DependencyProperty PopupCornerRadiusProperty =
            DependencyProperty.Register(nameof(PopupCornerRadius), typeof(CornerRadius), typeof(ComboBox), new PropertyMetadata(default(CornerRadius)));
    }
}
