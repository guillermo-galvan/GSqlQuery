using System.Collections.Generic;

namespace GSqlQuery
{
    /// <summary>
    /// Columns for 'Order By' query
    /// </summary>
    internal sealed class ColumnsOrderBy
    {
        /// <summary>
        /// Get columns
        /// </summary>
        public IEnumerable<PropertyOptions> Columns { get; }

        /// <summary>
        /// Order by Type
        /// </summary>
        public OrderBy OrderBy { get; }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="columns">columns</param>
        /// <param name="orderBy">Order by Type</param>
        public ColumnsOrderBy(IEnumerable<PropertyOptions> columns, OrderBy orderBy)
        {
            Columns = columns;
            OrderBy = orderBy;
        }
    }
}