using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using EleCho.WpfSuite.Properties;

namespace EleCho.WpfSuite.Controls
{
    public class Dialog : System.Windows.Controls.ContentControl
    {
        static Dialog()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Dialog), new FrameworkPropertyMetadata(typeof(Dialog)));
        }

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register(nameof(IsOpen), typeof(bool), typeof(Dialog), new FrameworkPropertyMetadata(false, flags: FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, propertyChangedCallback: OnIsOpenChanged));

        private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not Dialog dialog)
            {
                return;
            }

            if (DialogLayer.GetDialogLayer(dialog) is not DialogLayer layer)
            {
                throw new InvalidOperationException(StringResources.DialogLayerCouldNotBeFoundConsiderPlacingADialogLayerAsTheAncestorNodeOfThisDialog);
            }

            if (e.NewValue is true)
            {
                layer.Push(dialog);
            }
            else
            {
                layer.Remove(dialog);
            }
        }

        public static Dialog? GetDialog(DependencyObject dependencyObject)
        {
            while (true)
            {
                var parent = VisualTreeHelper.GetParent(dependencyObject);

                if (parent is Dialog layer)
                {
                    return layer;
                }

                if (parent is null)
                {
                    break;
                }

                dependencyObject = parent;
            }

            return null;
        }
    }
}
