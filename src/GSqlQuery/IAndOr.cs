using GSqlQuery.SearchCriteria;

namespace GSqlQuery
{
    public interface IAndOr<TReturn> : ISearchCriteriaBuilder<TReturn>, IBuilder<TReturn> where TReturn : IQuery
    {

    }

    public interface IAndOr<T, TReturn> : ISearchCriteriaBuilder<TReturn>, IBuilder<TReturn>, IAndOr<TReturn>
        where T : class, new()
        where TReturn : IQuery<T>
    {

    }
}