namespace GSqlQuery
{
    /// <summary>
    /// Join model
    /// </summary>
    /// <typeparam name="T1">Type for first table</typeparam>
    /// <typeparam name="T2">Type for second table</typeparam>
    public class Join<T1, T2> where T1 : class where T2 : class
    {
        /// <summary>
        /// First Table
        /// </summary>
        public T1 Table1 { get; set; }

        /// <summary>
        /// Second Table
        /// </summary>
        public T2 Table2 { get; set; }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="table1">Type for first table</param>
        /// <param name="table2">Type for second table</param>
        public Join(T1 table1, T2 table2)
        {
            Table1 = table1;
            Table2 = table2;
        }
    }

    /// <summary>
    /// Join model
    /// </summary>
    /// <typeparam name="T1">Type for first table</typeparam>
    /// <typeparam name="T2">Type for second table</typeparam>
    /// <typeparam name="T3">Type for third table</typeparam>
    public class Join<T1, T2, T3> : Join<T1, T2>
        where T1 : class where T2 : class where T3 : class
    {
        /// <summary>
        /// Third Table
        /// </summary>
        public T3 Table3 { get; set; }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="table1">Type for first table</param>
        /// <param name="table2">Type for second table</param>
        /// <param name="table3">Type for third table</param>
        public Join(T1 table1, T2 table2, T3 table3) : base(table1, table2)
        {
            Table3 = table3;
        }
    }
}