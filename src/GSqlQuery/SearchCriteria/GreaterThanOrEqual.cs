using System;
using System.Linq.Expressions;

namespace GSqlQuery.SearchCriteria
{
    /// <summary>
    /// Represents the search criteria Greater Than Or Equal(>=)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks>
    /// Initializes a new instance of the GreaterThanOrEqual class.
    /// </remarks>
    /// <param name="classOptions">ClassOptionsTupla</param>
    /// <param name="formats">Formats</param>
    /// <param name="value">Value</param>
    /// <param name="logicalOperator">Logical Operator</param>
    /// <param name="expression">Expression</param>
    internal class GreaterThanOrEqual<T, TProperties>(ClassOptions classOptions, IFormats formats, TProperties value, string logicalOperator, ref Expression<Func<T, TProperties>> expression) : Equal<T, TProperties>(classOptions, formats, value, logicalOperator, ref expression)
    {
        protected override string ParameterPrefix => "PGTE";

        protected override string RelationalOperator => ">=";
    }
}