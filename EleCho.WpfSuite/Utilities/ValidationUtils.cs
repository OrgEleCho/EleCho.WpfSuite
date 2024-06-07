namespace EleCho.WpfSuite
{
    /// <summary>
    /// Validation Utilities
    /// </summary>
    public static class ValidationUtils
    {
        /// <summary>
        /// Value is in range of double number
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsInRangeOfDouble(object value)
        {
            var v = (double)value;
            return !(double.IsNaN(v) || double.IsInfinity(v));
        }

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
        /// In in range of positive double number
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsInRangeOfPosDouble(object value)
        {
            var v = (double)value;
            return !(double.IsNaN(v) || double.IsInfinity(v)) && v > 0;
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
        /// Value is in range of negative double number
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsInRangeOfNegDouble(object value)
        {
            var v = (double)value;
            return !(double.IsNaN(v) || double.IsInfinity(v)) && v < 0;
        }

        /// <summary>
        /// Value is in range of negative double number include zero
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsInRangeOfNegDoubleIncludeZero(object value)
        {
            var v = (double)value;
            return !(double.IsNaN(v) || double.IsInfinity(v)) && v <= 0;
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

        /// <summary>
        /// Value is in range of positive integer include zero
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsInRangeOfPosIntIncludeZero(object value)
        {
            var v = (int)value;
            return v >= 0;
        }

        /// <summary>
        /// Value is in range of negative integer
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsInRangeOfNegInt(object value)
        {
            var v = (int)value;
            return v < 0;
        }

        /// <summary>
        /// Value is in range of negative integer include zero
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsInRangeOfNegIntIncludeZero(object value)
        {
            var v = (int) value;
            return v <= 0;
        }

        /// <summary>
        /// Value is not null
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNotNull(object? value)
        {
            return value is not null;
        }
    }
}
