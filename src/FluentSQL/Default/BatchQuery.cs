using FluentSQL.Extensions;
using System.Data;

namespace FluentSQL.Default
{
    internal class BatchQuery : QueryBase
    {
        private IEnumerable<IDataParameter> _parameters;

        internal IEnumerable<IDataParameter> Parameters => _parameters;

        public BatchQuery(string text, IEnumerable<ColumnAttribute> columns, IEnumerable<CriteriaDetail>? criteria, IStatements statements,
            IEnumerable<IDataParameter> parameters) 
            : base(text, columns, criteria, statements)
        {
            _parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
        }
    }
}
