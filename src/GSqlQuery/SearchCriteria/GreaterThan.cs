using GSqlQuery.Extensions;

namespace GSqlQuery.SearchCriteria
{
    /// <summary>
    /// Represents the search criteria Greater Than (>)
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    internal class GreaterThan<T> : Equal<T>
    {
        protected override string ParameterPrefix => "PGT";

        protected override string RelationalOperator => ">";

        /// <summary>
        /// Initializes a new instance of the GreaterThan class.
        /// </summary>
        /// <param name="classOptionsTupla">ClassOptionsTupla</param>
        /// <param name="formats">Formats</param>
        /// <param name="value">Value</param>
        public GreaterThan(ClassOptionsTupla<PropertyOptions> classOptionsTupla, IFormats formats, T value) :
            base(classOptionsTupla, formats, value)
        {
        }

        /// <summary>
        /// Initializes a new instance of the GreaterThan class.
        /// </summary>
        /// <param name="classOptionsTupla">ClassOptionsTupla</param>
        /// <param name="formats">Formats</param>
        /// <param name="value">Value</param>
        /// <param name="logicalOperator">Logical operator</param>
        public GreaterThan(ClassOptionsTupla<PropertyOptions> classOptionsTupla, IFormats formats, T value, string logicalOperator) :
            base(classOptionsTupla, formats, value, logicalOperator)
        { }
    }
}