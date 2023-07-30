namespace GSqlQuery
{
    public interface IWhere<TReturn> where TReturn : IQuery
    {
    }

    public interface IWhere<T, TReturn> : IWhere<TReturn>
        where T : class, new()
        where TReturn : IQuery<T>
    {

    }
}