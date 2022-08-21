namespace FluentSQL
{
    public interface IQueryBuilderWithWhere<T, TReturn> : IQueryBuilder<T, TReturn> where T : class, new() where TReturn : IQuery<T>
    {
        /// <summary>
        /// Add where statement in query
        /// </summary>
        /// <returns>Implementation of the IWhere interface</returns>
        IWhere<T, TReturn> Where();
    }
}
