using System.Collections.Generic;

namespace GSqlQuery
{
    internal class BatchQuery : QueryBase
    {
        public BatchQuery(string text, IEnumerable<PropertyOptions> columns, IEnumerable<CriteriaDetail> criteria)
            : base(text, columns, criteria)
        {
        }
    }
}