using GSqlQuery.Extensions;
using System.Collections.Generic;
using System.Reflection;

namespace GSqlQuery.Queries
{
    /// <summary>
    /// Order By Query Builder
    /// </summary>
    internal interface IOrderByQueryBuilder
    {
        /// <summary>
        /// Add Columns
        /// </summary>
        /// <param name="selectMember">Name of properties to search</param>
        /// <param name="orderBy">Order by Type</param>
        void AddOrderBy(ClassOptionsTupla<IEnumerable<MemberInfo>> selectMember, OrderBy orderBy);
    }
}
