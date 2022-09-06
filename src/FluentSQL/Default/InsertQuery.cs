using FluentSQL.Extensions;

namespace FluentSQL.Default
{
    /// <summary>
    /// Insert Query
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public class InsertQuery<T> : Query<T> , ISetDatabaseManagement<T> where T : class, new()
    {
        public object Entity { get; }

        /// <summary>
        /// Initializes a new instance of the InsertQuery class.
        /// </summary>
        /// <param name="text">The Query</param>
        /// <param name="columns">Columns of the query</param>
        /// <param name="criteria">Query criteria</param>
        /// <param name="statements">Statements to use in the query</param>        
        /// <exception cref="ArgumentNullException"></exception>
        public InsertQuery(string text, IEnumerable<ColumnAttribute> columns, IEnumerable<CriteriaDetail>? criteria, IStatements statements, object entity)
            : base(text, columns, criteria, statements)
        {
            Entity = entity ?? throw new ArgumentNullException(nameof(entity));
        }

        public IExecute<T, TDbConnection> SetDatabaseManagement<TDbConnection>(IDatabaseManagement<TDbConnection> databaseManagment)
        {
            databaseManagment.NullValidate(ErrorMessages.ParameterNotNull, nameof(databaseManagment));
            return new InsertExecute<TDbConnection, T>(databaseManagment, this);
        }
    }
}
