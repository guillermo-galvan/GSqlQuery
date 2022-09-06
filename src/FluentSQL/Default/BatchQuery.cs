using FluentSQL.Extensions;
using System.Data;

namespace FluentSQL.Default
{
    internal class BatchQuery : QueryBase, ISetDatabaseManagement<int>
    {
        private IEnumerable<IDataParameter> _parameters;

        internal IEnumerable<IDataParameter> Parameters => _parameters;

        public BatchQuery(string text, IEnumerable<ColumnAttribute> columns, IEnumerable<CriteriaDetail>? criteria, IStatements statements,
            IEnumerable<IDataParameter> parameters) 
            : base(text, columns, criteria, statements)
        {
            _parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
        }

        public IExecute<int, TDbConnection> SetDatabaseManagement<TDbConnection>(IDatabaseManagement<TDbConnection> databaseManagment)
        {
            databaseManagment.NullValidate(ErrorMessages.ParameterNotNull, nameof(databaseManagment));
            return new BatchQueryExecute<TDbConnection>(databaseManagment, this);
        }
    }
}
