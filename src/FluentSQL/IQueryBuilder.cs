using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQL
{
    /// <summary>
    /// Query Builder
    /// </summary>
    public interface IQueryBuilder
    {
        /// <summary>
        /// Get Query
        /// </summary>
        string Text { get; }

        /// <summary>
        /// Get Columns of the query
        /// </summary>
        IEnumerable<ColumnAttribute> Columns { get; }
    }
}
