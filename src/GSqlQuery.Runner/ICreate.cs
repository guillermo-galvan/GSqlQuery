namespace GSqlQuery.Runner
{
    public interface ICreate<T> : GSqlQuery.ICreate<T> where T : class, new()
    {
        IQueryBuilder<InsertQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>> Insert<TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions);
    }
}