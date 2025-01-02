using GSqlQuery.Queries;
using GSqlQuery.SearchCriteria;
using System;
using System.Collections.Generic;

namespace GSqlQuery.Cache
{
    internal class CountIdentify : QueryIdentity
    {
        private readonly List<Type> _searchCriteriaTypes = [];

        public Type IQueryBuilderType { get; }

        public Type IAndOrType { get; }

        public List<Type> SearchCriteriaTypes => _searchCriteriaTypes;

        public Type PropertiesColumns { get; }

        public CountIdentify(Type entity, QueryType queryType, Type queryOptions, Type format, Type queryBuilderType, Type iAndOrType, ISearchCriteriaBuilder searchCriteriaBuilder,
            IDynamicColumns dynamicColumns) : base(entity, queryType, queryOptions, format)
        {
            IQueryBuilderType = queryBuilderType;
            IAndOrType = iAndOrType;
            PropertiesColumns = dynamicColumns?.DynamicQuery?.Properties;

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
                _hashCode = _hashCode * 23 + QueryOptions.GetHashCode();
                _hashCode = _hashCode * 23 + Format.GetHashCode();
                _hashCode = _hashCode * 23 + (IQueryBuilderType?.GetHashCode() ?? 0);
                _hashCode = _hashCode * 23 + (IAndOrType?.GetHashCode() ?? 0);
                _hashCode = _hashCode * 23 + (PropertiesColumns?.GetHashCode() ?? 0);
                foreach (Type type in _searchCriteriaTypes)
                {
                    _hashCode = _hashCode * 23 + type.GetHashCode();
                }
            }
        }

        private bool SearchCriteriaTypesValidation(CountIdentify other)
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

        private bool PropertiesColumnsValidation(CountIdentify other)
        {
            if (PropertiesColumns == null && other.PropertiesColumns == null)
            {
                return true;
            }

            if (PropertiesColumns != null && other.PropertiesColumns != null && PropertiesColumns == other.PropertiesColumns)
            {
                return true;
            }

            return false;
        }

        public override bool Equals(QueryIdentity other)
        {
            if (other is CountIdentify countIdentify)
            {
                return EqualsBase(countIdentify) &&
                 IQueryBuilderType == countIdentify.IQueryBuilderType &&
                 IAndOrType == countIdentify.IAndOrType &&
                 SearchCriteriaTypesValidation(countIdentify) &&
                 PropertiesColumnsValidation(countIdentify);
            }

            return false;
        }
    }
}