using GSqlQuery.SearchCriteria;
using System;

namespace GSqlQuery.Cache
{
    internal class OrderByQueryIdentity : QueryIdentity
    {
        public OrderByQueryIdentity(Type entity, QueryType queryType, Type format, Type properties, ISearchCriteriaBuilder searchCriteriaBuilder) : base(entity, queryType, format)
        {
        }

        public override bool Equals(QueryIdentity other)
        {
            throw new NotImplementedException();
        }
    }
}