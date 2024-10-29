using GSqlQuery.SearchCriteria;
using System;
using System.Collections.Generic;

namespace GSqlQuery.Cache
{
    internal class DeleteQueryIdentity : QueryIdentity
    {
        private readonly List<Type> _searchCriteriaTypes = [];

        public bool IsEntityValue { get; }

        public List<Type> SearchCriteriaTypes => _searchCriteriaTypes;

        public DeleteQueryIdentity(Type entity, Type format, bool isEntityValue, ISearchCriteriaBuilder searchCriteriaBuilder) : base(entity, QueryType.Delete, format)
        {
            IsEntityValue = isEntityValue;

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
                _hashCode = _hashCode * 23 + IsEntityValue.GetHashCode();
                foreach (var type in _searchCriteriaTypes)
                {
                    _hashCode = _hashCode * 23 + type.GetHashCode();
                }
            }
        }

        private bool SearchCriteriaTypesValidation(DeleteQueryIdentity other)
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
            if (other is DeleteQueryIdentity deleteQueryIdentity)
            {
                if (ReferenceEquals(this, deleteQueryIdentity)) return true;
                if (other is null) return false;

                return QueryType == deleteQueryIdentity.QueryType
                    && Entity == deleteQueryIdentity.Entity
                    && Format == deleteQueryIdentity.Format
                    && IsEntityValue == deleteQueryIdentity.IsEntityValue
                    && SearchCriteriaTypesValidation(deleteQueryIdentity);
            }

            return false;
        }
    }
}
