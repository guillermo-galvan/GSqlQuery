namespace GSqlQuery.Runner.Test.Models
{
    internal class TestFormats : IFormats
    {
        public string Format => "[{0}]";

        public string ValueAutoIncrementingQuery => "SELECT SCOPE_IDENTITY();";

        public virtual string GetColumnName(string tableName, ColumnAttribute column, QueryType queryType)
        {
            return $"{tableName}.{string.Format(Format, column.Name)}";
        }
    }
}