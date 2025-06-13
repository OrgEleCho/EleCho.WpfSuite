using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using EleCho.WpfSuite.Internal.Effects;

namespace EleCho.WpfSuite.Controls
{
    /// <inheritdoc/>
    public class AcrylicBehindBorder : System.Windows.Controls.Border
    {
        static AcrylicBehindBorder()
        {
            BlurRadiusProperty.OverrideMetadata(typeof(AcrylicBehindBorder), new FrameworkPropertyMetadata(64.0));
        }

        private readonly Stack<UIElement> _panelStack = new();

        /// <summary>
        /// A geometry to clip the content of this border correctly
        /// </summary>
        public Geometry? ContentClip
        {
            get { return (Geometry)GetValue(ContentClipProperty); }
            set { SetValue(ContentClipProperty, value); }
        }

        /// <summary>
        /// Gets or sets the maximum depth of the visual tree to render.
        /// </summary>
        public int MaxDepth
        {
            get { return (int)GetValue(MaxDepthProperty); }
            set { SetValue(MaxDepthProperty, value); }
        }

        /// <summary>
        /// Gets or sets the radius of the blur effect applied to the background.
        /// </summary>
        public double BlurRadius
        {
            get { return (double)GetValue(BlurRadiusProperty); }
            set { SetValue(BlurRadiusProperty, value); }
        }

        /// <summary>
        /// Gets or sets the type of kernel used for the blur effect.
        /// </summary>
        public KernelType BlurKernelType
        {
            get { return (KernelType)GetValue(BlurKernelTypeProperty); }
            set { SetValue(BlurKernelTypeProperty, value); }
        }

        /// <summary>
        /// Gets or sets the rendering bias for the blur effect, which can affect performance and quality.
        /// </summary>
        public RenderingBias BlurRenderingBias
        {
            get { return (RenderingBias)GetValue(BlurRenderingBiasProperty); }
            set { SetValue(BlurRenderingBiasProperty, value); }
        }

        public Color BlurBaseColor
        {
            get { return (Color)GetValue(BlurBaseColorProperty); }
            set { SetValue(BlurBaseColorProperty, value); }
        }

        public double NoiseStrength
        {
            get { return (double)GetValue(NoiseStrengthProperty); }
            set { SetValue(NoiseStrengthProperty, value); }
        }

        public double NoiseScale
        {
            get { return (double)GetValue(NoiseScaleProperty); }
            set { SetValue(NoiseScaleProperty, value); }
        }

        public Color BlendColor
        {
            get { return (Color)GetValue(BlendColorProperty); }
            set { SetValue(BlendColorProperty, value); }
        }

        /// <inheritdoc/>
        protected override Size ArrangeOverride(Size finalSize)
        {
            SetValue(ContentClipPropertyKey, CalculateContentClip(this));

            return base.ArrangeOverride(finalSize);
        }

        /// <inheritdoc/>
        protected override Geometry? GetLayoutClip(Size layoutSlotSize)
        {
            if (!ClipToBounds)
            {
                return null;
            }

            return Border.CalculateLayoutClip(layoutSlotSize, BorderThickness, CornerRadius);
        }

        /// <inheritdoc/>
        protected override void OnVisualParentChanged(DependencyObject oldParentObject)
        {
            if (oldParentObject is UIElement oldParent)
            {
                oldParent.LayoutUpdated -= ParentLayoutUpdated;
            }

            if (Parent is UIElement newParent)
            {
                newParent.LayoutUpdated += ParentLayoutUpdated;
            }
        }

        private void ParentLayoutUpdated(object? sender, EventArgs e)
        {
            // cannot use 'InvalidateVisual' here, because it will cause infinite loop

            BackgroundPresenter.ForceRender(this);

            Debug.WriteLine("Parent layout updated, forcing render of BackgroundPresenter.");
        }

        private static Geometry? CalculateContentClip(System.Windows.Controls.Border border)
        {
            var borderThickness = border.BorderThickness;
            var cornerRadius = border.CornerRadius;
            var renderSize = border.RenderSize;

            var contentWidth = renderSize.Width - borderThickness.Left - borderThickness.Right;
            var contentHeight = renderSize.Height - borderThickness.Top - borderThickness.Bottom;

            if (contentWidth > 0 && contentHeight > 0)
            {
                var rect = new Rect(0, 0, contentWidth, contentHeight);
                var radii = new Border.Radii(cornerRadius, borderThickness, false);

                var contentGeometry = new StreamGeometry();
                using StreamGeometryContext ctx = contentGeometry.Open();
                Border.GenerateGeometry(ctx, rect, radii);

                contentGeometry.Freeze();
                return contentGeometry;
            }
            else
            {
                return null;
            }
        }

        /// <inheritdoc/>
        protected override void OnRender(DrawingContext dc)
        {
            double blurRadius = BlurRadius;
            double noiseScale = NoiseScale;

            DrawingVisual blurDrawingVisual = new DrawingVisual()
            {
                Effect = new BlurEffect()
                {
                    Radius = blurRadius,
                    KernelType = BlurKernelType,
                    RenderingBias = BlurRenderingBias
                }
            };

            using (DrawingContext visualContext = blurDrawingVisual.RenderOpen())
            {
                var baseColorArea = new Rect(-blurRadius, -blurRadius, RenderSize.Width + blurRadius * 2, RenderSize.Height + blurRadius * 2);
                if (!baseColorArea.IsEmpty &&
                    BlurBaseColor.A != 0)
                {
                    visualContext.DrawRectangle(new SolidColorBrush(BlurBaseColor), null, baseColorArea);
                }

                BackgroundPresenter.DrawBackground(visualContext, this, _panelStack, MaxDepth, false);
            }

            if (blurDrawingVisual.Drawing is not null)
            {
                DrawingVisual blendDrawingVisual = new DrawingVisual()
                {
                    Effect = new BlendEffect()
                    {
                        InputSize = new Size(
                            RenderSize.Width * noiseScale + blurRadius * 2, 
                            RenderSize.Height * noiseScale + blurRadius * 2),

                        NoiseStrength = NoiseStrength / 20,
                        OverlayColor = BlendColor,
                    }
                };

                using (DrawingContext visualContext = blendDrawingVisual.RenderOpen())
                {
                    // Draw the background with the blur effect
                    BackgroundPresenter.DrawVisual(visualContext, blurDrawingVisual, default, -blurRadius, -blurRadius, blurRadius, blurRadius);
                }

                var layoutClip = Border.CalculateLayoutClip(RenderSize, BorderThickness, CornerRadius);
                if (layoutClip != null)
                {
                    dc.PushClip(layoutClip);
                }

                BackgroundPresenter.DrawVisual(dc, blendDrawingVisual, default);

                if (layoutClip != null)
                {
                    dc.Pop();
                }
            }

            base.OnRender(dc);
        }



        /// <summary>
        /// The key needed set a read-only property
        /// </summary>
        private static readonly DependencyPropertyKey ContentClipPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(ContentClip), typeof(Geometry), typeof(AcrylicBehindBorder), new FrameworkPropertyMetadata(default(Geometry)));

        /// <summary>
        /// The DependencyProperty for the ContentClip property. <br/>
        /// Flags: None <br/>
        /// Default value: null
        /// </summary>
        public static readonly DependencyProperty ContentClipProperty =
            ContentClipPropertyKey.DependencyProperty;

        /// <summary>
        /// The maximum depth of the visual tree to render.
        /// </summary>
        public static readonly DependencyProperty MaxDepthProperty =
            BackgroundPresenter.MaxDepthProperty.AddOwner(typeof(AcrylicBehindBorder));

        /// <summary>
        /// The radius of the blur effect applied to the background.
        /// </summary>
        public static readonly DependencyProperty BlurRadiusProperty =
            BlurBehindBorder.BlurRadiusProperty.AddOwner(typeof(AcrylicBehindBorder));

        /// <summary>
        /// The type of kernel used for the blur effect.
        /// </summary>
        public static readonly DependencyProperty BlurKernelTypeProperty =
            BlurBehindBorder.BlurKernelTypeProperty.AddOwner(typeof(AcrylicBehindBorder));

        /// <summary>
        /// The rendering bias for the blur effect, which can affect performance and quality.
        /// </summary>
        public static readonly DependencyProperty BlurRenderingBiasProperty =
            BlurBehindBorder.BlurRenderingBiasProperty.AddOwner(typeof(AcrylicBehindBorder));

        public static readonly DependencyProperty BlurBaseColorProperty =
            BlurBehindBorder.BlurBaseColorProperty.AddOwner(typeof(AcrylicBehindBorder));

        /// <summary>
        /// The strength of the noise effect applied to the background.
        /// </summary>
        public static readonly DependencyProperty NoiseStrengthProperty =
            DependencyProperty.Register(nameof(NoiseStrength), typeof(double), typeof(AcrylicBehindBorder), 
                new FrameworkPropertyMetadata(1.0, propertyChangedCallback: OnRenderPropertyChanged));

        public static readonly DependencyProperty NoiseScaleProperty =
            DependencyProperty.Register(nameof(NoiseScale), typeof(double), typeof(AcrylicBehindBorder), 
                new FrameworkPropertyMetadata(1.0, propertyChangedCallback: OnRenderPropertyChanged));

        /// <summary>
        /// The color used to blend with the background.
        /// </summary>
        public static readonly DependencyProperty BlendColorProperty =
            DependencyProperty.Register(nameof(BlendColor), typeof(Color), typeof(AcrylicBehindBorder), 
                new FrameworkPropertyMetadata(Colors.Transparent, propertyChangedCallback: OnRenderPropertyChanged));

        private static void OnRenderPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement element)
            {
                BackgroundPresenter.ForceRender(element);
            }
        }
    }
}
