using System.Windows;

namespace EleCho.WpfSuite.FluentDesign
{
    /// <summary>
    /// Provides attached properties for Fluent theme related behaviors.
    /// </summary>
    public class Theme
    {
        /// <summary>
        /// Gets whether a target element is marked as primary.
        /// </summary>
        /// <param name="obj">The target dependency object.</param>
        /// <returns><see langword="true"/> if the element is primary; otherwise, <see langword="false"/>.</returns>
        public static bool GetIsPrimary(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsPrimaryProperty);
        }

        /// <summary>
        /// Sets whether a target element is marked as primary.
        /// </summary>
        /// <param name="obj">The target dependency object.</param>
        /// <param name="value">The primary state value.</param>
        public static void SetIsPrimary(DependencyObject obj, bool value)
        {
            obj.SetValue(IsPrimaryProperty, value);
        }


        /// <summary>
        /// Identifies the <c>IsPrimary</c> attached dependency property.
        /// </summary>
        public static readonly DependencyProperty IsPrimaryProperty =
            DependencyProperty.RegisterAttached("IsPrimary", typeof(bool), typeof(Theme), new PropertyMetadata(false));


    }
}
