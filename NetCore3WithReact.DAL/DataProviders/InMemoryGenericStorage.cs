using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace NetCore3WithReact.DAL.DataProviders
{
    public class InMemoryGenericStorage<TValue> where TValue : class
    {
        private static ConcurrentDictionary<string, TValue> _storage = new ConcurrentDictionary<string, TValue>();

        public IEnumerable<TValue> GetAllItems()
        {
            return _storage.Select(kv => kv.Value);
        }

        public TValue Get(string key)
        {
            return _storage.TryGetValue(key, out TValue value) ? value : null;
        }

        public void Set(string key, TValue value)
        {
            _storage.TryAdd(key, value);
        }

        public void Remove(string key)
        {
            _storage.TryRemove(key, out _);
        }

        public void Clear()
        {
            _storage.Clear();
        }
    }
}
