namespace FluentSQL.SearchCriteria
{
    /// <summary>
    /// 
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
        /// Get Criteria detail
        /// </summary>
        /// <param name="statements"></param>
        /// <returns></returns>
        CriteriaDetail GetCriteria(IStatements statements);
    }
}
