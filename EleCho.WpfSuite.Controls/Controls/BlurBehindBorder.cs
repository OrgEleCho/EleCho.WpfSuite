using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

namespace EleCho.WpfSuite.Controls
{
    /// <inheritdoc/>
    public class BlurBehindBorder : System.Windows.Controls.Border
    {
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
            DrawingVisual drawingVisual = new DrawingVisual()
            {
                Clip = new RectangleGeometry(new Rect(0, 0, RenderSize.Width, RenderSize.Height)),
                Effect = new BlurEffect()
                {
                    Radius = BlurRadius,
                    KernelType = BlurKernelType,
                    RenderingBias = BlurRenderingBias
                }
            };

            using (DrawingContext visualContext = drawingVisual.RenderOpen())
            {
                BackgroundPresenter.DrawBackground(visualContext, this, _panelStack, MaxDepth, false);
            }

            if (drawingVisual.Drawing is not null)
            {
                var layoutClip = Border.CalculateLayoutClip(RenderSize, BorderThickness, CornerRadius);
                if (layoutClip != null)
                {
                    dc.PushClip(layoutClip);
                }

                BackgroundPresenter.DrawVisual(dc, drawingVisual, default);

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
            DependencyProperty.RegisterReadOnly(nameof(ContentClip), typeof(Geometry), typeof(BlurBehindBorder), new FrameworkPropertyMetadata(default(Geometry)));

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
            BackgroundPresenter.MaxDepthProperty.AddOwner(typeof(BlurBehindBorder));

        /// <summary>
        /// The radius of the blur effect applied to the background.
        /// </summary>
        public static readonly DependencyProperty BlurRadiusProperty =
            DependencyProperty.Register(nameof(BlurRadius), typeof(double), typeof(BlurBehindBorder), new FrameworkPropertyMetadata(5.0, propertyChangedCallback: OnRenderPropertyChanged));

        /// <summary>
        /// The type of kernel used for the blur effect.
        /// </summary>
        public static readonly DependencyProperty BlurKernelTypeProperty =
            DependencyProperty.Register(nameof(BlurKernelType), typeof(KernelType), typeof(BlurBehindBorder), new FrameworkPropertyMetadata(KernelType.Gaussian, propertyChangedCallback: OnRenderPropertyChanged));

        /// <summary>
        /// The rendering bias for the blur effect, which can affect performance and quality.
        /// </summary>
        public static readonly DependencyProperty BlurRenderingBiasProperty =
            DependencyProperty.Register(nameof(BlurRenderingBias), typeof(RenderingBias), typeof(BlurBehindBorder), new FrameworkPropertyMetadata(RenderingBias.Performance, propertyChangedCallback: OnRenderPropertyChanged));

        private static void OnRenderPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement element)
            {
                BackgroundPresenter.ForceRender(element);
            }
        }
    }
}
