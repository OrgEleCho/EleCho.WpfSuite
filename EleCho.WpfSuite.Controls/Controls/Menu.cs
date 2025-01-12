using System.Windows;
using System.Windows.Media;
using EleCho.WpfSuite.Controls.SourceGeneration;

namespace EleCho.WpfSuite.Controls
{
    [GenerateCornerRadiusProperty]
    public partial class Menu : System.Windows.Controls.Menu
    {
        static Menu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Menu), new FrameworkPropertyMetadata(typeof(Menu)));
        }


        public Brush SeparatorBrush
        {
            get { return (Brush)GetValue(SeparatorBrushProperty); }
            set { SetValue(SeparatorBrushProperty, value); }
        }



        public static readonly DependencyProperty SeparatorBrushProperty =
            DependencyProperty.Register(nameof(SeparatorBrush), typeof(Brush), typeof(Menu), new FrameworkPropertyMetadata(null));

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new MenuItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is MenuItem;
        }
    }
}
