using GSqlQuery.SearchCriteria;

namespace GSqlQuery
{
    public interface IAndOr<T,TReturn> : ISearchCriteriaBuilder<T, TReturn>, IBuilder<TReturn> where TReturn : IQuery
    { 

    }
}
