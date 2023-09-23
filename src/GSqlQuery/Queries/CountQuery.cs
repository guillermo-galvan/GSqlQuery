using System.Collections.Generic;

namespace GSqlQuery
{
    public class CountQuery<T> : Query<T> where T : class
    {
        public CountQuery(string text, IEnumerable<PropertyOptions> columns, IEnumerable<CriteriaDetail> criteria, IFormats statements) :
            base(text, columns, criteria, statements)
        {
        }
    }
}