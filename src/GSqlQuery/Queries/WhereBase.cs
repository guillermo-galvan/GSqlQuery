namespace GSqlQuery
{
    /// <summary>
    /// Where
    /// </summary>
    public abstract class WhereBase<T, TReturn> : IWhere<T, TReturn> where T : class, new() where TReturn : IQuery
    {
        public WhereBase()
        {}
    }
}
