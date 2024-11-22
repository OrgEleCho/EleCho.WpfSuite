using System.Windows;
using System.Windows.Media;
using EleCho.WpfSuite.Media.Transition;

namespace EleCho.WpfSuite.Controls
{
    public class MenuItem : System.Windows.Controls.MenuItem
    {
        static MenuItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MenuItem), new FrameworkPropertyMetadata(typeof(MenuItem)));
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

        public Brush HighlightedForeground
        {
            get { return (Brush)GetValue(HighlightedForegroundProperty); }
            set { SetValue(HighlightedForegroundProperty, value); }
        }

        public Brush HighlightedBackground
        {
            get { return (Brush)GetValue(HighlightedBackgroundProperty); }
            set { SetValue(HighlightedBackgroundProperty, value); }
        }

        public Brush HighlightedBorderBrush
        {
            get { return (Brush)GetValue(HighlightedBorderBrushProperty); }
            set { SetValue(HighlightedBorderBrushProperty, value); }
        }

        public Brush PressedForeground
        {
            get { return (Brush)GetValue(PressedForegroundProperty); }
            set { SetValue(PressedForegroundProperty, value); }
        }

        public Brush PressedBackground
        {
            get { return (Brush)GetValue(PressedBackgroundProperty); }
            set { SetValue(PressedBackgroundProperty, value); }
        }

        public Brush PressedBorderBrush
        {
            get { return (Brush)GetValue(PressedBorderBrushProperty); }
            set { SetValue(PressedBorderBrushProperty, value); }
        }

        public Brush SelectedForeground
        {
            get { return (Brush)GetValue(SelectedForegroundProperty); }
            set { SetValue(SelectedForegroundProperty, value); }
        }

        public Brush SelectedBackground
        {
            get { return (Brush)GetValue(SelectedBackgroundProperty); }
            set { SetValue(SelectedBackgroundProperty, value); }
        }

        public Brush SelectedBorderBrush
        {
            get { return (Brush)GetValue(SelectedBorderBrushProperty); }
            set { SetValue(SelectedBorderBrushProperty, value); }
        }

        public Brush CheckedForeground
        {
            get { return (Brush)GetValue(CheckedForegroundProperty); }
            set { SetValue(CheckedForegroundProperty, value); }
        }

        public Brush CheckedBackground
        {
            get { return (Brush)GetValue(CheckedBackgroundProperty); }
            set { SetValue(CheckedBackgroundProperty, value); }
        }

        public Brush CheckedBorderBrush
        {
            get { return (Brush)GetValue(CheckedBorderBrushProperty); }
            set { SetValue(CheckedBorderBrushProperty, value); }
        }

        public Brush DisabledForeground
        {
            get { return (Brush)GetValue(DisabledForegroundProperty); }
            set { SetValue(DisabledForegroundProperty, value); }
        }

        public Brush DisabledBackground
        {
            get { return (Brush)GetValue(DisabledBackgroundProperty); }
            set { SetValue(DisabledBackgroundProperty, value); }
        }

        public Brush DisabledBorderBrush
        {
            get { return (Brush)GetValue(DisabledBorderBrushProperty); }
            set { SetValue(DisabledBorderBrushProperty, value); }
        }

        public Brush HighlightedDisabledForeground
        {
            get { return (Brush)GetValue(HighlightedDisabledForegroundProperty); }
            set { SetValue(HighlightedDisabledForegroundProperty, value); }
        }

        public Brush HighlightedDisabledBackground
        {
            get { return (Brush)GetValue(HighlightedDisabledBackgroundProperty); }
            set { SetValue(HighlightedDisabledBackgroundProperty, value); }
        }

        public Brush HighlightedDisabledBorderBrush
        {
            get { return (Brush)GetValue(HighlightedDisabledBorderBrushProperty); }
            set { SetValue(HighlightedDisabledBorderBrushProperty, value); }
        }



        public Brush PopupBackground
        {
            get { return (Brush)GetValue(PopupBackgroundProperty); }
            set { SetValue(PopupBackgroundProperty, value); }
        }

        public Brush PopupBorderBrush
        {
            get { return (Brush)GetValue(PopupBorderBrushProperty); }
            set { SetValue(PopupBorderBrushProperty, value); }
        }

        public CornerRadius PopupCornerRadius
        {
            get { return (CornerRadius)GetValue(PopupCornerRadiusProperty); }
            set { SetValue(PopupCornerRadiusProperty, value); }
        }

        public IContentTransition PopupContentTransition
        {
            get { return (IContentTransition)GetValue(PopupContentTransitionProperty); }
            set { SetValue(PopupContentTransitionProperty, value); }
        }

        public ContentTransitionMode PopupContentTransitionMode
        {
            get { return (ContentTransitionMode)GetValue(PopupContentTransitionModeProperty); }
            set { SetValue(PopupContentTransitionModeProperty, value); }
        }


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

        public static readonly DependencyProperty CornerRadiusProperty =
            Border.CornerRadiusProperty.AddOwner(typeof(MenuItem));

        public static readonly DependencyProperty SeparatorBrushProperty =
            DependencyProperty.Register(nameof(SeparatorBrush), typeof(Brush), typeof(MenuItem), new PropertyMetadata(null));

        public static readonly DependencyProperty HighlightedForegroundProperty =
            DependencyProperty.Register(nameof(HighlightedForeground), typeof(Brush), typeof(MenuItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty HighlightedBackgroundProperty =
            DependencyProperty.Register(nameof(HighlightedBackground), typeof(Brush), typeof(MenuItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty HighlightedBorderBrushProperty =
            DependencyProperty.Register(nameof(HighlightedBorderBrush), typeof(Brush), typeof(MenuItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty PressedForegroundProperty =
            DependencyProperty.Register(nameof(PressedForeground), typeof(Brush), typeof(MenuItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty PressedBackgroundProperty =
            DependencyProperty.Register(nameof(PressedBackground), typeof(Brush), typeof(MenuItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty PressedBorderBrushProperty =
            DependencyProperty.Register(nameof(PressedBorderBrush), typeof(Brush), typeof(MenuItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty SelectedForegroundProperty =
            DependencyProperty.Register(nameof(SelectedForeground), typeof(Brush), typeof(MenuItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty SelectedBackgroundProperty =
            DependencyProperty.Register(nameof(SelectedBackground), typeof(Brush), typeof(MenuItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty SelectedBorderBrushProperty =
            DependencyProperty.Register(nameof(SelectedBorderBrush), typeof(Brush), typeof(MenuItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty CheckedForegroundProperty =
            DependencyProperty.Register(nameof(CheckedForeground), typeof(Brush), typeof(MenuItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty CheckedBackgroundProperty =
            DependencyProperty.Register(nameof(CheckedBackground), typeof(Brush), typeof(MenuItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty CheckedBorderBrushProperty =
            DependencyProperty.Register(nameof(CheckedBorderBrush), typeof(Brush), typeof(MenuItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty DisabledForegroundProperty =
            DependencyProperty.Register(nameof(DisabledForeground), typeof(Brush), typeof(MenuItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty DisabledBackgroundProperty =
            DependencyProperty.Register(nameof(DisabledBackground), typeof(Brush), typeof(MenuItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty DisabledBorderBrushProperty =
            DependencyProperty.Register(nameof(DisabledBorderBrush), typeof(Brush), typeof(MenuItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty HighlightedDisabledForegroundProperty =
            DependencyProperty.Register(nameof(HighlightedDisabledForeground), typeof(Brush), typeof(MenuItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty HighlightedDisabledBackgroundProperty =
            DependencyProperty.Register(nameof(HighlightedDisabledBackground), typeof(Brush), typeof(MenuItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty HighlightedDisabledBorderBrushProperty =
            DependencyProperty.Register(nameof(HighlightedDisabledBorderBrush), typeof(Brush), typeof(MenuItem), new FrameworkPropertyMetadata(null));


        public static readonly DependencyProperty PopupBackgroundProperty =
            DependencyProperty.Register(nameof(PopupBackground), typeof(Brush), typeof(MenuItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty PopupBorderBrushProperty =
            DependencyProperty.Register(nameof(PopupBorderBrush), typeof(Brush), typeof(MenuItem), new PropertyMetadata(null));

        public static readonly DependencyProperty PopupCornerRadiusProperty =
            DependencyProperty.Register(nameof(PopupCornerRadius), typeof(CornerRadius), typeof(MenuItem), new FrameworkPropertyMetadata(default(CornerRadius)));

        public static readonly DependencyProperty PopupContentTransitionProperty =
            DependencyProperty.Register(nameof(PopupContentTransition), typeof(IContentTransition), typeof(MenuItem), new PropertyMetadata(null));

        public static readonly DependencyProperty PopupContentTransitionModeProperty =
            DependencyProperty.Register(nameof(PopupContentTransitionMode), typeof(ContentTransitionMode), typeof(MenuItem), new PropertyMetadata(ContentTransitionMode.ChangedOrLoaded));





    }
}
