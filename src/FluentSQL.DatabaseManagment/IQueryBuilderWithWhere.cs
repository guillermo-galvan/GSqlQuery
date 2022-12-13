namespace FluentSQL.DatabaseManagement
{
    public interface IQueryBuilderWithWhere<T, TReturn, TDbConnection> : IQueryBuilderWithWhere<T, TReturn>, IQueryBuilder<T, TReturn, TDbConnection>, IBuilder<TReturn>
        where T : class, new() where TReturn : IQuery
    {
    }
}
