using System.Linq;

namespace GSqlQuery.Queries
{
    internal abstract class DeleteQueryBuilder<T, TReturn> : QueryBuilderWithCriteria<T, TReturn>
        where T : class
        where TReturn : DeleteQuery<T>
    {
        public DeleteQueryBuilder(IStatements statements)
            : base(statements)
        {
        }

        internal string CreateQuery(IStatements statements)
        {
            string result;

            if (_andOr == null)
            {
                result = string.Format(statements.Delete, _tableName);
            }
            else
            {
                result = string.Format(statements.DeleteWhere, _tableName, GetCriteria());
            }

            return result;
        }
    }
    /// <summary>
    /// Delete query builder
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    internal class DeleteQueryBuilder<T> : DeleteQueryBuilder<T, DeleteQuery<T>>
        where T : class
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

        /// <summary>
        /// Build delete query
        /// </summary>
        public override DeleteQuery<T> Build()
        {
            return new DeleteQuery<T>(CreateQuery(Options), Columns, _criteria, Options);
        }
    }
}