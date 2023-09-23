namespace GSqlQuery
{
    /// <summary>
    /// Statements 
    /// </summary>
    public interface IFormats
    {
        /// <summary>
        /// Instructions to separate columns or table example "{0}"
        /// </summary>
        string Format { get; }

        /// <summary>
        /// 
        /// </summary>
        string ValueAutoIncrementingQuery { get; }

        string GetColumnName(string tableName, ColumnAttribute column, QueryType queryType);
    }
}