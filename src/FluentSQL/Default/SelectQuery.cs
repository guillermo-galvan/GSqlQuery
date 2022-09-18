using FluentSQL.Extensions;
using FluentSQL.Models;

namespace FluentSQL.Default
{
    /// <summary>
    /// Select query
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public class SelectQuery<T> : Query<T> where T : class, new()
    {
        /// <summary>
        /// Initializes a new instance of the SelectQuery class.
        /// </summary>
        /// <param name="text">The Query</param>
        /// <param name="columns">Columns of the query</param>
        /// <param name="criteria">Query criteria</param>
        /// <param name="statements">Statements to use in the query</param>        
        /// <exception cref="ArgumentNullException"></exception>
        public SelectQuery(string text, IEnumerable<ColumnAttribute> columns, IEnumerable<CriteriaDetail>? criteria, IStatements statements) :
            base(text, columns, criteria, statements)
        { }
    }


    public class SelectQuery<T, TDbConnection> : Query<T, TDbConnection, IEnumerable<T>>, IQuery<T, TDbConnection, IEnumerable<T>>, 
        IExecute<IEnumerable<T>, TDbConnection> where T : class, new()
    {
        public SelectQuery(string text, IEnumerable<ColumnAttribute> columns, IEnumerable<CriteriaDetail>? criteria, ConnectionOptions<TDbConnection> connectionOptions) : 
            base(text, columns, criteria, connectionOptions)
        {
        }

        public override IEnumerable<T> Exec()
        {
            return ConnectionOptions.DatabaseManagment.ExecuteReader<T>(this, GetClassOptions().PropertyOptions, 
                this.GetParameters<T,TDbConnection>(ConnectionOptions.DatabaseManagment));
        }

        public override IEnumerable<T> Exec(TDbConnection dbConnection)
        {
            dbConnection!.NullValidate(ErrorMessages.ParameterNotNull, nameof(dbConnection));
            return ConnectionOptions.DatabaseManagment.ExecuteReader<T>(dbConnection,this, GetClassOptions().PropertyOptions, 
                this.GetParameters<T, TDbConnection>(ConnectionOptions.DatabaseManagment));
        }
    }
}
