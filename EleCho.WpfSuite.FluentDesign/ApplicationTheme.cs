namespace EleCho.WpfSuite.FluentDesign
{
    /// <summary>
    /// Represents supported application theme modes.
    /// </summary>
    public enum ApplicationTheme
    {
        /// <summary>
        /// Theme state is unknown.
        /// </summary>
        Unknown = -1,
        /// <summary>
        /// Theme follows current system preference.
        /// </summary>
        Auto = 0,
        /// <summary>
        /// Light theme.
        /// </summary>
        Light = 1, 
        /// <summary>
        /// Dark theme.
        /// </summary>
        Dark = 2
    }
}
