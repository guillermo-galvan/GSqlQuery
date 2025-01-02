using GSqlQuery.Queries;
using System.Linq.Expressions;

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
        public DynamicQuery DynamicQuery { get; }

        /// <summary>
        /// Order by Type
        /// </summary>
        public OrderBy OrderBy { get; }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="columns">columns</param>
        /// <param name="orderBy">Order by Type</param>
        public ColumnsOrderBy(DynamicQuery dynamicQuery, OrderBy orderBy)
        {
            DynamicQuery = dynamicQuery;
            OrderBy = orderBy;
        }
    }

    internal sealed class ColumnsJoinOrderBy
    {
        /// <summary>
        /// Get columns
        /// </summary>
        public Expression Expression { get; }

        /// <summary>
        /// Order by Type
        /// </summary>
        public OrderBy OrderBy { get; }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="columns">columns</param>
        /// <param name="orderBy">Order by Type</param>
        public ColumnsJoinOrderBy(Expression expression, OrderBy orderBy)
        {
            Expression = expression;
            OrderBy = orderBy;
        }
    }
}