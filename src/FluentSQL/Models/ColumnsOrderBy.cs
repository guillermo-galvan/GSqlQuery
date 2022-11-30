﻿using FluentSQL.Default;

namespace FluentSQL.Models
{
    public sealed class ColumnsOrderBy
    {
        public IEnumerable<PropertyOptions> Columns { get; }

        public OrderBy OrderBy { get;  }

        public ColumnsOrderBy(IEnumerable<PropertyOptions> columns, OrderBy orderBy)
        {
            Columns = columns;
            OrderBy = orderBy;
        }
    }
}
