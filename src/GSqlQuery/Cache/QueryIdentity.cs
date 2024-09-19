using System;

namespace GSqlQuery.Cache
{
    internal abstract class QueryIdentity(Type entity, QueryType queryType, Type format) : IEquatable<QueryIdentity>
    {
        protected int _hashCode;

        public QueryType QueryType { get; } = queryType;

        public Type Entity { get; } = entity ?? throw new ArgumentNullException(nameof(entity));

        public Type Format { get; } = format ?? throw new ArgumentNullException(nameof(format));

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
    }
}