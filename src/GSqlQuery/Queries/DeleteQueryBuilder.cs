using System.Linq;

namespace GSqlQuery.Queries
{
    /// <summary>
    /// Delete Query Builder
    /// </summary>
    /// <typeparam name="T">Type to create the query</typeparam>
    /// <typeparam name="TReturn">Query</typeparam>
    internal abstract class DeleteQueryBuilder<T, TReturn> : QueryBuilderWithCriteria<T, TReturn>
        where T : class
        where TReturn : DeleteQuery<T>
    {
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="formats">Formats</param>
        public DeleteQueryBuilder(IFormats formats)
            : base(formats)
        {
        }

        /// <summary>
        /// Create query
        /// </summary>
        /// <returns>Query text</returns>
        internal string CreateQuery()
        {
            string result;

            if (_andOr == null)
            {
                result = string.Format(ConstFormat.DELETE, _tableName);
            }
            else
            {
                result = string.Format(ConstFormat.DELETEWHERE, _tableName, GetCriteria());
            }

            return result;
        }
    }

    /// <summary>
    /// Delete Query Builder
    /// </summary>
    /// <typeparam name="T">Type to create the query</typeparam>
    internal class DeleteQueryBuilder<T> : DeleteQueryBuilder<T, DeleteQuery<T>>
        where T : class
    {
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="formats">Formats</param>
        public DeleteQueryBuilder(IFormats formats)
            : base(formats)
        {
        }

        /// <summary>
        /// Build the query
        /// </summary>
        /// <returns>Count Query</returns>
        public override DeleteQuery<T> Build()
        {
            return new DeleteQuery<T>(CreateQuery(), Columns, _criteria, Options);
        }
    }
}