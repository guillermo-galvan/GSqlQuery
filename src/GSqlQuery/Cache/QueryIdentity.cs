using System;

namespace GSqlQuery.Cache
{
    internal abstract class QueryIdentity(Type entity, QueryType queryType, Type queryOptions, Type format) : IEquatable<QueryIdentity>
    {
        protected int _hashCode;

        public QueryType QueryType { get; } = queryType;

        public Type Entity { get; } = entity ?? throw new ArgumentNullException(nameof(entity));

        public Type Format { get; } = format ?? throw new ArgumentNullException(nameof(format));

        public Type QueryOptions { get; } = queryOptions ?? throw new ArgumentNullException(nameof(queryOptions));

        public override int GetHashCode()
        {
            return _hashCode;
        }

        public abstract bool Equals(QueryIdentity other);

        public override bool Equals(object obj)
        {
            if (obj is QueryIdentity queryIdentity)
            {
                return Equals(queryIdentity);
            }

            return false;
        }

        protected bool EqualsBase(QueryIdentity other)
        {
            return Entity == other.Entity
                && QueryType == other.QueryType
                && QueryOptions == other.QueryOptions
                && Format == other.Format;
        }
    }
}