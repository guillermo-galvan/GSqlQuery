namespace FluentSQL.SearchCriteria
{
    /// <summary>
    /// Represents the search criteria group ()
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    internal class Group<T> : Criteria, ISearchCriteria, IWhere<T>, IAndOr<T> where T : class, new()
    {
        private List<ISearchCriteria> _searchCriterias = new();
        private readonly IAndOr<T> _andOr;

        /// <summary>
        /// Get IAndOr
        /// </summary>
        public IAndOr<T> AndOr => _andOr;

        /// <summary>
        /// Initializes a new instance of the Group class.
        /// </summary>
        /// <param name="table">Table Attribute</param>
        /// <param name="logicalOperator">Logical operator</param>
        /// <param name="andOr">IAndOr</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Group(TableAttribute table, string? logicalOperator, IAndOr<T> andOr) : base(table, new ColumnAttribute("Group"), logicalOperator)
        {
            _andOr = andOr ?? throw new ArgumentNullException(nameof(andOr));
        }

        /// <summary>
        /// Get Criteria detail
        /// </summary>
        /// <param name="statements">Statements</param>
        /// <returns>Details of the criteria</returns>
        public override CriteriaDetail GetCriteria(IStatements statements)
        {
            string criterion = string.Empty;
            List<CriteriaDetail> criterias = new();
            List<ParameterDetail> parameters = new();

            foreach (var item in _searchCriterias)
            {
                criterias.Add(item.GetCriteria(statements));
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

        IEnumerable<CriteriaDetail> ISearchCriteriaBuilder.BuildCriteria()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Build Query
        /// </summary>
        /// <returns>Instance of IQuery</returns>
        public IQuery<T> Build()
        {
            return _andOr.Build();
        }
    }
}
