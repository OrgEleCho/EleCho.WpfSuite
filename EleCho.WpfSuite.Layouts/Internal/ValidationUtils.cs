namespace EleCho.WpfSuite
{
    /// <summary>
    /// Validation Utilities
    /// </summary>
    internal static class ValidationUtils
    {
        /// <summary>
        /// Value is not double infinity
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNotDoubleInfinity(object value)
        {
            var v = (double)value;
            return !double.IsInfinity(v);
        }

        /// <summary>
        /// Value is in range of positive double number include zero
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsInRangeOfPosDoubleIncludeZero(object value)
        {
            var v = (double)value;
            return !(double.IsNaN(v) || double.IsInfinity(v)) && v >= 0;
        }

        /// <summary>
        /// Value is in range of positive integer
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsInRangeOfPosInt(object value)
        {
            var v = (int)value;
            return v > 0;
        }
    }
}
