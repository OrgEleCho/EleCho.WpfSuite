using System;

namespace EleCho.WpfSuite.ValueConverters
{
    /// <summary>
    /// Number comparison rule
    /// </summary>
    public enum NumberComparison
    {
        /// <summary>
        /// Check if A equals B
        /// </summary>
        Equal, 

        /// <summary>
        /// Check if A not equals B
        /// </summary>
        NotEqual,

        /// <summary>
        /// Check if A is greater than B
        /// </summary>
        GreaterThan, 
        
        /// <summary>
        /// Check if A is less than B
        /// </summary>
        LessThan,

        /// <summary>
        /// Check if A is greater than B or equals B
        /// </summary>
        GreaterOrEqual,

        /// <summary>
        /// Check if A is less than B or equals B
        /// </summary>
        LessOrEqual
    }
}
