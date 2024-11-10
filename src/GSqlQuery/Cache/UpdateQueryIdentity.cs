using GSqlQuery.SearchCriteria;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GSqlQuery.Cache
{
    internal class UpdateQueryIdentity : QueryIdentity
    {
        private readonly ExpressionComparer _comparer = new ExpressionComparer();

        private readonly List<Type> _searchCriteriaTypes = [];

        public List<Expression> Expressions { get; }

        public List<Type> SearchCriteriaTypes => _searchCriteriaTypes;

        public UpdateQueryIdentity(Type entity, Type format, IEnumerable<Expression> expressions, ISearchCriteriaBuilder searchCriteriaBuilder) : base(entity, QueryType.Update, format)
        {
            Expressions = new List<Expression>(expressions);

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

                foreach (Type type in _searchCriteriaTypes)
                {
                    _hashCode = _hashCode * 23 + type.GetHashCode();
                }
            }
        }

        private bool SearchCriteriaTypesValidation(UpdateQueryIdentity other)
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

        private bool ExpressionsValidation(UpdateQueryIdentity other)
        {
            if (Expressions.Count != other.Expressions.Count)
            {
                return false;
            }

            for (int i = 0; i < Expressions.Count; i++)
            {
                if (!_comparer.Equals(Expressions[i], other.Expressions[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public override bool Equals(QueryIdentity other)
        {
            if (other is UpdateQueryIdentity updateQueryIdentity)
            {
                return Entity == updateQueryIdentity.Entity
                    && QueryType == updateQueryIdentity.QueryType
                    && Format == updateQueryIdentity.Format
                    && SearchCriteriaTypesValidation(updateQueryIdentity)
                    && ExpressionsValidation(updateQueryIdentity);
            }

            return false;
        }
    }
}
