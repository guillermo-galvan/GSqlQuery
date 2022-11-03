using FluentSQL.SearchCriteria;

namespace FluentSQL
{
    public interface IAndOr<T,TReturn> : ISearchCriteriaBuilder<T, TReturn>, IBuilder<TReturn> where TReturn : IQuery
    { 

    }
}
