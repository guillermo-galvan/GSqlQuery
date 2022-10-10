using FluentSQL.Default;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQL.Models
{
    internal class ColumnsOrderBy
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
