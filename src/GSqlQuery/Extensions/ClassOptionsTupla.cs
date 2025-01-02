namespace GSqlQuery.Extensions
{
    /// <summary>
    /// Generic class for internal use
    /// </summary>
    /// <typeparam name="T">The type of the Columns property</typeparam>
    internal class ClassOptionsTupla<T>
    {
        /// <summary>
        /// Class to which Columns belongs
        /// </summary>
        public ClassOptions ClassOptions { get; set; }

        /// <summary>
        /// Columns 
        /// </summary>
        public T Columns { get; set; }

        /// <summary>
        /// Generic class for internal use
        /// </summary>
        /// <param name="classOptions">Class to which Columns belongs</param>
        /// <param name="columns">Columns</param>
        public ClassOptionsTupla(ClassOptions classOptions, T columns)
        {
            ClassOptions = classOptions;
            Columns = columns;
        }
    }
}