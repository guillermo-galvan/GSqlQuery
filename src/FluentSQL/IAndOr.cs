using FluentSQL.SearchCriteria;

namespace FluentSQL
{
    /// <summary>
    /// AndOr
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public interface IAndOr<T, TReturn> : ISearchCriteriaBuilder, IBuilder<TReturn> where T : class, new() where TReturn : IQuery<T>
    {
    }
}
