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
        /// Criteria Detail
        /// </summary>
        public CriteriaDetailCollection CriteriaDetail { get; set; }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="columnName">Column Name</param>
        /// <param name="criteriaDetail">Criteria Detail</param>
        public ColumnParameterDetail(string columnName, CriteriaDetailCollection criteriaDetail)
        {
            ColumnName = columnName;
            CriteriaDetail = criteriaDetail;
        }
    }
}