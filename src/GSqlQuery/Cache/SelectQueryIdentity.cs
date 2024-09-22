using GSqlQuery.SearchCriteria;
using System;
using System.Collections.Generic;

namespace GSqlQuery.Cache
{
    internal sealed class SelectQueryIdentity : QueryIdentity
    {
        private readonly List<Type> _searchCriteriaTypes = [];

        public List<Type> SearchCriteriaTypes => _searchCriteriaTypes;

        public Type Properties { get; }

        public SelectQueryIdentity(Type entity, Type format, Type properties, ISearchCriteriaBuilder searchCriteriaBuilder) : base(entity, QueryType.Read, format)
        {
            Properties = properties;

            if (searchCriteriaBuilder != null)
            {
                foreach (ISearchCriteria item in searchCriteriaBuilder?.SearchCriterias ?? [])
                {
                    _searchCriteriaTypes.Add(item.GetType());
                }
            }

            unchecked
            {
                _hashCode = 17;
                _hashCode = _hashCode * 23 + Entity.GetHashCode();
                _hashCode = _hashCode * 23 + QueryType.GetHashCode();
                _hashCode = _hashCode * 23 + Format.GetHashCode();
                _hashCode = _hashCode * 23 + (Properties?.GetHashCode() ?? 0);
                foreach (var type in _searchCriteriaTypes)
                {
                    _hashCode = _hashCode * 23 + type.GetHashCode();
                }
            }
        }

        private bool PropertiesValidation(SelectQueryIdentity other)
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

        private bool SearchCriteriaTypesValidation(SelectQueryIdentity other)
        {
            if (_searchCriteriaTypes.Count != other.SearchCriteriaTypes.Count)
            {
                return false;
            }

            for (int i = 0; i < _searchCriteriaTypes.Count; i++)
            {
                if (_searchCriteriaTypes[i] != other.SearchCriteriaTypes[i])
                {
                    return false;
                }
            }

            return true;
        }

        public override bool Equals(QueryIdentity other)
        {
            if (other is SelectQueryIdentity selectQueryIdentity)
            {
                if (ReferenceEquals(this, selectQueryIdentity)) return true;
                if (other is null) return false;

                return QueryType == selectQueryIdentity.QueryType
                    && Entity == selectQueryIdentity.Entity
                    && Format == selectQueryIdentity.Format
                    && PropertiesValidation(selectQueryIdentity)
                    && SearchCriteriaTypesValidation(selectQueryIdentity);
            }

            return false;
        }
    }
}
