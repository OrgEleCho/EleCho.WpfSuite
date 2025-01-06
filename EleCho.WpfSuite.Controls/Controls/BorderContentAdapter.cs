﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EleCho.WpfSuite.Controls
{
    /// <summary>
    /// Auto clip content depends on the parent border
    /// </summary>
    public class BorderContentAdapter : Decorator
    {
        static BorderContentAdapter()
        {
            ClipToBoundsProperty.OverrideMetadata(typeof(BorderContentAdapter), new PropertyMetadata(true));
        }

        private static Geometry? CalculateBorderContentClip(System.Windows.Controls.Border border, Size contentSize)
        {
            if (contentSize.Width <= 0 || 
                contentSize.Height <= 0)
            {
                return null;
            }

            var rect = new Rect(0, 0, contentSize.Width, contentSize.Height);
            var radii = new Border.Radii(border.CornerRadius, border.BorderThickness, false);

            var contentGeometry = new StreamGeometry();
            using StreamGeometryContext ctx = contentGeometry.Open();
            Border.GenerateGeometry(ctx, rect, radii);

            contentGeometry.Freeze();
            return contentGeometry;
        }

        /// <inheritdoc/>
        protected override Geometry? GetLayoutClip(Size layoutSlotSize)
        {
            var renderSize = RenderSize;

            if (!ClipToBounds)
            {
                return null;
            }

            if (Parent is EleCho.WpfSuite.Controls.Border border)
            {
                return border.ContentClip;
            }
            else if (Parent is System.Windows.Controls.Border nativeBorder &&
                CalculateBorderContentClip(nativeBorder, layoutSlotSize) is { } nativeBorderContentClip)
            {
                return nativeBorderContentClip;
            }

            RectangleGeometry rect = new RectangleGeometry(new Rect(RenderSize));
            rect.Freeze();
            return rect;
        }
    }

}
