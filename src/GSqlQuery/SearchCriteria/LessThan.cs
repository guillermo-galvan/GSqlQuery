using System;
using System.Linq.Expressions;

namespace GSqlQuery.SearchCriteria
{
    /// <summary>
    /// Represents the search criteria Less Than (<)
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    /// <remarks>
    /// Initializes a new instance of the LessThan class.
    /// </remarks>
    /// <param name="classOptions">ClassOptions</param>
    /// <param name="formats">Formats</param>
    /// <param name="value">Value</param>
    /// <param name="logicalOperator">Logical operator</param>
    /// <param name="expression">Expression</param>
    internal class LessThan<T, TProperties>(ClassOptions classOptions, IFormats formats, TProperties value, string logicalOperator, ref Expression<Func<T, TProperties>> expression) : Equal<T, TProperties>(classOptions, formats, value, logicalOperator, ref expression)
    {
        protected override string ParameterPrefix => "PLT";

        protected override string RelationalOperator => "<";
    }
}