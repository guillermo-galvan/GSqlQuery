using GSqlQuery.SearchCriteria;

namespace GSqlQuery
{
    /// <summary>
    /// And or
    /// </summary>
    /// <typeparam name="TReturn"></typeparam>
    public interface IAndOr<TReturn> : ISearchCriteriaBuilder, IBuilder<TReturn> where TReturn : IQuery
    {

    }

    /// <summary>
    /// And Or
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TReturn"></typeparam>
    public interface IAndOr<T, TReturn, TQueryOptions> : ISearchCriteriaBuilder, IBuilder<TReturn>, IAndOr<TReturn>, IQueryOptions<TQueryOptions>
        where T : class
        where TReturn : IQuery<T, TQueryOptions>
        where TQueryOptions : QueryOptions
    {

    }
}