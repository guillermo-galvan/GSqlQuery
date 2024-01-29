using GSqlQuery.SearchCriteria;

namespace GSqlQuery
{
    /// <summary>
    /// And or
    /// </summary>
    /// <typeparam name="TReturn"></typeparam>
    public interface IAndOr<TReturn> : ISearchCriteriaBuilder<TReturn>, IBuilder<TReturn> where TReturn : IQuery
    {

    }

    /// <summary>
    /// And Or
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TReturn"></typeparam>
    public interface IAndOr<T, TReturn> : ISearchCriteriaBuilder<TReturn>, IBuilder<TReturn>, IAndOr<TReturn>
        where T : class
        where TReturn : IQuery<T>
    {
        /// <summary>
        /// Formats
        /// </summary>
        IFormats Formats { get; }
    }
}