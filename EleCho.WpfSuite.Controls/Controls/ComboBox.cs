using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EleCho.WpfSuite.Controls.States;
using EleCho.WpfSuite.Controls.SourceGeneration;
using EleCho.WpfSuite.Media.Transition;

namespace EleCho.WpfSuite.Controls
{
    /// <summary>
    /// Represents a selection control with a drop-down list that can be shown or hidden by clicking the arrow on the control.
    /// </summary>
    [GenerateStates]
    [GenerateStatesState(State.Hover)]
    [GenerateStatesState(State.Pressed)]
    [GenerateStatesState(State.Disabled)]
    [GenerateStatesProperty(StateProperty.GlyphBrush)]
    [GenerateStatesProperty(StateProperty.Background)]
    [GenerateStatesProperty(StateProperty.Foreground)]
    [GenerateStatesProperty(StateProperty.BorderBrush)]
    [GenerateStatesProperty(StateProperty.Padding)]
    [GenerateStatesProperty(StateProperty.BorderThickness)]
    [GenerateStatesProperty(StateProperty.CornerRadius)]
    [GenerateStatesProperty(StateProperty.EditableBackground)]
    [GenerateStatesProperty(StateProperty.EditableForeground)]
    [GenerateStatesProperty(StateProperty.EditableBorderBrush)]
    [GenerateStatesProperty(StateProperty.EditablePadding)]
    [GenerateStatesProperty(StateProperty.EditableBorderThickness)]
    [GenerateStatesProperty(StateProperty.EditableCornerRadius)]
    [GenerateStatesProperty(StateProperty.EditableButtonBackground)]
    [GenerateStatesProperty(StateProperty.EditableButtonBorderBrush)]
    [GenerateStatesProperty(StateProperty.EditableButtonPadding)]
    [GenerateStatesProperty(StateProperty.EditableButtonBorderThickness)]
    [GenerateStatesProperty(StateProperty.EditableButtonCornerRadius)]
    [GenerateCornerRadiusProperty]
    [GeneratePopupProperties]
    public partial class ComboBox : System.Windows.Controls.ComboBox
    {
        static ComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ComboBox), new FrameworkPropertyMetadata(typeof(ComboBox)));
        }


        /// <inheritdoc/>
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ComboBoxItem();
        }

        /// <inheritdoc/>
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is ComboBoxItem;
        }
    }
}
