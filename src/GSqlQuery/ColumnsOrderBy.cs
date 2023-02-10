using System.Collections.Generic;

namespace GSqlQuery
{
    internal sealed class ColumnsOrderBy
    {
        public IEnumerable<PropertyOptions> Columns { get; }

        public OrderBy OrderBy { get; }

        public ColumnsOrderBy(IEnumerable<PropertyOptions> columns, OrderBy orderBy)
        {
            Columns = columns;
            OrderBy = orderBy;
        }
    }
}
