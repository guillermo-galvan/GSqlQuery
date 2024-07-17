namespace GSqlQuery.SearchCriteria
{
    /// <summary>
    /// Search criteria
    /// </summary>
    public interface ISearchCriteria
    {
        /// <summary>
        /// Get Column
        /// </summary>
        ColumnAttribute Column { get; }

        /// <summary>
        /// Get Table
        /// </summary>
        TableAttribute Table { get; }

        /// <summary>
        /// Get Formats
        /// </summary>
        IFormats Formats { get; }

        /// <summary>
        /// Get ClassOptions
        /// </summary>
        ClassOptions ClassOptions { get; }

        /// <summary>
        /// Get Criteria detail
        /// </summary>
        /// <returns>Details of the criteria</returns>
        CriteriaDetail GetCriteria();
    }
}