using System.Windows;
using System.Windows.Media;
using EleCho.WpfSuite.Controls.SourceGeneration;
using EleCho.WpfSuite.Controls.States;

namespace EleCho.WpfSuite.Controls
{
    /// <inheritdoc/>
    [GenerateStates]
    [GenerateStatesState(State.Disabled)]
    [GenerateStatesProperty(StateProperty.Background)]
    [GenerateStatesProperty(StateProperty.BorderBrush)]
    [GenerateStatesProperty(StateProperty.Padding)]
    [GenerateStatesProperty(StateProperty.BorderThickness)]
    [GenerateStatesProperty(StateProperty.CornerRadius)]
    [GenerateCornerRadiusProperty]
    public partial class ListView : System.Windows.Controls.ListView
    {
        static ListView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ListView), new FrameworkPropertyMetadata(typeof(ListView)));
        }

        /// <inheritdoc/>
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ListViewItem();
        }

        /// <inheritdoc/>
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is ListViewItem;
        }
    }
}
