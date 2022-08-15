namespace FluentSQL.SearchCriteria
{
    /// <summary>
    /// Search Criteria Builder 
    /// </summary>
    public interface ISearchCriteriaBuilder
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
        internal IEnumerable<CriteriaDetail> BuildCriteria();
    }
}
