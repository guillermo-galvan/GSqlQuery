using GSqlQuery.Cache;
using GSqlQuery.Queries;
using GSqlQuery.SearchCriteria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GSqlQuery.Extensions
{
    internal static class CacheQueryBuilderExtension
    {
        private static TReturn TryAddQueryCache<T, TReturn, TQueryOptions>(QueryIdentity identity, List<Type> searchCriteriaTypes, ISearchCriteriaBuilder andOr, Func<TReturn> createQuery, Func<string, PropertyOptionsCollection, IEnumerable<CriteriaDetailCollection>, TQueryOptions, TReturn> getQuery)
            where T : class
           where TReturn : IQuery<T, TQueryOptions>
           where TQueryOptions : QueryOptions
        {
            if (QueryCache.Cache.TryGetValue(identity, out IQuery query))
            {
                if (query is IQuery<T, TQueryOptions> tmpQuery && searchCriteriaTypes.Count > 0)
                {
                    List<CriteriaDetailCollection> tmp = new List<CriteriaDetailCollection>(tmpQuery.Criteria);
                    int count = 0;
                    foreach (ISearchCriteria item in andOr?.SearchCriterias ?? [])
                    {
                        CriteriaDetailCollection criteria = item.ReplaceValue(tmp[count]);
                        if (criteria == null)
                        {
                            QueryCache.Cache.TryRemove(identity, out _);
                            return AddQueryCache(identity, createQuery);
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
                return AddQueryCache(identity, createQuery);
            }
        }

        private static TReturn TryAddQueryCacheByEntity<T, TReturn, TQueryOptions>(QueryIdentity identity, object values, Func<TReturn> createQuery, Func<string, PropertyOptionsCollection, IEnumerable<CriteriaDetailCollection>, TQueryOptions, TReturn> getQuery)
            where T : class
           where TReturn : IQuery<T, TQueryOptions>
           where TQueryOptions : QueryOptions
        {
            if (QueryCache.Cache.TryGetValue(identity, out IQuery query))
            {
                if (query is IQuery<T, TQueryOptions> tmpQuery)
                {
                    List<CriteriaDetailCollection> tmp = [];
                    foreach (CriteriaDetailCollection item in tmpQuery.Criteria ?? [])
                    {
                        object value = ExpressionExtension.GetValue(item.PropertyOptions, values);
                        ParameterDetail parameterDetail = new ParameterDetail(item.Keys.First(), value);
                        tmp.Add(new CriteriaDetailCollection(item.QueryPart, item.PropertyOptions, [parameterDetail]));
                    }

                    return getQuery(tmpQuery.Text, tmpQuery.Columns, tmp, tmpQuery.QueryOptions);
                }

                return (TReturn)query;
            }
            else
            {
                return AddQueryCache(identity, createQuery);
            }
        }

        private static TReturn AddQueryCache<TReturn>(QueryIdentity identity, Func<TReturn> createQuery)
            where TReturn : IQuery
        {
            TReturn result = createQuery();
            QueryCache.Cache.Add(identity, result);
            return result;
        }

        internal static TReturn CreateSelectQuery<T, TReturn, TQueryOptions>(TQueryOptions queryOptions, DynamicQuery dynamicQuery, IAndOr<TReturn> andOr, Func<TReturn> createQuery, Func<string, PropertyOptionsCollection, IEnumerable<CriteriaDetailCollection>, TQueryOptions, TReturn> getQuery)
            where T : class
            where TReturn : IQuery<T, TQueryOptions>
            where TQueryOptions : QueryOptions
        {
            SelectQueryIdentity identity = new SelectQueryIdentity(typeof(T), queryOptions.Formats.GetType(), dynamicQuery?.Properties, andOr);

            return TryAddQueryCache<T, TReturn, TQueryOptions>(identity, identity.SearchCriteriaTypes, andOr, createQuery, getQuery);
        }

        internal static TReturn CreateOrderByQuery<T, TReturn, TQueryOptions, TSelectQuery>(TQueryOptions queryOptions, IQueryBuilderWithWhere<TSelectQuery, TQueryOptions> queryBuilderWithWhere, IAndOr<T, TSelectQuery, TQueryOptions> andOr, List<ColumnsOrderBy> columnsOrders, Func<TReturn> createQuery, Func<string, PropertyOptionsCollection, IEnumerable<CriteriaDetailCollection>, TQueryOptions, TReturn> getQuery)
           where T : class
           where TReturn : IQuery<T, TQueryOptions>
           where TQueryOptions : QueryOptions
           where TSelectQuery : IQuery<T, TQueryOptions>
        {
            IDynamicColumns dynamicColumns = null;

            if (queryBuilderWithWhere is not null && queryBuilderWithWhere is IDynamicColumns dynamic)
            {
                dynamicColumns = dynamic;
            }
            else if (andOr is not null && andOr is IDynamicColumns dynamic1)
            {
                dynamicColumns = dynamic1;
            }

            if (dynamicColumns is null)
            {
                return createQuery();
            }

            OrderByQueryIdentity identity = new OrderByQueryIdentity(typeof(T), QueryType.Read, queryOptions.Formats.GetType(), queryBuilderWithWhere?.GetType(), andOr?.GetType(), andOr, columnsOrders, dynamicColumns);

            return TryAddQueryCache<T, TReturn, TQueryOptions>(identity, identity.SearchCriteriaTypes, andOr, createQuery, getQuery);
        }

        internal static TReturn CreateCountSelectQuery<T, TReturn, TQueryOptions, TSelectQuery>(TQueryOptions queryOptions, IQueryBuilder<TSelectQuery, TQueryOptions> queryBuilder, IAndOr<TReturn> andOr, Func<TReturn> createQuery, Func<string, PropertyOptionsCollection, IEnumerable<CriteriaDetailCollection>, TQueryOptions, TReturn> getQuery)
            where T : class
            where TReturn : IQuery<T, TQueryOptions>
            where TQueryOptions : QueryOptions
            where TSelectQuery : IQuery<T, TQueryOptions>
        {
            IDynamicColumns dynamicColumns = null;

            if (queryBuilder is not null && queryBuilder is IDynamicColumns dynamic)
            {
                dynamicColumns = dynamic;
            }
            else if (andOr is not null && andOr is IDynamicColumns dynamic1)
            {
                dynamicColumns = dynamic1;
            }

            if (dynamicColumns is null)
            {
                return createQuery();
            }

            CountIdentify identity = new CountIdentify(typeof(T), QueryType.Read, queryOptions.Formats.GetType(), queryBuilder?.GetType(), andOr?.GetType(), andOr, dynamicColumns);

            return TryAddQueryCache<T, TReturn, TQueryOptions>(identity, identity.SearchCriteriaTypes, andOr, createQuery, getQuery);
        }

        internal static TReturn CreateJoinQuery<T, TReturn, TQueryOptions>(TQueryOptions queryOptions, List<JoinInfo> joinInfos, IAndOr<TReturn> andOr, Func<TReturn> createQuery, Func<string, PropertyOptionsCollection, IEnumerable<CriteriaDetailCollection>, TQueryOptions, TReturn> getQuery)
            where T : class
            where TReturn : IQuery<T, TQueryOptions>
            where TQueryOptions : QueryOptions
        {
            JoinQueryIdentity identity = new JoinQueryIdentity(typeof(T), QueryType.Join, queryOptions.Formats.GetType(), joinInfos, andOr);

            return TryAddQueryCache<T, TReturn, TQueryOptions>(identity, identity.SearchCriteriaTypes, andOr, createQuery, getQuery);
        }

        internal static TReturn CreateDeleteQuery<T, TReturn, TQueryOptions>(TQueryOptions queryOptions, IAndOr<TReturn> andOr, object entity, Func<TReturn> createQuery, Func<string, PropertyOptionsCollection, IEnumerable<CriteriaDetailCollection>, TQueryOptions, TReturn> getQuery)
           where T : class
           where TReturn : IQuery<T, TQueryOptions>
           where TQueryOptions : QueryOptions
        {
            DeleteQueryIdentity identity = new DeleteQueryIdentity(typeof(T), queryOptions.Formats.GetType(), entity is not null, andOr);

            if (entity is not null)
            {
                return TryAddQueryCacheByEntity<T, TReturn, TQueryOptions>(identity, entity, createQuery, getQuery);
            }
            else
            {
                return TryAddQueryCache<T, TReturn, TQueryOptions>(identity, identity.SearchCriteriaTypes, andOr, createQuery, getQuery);
            }
        }

        internal static TReturn CreateInsertQuery<T, TReturn, TQueryOptions>(TQueryOptions queryOptions, object entity, Func<TReturn> createQuery, Func<string, PropertyOptionsCollection, IEnumerable<CriteriaDetailCollection>, TQueryOptions, TReturn> getQuery)
          where T : class
          where TReturn : IQuery<T, TQueryOptions>
          where TQueryOptions : QueryOptions
        {
            InsertQueryIdentity identity = new InsertQueryIdentity(typeof(T), queryOptions.Formats.GetType());

            return TryAddQueryCacheByEntity<T, TReturn, TQueryOptions>(identity, entity, createQuery, getQuery);

        }

        internal static TReturn CreateUpdateQuery<T, TReturn, TQueryOptions>(TQueryOptions queryOptions, IAndOr<TReturn> andOr, object entity, IEnumerable<Expression> expressions, Func<TReturn> createQuery, Func<string, PropertyOptionsCollection, IEnumerable<CriteriaDetailCollection>, TQueryOptions, TReturn> getQuery)
          where T : class
          where TReturn : IQuery<T, TQueryOptions>
          where TQueryOptions : QueryOptions
        {
            if(entity == null)
            {
                return createQuery();
            }

            UpdateQueryIdentity identity = new UpdateQueryIdentity(typeof(T), queryOptions.Formats.GetType(), expressions, andOr);

            if (QueryCache.Cache.TryGetValue(identity, out IQuery query))
            {
                if (query is IQuery<T, TQueryOptions> tmpQuery)
                {
                    List<CriteriaDetailCollection> tmp = [];

                    IEnumerable<CriteriaDetailCollection> updateColumnsTmp = tmpQuery.Criteria?.Where(x => x.SearchCriteria is null) ?? [];

                    foreach (CriteriaDetailCollection item in updateColumnsTmp)
                    {
                        object value = ExpressionExtension.GetValue(item.PropertyOptions, entity);
                        ParameterDetail parameterDetail = new ParameterDetail(item.Keys.First(), value);
                        tmp.Add(new CriteriaDetailCollection(item.QueryPart, item.PropertyOptions, [parameterDetail]));
                    }

                    List<CriteriaDetailCollection> searchCriterias = tmpQuery.Criteria?.Where(x => x.SearchCriteria is not null).ToList() ?? [];

                    if (searchCriterias.Count != (andOr?.SearchCriterias?.Count() ?? 0))
                    {
                        QueryCache.Cache.TryRemove(identity, out _);
                        return AddQueryCache(identity, createQuery);
                    }

                    int count = 0;
                    foreach (ISearchCriteria item in andOr?.SearchCriterias ?? [])
                    {
                        CriteriaDetailCollection criteria = item.ReplaceValue(searchCriterias[count]);
                        if (criteria == null)
                        {
                            QueryCache.Cache.TryRemove(identity, out _);
                            return AddQueryCache(identity, createQuery);
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
                return AddQueryCache(identity, createQuery);
            }
        }
    }
}