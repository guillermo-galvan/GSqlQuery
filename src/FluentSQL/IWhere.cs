namespace FluentSQL
{
    public interface IWhere<TReturn> where TReturn : IQuery
    {
    }

    /// <summary>
    /// Where
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public interface IWhere<T, TReturn>: IWhere<TReturn> where T : class, new() where TReturn : IQuery
    {   
    }

    public interface IWhere<T, TReturn, TDbConnection, TResult> : IWhere<T, TReturn>
        where T : class, new() where TReturn : IQuery
    {
    }
}
