using FluentSQL.Extensions;
using FluentSQL.Models;

namespace FluentSQL.Default
{
    /// <summary>
    /// Update query
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public class UpdateQuery<T> : Query<T> where T : class, new()
    {
        /// <summary>
        /// Initializes a new instance of the UpdateQuery class.
        /// </summary>
        /// <param name="text">The Query</param>
        /// <param name="columns">Columns of the query</param>
        /// <param name="criteria">Query criteria</param>
        /// <param name="statements">Statements to use in the query</param>        
        /// <exception cref="ArgumentNullException"></exception>
        public UpdateQuery(string text, IEnumerable<ColumnAttribute> columns, IEnumerable<CriteriaDetail>? criteria, IStatements statements) :
            base(text, columns, criteria, statements)
        { }
    }

    public class UpdateQuery<T, TDbConnection> : Query<T, TDbConnection, int>, IQuery<T, TDbConnection, int>,
        IExecute<int, TDbConnection> where T : class, new()
    {
        public UpdateQuery(string text, IEnumerable<ColumnAttribute> columns, IEnumerable<CriteriaDetail>? criteria, ConnectionOptions<TDbConnection> connectionOptions) :
            base(text, columns, criteria, connectionOptions)
        {
        }

        public override int Exec()
        {
            return DatabaseManagment.ExecuteNonQuery(this, this.GetParameters<T, TDbConnection>(DatabaseManagment));
        }

        public override int Exec(TDbConnection dbConnection)
        {
            dbConnection!.NullValidate(ErrorMessages.ParameterNotNull, nameof(dbConnection));
            return DatabaseManagment.ExecuteNonQuery(dbConnection, this, this.GetParameters<T, TDbConnection>(DatabaseManagment));
        }
    }
}
