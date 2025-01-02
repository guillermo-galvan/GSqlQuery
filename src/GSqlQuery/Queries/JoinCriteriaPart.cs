using System.Collections.Generic;
using System.Linq.Expressions;

namespace GSqlQuery.Queries
{
    /// <summary>
    /// Join Criteria Part
    /// </summary>
    internal class JoinCriteriaPart(DynamicQuery dynamicQuery, Expression expression)
    {
        public DynamicQuery DynamicQuery { get; set; } = dynamicQuery;

        public Expression Expression { get; set; } = expression;
    }
}