namespace GSqlQuery.Queries
{
    internal class AutoIncrementingClass
    {
        public bool WithAutoIncrementing { get; set; }

        public ColumnParameterDetail[] ColumnParameters { get; set; }

        public AutoIncrementingClass(bool withAutoIncrementing, ColumnParameterDetail[] columnParameters)
        {
            WithAutoIncrementing = withAutoIncrementing;
            ColumnParameters = columnParameters;
        }
    }
}
