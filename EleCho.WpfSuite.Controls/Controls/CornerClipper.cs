using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EleCho.WpfSuite.Controls
{
    /// <summary>
    /// A control that clips its content to a rounded rectangle shape with corners clipped.
    /// </summary>
    public class CornerClipper : Decorator
    {
        static CornerClipper()
        {
            ClipToBoundsProperty.OverrideMetadata(typeof(CornerClipper), new FrameworkPropertyMetadata(true));
        }

        /// <summary>
        /// Gets or sets the corner radius of the clipped corners.
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        /// <inheritdoc/>
        protected override Geometry? GetLayoutClip(Size layoutSlotSize)
        {
            if (!ClipToBounds)
            {
                return null;
            }

            StreamGeometry streamGeometry = new StreamGeometry();
            using (var streamGeometryContext = streamGeometry.Open())
            {
                Border.GenerateGeometry(
                    streamGeometryContext,
                    new Rect(0, 0, RenderSize.Width, RenderSize.Height),
                    new Border.Radii(CornerRadius, default, true));
            }

            streamGeometry.Freeze();

            return streamGeometry;
        }

        /// <summary>
        /// Identifies the CornerRadius dependency property.
        /// </summary>
        public static DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(CornerClipper),
                new FrameworkPropertyMetadata(default(CornerRadius), FrameworkPropertyMetadataOptions.AffectsRender));
    }
}
