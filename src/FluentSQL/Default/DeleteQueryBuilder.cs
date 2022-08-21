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
        /// <param name="selectMember">Selected Member Set</param>
        /// <param name="statements">Statements to build the query</param>        
        /// <exception cref="ArgumentNullException"></exception>
        public DeleteQueryBuilder(ClassOptions options, IEnumerable<string> selectMember, ConnectionOptions connectionOptions)
            : base(options, selectMember, connectionOptions, QueryType.Delete)
        {
        }

        protected override string GenerateQuery()
        {
            string result = string.Empty;

            if (_queryType == QueryType.Delete)
            {
                result = string.Format(_connectionOptions.Statements.Delete, _tableName);
            }
            else if (_queryType == QueryType.DeleteWhere)
            {
                result = string.Format(_connectionOptions.Statements.DeleteWhere, _tableName, GetCriteria());
            }

            return result;
        }

        /// <summary>
        /// Build delete query
        /// </summary>
        public virtual DeleteQuery<T> Build()
        {
            return new DeleteQuery<T>(GenerateQuery(), _columns, _criteria, _connectionOptions);
        }

        /// <summary>
        /// Add where query
        /// </summary>
        /// <returns>Implementation of the IWhere interface</returns>
        public IWhere<T, DeleteQuery<T>> Where()
        {
            ChangeQueryType();            
            _andOr = new DeleteWhere<T>(this);
            return (IWhere<T, DeleteQuery<T>>)_andOr;
        }
    }
}
