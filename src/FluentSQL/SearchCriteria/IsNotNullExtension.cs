﻿using FluentSQL.Extensions;
using FluentSQL.Helpers;
using System.Linq.Expressions;

namespace FluentSQL.SearchCriteria
{
    public static class IsNotNullExtension
    {
        /// <summary>
        /// Adds the criteria is not null to the query
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="where">Instance of IWhere</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="value">Value</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T, TReturn> IsNotNull<T, TReturn, TProperties>(this IWhere<T, TReturn> where, Expression<Func<T, TProperties>> expression) 
            where T : class, new() where TReturn : IQuery<T>
        {
            IAndOr<T, TReturn> andor = where.GetAndOr(expression);
            andor.Add(new IsNotNull(ClassOptionsFactory.GetClassOptions(typeof(T)).Table, expression.GetColumnAttribute()));
            return andor;
        }

        /// <summary>
        /// Adds the criteria is not null to the query with the logical operator AND
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="andOr">Instance of IAndOr</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="value">Value</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T, TReturn> AndIsNotNull<T, TReturn, TProperties>(this IAndOr<T, TReturn> andOr, Expression<Func<T, TProperties>> expression) 
            where T : class, new() where TReturn : IQuery<T>
        {
            andOr.Validate(expression);
            andOr.Add(new IsNotNull(ClassOptionsFactory.GetClassOptions(typeof(T)).Table, expression.GetColumnAttribute(), "AND"));
            return andOr;
        }

        /// <summary>
        /// Adds the criteria is not null to the query with the logical operator OR
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="andOr">Instance of IAndOr</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="value">Value</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T, TReturn> OrIsNotNull<T, TReturn, TProperties>(this IAndOr<T, TReturn> andOr, Expression<Func<T, TProperties>> expression) 
            where T : class, new() where TReturn : IQuery<T>
        {
            andOr.Validate(expression);
            andOr.Add(new IsNotNull(ClassOptionsFactory.GetClassOptions(typeof(T)).Table, expression.GetColumnAttribute(), "OR"));
            return andOr;
        }
    }
}