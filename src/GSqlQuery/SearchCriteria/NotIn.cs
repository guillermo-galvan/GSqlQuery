using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GSqlQuery.SearchCriteria
{
    /// <summary>
    /// Represents the search criteria not in(NOT IN)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks>
    /// Initializes a new instance of the NotIn class.
    /// </remarks>
    /// <param name="classOptions">ClassOptionsTupla</param>
    /// <param name="formats">Formats</param>
    /// <param name="values">Equality value</param>
    /// <param name="logicalOperator">Logical operator </param>
    /// <param name="dynamicQuery">DynamicQuery</param>
    internal class NotIn<T, TProperties>(ClassOptions classOptions, IFormats formats, IEnumerable<TProperties> values, string logicalOperator, ref Expression<Func<T, TProperties>> expression) : In<T, TProperties>(classOptions, formats, values, logicalOperator,ref expression)
    {
        protected override string RelationalOperator => "NOT IN";

        protected override string ParameterPrefix => "PI";
    }
}