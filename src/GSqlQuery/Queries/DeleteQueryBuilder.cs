namespace GSqlQuery.Queries
{
    /// <summary>
    /// Delete Query Builder
    /// </summary>
    /// <typeparam name="T">Type to create the query</typeparam>
    /// <typeparam name="TReturn">Query</typeparam>
    /// <param name="formats">Formats</param>
    internal abstract class DeleteQueryBuilder<T, TReturn>(IFormats formats) : QueryBuilderWithCriteria<T, TReturn>(formats)
        where T : class
        where TReturn : DeleteQuery<T>
    {

        /// <summary>
        /// Create query
        /// </summary>
        /// <returns>Query text</returns>
        internal string CreateQuery()
        {
            if (_andOr == null)
            {
               return ConstFormat.DELETE.Replace("{0}", _tableName);
            }
            else
            {
                string criteria = GetCriteria();
                return ConstFormat.DELETEWHERE.Replace("{0}", _tableName).Replace("{1}", criteria);
            }
        }
    }

    /// <summary>
    /// Delete Query Builder
    /// </summary>
    /// <typeparam name="T">Type to create the query</typeparam>
    /// <param name="formats">Formats</param>
    internal class DeleteQueryBuilder<T>(IFormats formats) : DeleteQueryBuilder<T, DeleteQuery<T>>(formats)
        where T : class
    {

        /// <summary>
        /// Build the query
        /// </summary>
        /// <returns>Count Query</returns>
        public override DeleteQuery<T> Build()
        {
            string text = CreateQuery();
            return new DeleteQuery<T>(text, Columns, _criteria, Options);
        }
    }
}