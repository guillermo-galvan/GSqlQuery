namespace GSqlQuery.Runner
{
    public interface IRead<T> : GSqlQuery.IRead<T> where T : class, new()
    {

    }
}