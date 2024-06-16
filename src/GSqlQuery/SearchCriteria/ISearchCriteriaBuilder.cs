using System.Collections.Generic;

namespace GSqlQuery.SearchCriteria
{
    /// <summary>
    /// Search Criteria Builder 
    /// </summary>
    public interface ISearchCriteriaBuilder<TReturn> : IBuilder<TReturn> where TReturn : IQuery
    {
        /// <summary>
        /// Add a search criteria
        /// </summary>
        /// <param name="criteria"></param>
        void Add(ISearchCriteria criteria);

        /// <summary>
        /// Build the criteria
        /// </summary>
        /// <returns>Criteria detail enumerable</returns>
        IEnumerable<CriteriaDetail> BuildCriteria();
    }
}