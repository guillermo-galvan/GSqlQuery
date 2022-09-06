using FluentSQL.Extensions;

namespace FluentSQL.Default
{
    public abstract class QueryBase : IQuery
    {
        private readonly IEnumerable<ColumnAttribute> _columns;
        private readonly IEnumerable<CriteriaDetail>? _criteria;
        private readonly IStatements _statements;
        private string _text;
        

        public string Text { get => _text; set => _text = value; }

        public IEnumerable<ColumnAttribute> Columns => _columns;

        public IEnumerable<CriteriaDetail>? Criteria => _criteria;

        public IStatements Statements => _statements;

        public QueryBase(string text, IEnumerable<ColumnAttribute> columns, IEnumerable<CriteriaDetail>? criteria, IStatements statements)
        {
            _columns = columns ?? throw new ArgumentNullException(nameof(columns));
            _statements = statements ?? throw new ArgumentNullException(nameof(statements));
            text.NullValidate("", nameof(text));
            _text = text;
            _criteria = criteria;
        }
    }
}
