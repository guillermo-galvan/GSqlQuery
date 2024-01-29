namespace GSqlQuery
{
    public interface IWhere<TReturn> where TReturn : IQuery
    {
    }

    public interface IWhere<T, TReturn> : IWhere<TReturn>
        where T : class
        where TReturn : IQuery<T>
    {
        /// <summary>
        /// Formats
        /// </summary>
        IFormats Formats { get; }
    }
}