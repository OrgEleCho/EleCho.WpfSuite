using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace EleCho.WpfSuite
{
    public abstract class SingletonValueConverterBase<TSelf> : ValueConverterBase<TSelf>
        where TSelf : SingletonValueConverterBase<TSelf>, new()
    {
        private static TSelf? _instance = null;

        public static TSelf Instance => _instance ??= new();
    }

    public abstract class SingletonValueConverterBase<TSelf, TSourceValue, TTargetValue> : ValueConverterBase<TSelf, TSourceValue, TTargetValue>
        where TSelf : SingletonValueConverterBase<TSelf, TSourceValue, TTargetValue>, new()
    {
        private static TSelf? _instance = null;

        public static TSelf Instance => _instance ??= new();
    }
}
