using System.Windows;

namespace EleCho.WpfSuite.FluentDesign
{
    public class Theme
    {
        public static bool GetIsPrimary(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsPrimaryProperty);
        }

        public static void SetIsPrimary(DependencyObject obj, bool value)
        {
            obj.SetValue(IsPrimaryProperty, value);
        }


        public static readonly DependencyProperty IsPrimaryProperty =
            DependencyProperty.RegisterAttached("IsPrimary", typeof(bool), typeof(Theme), new PropertyMetadata(false));


    }
}
