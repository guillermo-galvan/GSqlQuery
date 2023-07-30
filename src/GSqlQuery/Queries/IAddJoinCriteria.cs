using GSqlQuery.Queries;
using System.Collections.Generic;

namespace GSqlQuery
{
    internal interface IAddJoinCriteria<T>
    {
        IEnumerable<JoinInfo> JoinInfos { get; }

        void AddColumns(T joinModel);
    }
}