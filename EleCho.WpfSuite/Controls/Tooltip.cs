using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EleCho.WpfSuite.Controls
{
    public class Tooltip : System.Windows.Controls.ToolTip
    {
        static Tooltip()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Tooltip), new FrameworkPropertyMetadata(typeof(Tooltip)));
        }

        /// <summary>
        /// The CornerRadius property allows users to control the roundness of the corners independently by
        /// setting a radius value for each corner.  Radius values that are too large are scaled so that they
        /// smoothly blend from corner to corner.
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }


        public static readonly DependencyProperty CornerRadiusProperty =
            Border.CornerRadiusProperty.AddOwner(typeof(Tooltip));
    }
}
