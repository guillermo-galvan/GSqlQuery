namespace GSqlQuery.Queries
{
    internal class ColumnParameterDetail
    {
        public string ColumnName { get; set; }

        public ParameterDetail ParameterDetail { get; set; }

        public ColumnParameterDetail(string columnName, ParameterDetail parameterDetail)
        {
            ColumnName = columnName;
            ParameterDetail = parameterDetail;
        }
    }
}