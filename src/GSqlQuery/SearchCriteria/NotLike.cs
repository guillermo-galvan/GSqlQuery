using System;
using System.Linq.Expressions;

namespace GSqlQuery.SearchCriteria
{
    /// <summary>
    /// Represents the search criteria not like (NOT LIKE)
    /// </summary>
    /// <param name="classOptions">ClassOptions</param>
    /// <param name="formats">Formats</param>
    /// <param name="value">Equality value</param>
    /// <param name="logicalOperator">Logical Operator</param>
    /// <param name="expression">Expression</param>
    internal class NotLike<T, TProperties>(ClassOptions classOptions, IFormats formats, string value, string logicalOperator, ref Expression<Func<T, TProperties>> expression) : Like<T, TProperties>(classOptions, formats, value, logicalOperator, ref expression)
    {
        protected override string RelationalOperator => "NOT LIKE";

        protected override string ParameterPrefix => "PNL";
    }
}