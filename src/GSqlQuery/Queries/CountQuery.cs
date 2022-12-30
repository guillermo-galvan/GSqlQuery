using System.Collections.Generic;

namespace GSqlQuery
{
    public sealed class CountQuery<T> : Query<T> where T : class, new()
    {
        public CountQuery(string text, IEnumerable<ColumnAttribute> columns, IEnumerable<CriteriaDetail> criteria, IStatements statements) :
            base(text, columns, criteria, statements)
        {
        }
    }
}
