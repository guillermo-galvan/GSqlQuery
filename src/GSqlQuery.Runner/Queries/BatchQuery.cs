namespace GSqlQuery
{
    internal class BatchQuery : QueryBase
    {
        public BatchQuery(string text, IEnumerable<ColumnAttribute> columns, IEnumerable<CriteriaDetail>? criteria)
            : base(text, columns, criteria)
        {
        }
    }
}
