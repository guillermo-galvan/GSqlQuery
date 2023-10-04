namespace GSqlQuery.Queries
{
    /// <summary>
    /// Contains the name of the column and its corresponding parameter.
    /// </summary>
    internal class ColumnParameterDetail
    {
        /// <summary>
        /// Column Name
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// Parameter Detail
        /// </summary>
        public ParameterDetail ParameterDetail { get; set; }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="columnName">Column Name</param>
        /// <param name="parameterDetail">Parameter Detail</param>
        public ColumnParameterDetail(string columnName, ParameterDetail parameterDetail)
        {
            ColumnName = columnName;
            ParameterDetail = parameterDetail;
        }
    }
}