using FluentSQL.Models;

namespace FluentSQL.Default
{
    /// <summary>
    /// Delete query builder
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    internal class DeleteQueryBuilder<T> : QueryBuilderWithCriteria<T, DeleteQuery<T>>, IQueryBuilderWithWhere<T, DeleteQuery<T>> where T : class, new()
    {
        /// <summary>
        /// Initializes a new instance of the DeleteQueryBuilder class.
        /// </summary>
        /// <param name="options">Detail of the class to transform</param>
        /// <param name="statements">Statements to build the query</param>
        /// <exception cref="ArgumentNullException"></exception>
        public DeleteQueryBuilder(IStatements statements)
            : base( statements, QueryType.Delete)
        {
        }

        protected override string GenerateQuery()
        {
            string result = string.Empty;

            if (_queryType == QueryType.Delete)
            {
                result = string.Format(Statements.Delete, _tableName);
            }
            else if (_queryType == QueryType.DeleteWhere)
            {
                result = string.Format(Statements.DeleteWhere, _tableName, GetCriteria());
            }

            return result;
        }

        /// <summary>
        /// Build delete query
        /// </summary>
        public override DeleteQuery<T> Build()
        {
            return new DeleteQuery<T>(GenerateQuery(), Columns.Select(x => x.ColumnAttribute), _criteria, Statements);
        }

        /// <summary>
        /// Add where query
        /// </summary>
        /// <returns>Implementation of the IWhere interface</returns>
        public override IWhere<T, DeleteQuery<T>> Where()
        {
            ChangeQueryType();            
            _andOr = new DeleteWhere<T>(this);
            return (IWhere<T, DeleteQuery<T>>)_andOr;
        }
    }

    internal class DeleteQueryBuilder<T, TDbConnection> : QueryBuilderWithCriteria<T, DeleteQuery<T, TDbConnection>, TDbConnection, int>,
        IQueryBuilderWithWhere<T, DeleteQuery<T, TDbConnection>, TDbConnection, int>,
        IQueryBuilder<T, DeleteQuery<T, TDbConnection>, TDbConnection, int>, IBuilder<DeleteQuery<T, TDbConnection>>
        where T : class, new()
    {
        public DeleteQueryBuilder(ConnectionOptions<TDbConnection> connectionOptions) : base(connectionOptions, QueryType.Delete)
        {
        }

        public override DeleteQuery<T, TDbConnection> Build()
        {
            return new DeleteQuery<T,TDbConnection>(GenerateQuery(), Columns.Select(x => x.ColumnAttribute), _criteria, ConnectionOptions);
        }

        public override IWhere<T, DeleteQuery<T, TDbConnection>, TDbConnection> Where()
        {
            ChangeQueryType();
            _andOr = new DeleteWhere<T,TDbConnection>(this);
            return (IWhere<T, DeleteQuery<T, TDbConnection>, TDbConnection>)_andOr;
        }

        protected override string GenerateQuery()
        {
            string result = string.Empty;

            if (_queryType == QueryType.Delete)
            {
                result = string.Format(ConnectionOptions.Statements.Delete, _tableName);
            }
            else if (_queryType == QueryType.DeleteWhere)
            {
                result = string.Format(ConnectionOptions.Statements.DeleteWhere, _tableName, GetCriteria());
            }

            return result;
        }
    }
}
