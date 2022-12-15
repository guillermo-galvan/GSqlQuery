using GSqlQuery.SearchCriteria;

namespace GSqlQuery.Runner
{
    internal class Group<T, TReturn, TDbConnection, TResult> : Criteria, ISearchCriteria, IAndOr<T, TReturn>,
        IWhere<T, TReturn> where T : class, new() where TReturn : IQuery
    {
        private readonly Queue<ISearchCriteria> _searchCriterias = new();
        private readonly IAndOr<T, TReturn> _andOr;

        public IAndOr<T, TReturn> AndOr => _andOr;

        public Group(TableAttribute table, string? logicalOperator, IAndOr<T, TReturn> andOr) :
            base(table, new ColumnAttribute("Group"), logicalOperator)
        {
            _andOr = andOr ?? throw new ArgumentNullException(nameof(andOr));
        }

        public override CriteriaDetail GetCriteria(IStatements statements, IEnumerable<PropertyOptions> propertyOptions)
        {
            string criterion = string.Empty;
            Queue<CriteriaDetail> criterias = new();
            Queue<ParameterDetail> parameters = new();

            foreach (var item in _searchCriterias)
            {
                criterias.Enqueue(item.GetCriteria(statements, propertyOptions));
            }

            criterion = string.IsNullOrEmpty(LogicalOperator) ? $"({string.Join(" ", criterias.Select(x => x.QueryPart))})" :
                $"{LogicalOperator} ({string.Join(" ", criterias.Select(x => x.QueryPart))})";

            foreach (var cri in criterias)
            {
                foreach (var item in cri.ParameterDetails)
                {
                    parameters.Enqueue(item);
                }
            }

            return new CriteriaDetail(this, criterion, parameters);
        }

        public TReturn Build()
        {
            return _andOr.Build();
        }

        public void Add(ISearchCriteria criteria)
        {
            _searchCriterias.Enqueue(criteria);
        }

        public IEnumerable<CriteriaDetail> BuildCriteria(IStatements statements)
        {
            throw new NotImplementedException();
        }
    }
}
