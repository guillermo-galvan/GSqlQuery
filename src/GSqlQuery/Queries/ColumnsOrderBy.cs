using System.Collections.Generic;

namespace GSqlQuery
{
    public sealed class ColumnsOrderBy
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