namespace GSqlQuery
{
    public class DefaultFormats : IFormats
    {
        /// <summary>
        /// Instructions to separate columns or table example "{0}"
        /// </summary>
        public virtual string Format => "{0}";

        /// <summary>
        /// 
        /// </summary>
        public virtual string ValueAutoIncrementingQuery => "";

        public virtual string GetColumnName(string tableName, ColumnAttribute column, QueryType queryType)
        {
            return $"{tableName}.{string.Format(Format, column.Name)}";
        }
    }
}