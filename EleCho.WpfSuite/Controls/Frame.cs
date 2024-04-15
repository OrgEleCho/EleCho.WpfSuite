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
    public class Frame : System.Windows.Controls.Frame
    {
        static Frame()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Frame), new FrameworkPropertyMetadata(typeof(Frame)));
        }



        public IContentTransition Transition
        {
            get { return (IContentTransition)GetValue(TransitionProperty); }
            set { SetValue(TransitionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Transition.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TransitionProperty =
            TransitioningContentControl.TransitionProperty.AddOwner(typeof(Frame));


    }
}
