using GSqlQuery.Queries;
using GSqlQuery.SearchCriteria;
using System;
using System.Collections.Generic;

namespace GSqlQuery.Extensions
{
    internal static class QueryBuilderExtension
    {
        public static TReturn GetCriteria<T, TReturn, TQueryOptions>(QueryType queryType, TQueryOptions queryOptions, DynamicQuery dynamicQuery, IAndOr<TReturn> andOr, Func<TReturn> createQuery, Func<string, PropertyOptionsCollection, IEnumerable<CriteriaDetailCollection>, TQueryOptions, TReturn> getQuery)
            where T : class
            where TReturn : IQuery<T, TQueryOptions>
            where TQueryOptions : QueryOptions
        {
            QueryIdentity identity = new QueryIdentity(typeof(T), QueryType.Read, queryOptions.Formats.GetType(), dynamicQuery?.Properties, andOr);

            if (QueryCache.Cache.TryGetValue(identity, out IQuery query))
            {
                var tmpQuery = query as IQuery<T, TQueryOptions>;
                if (tmpQuery != null && identity.SearchCriteriaTypes.Count > 0)
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
            var result = createQuery();
            QueryCache.Cache.Add(identity, result);
            return result;
        }
    }
}
