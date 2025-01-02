using System;
using System.Linq.Expressions;

namespace GSqlQuery.SearchCriteria
{
    /// <summary>
    /// Represents the search criteria is not null
    /// </summary>
    /// <param name="classOptions">ClassOptions</param>
    /// <param name="formats">Formats</param>
    /// <param name="dynamicQuery">DynamicQuery</param>
    internal class IsNotNull<T, TProperties>(ClassOptions classOptions, IFormats formats, string logicalOperator, ref Expression<Func<T, TProperties>> expression) :
        IsNull<T, TProperties>(classOptions, formats, logicalOperator, ref expression)
    {
        protected override string RelationalOperator => "IS NOT NULL";
    }
}