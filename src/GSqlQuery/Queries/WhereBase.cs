namespace GSqlQuery
{
    /// <summary>
    /// Where
    /// </summary>
    public abstract class WhereBase<TReturn> : IWhere<TReturn> where TReturn : IQuery
    {
        public WhereBase()
        { }
    }
}