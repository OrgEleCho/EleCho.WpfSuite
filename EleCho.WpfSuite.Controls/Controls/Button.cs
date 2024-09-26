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

namespace EleCho.WpfSuite.Controls
{
    /// <summary>
    /// Represents the enhanced button component that inherently reacts to the Click event.
    /// </summary>
    public class Button : System.Windows.Controls.Button
    {
        static Button()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Button), new FrameworkPropertyMetadata(typeof(Button)));

            ForegroundProperty.OverrideMetadata(typeof(Button), new FrameworkPropertyMetadata(SystemColors.ControlTextBrush));
            BackgroundProperty.OverrideMetadata(typeof(Button), new FrameworkPropertyMetadata(SystemColors.ControlBrush));
            BorderBrushProperty.OverrideMetadata(typeof(Button), new FrameworkPropertyMetadata(Border.BorderBrushProperty.DefaultMetadata.DefaultValue));
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

        /// <summary>
        /// Foreground when mouse is over
        /// </summary>
        public Brush HoverForeground
        {
            get { return (Brush)GetValue(HoverForegroundProperty); }
            set { SetValue(HoverForegroundProperty, value); }
        }

        /// <summary>
        /// Background when mouse is over
        /// </summary>
        public Brush HoverBackground
        {
            get { return (Brush)GetValue(HoverBackgroundProperty); }
            set { SetValue(HoverBackgroundProperty, value); }
        }

        /// <summary>
        /// BorderBrush when mouse is over
        /// </summary>
        public Brush HoverBorderBrush
        {
            get { return (Brush)GetValue(HoverBorderBrushProperty); }
            set { SetValue(HoverBorderBrushProperty, value); }
        }

        /// <summary>
        /// Foreground when pressed by mouse
        /// </summary>
        public Brush PressedForeground
        {
            get { return (Brush)GetValue(PressedForegroundProperty); }
            set { SetValue(PressedForegroundProperty, value); }
        }

        /// <summary>
        /// Background when pressed by mouse
        /// </summary>
        public Brush PressedBackground
        {
            get { return (Brush)GetValue(PressedBackgroundProperty); }
            set { SetValue(PressedBackgroundProperty, value); }
        }

        /// <summary>
        /// BorderBrush when pressed by mouse
        /// </summary>
        public Brush PressedBorderBrush
        {
            get { return (Brush)GetValue(PressedBorderBrushProperty); }
            set { SetValue(PressedBorderBrushProperty, value); }
        }

        /// <summary>
        /// Foreground when disabled
        /// </summary>
        public Brush DisabledForeground
        {
            get { return (Brush)GetValue(DisabledForegroundProperty); }
            set { SetValue(DisabledForegroundProperty, value); }
        }

        /// <summary>
        /// Background when disabled
        /// </summary>
        public Brush DisabledBackground
        {
            get { return (Brush)GetValue(DisabledBackgroundProperty); }
            set { SetValue(DisabledBackgroundProperty, value); }
        }

        /// <summary>
        /// BorderBrush when pressed by mouse
        /// </summary>
        public Brush DisabledBorderBrush
        {
            get { return (Brush)GetValue(DisabledBorderBrushProperty); }
            set { SetValue(DisabledBorderBrushProperty, value); }
        }

        /// <summary>
        /// Highlight brush
        /// </summary>
        public Brush HighlightBrush
        {
            get { return (Brush)GetValue(HighlightBrushProperty); }
            set { SetValue(HighlightBrushProperty, value); }
        }




        /// <summary>
        /// The DependencyProperty of <see cref="CornerRadius"/> property
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            Border.CornerRadiusProperty.AddOwner(typeof(Button));

        /// <summary>
        /// The DependencyProperty of <see cref="HoverForeground"/> property
        /// </summary>
        public static readonly DependencyProperty HoverForegroundProperty =
            DependencyProperty.Register(nameof(HoverForeground), typeof(Brush), typeof(Button), new FrameworkPropertyMetadata(null));

        /// <summary>
        /// The DependencyProperty of <see cref="HoverBackground"/> property
        /// </summary>
        public static readonly DependencyProperty HoverBackgroundProperty =
            DependencyProperty.Register(nameof(HoverBackground), typeof(Brush), typeof(Button), new FrameworkPropertyMetadata(null));

        /// <summary>
        /// The DependencyProperty of <see cref="HoverBorderBrush"/> property
        /// </summary>
        public static readonly DependencyProperty HoverBorderBrushProperty =
            DependencyProperty.Register(nameof(HoverBorderBrush), typeof(Brush), typeof(Button), new FrameworkPropertyMetadata(null));

        /// <summary>
        /// The DependencyProperty of <see cref="PressedForeground"/> property
        /// </summary>
        public static readonly DependencyProperty PressedForegroundProperty =
            DependencyProperty.Register(nameof(PressedForeground), typeof(Brush), typeof(Button), new FrameworkPropertyMetadata(null));

        /// <summary>
        /// The DependencyProperty of <see cref="PressedBackground"/> property
        /// </summary>
        public static readonly DependencyProperty PressedBackgroundProperty =
            DependencyProperty.Register(nameof(PressedBackground), typeof(Brush), typeof(Button), new FrameworkPropertyMetadata(null));

        /// <summary>
        /// The DependencyProperty of <see cref="PressedBorderBrush"/> property
        /// </summary>
        public static readonly DependencyProperty PressedBorderBrushProperty =
            DependencyProperty.Register(nameof(PressedBorderBrush), typeof(Brush), typeof(Button), new FrameworkPropertyMetadata(null));

        /// <summary>
        /// The DependencyProperty of <see cref="DisabledForeground"/> property
        /// </summary>
        public static readonly DependencyProperty DisabledForegroundProperty =
            DependencyProperty.Register(nameof(DisabledForeground), typeof(Brush), typeof(Button), new FrameworkPropertyMetadata(null));

        /// <summary>
        /// The DependencyProperty of <see cref="DisabledBackground"/> property
        /// </summary>
        public static readonly DependencyProperty DisabledBackgroundProperty =
            DependencyProperty.Register(nameof(DisabledBackground), typeof(Brush), typeof(Button), new FrameworkPropertyMetadata(null));

        /// <summary>
        /// The DependencyProperty of <see cref="DisabledBorderBrush"/> property
        /// </summary>
        public static readonly DependencyProperty DisabledBorderBrushProperty =
            DependencyProperty.Register(nameof(DisabledBorderBrush), typeof(Brush), typeof(Button), new FrameworkPropertyMetadata(null));

        /// <summary>
        /// The DependencyProperty of <see cref="HighlightBrush"/> property
        /// </summary>
        public static readonly DependencyProperty HighlightBrushProperty =
            DependencyProperty.Register(nameof(HighlightBrush), typeof(Brush), typeof(Button), new FrameworkPropertyMetadata(SystemColors.HighlightBrush));
    }
}
