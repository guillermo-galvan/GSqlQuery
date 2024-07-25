using System;
using System.Linq.Expressions;

namespace GSqlQuery
{
    internal class QueryIdentity : IEquatable<QueryIdentity>
    {
        protected readonly int _hashCode;

        public QueryType QueryType { get; }

        public Type Entity { get; }

        public Type Format { get; }

        public Type Properties { get; }

        public Type AndOr { get; }

        public QueryIdentity(Type entity, QueryType queryType, Type format,  Type properties,  Type andOr)
        {
            QueryType = queryType; 
            Entity = entity ?? throw new ArgumentNullException(nameof(entity));
            Format = format ?? throw new ArgumentNullException(nameof(format));
            Properties = properties;
            AndOr = andOr;

            unchecked
            {
                _hashCode = 17;
                _hashCode = (_hashCode * 23) + Entity.GetHashCode();
                _hashCode = (_hashCode * 23) + QueryType.GetHashCode();
                _hashCode = (_hashCode * 23) + Format.GetHashCode();
                _hashCode = (_hashCode * 23) + (Properties?.GetHashCode() ?? 0);
                _hashCode = (_hashCode * 23) + (AndOr?.GetHashCode() ?? 0);
            }
        }

        protected virtual bool ExpresionValidation(QueryIdentity other)
        {
            if (Properties == null && other.Properties == null) 
            {
                return true;
            }

            if (Properties != null && other.Properties != null && Properties == other.Properties) 
            {
                return true;
            }

            return false;
        }

        protected virtual bool ObjectValidation(object source, object validate)
        {
            if (source == null && validate == null)
            {
                return true;
            }

            if (source != null && validate != null && source.Equals(validate))
            {
                return true;
            }

            return false;
        }

        public bool Equals(QueryIdentity other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;

            return QueryType == other.QueryType
                && Entity == other.Entity
                && Format == other.Format
                && ExpresionValidation(other)
                && ObjectValidation(AndOr, other.AndOr);
        }

        public override int GetHashCode()
        {
            return _hashCode;
        }

        public override bool Equals(object obj) => Equals(obj as QueryIdentity);
    }
}
