using FluentSQL.Default;
using System;
using System.Linq.Expressions;

namespace FluentSQL.Extensions
{
    public static class FluentSQLExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Type of class</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="where">IWhere</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <returns>IAndOr</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IAndOr<T> GetAndOr<T, TProperties>(this IWhere<T> where, Expression<Func<T, TProperties>> expression) where T : class, new()
        {
            IAndOr<T>? result = null;

            if (where is IAndOr<T> andor)
            {
                result = andor;
            }
            else if (where is Where<T> where1)
            {
                result = where1;
            }

#pragma warning disable CS8604 // Possible null reference argument.
            result.Validate(expression);
#pragma warning restore CS8604 // Possible null reference argument.
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Type of class</typeparam>        
        /// <param name="where">IWhere</param>        
        /// <returns>IAndOr</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IAndOr<T> GetAndOr<T>(this IWhere<T> where) where T : class, new()
        {
            IAndOr<T>? result = null;

            if (where is IAndOr<T> andor)
            {
                result = andor;
            }
            else if (where is Where<T> where1)
            {
                result = where1;
            }

#pragma warning disable CS8604 // Possible null reference argument.
            result.NullValidate(ErrorMessages.ParameterNotNull, nameof(where));
#pragma warning restore CS8604 // Possible null reference argument.
            return result;
        }

        /// <summary>
        /// Validate if andor and expression are not null
        /// </summary>
        /// <typeparam name="T">Type of class</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="andOr">IAndOr</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Validate<T, TProperties>(this IAndOr<T> andOr, Expression<Func<T, TProperties>> expression) where T : class, new()
        {
            andOr.NullValidate(ErrorMessages.ParameterNotNull, nameof(andOr));
            expression.NullValidate(ErrorMessages.ParameterNotNull, nameof(expression));
        }
    }
}
