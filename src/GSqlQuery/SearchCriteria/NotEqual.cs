using GSqlQuery.Extensions;

namespace GSqlQuery.SearchCriteria
{
    /// <summary>
    /// Represents the search criteria equal(<>)
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    internal class NotEqual<T> : Equal<T>, ISearchCriteria
    {
        protected override string ParameterPrefix => "PNE";

        protected override string RelationalOperator => "<>";

        /// <summary>
        /// Initializes a new instance of the Equal class.
        /// </summary>
        /// <param name="classOptionsTupla">ClassOptionsTupla</param>
        /// <param name="formats">Formats</param>
        /// <param name="value">Equality value</param>
        public NotEqual(ClassOptionsTupla<ColumnAttribute> classOptionsTupla, IFormats formats, T value) :
            base(classOptionsTupla, formats, value)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Equal class.
        /// </summary>
        /// <param name="classOptionsTupla">ClassOptionsTupla</param>
        /// <param name="formats">Formats</param>
        /// <param name="value">Equality value</param>
        /// <param name="logicalOperator">Logical Operator</param>
        public NotEqual(ClassOptionsTupla<ColumnAttribute> classOptionsTupla, IFormats formats, T value, string logicalOperator) :
            base(classOptionsTupla, formats, value, logicalOperator)
        { }
    }
}