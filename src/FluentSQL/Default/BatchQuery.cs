using FluentSQL.Extensions;
using System.Data;

namespace FluentSQL.Default
{
    internal class BatchQuery : QueryBase
    {
        private IEnumerable<IDataParameter> _parameters;

        internal IEnumerable<IDataParameter> Parameters => _parameters;

        public BatchQuery(string text, IEnumerable<ColumnAttribute> columns, IEnumerable<CriteriaDetail>? criteria, IEnumerable<IDataParameter> parameters) 
            : base(text, columns, criteria)
        {
            _parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
        }
    }
}
