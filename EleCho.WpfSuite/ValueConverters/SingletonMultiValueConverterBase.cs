namespace EleCho.WpfSuite
{
    public abstract class SingletonMultiValueConverterBase<TSelf> : MultiValueConverterBase<TSelf>
        where TSelf : SingletonMultiValueConverterBase<TSelf>, new()
    {
        private static TSelf? _instance = null;
        public static TSelf Instance => _instance ?? new();
    }
}
