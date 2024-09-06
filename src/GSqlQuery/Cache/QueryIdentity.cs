using GSqlQuery.SearchCriteria;
using System;
using System.Collections.Generic;

namespace GSqlQuery
{
    internal class QueryIdentity : IEquatable<QueryIdentity>
    {
        protected readonly int _hashCode;
        protected List<Type> _searchCriteriaTypes = [];

        public QueryType QueryType { get; }

        public Type Entity { get; }

        public Type Format { get; }

        public Type Properties { get; }

        public List<Type> SearchCriteriaTypes => _searchCriteriaTypes;

        public QueryIdentity(Type entity, QueryType queryType, Type format, Type properties, ISearchCriteriaBuilder searchCriteriaBuilder)
        {
            QueryType = queryType;
            Entity = entity ?? throw new ArgumentNullException(nameof(entity));
            Format = format ?? throw new ArgumentNullException(nameof(format));
            Properties = properties;

            if(searchCriteriaBuilder != null)
            {
                foreach (ISearchCriteria item in searchCriteriaBuilder?.SearchCriterias ?? [])
                {
                    _searchCriteriaTypes.Add(item.GetType());
                }
            }

            unchecked
            {
                _hashCode = 17;
                _hashCode = (_hashCode * 23) + Entity.GetHashCode();
                _hashCode = (_hashCode * 23) + QueryType.GetHashCode();
                _hashCode = (_hashCode * 23) + Format.GetHashCode();
                _hashCode = (_hashCode * 23) + (Properties?.GetHashCode() ?? 0);
                foreach (var type in _searchCriteriaTypes)
                {
                    _hashCode = (_hashCode * 23) + type.GetHashCode();
                }
            }
        }

        protected virtual bool PropertiesValidation(QueryIdentity other)
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

        protected virtual bool SearchCriteriaTypesValidation(QueryIdentity other) 
        {
            if(_searchCriteriaTypes.Count != other._searchCriteriaTypes.Count)
            {
                return false;
            }

            for (int i = 0; i < _searchCriteriaTypes.Count; i++)
            {
                if (_searchCriteriaTypes[i] != other._searchCriteriaTypes[i])
                {
                    return false;
                }
            }

            return true;
        }

        public bool Equals(QueryIdentity other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;

            return QueryType == other.QueryType
                && Entity == other.Entity
                && Format == other.Format
                && PropertiesValidation(other) 
                && SearchCriteriaTypesValidation(other);
        }

        public override int GetHashCode()
        {
            return _hashCode;
        }

        public override bool Equals(object obj) => Equals(obj as QueryIdentity);
    }
}