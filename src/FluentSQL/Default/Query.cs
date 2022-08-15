namespace FluentSQL.Default
{
    /// <summary>
    /// Query
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    internal class Query<T> : IQuery<T> where T : class, new()
    {
        private readonly string _text;
        private readonly IEnumerable<ColumnAttribute> _columns;
        private readonly IEnumerable<CriteriaDetail>? _criteria;
        private readonly IStatements _statements;

        /// <summary>
        /// Columns of the query
        /// </summary>
        public IEnumerable<ColumnAttribute> Columns => _columns;

        /// <summary>
        /// Query criteria
        /// </summary>
        public IEnumerable<CriteriaDetail>? Criteria => _criteria;

        /// <summary>
        /// Statements to use in the query
        /// </summary>
        public IStatements Statements => _statements;

        /// <summary>
        /// The Query
        /// </summary>
        public string Text => _text;

        /// <summary>
        /// Create IQuery object 
        /// </summary>
        /// <param name="columns">Columns of the query</param>
        /// <param name="criteria">Query criteria</param>
        /// <param name="statements">Statements to use in the query</param>
        /// <param name="text">The Query</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Query(IEnumerable<ColumnAttribute> columns, IEnumerable<CriteriaDetail>? criteria, IStatements statements, string text)
        {
            _columns = columns ?? throw new ArgumentNullException(nameof(columns));            
            _statements = statements ?? throw new ArgumentNullException(nameof(statements));
            _text = text ?? throw new ArgumentNullException(nameof(text));
            _criteria = criteria;
        }
    }
}
