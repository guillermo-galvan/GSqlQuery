namespace FluentSQL
{
    public interface ISetDatabaseManagement<TResult>
    {
        IExecute<TResult, TDbConnection> SetDatabaseManagement<TDbConnection>(IDatabaseManagement<TDbConnection> databaseManagment);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface IExecute<TResult, TDbConnection>
    {
        TResult Exec();

        TResult Exec(TDbConnection dbConnection);
    }
}
