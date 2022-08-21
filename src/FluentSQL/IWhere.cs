namespace FluentSQL
{
    /// <summary>
    /// Where
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public interface IWhere<T, TReturn> where T : class, new() where TReturn : IQuery<T>
    {   
    }
}
