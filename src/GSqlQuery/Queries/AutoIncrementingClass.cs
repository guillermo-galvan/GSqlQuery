namespace GSqlQuery.Queries
{
    /// <summary>
    /// Identifies if the class has columns marked with auto-increment
    /// </summary>
    internal class AutoIncrementingClass
    {
        /// <summary>
        /// It has an auto-increment column.
        /// </summary>
        public bool WithAutoIncrementing { get; set; }

        /// <summary>
        /// Get columns
        /// </summary>
        public ColumnParameterDetail[] ColumnParameters { get; set; }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="withAutoIncrementing">It has an auto-increment column.</param>
        /// <param name="columnParameters">Columns</param>
        public AutoIncrementingClass(bool withAutoIncrementing, ColumnParameterDetail[] columnParameters)
        {
            WithAutoIncrementing = withAutoIncrementing;
            ColumnParameters = columnParameters;
        }
    }
}