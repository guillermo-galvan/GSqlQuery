using System;

namespace GSqlQuery
{
    internal class ColumnIdentity : IEquatable<ColumnIdentity>
    {
        private readonly int _hashCode;

        public QueryType QueryType { get; }

        public Type Type { get; }

        internal ColumnIdentity(Type type, QueryType queryType)
        {
            QueryType = queryType;
            Type = type;

            unchecked
            {
                _hashCode = 17;
                _hashCode = (_hashCode * 23) + Type.GetHashCode();
                _hashCode = (_hashCode * 23) + QueryType.GetHashCode();
            }
        }

        public override int GetHashCode()
        {
            return _hashCode;
        }

        public override bool Equals(object obj) => Equals(obj as ColumnIdentity);

        public bool Equals(ColumnIdentity other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;

            return QueryType == other.QueryType && Type == other.Type;
        }
    }
}
