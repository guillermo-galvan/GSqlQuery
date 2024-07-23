using System;

namespace GSqlQuery
{
    internal class QueryIdentity : IEquatable<QueryIdentity>
    {
        protected readonly int _hashCode;

        public PropertyOptionsCollection Columns { get; }

        public QueryType QueryType { get; }

        public Type Entity { get; }

        public Type Format { get; set; }

        public QueryIdentity(Type entity, QueryType queryType, PropertyOptionsCollection columns, Type format)
        {
            QueryType = queryType; 
            Entity = entity ?? throw new ArgumentNullException(nameof(entity)); ;
            Columns = columns ?? throw new ArgumentNullException(nameof(columns));
            Format = format ?? throw new ArgumentNullException(nameof(format));

            unchecked
            {
                _hashCode = 17;
                _hashCode = (_hashCode * 23) + Entity.GetHashCode();
                _hashCode = (_hashCode * 23) + Columns.GetHashCode();
                _hashCode = (_hashCode * 23) + QueryType.GetHashCode();
                _hashCode = (_hashCode * 23) + Format.GetHashCode();
            }
        }

        public bool Equals(QueryIdentity other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;

            return QueryType == other.QueryType
                && Entity == other.Entity
                && Format == other.Format
                && Columns.Equals(other.Columns);
        }

        public override int GetHashCode()
        {
            return _hashCode;
        }

        public override bool Equals(object obj) => Equals(obj as QueryIdentity);
    }
}
