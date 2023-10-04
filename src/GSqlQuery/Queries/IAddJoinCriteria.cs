using GSqlQuery.Queries;
using System.Collections.Generic;

namespace GSqlQuery
{
    /// <summary>
    /// IAddJoinCriteria
    /// </summary>
    /// <typeparam name="T">Type to create the query</typeparam>
    internal interface IAddJoinCriteria<T>
    {
        /// <summary>
        /// Get JoinInfo
        /// </summary>
        IEnumerable<JoinInfo> JoinInfos { get; }

        /// <summary>
        /// Add a column
        /// </summary>
        /// <param name="joinModel">Join model</param>
        void AddColumns(T joinModel);
    }
}