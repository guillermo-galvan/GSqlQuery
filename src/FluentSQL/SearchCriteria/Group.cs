using FluentSQL.Models;

namespace FluentSQL.SearchCriteria
{
    /// <summary>
    /// Represents the search criteria group ()
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    internal class Group<T, TReturn> : Criteria, ISearchCriteria, IWhere<T, TReturn>, IAndOr<T, TReturn> where T : class, new() where TReturn : IQuery
    {
        private readonly List<ISearchCriteria> _searchCriterias = new();
        private readonly IAndOr<T, TReturn> _andOr;

        /// <summary>
        /// Get IAndOr
        /// </summary>
        public IAndOr<T, TReturn> AndOr => _andOr;

        /// <summary>
        /// Initializes a new instance of the Group class.
        /// </summary>
        /// <param name="table">Table Attribute</param>
        /// <param name="logicalOperator">Logical operator</param>
        /// <param name="andOr">IAndOr</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Group(TableAttribute table, string? logicalOperator, IAndOr<T, TReturn> andOr) : base(table, new ColumnAttribute("Group"), logicalOperator)
        {
            _andOr = andOr ?? throw new ArgumentNullException(nameof(andOr));
        }

        /// <summary>
        /// Get Criteria detail
        /// </summary>
        /// <param name="statements">Statements</param>
        /// <returns>Details of the criteria</returns>
        public override CriteriaDetail GetCriteria(IStatements statements, IEnumerable<PropertyOptions> propertyOptions)
        {
            string criterion = string.Empty;
            List<CriteriaDetail> criterias = new();
            List<ParameterDetail> parameters = new();

            foreach (var item in _searchCriterias)
            {
                criterias.Add(item.GetCriteria(statements, propertyOptions));
            }

            criterion = string.IsNullOrEmpty(LogicalOperator) ? $"({string.Join(" ", criterias.Select(x => x.QueryPart))})" :
                $"{LogicalOperator} ({string.Join(" ", criterias.Select(x => x.QueryPart))})";

            criterias.ForEach(x => parameters.AddRange(x.ParameterDetails));

            return new CriteriaDetail(this, criterion, parameters);
        }

        /// <summary>
        /// Add a search criteria
        /// </summary>
        /// <param name="criteria"></param>
        void ISearchCriteriaBuilder.Add(ISearchCriteria criteria)
        {
            _searchCriterias.Add(criteria);
        }

        IEnumerable<CriteriaDetail> ISearchCriteriaBuilder.BuildCriteria(IStatements statements)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Build Query
        /// </summary>
        /// <returns>Instance of IQuery</returns>
        public TReturn Build()
        {
            return _andOr.Build();
        }
    }

    internal class Group<T, TReturn, TDbConnection, TResult> : Criteria, ISearchCriteria, IAndOr<T, TReturn, TDbConnection, TResult>,
        IWhere<T, TReturn, TDbConnection, TResult> where T : class, new() where TReturn : IQuery
    {
        private readonly List<ISearchCriteria> _searchCriterias = new();
        private readonly IAndOr<T, TReturn, TDbConnection, TResult> _andOr;

        public IAndOr<T, TReturn, TDbConnection, TResult> AndOr => _andOr;

        public Group(TableAttribute table, string? logicalOperator, IAndOr<T, TReturn, TDbConnection, TResult> andOr) : 
            base(table, new ColumnAttribute("Group"), logicalOperator)
        {
            _andOr = andOr ?? throw new ArgumentNullException(nameof(andOr));
        }

        public override CriteriaDetail GetCriteria(IStatements statements, IEnumerable<PropertyOptions> propertyOptions)
        {
            string criterion = string.Empty;
            List<CriteriaDetail> criterias = new();
            List<ParameterDetail> parameters = new();

            foreach (var item in _searchCriterias)
            {
                criterias.Add(item.GetCriteria(statements, propertyOptions));
            }

            criterion = string.IsNullOrEmpty(LogicalOperator) ? $"({string.Join(" ", criterias.Select(x => x.QueryPart))})" :
                $"{LogicalOperator} ({string.Join(" ", criterias.Select(x => x.QueryPart))})";

            criterias.ForEach(x => parameters.AddRange(x.ParameterDetails));

            return new CriteriaDetail(this, criterion, parameters);
        }

        void ISearchCriteriaBuilder.Add(ISearchCriteria criteria)
        {
            _searchCriterias.Add(criteria);
        }

        IEnumerable<CriteriaDetail> ISearchCriteriaBuilder.BuildCriteria(IStatements statements)
        {
            throw new NotImplementedException();
        }

        public TReturn Build()
        {
            return _andOr.Build();
        }
    }
}
