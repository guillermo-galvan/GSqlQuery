using System.Collections.Generic;

namespace GSqlQuery
{
    public class JoinQuery<T> : SelectQuery<T> where T : class, new()
    {
        internal JoinQuery(string text, IEnumerable<PropertyOptions> columns, IEnumerable<CriteriaDetail> criteria, IStatements statements) :
            base(text, columns, criteria, statements)
        { }
    }
}