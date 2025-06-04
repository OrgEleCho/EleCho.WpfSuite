using System.Runtime.InteropServices;
using System.Windows.Media;
using System.Windows;

namespace EleCho.WpfSuite.Helpers
{
    /// <summary>
    /// DPI
    /// </summary>
    /// <param name="X">Horizontal value</param>
    /// <param name="Y">Vertical value</param>
    public record struct Dpi(int X, int Y)
    {
        /// <summary>
        /// Horizontal factor to standard DPI
        /// </summary>
        public double FactorX => (double)X / Standard.X;

        /// <summary>
        /// Vertical factor to standard DPI
        /// </summary>
        public double FactorY => (double)Y / Standard.Y;

        /// <summary>
        /// Standard DPI
        /// </summary>
        public static Dpi Standard => new Dpi(96, 96);

        /// <summary>
        /// System DPI
        /// </summary>
        public static Dpi System => new Dpi(GetDeviceCaps(default, LOGPIXELSX), GetDeviceCaps(default, LOGPIXELSY));

        /// <summary>
        /// Get <see cref="Dpi"/> from <see cref="Visual"/>
        /// </summary>
        /// <param name="visual"></param>
        /// <returns></returns>
        public static Dpi GetDpiFromVisual(Visual visual)
        {
            var source = PresentationSource.FromVisual(visual);
            var dpiX = 96;
            var dpiY = 96;
            if (source?.CompositionTarget != null)
            {
                dpiX = (int)(96 * source.CompositionTarget.TransformToDevice.M11);
                dpiY = (int)(96 * source.CompositionTarget.TransformToDevice.M22);
            }
            return new Dpi(dpiX, dpiY);
        }


        const int LOGPIXELSX = 88;
        const int LOGPIXELSY = 90;

        [DllImport("Gdi32", ExactSpelling = true)]
        private static extern int GetDeviceCaps(nint hDC, int index);
    }
}
