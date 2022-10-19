using FluentSQL.Helpers;
using FluentSQL.Models;
using System.Linq.Expressions;
using System.Reflection;

namespace FluentSQL.Extensions
{
    internal static class ExpressionExtension
    {
        /// <summary>
        /// Get members information
        /// </summary>
        /// <typeparam name="T">Type of class</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="expression">Expression to evaluate</param>
        /// <returns>IEnumerable of MemberInfo</returns>
		public static IEnumerable<MemberInfo> GetMembers<T, TProperties>(this Expression<Func<T, TProperties>> expression)
		{
            Expression withoutUnary = RemoveUnary(expression.Body);

            Queue<MemberInfo> result = new();

            if (withoutUnary.NodeType == ExpressionType.MemberAccess && withoutUnary is MemberExpression memberExpression)
            {
                result.Enqueue(memberExpression.Member);
            }
            else if (withoutUnary.NodeType == ExpressionType.New && withoutUnary is NewExpression newExpression && newExpression.Members != null)
            {
                foreach (var item in newExpression.Members)
                {
                    result.Enqueue(item);
                }
            }

			return result;
		}

        /// <summary>
        /// Get member information
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProperties"></typeparam>
        /// <param name="expression"></param>
        /// <returns>MemberInfo</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static MemberInfo GetMember<T, TProperties>(this Expression<Func<T, TProperties>> expression)
        {
            Expression withoutUnary = RemoveUnary(expression.Body);
            ClassOptions options = ClassOptionsFactory.GetClassOptions(typeof(T));
            MemberInfo result = null;

            if (withoutUnary.NodeType == ExpressionType.MemberAccess && withoutUnary is MemberExpression memberExpression)
            {
                result = memberExpression.Member;
            }

            return result ?? throw new InvalidOperationException($"Could not infer property name for expression.");
        }

        /// <summary>
        /// Get ColumnAttribute
        /// </summary>
        /// <typeparam name="T">Type of class</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="expression">Expression to evaluate</param>
        /// <returns>ColumnAttribute</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static ColumnAttribute GetColumnAttribute<T, TProperties>(this Expression<Func<T, TProperties>> expression)
        {
            MemberInfo memberInfo = expression.GetMember();
            ClassOptions options = ClassOptionsFactory.GetClassOptions(typeof(T));
            return memberInfo.ValidateMemberInfo(options).ColumnAttribute;
        }

        private static Expression RemoveUnary(Expression toUnwrap)
		{
            return toUnwrap is UnaryExpression expression ? expression.Operand : toUnwrap;
        }
    }
}
