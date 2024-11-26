using GSqlQuery.Cache;
using System.Collections.Generic;

namespace GSqlQuery
{
    internal class BatchQuery(string text, TableAttribute table, PropertyOptionsCollection columns, IEnumerable<CriteriaDetailCollection> criteria) : Query(ref text, table, columns, criteria)
    {
    }
}