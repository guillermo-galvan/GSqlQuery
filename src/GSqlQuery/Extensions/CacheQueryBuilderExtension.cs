using GSqlQuery.Cache;
using GSqlQuery.Queries;
using System;
using System.Collections.Generic;

namespace GSqlQuery.Extensions
{
    internal static class CacheQueryBuilderExtension
    {
        internal static TReturn CreateSelectQuery<T, TReturn, TQueryOptions>(TQueryOptions queryOptions, DynamicQuery dynamicQuery, IAndOr<TReturn> andOr, Func<TReturn> createQuery, Func<string, PropertyOptionsCollection, IEnumerable<CriteriaDetailCollection>, TQueryOptions, TReturn> getQuery)
            where T : class
            where TReturn : IQuery<T, TQueryOptions>
            where TQueryOptions : QueryOptions
        {
            SelectQueryIdentity identity = new SelectQueryIdentity(typeof(T), queryOptions.Formats.GetType(), dynamicQuery?.Properties, andOr);

            if (QueryCache.Cache.TryGetValue(identity, out IQuery query))
            {
                if (query is IQuery<T, TQueryOptions> tmpQuery && identity.SearchCriteriaTypes.Count > 0)
                {
                    var tmp = new List<CriteriaDetailCollection>(tmpQuery.Criteria);
                    int count = 0;
                    foreach (var item in andOr.SearchCriterias)
                    {
                        var criteria = item.ReplaceValue(tmp[count]);
                        if (criteria == null)
                        {
                            QueryCache.Cache.TryRemove(identity, out _);
                            return AddCriteriaCache(identity, createQuery);
                        }
                        tmp[count] = criteria;
                        count++;
                    }

                    return getQuery(tmpQuery.Text, tmpQuery.Columns, tmp, tmpQuery.QueryOptions);
                }

                return (TReturn)query;
            }
            else
            {
                return AddCriteriaCache(identity, createQuery);
            }
        }

        internal static TReturn AddCriteriaCache<TReturn>(QueryIdentity identity, Func<TReturn> createQuery)
            where TReturn : IQuery
        {
            TReturn result = createQuery();
            QueryCache.Cache.Add(identity, result);
            return result;
        }
    }
}