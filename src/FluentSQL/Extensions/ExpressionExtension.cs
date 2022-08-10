using System.Linq.Expressions;
using System.Reflection;

namespace FluentSQL.Extensions
{
    internal static class ExpressionExtension
    {
		public static IEnumerable<MemberInfo> GetMembers<T, TProperties>(this Expression<Func<T, TProperties>> expression)
		{
            Expression withoutUnary = RemoveUnary(expression.Body);

            List<MemberInfo> result = new();

            if (withoutUnary.NodeType == ExpressionType.MemberAccess && withoutUnary is MemberExpression memberExpression)
            {
                result.Add(memberExpression.Member);
            }
            else if (withoutUnary.NodeType == ExpressionType.New && withoutUnary is NewExpression newExpression && newExpression.Members != null)
            {
                result.AddRange(newExpression.Members);
            }

			return result;
		}

		private static Expression RemoveUnary(Expression toUnwrap)
		{
            return toUnwrap is UnaryExpression expression ? expression.Operand : toUnwrap;
        }
    }
}
