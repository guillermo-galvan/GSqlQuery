using GSqlQuery.Extensions;

namespace GSqlQuery.SearchCriteria
{
    /// <summary>
    ///  Represents the search criteria NOT BETWEEN
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    /// <param name="classOptionsTupla">ClassOptionsTupla</param>
    /// <param name="formats">Formats</param>
    /// <param name="initialValue">Initial value</param>
    /// <param name="finalValue">Final value</param>
    /// <param name="logicalOperator">Logical Operator</param>
    internal class NotBetween<T>(ClassOptionsTupla<PropertyOptions> classOptionsTupla, IFormats formats, T initialValue, T finalValue, string logicalOperator) :
        Between<T>(classOptionsTupla, formats, initialValue, finalValue, logicalOperator)
    {
        protected override string RelationalOperator => "NOT BETWEEN";

        protected override string ParameterPrefix => "PNB";

        /// <summary>
        /// Initializes a new instance of the NotBetween2 class.
        /// </summary>
        /// <param name="classOptionsTupla">ClassOptionsTupla</param>
        /// <param name="formats">Formats</param>
        /// <param name="initialValue">Initial value</param>
        /// <param name="finalValue">Final value</param>
        public NotBetween(ClassOptionsTupla<PropertyOptions> classOptionsTupla, IFormats formats, T initialValue, T finalValue) :
            this(classOptionsTupla, formats, initialValue, finalValue, null)
        {

        }
    }
}