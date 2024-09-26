using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace EleCho.WpfSuite.ValueConverters
{
    /// <summary>
    /// Base class of singleton value converter
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    public abstract class SingletonValueConverterBase<TSelf> : ValueConverterBase<TSelf>
        where TSelf : SingletonValueConverterBase<TSelf>, new()
    {
        private static TSelf? _instance = null;

        /// <summary>
        /// Get an instance of <typeparamref name="TSelf"/>
        /// </summary>
        public static TSelf Instance => _instance ??= new();
    }

    /// <summary>
    /// Base class of singleton value converter
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <typeparam name="TSourceValue"></typeparam>
    /// <typeparam name="TTargetValue"></typeparam>
    public abstract class SingletonValueConverterBase<TSelf, TSourceValue, TTargetValue> : ValueConverterBase<TSelf, TSourceValue, TTargetValue>
        where TSelf : SingletonValueConverterBase<TSelf, TSourceValue, TTargetValue>, new()
    {
        private static TSelf? _instance = null;

        /// <summary>
        /// Get an instance of <typeparamref name="TSelf"/>
        /// </summary>
        public static TSelf Instance => _instance ??= new();
    }
}
