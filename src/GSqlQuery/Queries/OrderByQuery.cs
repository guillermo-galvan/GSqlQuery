namespace GSqlQuery
{
    public class OrderByQuery<T> : Query<T> where T : class, new()
    {
        public OrderByQuery(string text, IEnumerable<ColumnAttribute> columns, IEnumerable<CriteriaDetail>? criteria, IStatements statements) :
            base(text, columns, criteria, statements)
        {
        }
    }
}
