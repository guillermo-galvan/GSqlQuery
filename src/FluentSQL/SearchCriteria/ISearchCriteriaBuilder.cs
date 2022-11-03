namespace FluentSQL.SearchCriteria
{
    /// <summary>
    /// Search Criteria Builder 
    /// </summary>
    public interface ISearchCriteriaBuilder<T,TReturn> : IBuilder<TReturn> where TReturn : IQuery
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
        public IEnumerable<CriteriaDetail> BuildCriteria(IStatements statements);
    }
}
