using System;
using System.Linq.Expressions;

namespace GSqlQuery.SearchCriteria
{
    /// <summary>
    /// Represents the search criteria Less Than Or Equal(<=)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks>
    /// Initializes a new instance of the LessThanOrEqual class.
    /// </remarks>
    /// <param name="classOptions">ClassOptions</param>
    /// <param name="formats">Formats</param>
    /// <param name="value">Value</param>
    /// <param name="logicalOperator">Logical operator</param>
    /// <param name="dynamicQuery">DynamicQuery</param>
    internal class LessThanOrEqual<T, TProperties>(ClassOptions classOptions, IFormats formats, TProperties value, string logicalOperator, ref Expression<Func<T, TProperties>> expression) : Equal<T, TProperties>(classOptions, formats, value, logicalOperator, ref expression)
    {
        protected override string ParameterPrefix => "PLTE";

        protected override string RelationalOperator => "<=";
    }
}