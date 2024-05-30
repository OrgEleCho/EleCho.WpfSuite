using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EleCho.WpfSuite
{
    public class Tooltip : System.Windows.Controls.ToolTip
    {
        static Tooltip()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Tooltip), new FrameworkPropertyMetadata(typeof(Tooltip)));
        }

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }


        public static readonly DependencyProperty CornerRadiusProperty =
            Border.CornerRadiusProperty.AddOwner(typeof(Tooltip));
    }
}
