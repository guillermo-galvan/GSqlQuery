using FluentSQL.Default;
using FluentSQL.Extensions;
using FluentSQL.Models;
using System.Linq.Expressions;

namespace FluentSQL
{
    /// <summary>
    /// Base class to generate the update query
    /// </summary>
    /// <typeparam name="T">The type of object from which the query is generated</typeparam>
    public interface IUpdate<T> where T : class, new()
    {
        /// <summary>
        /// Generate the update query
        /// </summary>
        /// <typeparam name="TProperties">The property or properties for the query</typeparam>
        /// <param name="key">The name of the statement collection</param>
        /// <param name="expression">The expression representing the property or properties</param>
        /// <returns>Instance of ISet</returns>
        ISet<T, UpdateQuery<T>> Update<TProperties>(IStatements statements, Expression<Func<T, TProperties>> expression);

        /// <summary>
        /// Generate the update query
        /// </summary>
        /// <typeparam name="TProperties">The property or properties for the query</typeparam>
        /// <param name="key">The name of the statement collection</param>
        /// <param name="expression">The expression representing the property or properties</param>
        /// <param name="value">Value</param>
        /// <returns>Instance of ISet</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static ISet<T, UpdateQuery<T>> Update<TProperties>(IStatements statements, Expression<Func<T, TProperties>> expression, TProperties value)
        {
            statements.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(statements));
            var (options, memberInfos) = expression.GetOptionsAndMember();
            memberInfos.ValidateMemberInfo(options);
            return new UpdateQueryBuilder<T>(statements,new string[] { memberInfos.Name }, value);
        }
    }
}
