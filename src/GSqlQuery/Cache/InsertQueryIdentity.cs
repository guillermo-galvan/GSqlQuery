using System;

namespace GSqlQuery.Cache
{
    internal class InsertQueryIdentity : QueryIdentity
    {
        public InsertQueryIdentity(Type entity, Type format) : base(entity, QueryType.Create, format)
        {
            unchecked
            {
                _hashCode = 17;
                _hashCode = _hashCode * 23 + Entity.GetHashCode();
                _hashCode = _hashCode * 23 + QueryType.GetHashCode();
                _hashCode = _hashCode * 23 + Format.GetHashCode();
            }
        }

        public override bool Equals(QueryIdentity other)
        {
            if (other is InsertQueryIdentity deleteQueryIdentity)
            {
                if (ReferenceEquals(this, deleteQueryIdentity)) return true;
                if (other is null) return false;

                return QueryType == deleteQueryIdentity.QueryType
                    && Entity == deleteQueryIdentity.Entity
                    && Format == deleteQueryIdentity.Format;
            }

            return false;
        }
    }
}
