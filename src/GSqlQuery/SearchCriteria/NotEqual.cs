using System;
using System.Linq.Expressions;

namespace GSqlQuery.SearchCriteria
{
    /// <summary>
    /// Represents the search criteria equal(<>)
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    /// <remarks>
    /// Initializes a new instance of the Equal class.
    /// </remarks>
    /// <param name="classOptions">ClassOptions</param>
    /// <param name="formats">Formats</param>
    /// <param name="value">Value</param>
    /// <param name="logicalOperator">Logical operator</param>
    /// <param name="expression">Expression</param>
    internal class NotEqual<T, TProperties>(ClassOptions classOptions, IFormats formats, TProperties value, string logicalOperator,ref Expression<Func<T, TProperties>> expression) : Equal<T, TProperties>(classOptions, formats, value, logicalOperator,ref expression), ISearchCriteria
    {
        protected override string ParameterPrefix => "PNE";

        protected override string RelationalOperator => "<>";
    }
}