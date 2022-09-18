namespace FluentSQL
{
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
