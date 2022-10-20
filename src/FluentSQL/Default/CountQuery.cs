using FluentSQL.Extensions;
using FluentSQL.Models;

namespace FluentSQL.Default
{
    public class CountQuery<T> : Query<T> where T : class, new()
    {
        public CountQuery(string text, IEnumerable<ColumnAttribute> columns, IEnumerable<CriteriaDetail>? criteria, IStatements statements) :
            base(text, columns, criteria, statements)
        {
        }
    }

    public class CountQuery<T, TDbConnection> : Query<T, TDbConnection, int>, IQuery<T, TDbConnection, int>,
        IExecute<int, TDbConnection> where T : class, new()
    {
        public CountQuery(string text, IEnumerable<ColumnAttribute> columns, IEnumerable<CriteriaDetail>? criteria, ConnectionOptions<TDbConnection> connectionOptions) 
            : base(text, columns, criteria, connectionOptions)
        {
        }

        public override int Exec()
        {
            return (int)DatabaseManagment.ExecuteScalar(this, this.GetParameters<T, TDbConnection>(DatabaseManagment), typeof(int));
        }

        public override int Exec(TDbConnection dbConnection)
        {
            dbConnection!.NullValidate(ErrorMessages.ParameterNotNull, nameof(dbConnection));
            return (int)DatabaseManagment.ExecuteScalar(dbConnection, this, this.GetParameters<T, TDbConnection>(DatabaseManagment), typeof(int));
        }
    }
}
