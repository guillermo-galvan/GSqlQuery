namespace GSqlQuery.SearchCriteria
{
    /// <summary>
    /// Search criteria
    /// </summary>
    public interface ISearchCriteria
    {
        public object Value { get; }

        /// <summary>
        /// Get Formats
        /// </summary>
        IFormats Formats { get; }

        /// <summary>
        /// Get ClassOptions
        /// </summary>
        ClassOptions ClassOptions { get; }

        /// <summary>
        /// Get Detail Criteria
        /// </summary>
        /// <param name="parameterId">Id to identify the query parameter</param>
        /// <returns>CriteriaDetailCollection</returns>
        CriteriaDetailCollection GetCriteria(ref uint parameterId);

        CriteriaDetailCollection ReplaceValue(CriteriaDetailCollection criteriaDetailCollection);
    }
}