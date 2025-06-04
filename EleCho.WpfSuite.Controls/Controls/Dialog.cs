using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using EleCho.WpfSuite.Controls.SourceGeneration;
using EleCho.WpfSuite.Properties;

namespace EleCho.WpfSuite.Controls
{
    /// <summary>
    /// Dialog
    /// </summary>
    [GenerateCornerRadiusProperty]
    public partial class Dialog : System.Windows.Controls.ContentControl
    {
        static Dialog()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Dialog), new FrameworkPropertyMetadata(typeof(Dialog)));
        }

        /// <summary>
        /// Dialog is open
        /// </summary>
        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        /// <summary>
        /// DependencyProperty of <see cref="IsOpen"/>
        /// </summary>
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
    }
}
