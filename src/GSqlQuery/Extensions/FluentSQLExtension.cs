using System.Linq.Expressions;

namespace GSqlQuery.Extensions
{
    public static class FluentSQLExtension
    {
        public static IAndOr<T, TReturn> GetAndOr<T, TReturn, TProperties>(this IWhere<T, TReturn> where, Expression<Func<T, TProperties>> expression)
           where T : class, new() where TReturn : IQuery
        {
            IAndOr<T, TReturn>? result = null;

            if (where is IAndOr<T, TReturn> andor)
            {
                result = andor;
            }

            result!.Validate(expression);
            return result!;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Type of class</typeparam>        
        /// <param name="where">IWhere</param>        
        /// <returns>IAndOr</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IAndOr<T, TReturn> GetAndOr<T, TReturn>(this IWhere<T, TReturn> where)
            where T : class, new() where TReturn : IQuery
        {
            IAndOr<T, TReturn>? result = null;

            if (where is IAndOr<T, TReturn> andor)
            {
                result = andor;
            }

            result!.NullValidate(ErrorMessages.ParameterNotNull, nameof(where));
            return result!;
        }

        /// <summary>
        /// Validate if andor and expression are not null
        /// </summary>
        /// <typeparam name="T">Type of class</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="andOr">IAndOr</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Validate<T, TReturn, TProperties>(this IAndOr<T, TReturn> andOr, Expression<Func<T, TProperties>> expression)
            where T : class, new() where TReturn : IQuery
        {
            andOr.NullValidate(ErrorMessages.ParameterNotNull, nameof(andOr));
            expression.NullValidate(ErrorMessages.ParameterNotNull, nameof(expression));
        }
    }
}
