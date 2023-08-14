using System.Collections.Generic;

namespace GSqlQuery
{
    public interface IBulkCopy
    {
        IBulkCopyExecute Copy<T>(IEnumerable<T> values);
    }
}