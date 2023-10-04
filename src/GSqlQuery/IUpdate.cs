using System;
using System.Linq.Expressions;

namespace GSqlQuery
{
    /// <summary>
    /// Base class to generate the update query
    /// </summary>
    /// <typeparam name="T">The type of object from which the query is generated</typeparam>
    public interface IUpdate<T> where T : class
    {
        /// <summary>
        /// Generate the update query
        /// </summary>
        /// <typeparam name="TProperties">The property or properties for the query</typeparam>
        /// <param name="formats">Formats</param>
        /// <param name="expression">The expression representing the property or properties</param>
        /// <returns>Instance of ISet</returns>

        ISet<T, UpdateQuery<T>, IFormats> Update<TProperties>(IFormats formats, Expression<Func<T, TProperties>> expression);
    }
}