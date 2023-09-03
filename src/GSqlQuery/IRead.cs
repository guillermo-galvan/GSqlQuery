namespace GSqlQuery
{
    /// <summary>
    /// Base class for reading.
    /// </summary>
    /// <typeparam name="T">The type of object from which the query is generated</typeparam>
    public interface IRead<T> where T : class, new()
    {

    }
}