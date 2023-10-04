namespace GSqlQuery
{
    /// <summary>
    /// Default Formats
    /// </summary>
    public class DefaultFormats : IFormats
    {
        /// <summary>
        /// Formats the column and table name example "{0}"
        /// </summary>
        public virtual string Format => "{0}";

        /// <summary>
        /// Instruction to obtain the id of a column that is found as an automatic increment
        /// </summary>
        public virtual string ValueAutoIncrementingQuery => "";

        /// <summary>
        /// Gets the column name
        /// </summary>
        /// <param name="tableName">Table name</param>
        /// <param name="column">Column</param>
        /// <param name="queryType">Query Type</param>
        /// <returns>Column Name</returns>
        public virtual string GetColumnName(string tableName, ColumnAttribute column, QueryType queryType)
        {
            return $"{tableName}.{string.Format(Format, column.Name)}";
        }
    }
}