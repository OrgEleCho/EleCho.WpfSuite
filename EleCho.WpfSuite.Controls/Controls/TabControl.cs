using EleCho.WpfSuite.Controls.SourceGeneration;
using EleCho.WpfSuite.Controls.States;
using EleCho.WpfSuite.Media.Transition;
using System.Windows;
using System.Windows.Media;

namespace EleCho.WpfSuite.Controls
{
    [GenerateStates]
    [GenerateStatesState(State.Disabled)]
    [GenerateStatesProperty(StateProperty.Background)]
    [GenerateStatesProperty(StateProperty.Foreground)]
    [GenerateStatesProperty(StateProperty.BorderBrush)]
    [GenerateStatesProperty(StateProperty.Padding)]
    [GenerateStatesProperty(StateProperty.BorderThickness)]
    [GenerateStatesProperty(StateProperty.CornerRadius)]
    [GenerateCornerRadiusProperty]
    public partial class TabControl : System.Windows.Controls.TabControl
    {
        static TabControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TabControl), new FrameworkPropertyMetadata(typeof(TabControl)));
        }

        public IContentTransition? Transition
        {
            get { return (IContentTransition?)GetValue(TransitionProperty); }
            set { SetValue(TransitionProperty, value); }
        }

        public ContentTransitionMode TransitionMode
        {
            get { return (ContentTransitionMode)GetValue(TransitionModeProperty); }
            set { SetValue(TransitionModeProperty, value); }
        }

        public static readonly DependencyProperty TransitionProperty =
            ContentControl.TransitionProperty.AddOwner(typeof(TabControl));

        public static readonly DependencyProperty TransitionModeProperty =
            ContentControl.TransitionModeProperty.AddOwner(typeof(TabControl));
    }
}
