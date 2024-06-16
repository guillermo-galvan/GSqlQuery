using GSqlQuery.Extensions;
using System.Collections.Generic;

namespace GSqlQuery.SearchCriteria
{
    /// <summary>
    /// Represents the search criteria not in(NOT IN)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks>
    /// Initializes a new instance of the NotIn class.
    /// </remarks>
    /// <param name="classOptionsTupla">ClassOptionsTupla</param>
    /// <param name="formats">Formats</param>
    /// <param name="values">Equality value</param>
    /// <param name="logicalOperator">Logical operator </param>
    /// <exception cref="ArgumentNullException"></exception>
    internal class NotIn<T>(ClassOptionsTupla<ColumnAttribute> classOptionsTupla, IFormats formats, IEnumerable<T> values, string logicalOperator)
        : In<T>(classOptionsTupla, formats, values, logicalOperator)
    {
        protected override string RelationalOperator => "NOT IN";

        protected override string ParameterPrefix => "PI";

        /// <summary>
        /// Initializes a new instance of the NotIn class.
        /// </summary>
        /// <param name="classOptionsTupla">ClassOptionsTupla</param>
        /// <param name="formats">Formats</param>
        /// <param name="values">Equality value</param>
        /// <exception cref="ArgumentNullException"></exception>
        public NotIn(ClassOptionsTupla<ColumnAttribute> classOptionsTupla, IFormats formats, IEnumerable<T> values) : this(classOptionsTupla, formats, values, null)
        { }
    }
}