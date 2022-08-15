using FluentSQL.SearchCriteria;

namespace FluentSQL
{
    /// <summary>
    /// AndOr
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public interface IAndOr<T> : ISearchCriteriaBuilder where T : class, new()
    {
        /// <summary>
        /// Build Query
        /// </summary>
        IQuery<T> Build();
    }
}
