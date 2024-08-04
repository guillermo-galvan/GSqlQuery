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
        /// Get Formats
        /// </summary>
        IFormats Formats { get; }

        /// <summary>
        /// Get ClassOptions
        /// </summary>
        ClassOptions ClassOptions { get; }

        CriteriaDetailCollection GetCriteria(ref uint parameterId);
    }
}