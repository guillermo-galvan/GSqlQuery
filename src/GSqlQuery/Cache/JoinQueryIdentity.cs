using GSqlQuery.Queries;
using GSqlQuery.SearchCriteria;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GSqlQuery.Cache
{
    internal class JoinCriteriaPartType(Type property, Expression expression)
    {
        private readonly ExpressionComparer _comparer = new ExpressionComparer();

        public Type Property { get; } = property;

        public Expression Expression { get; } = expression;

        public bool Equals(JoinCriteriaPartType other)
        {
            if (other == null)
            {
                return false;
            }

            return Property == other.Property && _comparer.Equals(Expression, other.Expression);
        }
    }

    internal class JoinCriteriaType(JoinCriteriaPartType leftPart, Queries.JoinCriteriaType joinCriteria, JoinCriteriaPartType rigthPart, string logicalOperator)
    {
        public JoinCriteriaPartType LeftPart { get; } = leftPart;

        public Queries.JoinCriteriaType JoinCriteria { get; } = joinCriteria;

        public JoinCriteriaPartType RigthPart { get; } = rigthPart;

        public string LogicalOperator { get; } = logicalOperator;

        public bool Equals(JoinCriteriaType other)
        {
            if (other == null)
            {
                return false;
            }

            return LeftPart.Equals(other.LeftPart) &&
                   JoinCriteria == other.JoinCriteria &&
                   RigthPart.Equals(other.RigthPart) &&
                   LogicalOperator == other.LogicalOperator;
        }
    }

    internal class JoinPropertiesType(Type entity, Type property, JoinType joinEnum, bool isMain, List<JoinCriteriaType> joinCriteriaTypes)
    {
        public Type Entity { get; } = entity;

        public Type Property { get; } = property;

        public JoinType JoinEnum { get; } = joinEnum;

        public bool IsMain { get; } = isMain;

        public List<JoinCriteriaType> JoinCriteriaTypes { get; } = joinCriteriaTypes ?? [];

        private bool JoinCriteriaTypesValid(JoinPropertiesType other)
        {
            if (JoinCriteriaTypes.Count != other.JoinCriteriaTypes.Count)
            {
                return false;
            }

            for (int i = 0; i < JoinCriteriaTypes.Count; i++)
            {
                if (!JoinCriteriaTypes[i].Equals(other.JoinCriteriaTypes[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public bool Equals(JoinPropertiesType other)
        {
            if (other == null)
            {
                return false;
            }

            return Entity == other.Entity &&
                   Property == other.Property &&
                   JoinEnum == other.JoinEnum &&
                   IsMain == other.IsMain &&
                   JoinCriteriaTypesValid(other);
        }
    }

    internal class JoinQueryIdentity : QueryIdentity
    {
        private readonly List<Type> _searchCriteriaTypes = [];

        public List<Type> SearchCriteriaTypes => _searchCriteriaTypes;

        private readonly List<JoinPropertiesType> _joinPropertiesType = [];

        public List<JoinPropertiesType> JoinPropertiesTypes => _joinPropertiesType;

        public JoinQueryIdentity(Type entity, QueryType queryType, Type queryOptions, Type format, List<JoinInfo> joinInfos, ISearchCriteriaBuilder searchCriteriaBuilder) : base(entity, queryType, queryOptions, format)
        {
            if (searchCriteriaBuilder != null)
            {
                foreach (ISearchCriteria item in searchCriteriaBuilder?.SearchCriterias ?? [])
                {
                    _searchCriteriaTypes.Add(item.GetType());
                }
            }

            foreach (JoinInfo item in joinInfos)
            {
                List<JoinCriteriaType> joinCriteriaTypes = [];

                foreach (JoinModel joinModel in item.Joins)
                {
                    JoinCriteriaPartType leftPart = new JoinCriteriaPartType(joinModel.JoinModel1.DynamicQuery?.Properties, joinModel.JoinModel1.Expression);

                    JoinCriteriaPartType rigthPart = new JoinCriteriaPartType(joinModel.JoinModel2.DynamicQuery?.Properties, joinModel.JoinModel2.Expression);

                    joinCriteriaTypes.Add(new JoinCriteriaType(leftPart, joinModel.JoinCriteria, rigthPart, joinModel.LogicalOperator));
                }


                JoinPropertiesType joinPropertiesType = new(item.ClassOptions.Type, item.DynamicQuery?.Properties, item.JoinEnum, item.IsMain, joinCriteriaTypes);

                _joinPropertiesType.Add(joinPropertiesType);
            }

            unchecked
            {
                _hashCode = 17;
                _hashCode = _hashCode * 23 + Entity.GetHashCode();
                _hashCode = _hashCode * 23 + QueryType.GetHashCode();
                _hashCode = _hashCode * 23 + QueryOptions.GetHashCode();
                _hashCode = _hashCode * 23 + Format.GetHashCode();
                foreach (Type type in _searchCriteriaTypes)
                {
                    _hashCode = _hashCode * 23 + type.GetHashCode();
                }
            }
        }

        private bool JoinPropertiesTypesValidation(JoinQueryIdentity other)
        {
            if (JoinPropertiesTypes.Count != other.JoinPropertiesTypes.Count)
            {
                return false;
            }

            for (int i = 0; i < JoinPropertiesTypes.Count; i++)
            {
                if (!JoinPropertiesTypes[i].Equals(other.JoinPropertiesTypes[i]))
                {
                    return false;
                }
            }

            return true;
        }

        private bool SearchCriteriaTypesValidation(JoinQueryIdentity other)
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
            if (other is JoinQueryIdentity joinQueryIdentity)
            {
                return EqualsBase(joinQueryIdentity)
                    && JoinPropertiesTypesValidation(joinQueryIdentity)
                    && SearchCriteriaTypesValidation(joinQueryIdentity);
            }

            return false;
        }
    }
}