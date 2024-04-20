using System.Windows;

namespace EleCho.WpfSuite
{
    public class BindingProxy : Freezable
    {
        public object Data
        {
            get { return (object)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register(nameof(Data), typeof(object), typeof(BindingProxy), new PropertyMetadata(null));

        protected override Freezable CreateInstanceCore() => new BindingProxy();
    }
}
