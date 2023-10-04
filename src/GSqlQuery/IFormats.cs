namespace GSqlQuery
{
    /// <summary>
    /// Formats 
    /// </summary>
    public interface IFormats
    {
        /// <summary>
        /// Formats the column and table name example "{0}"
        /// </summary>
        string Format { get; }

        /// <summary>
        /// Instruction to obtain the id of a column that is found as an automatic increment
        /// </summary>
        string ValueAutoIncrementingQuery { get; }

        /// <summary>
        /// Gets the column name
        /// </summary>
        /// <param name="tableName">Table name</param>
        /// <param name="column">Column</param>
        /// <param name="queryType">Query Type</param>
        /// <returns>Column Name</returns>
        string GetColumnName(string tableName, ColumnAttribute column, QueryType queryType);
    }
}