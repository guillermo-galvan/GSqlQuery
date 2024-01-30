using GSqlQuery.Extensions;

namespace GSqlQuery.SearchCriteria
{
    /// <summary>
    /// Represents the search criteria Less Than Or Equal(<=)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class LessThanOrEqual<T> : Equal<T>
    {
        protected override string ParameterPrefix => "PLTE";

        protected override string RelationalOperator => "<=";

        /// <summary>
        /// Initializes a new instance of the LessThanOrEqual class.
        /// </summary>
        /// <param name="classOptionsTupla">ClassOptionsTupla</param>
        /// <param name="formats">Formats</param>
        /// <param name="value">Value</param>
        public LessThanOrEqual(ClassOptionsTupla<ColumnAttribute> classOptionsTupla, IFormats formats, T value) : 
            base(classOptionsTupla, formats, value)
        {
        }

        /// <summary>
        /// Initializes a new instance of the LessThanOrEqual class.
        /// </summary>
        /// <param name="classOptionsTupla">ClassOptionsTupla</param>
        /// <param name="formats">Formats</param>
        /// <param name="value">Value</param>
        /// <param name="logicalOperator">Logical Operator</param>
        public LessThanOrEqual(ClassOptionsTupla<ColumnAttribute> classOptionsTupla, IFormats formats, T value, string logicalOperator) : 
            base(classOptionsTupla, formats, value, logicalOperator)
        { }
    }
}