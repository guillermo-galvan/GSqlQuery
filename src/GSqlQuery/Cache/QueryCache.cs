using System.Collections.Concurrent;
using System.Collections.Generic;

namespace GSqlQuery.Cache
{
    internal class QueryCache
    {
        internal static QueryCache Cache = new QueryCache();

        private readonly ConcurrentDictionary<QueryIdentity, IQuery> _queryCache = new ConcurrentDictionary<QueryIdentity, IQuery>();

        public int Count => _queryCache.Count;

        public IEnumerable<QueryIdentity> Keys => _queryCache.Keys;

        public IEnumerable<IQuery> Values => _queryCache.Values;

        public bool TryGetValue(QueryIdentity queryIdentity, out IQuery value)
        {
            return _queryCache.TryGetValue(queryIdentity, out value);
        }

        public void Add(QueryIdentity queryIdentity, IQuery value)
        {
            _queryCache[queryIdentity] = value;
        }

        public bool TryRemove(QueryIdentity queryIdentity, out IQuery value)
        {
            return _queryCache.TryRemove(queryIdentity, out value);
        }
    }
}