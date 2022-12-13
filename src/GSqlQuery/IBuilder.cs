namespace GSqlQuery
{
    public interface IBuilder<TReturn> where TReturn : IQuery
    {
        TReturn Build();
    }
}
