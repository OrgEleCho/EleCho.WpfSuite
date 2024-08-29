using System.Runtime.InteropServices;

namespace EleCho.WpfSuite
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



        const int LOGPIXELSX = 88;
        const int LOGPIXELSY = 90;

        [DllImport("Gdi32", ExactSpelling = true)]
        private static extern int GetDeviceCaps(nint hDC, int index);
    }
}
