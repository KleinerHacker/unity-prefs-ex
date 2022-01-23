namespace UnityPrefsEx.Runtime.prefs_ex.Scripts.Runtime.Utils
{
    internal abstract class DynamicCache
    {
        public string Key { get; }

        public DynamicCache(string key)
        {
            Key = key;
        }
    }

    internal sealed class DynamicCache<T> : DynamicCache
    {
        public T Value { get; set; }

        public DynamicCache(string key, T value) : base(key)
        {
            Value = value;
        }
    }
}