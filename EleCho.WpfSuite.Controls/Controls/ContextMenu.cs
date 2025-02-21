using System.Windows;
using System.Windows.Media;
using EleCho.WpfSuite.Controls.SourceGeneration;
using EleCho.WpfSuite.Internal;
using EleCho.WpfSuite.Media.Transition;

namespace EleCho.WpfSuite.Controls
{
    [GenerateCornerRadiusProperty]
    public partial class ContextMenu : System.Windows.Controls.ContextMenu
    {
        static ContextMenu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ContextMenu), new FrameworkPropertyMetadata(typeof(ContextMenu)));
        }

        public Brush SeparatorBrush
        {
            get { return (Brush)GetValue(SeparatorBrushProperty); }
            set { SetValue(SeparatorBrushProperty, value); }
        }

        public IContentTransition Transition
        {
            get { return (IContentTransition)GetValue(TransitionProperty); }
            set { SetValue(TransitionProperty, value); }
        }

        public ContentTransitionMode TransitionMode
        {
            get { return (ContentTransitionMode)GetValue(TransitionModeProperty); }
            set { SetValue(TransitionModeProperty, value); }
        }

        public static readonly DependencyProperty SeparatorBrushProperty =
            DependencyProperty.Register(nameof(SeparatorBrush), typeof(Brush), typeof(ContextMenu), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty TransitionProperty =
            ContentControl.TransitionProperty.AddOwner(typeof(ContextMenu));

        public static readonly DependencyProperty TransitionModeProperty =
            ContentControl.TransitionModeProperty.AddOwner(typeof(ContextMenu));

        /// <inheritdoc/>
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new MenuItem();
        }

        /// <inheritdoc/>
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is MenuItem;
        }
    }
}
