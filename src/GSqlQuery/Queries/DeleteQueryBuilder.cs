using System.Linq;

namespace GSqlQuery.Queries
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
            : base(statements)
        {
        }

        internal static string CreateQuery(bool isWhere, IStatements statements, string tableName, string criterias)
        {
            string result;

            if (!isWhere)
            {
                result = string.Format(statements.Delete, tableName);
            }
            else
            {
                result = string.Format(statements.DeleteWhere, tableName, criterias);
            }

            return result;
        }

        /// <summary>
        /// Build delete query
        /// </summary>
        public override DeleteQuery<T> Build()
        {
            var query = CreateQuery(_andOr != null, Statements, _tableName, _andOr != null ? GetCriteria() : string.Empty);
            return new DeleteQuery<T>(query, Columns.Select(x => x.ColumnAttribute), _criteria, Statements);
        }

        /// <summary>
        /// Add where query
        /// </summary>
        /// <returns>Implementation of the IWhere interface</returns>
        public override IWhere<T, DeleteQuery<T>> Where()
        {
            _andOr = new AndOrBase<T,DeleteQuery<T>>(this);
            return (IWhere<T, DeleteQuery<T>>)_andOr;
        }
    }
}
