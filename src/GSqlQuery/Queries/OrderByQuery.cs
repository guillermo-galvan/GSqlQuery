using System.Collections.Generic;

namespace GSqlQuery
{
    public class OrderByQuery<T> : Query<T> where T : class, new()
    {
        internal OrderByQuery(string text, IEnumerable<PropertyOptions> columns, IEnumerable<CriteriaDetail> criteria, IStatements statements) :
            base(text, columns, criteria, statements)
        {
        }
    }
}