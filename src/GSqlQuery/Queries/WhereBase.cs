namespace GSqlQuery
{
    /// <summary>
    /// Where Base
    /// </summary>
    public abstract class WhereBase<TReturn> : IWhere<TReturn> where TReturn : IQuery
    {
        /// <summary>
        /// Class constructor
        /// </summary>
        public WhereBase()
        { }
    }
}