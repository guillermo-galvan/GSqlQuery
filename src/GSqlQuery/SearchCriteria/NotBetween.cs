using System;
using System.Linq.Expressions;

namespace GSqlQuery.SearchCriteria
{
    /// <summary>
    ///  Represents the search criteria NOT BETWEEN
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    /// <param name="classOptions">ClassOptions</param>
    /// <param name="formats">Formats</param>
    /// <param name="initialValue">Initial value</param>
    /// <param name="finalValue">Final value</param>
    /// <param name="logicalOperator">Logical Operator</param>
    /// <param name="dynamicQuery">DynamicQuery</param>
    internal class NotBetween<T, TProperties>(ClassOptions classOptions, IFormats formats, TProperties initialValue, TProperties finalValue, string logicalOperator, Expression<Func<T, TProperties>> expression) :
        Between<T, TProperties>(classOptions, formats, initialValue, finalValue, logicalOperator, expression)
    {
        protected override string RelationalOperator => "NOT BETWEEN";

        protected override string ParameterPrefix => "PNB";
    }
}