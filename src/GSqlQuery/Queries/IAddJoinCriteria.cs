namespace GSqlQuery
{
    internal interface IAddJoinCriteria<T>
    {
        void AddColumns(T joinModel);
    }
}
