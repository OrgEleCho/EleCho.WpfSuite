using System.Windows;
using System.Windows.Media;
using EleCho.WpfSuite.Media.Transition;

namespace EleCho.WpfSuite.Controls
{
    public class ContextMenu : System.Windows.Controls.ContextMenu
    {
        static ContextMenu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ContextMenu), new FrameworkPropertyMetadata(typeof(ContextMenu)));
        }


        /// <summary>
        /// The CornerRadius property allows users to control the roundness of the corners independently by
        /// setting a radius value for each corner.  Radius values that are too large are scaled so that they
        /// smoothly blend from corner to corner.
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
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



        public static readonly DependencyProperty CornerRadiusProperty =
            Border.CornerRadiusProperty.AddOwner(typeof(ContextMenu));

        public static readonly DependencyProperty SeparatorBrushProperty =
            DependencyProperty.Register(nameof(SeparatorBrush), typeof(Brush), typeof(ContextMenu), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty TransitionProperty =
            ContentControl.TransitionProperty.AddOwner(typeof(ContextMenu));

        public static readonly DependencyProperty TransitionModeProperty =
            ContentControl.TransitionModeProperty.AddOwner(typeof(ContextMenu));


        protected override DependencyObject GetContainerForItemOverride()
        {
            return new MenuItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is MenuItem;
        }
    }
}
