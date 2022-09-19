using FluentSQL.Extensions;
using FluentSQL.Models;

namespace FluentSQL.Default
{
    /// <summary>
    /// Insert Query
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public class InsertQuery<T> : Query<T> where T : class, new()
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
    }

    public class InsertQuery<T, TDbConnection> : Query<T, TDbConnection, T>, IQuery<T, TDbConnection, T>,
        IExecute<T, TDbConnection> where T : class, new()
    {
        public object Entity { get; }

        public InsertQuery(string text, IEnumerable<ColumnAttribute> columns, IEnumerable<CriteriaDetail>? criteria, ConnectionOptions<TDbConnection> connectionOptions,
            object entity) : 
            base(text, columns, criteria, connectionOptions)
        {
            Entity = entity ?? throw new ArgumentNullException(nameof(entity));
        }

        private void InsertAutoIncrementing(TDbConnection? connection = default)
        {
            var classOptions = GetClassOptions();
            var columnAutoIncrementing = Columns.First(x => x.IsAutoIncrementing);
            var propertyOptions = classOptions.PropertyOptions.First(x => x.ColumnAttribute.Name == columnAutoIncrementing.Name);

            object idResult;
            if (connection == null)
            {
                idResult = ConnectionOptions.DatabaseManagment.ExecuteScalar(this, this.GetParameters<T, TDbConnection>(ConnectionOptions.DatabaseManagment),
                    propertyOptions.PropertyInfo.PropertyType);
            }
            else
            {
                idResult = ConnectionOptions.DatabaseManagment.ExecuteScalar(connection, this, this.GetParameters<T, TDbConnection>(ConnectionOptions.DatabaseManagment),
                    propertyOptions.PropertyInfo.PropertyType);
            }

            propertyOptions.PropertyInfo.SetValue(this.Entity, idResult);
        }

        public override T Exec()
        {
            if (Columns.Any(x => x.IsAutoIncrementing))
            {
                InsertAutoIncrementing();
            }
            else
            {
                ConnectionOptions.DatabaseManagment.ExecuteNonQuery(this, this.GetParameters<T, TDbConnection>(ConnectionOptions.DatabaseManagment));
            }

            return (T)Entity;
        }

        public override T Exec(TDbConnection dbConnection)
        {
            dbConnection.NullValidate(ErrorMessages.ParameterNotNull, nameof(dbConnection));

            if (Columns.Any(x => x.IsAutoIncrementing))
            {
                InsertAutoIncrementing(dbConnection);
            }
            else
            {
                ConnectionOptions.DatabaseManagment.ExecuteNonQuery(dbConnection, this, this.GetParameters<T, TDbConnection>(ConnectionOptions.DatabaseManagment));
            }

            return (T)Entity;
        }
    }
}
