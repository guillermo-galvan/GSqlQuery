using GSqlQuery.Queries;
using GSqlQuery.SearchCriteria;
using System;
using System.Collections.Generic;

namespace GSqlQuery.Cache
{
    internal class OrderByPropertiesType(Type property, OrderBy orderBy)
    {
        public Type Property { get; } = property;

        public OrderBy OrderBy { get; } = orderBy;
    }

    internal sealed class OrderByQueryIdentity : QueryIdentity
    {
        private readonly List<Type> _searchCriteriaTypes = [];
        private readonly List<OrderByPropertiesType> _properties = [];

        public Type IQueryBuilderType { get; }

        public Type IAndOrType { get; }

        public List<Type> SearchCriteriaTypes => _searchCriteriaTypes;

        public List<OrderByPropertiesType> PropertiesTypes => _properties;

        public Type PropertiesColumns { get; }

        // agrega properties select
        public OrderByQueryIdentity(Type entity, QueryType queryType, Type queryOptions, Type format, Type queryBuilderType, Type iAndOrType, ISearchCriteriaBuilder searchCriteriaBuilder, List<ColumnsOrderBy> columnsOrders, IDynamicColumns dynamicColumns) : base(entity, queryType, queryOptions, format)
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

            foreach (ColumnsOrderBy item in columnsOrders)
            {
                _properties.Add(new OrderByPropertiesType(item.DynamicQuery.Properties, item.OrderBy));
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

                foreach (OrderByPropertiesType item in _properties)
                {
                    _hashCode = _hashCode * 23 + item.Property.GetHashCode();
                    _hashCode = _hashCode * 23 + item.OrderBy.GetHashCode();
                }
            }
        }

        private bool SearchCriteriaTypesValidation(OrderByQueryIdentity other)
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

        private bool PropertiesTypesValidation(OrderByQueryIdentity other)
        {
            if (PropertiesTypes.Count != other.PropertiesTypes.Count)
            {
                return false;
            }

            for (int i = 0; i < PropertiesTypes.Count; i++)
            {
                if (PropertiesTypes[i].Property != other.PropertiesTypes[i].Property ||
                    PropertiesTypes[i].OrderBy != other.PropertiesTypes[i].OrderBy)
                {
                    return false;
                }
            }

            return true;
        }

        private bool PropertiesColumnsValidation(OrderByQueryIdentity other)
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
            if (other is OrderByQueryIdentity orderByQueryIdentity)
            {
                return EqualsBase(orderByQueryIdentity) &&
                 IQueryBuilderType == orderByQueryIdentity.IQueryBuilderType &&
                 IAndOrType == orderByQueryIdentity.IAndOrType &&
                 SearchCriteriaTypesValidation(orderByQueryIdentity) &&
                 PropertiesTypesValidation(orderByQueryIdentity) &&
                 PropertiesColumnsValidation(orderByQueryIdentity);
            }

            return false;
        }
    }
}