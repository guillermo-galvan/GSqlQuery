using GSqlQuery.Extensions;
using GSqlQuery.SearchCriteria;
using System;
using System.Linq.Expressions;

namespace GSqlQuery
{
    public static class EqualExtensions
    {
        public static IAndOr<T, TReturn> Equal<T, TReturn, TProperties>(this IWhere<T, TReturn> where,
           Expression<Func<T, TProperties>> expression, TProperties value)
            where T : class, new()
            where TReturn : IQuery<T>
        {
            IAndOr<T, TReturn> andor = where.GetAndOr(expression);
            var columnInfo = expression.GetColumnAttribute();
            andor.Add(new Equal<TProperties>(columnInfo.ClassOptions.Table, columnInfo.MemberInfo, value));
            return andor;
        }

        public static IAndOr<T, TReturn> AndEqual<T, TReturn, TProperties>(this IAndOr<T, TReturn> andOr, Expression<Func<T, TProperties>> expression, TProperties value)
            where T : class, new() where TReturn : IQuery<T>
        {
            andOr.Validate(expression);
            var columnInfo = expression.GetColumnAttribute();
            andOr.Add(new Equal<TProperties>(columnInfo.ClassOptions.Table, columnInfo.MemberInfo, value, "AND"));
            return andOr;
        }

        public static IAndOr<T, TReturn> OrEqual<T, TReturn, TProperties>(this IAndOr<T, TReturn> andOr, Expression<Func<T, TProperties>> expression, TProperties value)
            where T : class, new() where TReturn : IQuery<T>
        {
            andOr.Validate(expression);
            var columnInfo = expression.GetColumnAttribute();
            andOr.Add(new Equal<TProperties>(columnInfo.ClassOptions.Table, columnInfo.MemberInfo, value, "OR"));
            return andOr;
        }
    }
}