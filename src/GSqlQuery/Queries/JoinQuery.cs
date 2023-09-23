using System.Collections.Generic;

namespace GSqlQuery
{
    public class JoinQuery<T> : SelectQuery<T> where T : class
    {
        internal JoinQuery(string text, IEnumerable<PropertyOptions> columns, IEnumerable<CriteriaDetail> criteria, IFormats statements) :
            base(text, columns, criteria, statements)
        { }
    }
}