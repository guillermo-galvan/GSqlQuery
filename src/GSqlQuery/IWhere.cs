using GSqlQuery.SearchCriteria;

namespace GSqlQuery
{
    public interface IWhere<TReturn> where TReturn : IQuery
    {
    }

    public interface IWhere<T, TReturn, TQueryOptions> : IWhere<TReturn>, IQueryOptions<TQueryOptions>, ISearchCriteriaBuilder
        where T : class
        where TReturn : IQuery<T, TQueryOptions>
        where TQueryOptions : QueryOptions
    {
        public IAndOr<T, TReturn, TQueryOptions> AndOr { get; }
    }
}