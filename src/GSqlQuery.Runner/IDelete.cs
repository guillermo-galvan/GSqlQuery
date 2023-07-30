namespace GSqlQuery.Runner
{
    public interface IDelete<T> : GSqlQuery.IDelete<T> where T : class, new()
    {
    }
}