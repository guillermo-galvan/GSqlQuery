using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GSqlQuery.Cache
{
    public sealed class PropertyOptionsCollection : IEnumerable<KeyValuePair<string, PropertyOptions>>
    {
        private readonly List<PropertyOptions> _values;
        private readonly Dictionary<string, int> _index;
        private readonly List<string> _keys;
        private readonly List<KeyValuePair<string, PropertyOptions>> _keyValues;

        private PropertyOptionsCollection()
        {
            _values = [];
            _index = [];
            _keys = [];
            _keyValues = [];
        }

        internal PropertyOptionsCollection(IEnumerable<KeyValuePair<string, PropertyOptions>> keyValues) : this()
        {
            AddRange(keyValues);
        }

        internal IEnumerable<string> Keys { get { return _keys; } }

        internal IEnumerable<PropertyOptions> Values { get { return _values; } }

        public int Count { get { return _values.Count; } }

        private bool TryAdd(string key, int value)
        {
            IEnumerable<KeyValuePair<string, int>> aa = _index;
            if (_index.ContainsKey(key))
            {
                return false;
            }

            _index.Add(key, value);

            return true;
        }

        private void Add(string key, PropertyOptions propertyOptions)
        {
            _keys.Add(key);
            _keyValues.Add(new KeyValuePair<string, PropertyOptions>(key, propertyOptions));
            _values.Add(propertyOptions);
        }

        internal void AddRange(IEnumerable<KeyValuePair<string, PropertyOptions>> keyValues)
        {
            if (keyValues == null || !keyValues.Any())
            {
                return;
            }

            int index = _values.Count == 0 ? 0 : _values.Count - 1;

            foreach (KeyValuePair<string, PropertyOptions> item in keyValues)
            {
                bool tryKey = TryAdd(item.Key, index);
                bool tryColumnAttribute = TryAdd(item.Value.ColumnAttribute.Name, index);
                bool lowerCase = TryAdd(item.Key.ToUpper(), index);

                if (tryKey || tryColumnAttribute || lowerCase)
                {
                    Add(item.Key, item.Value);
                    index++;
                }
            }
        }

        public PropertyOptions GetValue(string Key)
        {
            if (_index.TryGetValue(Key, out int value))
            {
                return _values[value];
            }

            throw new KeyNotFoundException();
        }

        internal PropertyOptions GetValue(ColumnAttribute column)
        {
            return GetValue(column.Name);
        }

        public PropertyOptions First()
        {
            return _values[0];
        }

        public PropertyOptions this[string key]
        {
            get
            {
                return GetValue(key);
            }
        }

        public bool ContainsKey(string key)
        {
            return _index.ContainsKey(key);
        }

        public IEnumerator<KeyValuePair<string, PropertyOptions>> GetEnumerator()
        {
            foreach (KeyValuePair<string, PropertyOptions> item in _keyValues)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}