using System;
using System.Windows;
using System.Windows.Controls;

namespace EleCho.WpfSuite
{
    /// <summary>
    /// Proxy for binding
    /// </summary>
    /// <remarks>
    /// You can bind data to the Data of the BindingProxy and make the BindingProxy a static resource, <br/>
    /// so that elements outside of the main visual tree can also bind to the corresponding data through the BindingProxy
    /// </remarks>
    public class BindingProxy : Freezable
    {
        /// <summary>
        /// Data
        /// </summary>
        public object Data
        {
            get { return (object)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        /// <summary>
        /// Dependency property of Data property
        /// </summary>
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register(nameof(Data), typeof(object), typeof(BindingProxy), new PropertyMetadata(null));

        /// <inheritdoc/>
        protected override Freezable CreateInstanceCore() => new BindingProxy();
    }
}
