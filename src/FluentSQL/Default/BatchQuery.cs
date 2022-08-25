using FluentSQL.Models;
using System.Data;
using System.Data.Common;

namespace FluentSQL.Default
{
    internal class BatchQuery : Query
    {
        private IEnumerable<IDataParameter> _parameters;

        public BatchQuery(string text, IEnumerable<ColumnAttribute> columns, IEnumerable<CriteriaDetail>? criteria, ConnectionOptions connectionOptions,
            IEnumerable<IDataParameter> parameters) 
            : base(text, columns, criteria, connectionOptions)
        {
            _parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
        }

        public override object? Exec()
        {
            return ConnectionOptions.DatabaseManagment.ExecuteNonQuery(this, _parameters);
        }

        public override object? Exec(DbConnection connection)
        {
            return ConnectionOptions.DatabaseManagment.ExecuteNonQuery(connection,this, _parameters);
        }
    }
}
