using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace GSqlQuery.Extensions
{
    /// <summary>
    /// Expression Extension
    /// </summary>
    internal static class ExpressionExtension
    {
        /// <summary>
        /// Get members information
        /// </summary>
        /// <typeparam name="T">Type of class</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="expression">Expression to evaluate</param>
        /// <returns>IEnumerable of MemberInfo</returns>
		internal static IEnumerable<MemberInfo> GetMembers<T, TProperties>(Expression<Func<T, TProperties>> expression)
        {
            Expression withoutUnary = expression.Body is UnaryExpression unaryExpression ? unaryExpression.Operand : expression.Body;

            if (withoutUnary.NodeType == ExpressionType.MemberAccess && withoutUnary is MemberExpression memberExpression)
            {
                return [memberExpression.Member];
            }
            else if (withoutUnary.NodeType == ExpressionType.New && withoutUnary is NewExpression newExpression && newExpression.Members != null)
            {
                return newExpression.Members;
            }

            return []; 
        }

        /// <summary>
        /// Get member information
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProperties"></typeparam>
        /// <param name="expression"></param>
        /// <returns>MemberInfo</returns>
        /// <exception cref="InvalidOperationException"></exception>
        internal static MemberInfo GetMember<T, TProperties>(Expression<Func<T, TProperties>> expression)
        {
            Expression withoutUnary = expression.Body is UnaryExpression unaryExpression ? unaryExpression.Operand : expression.Body;

            if (withoutUnary.NodeType == ExpressionType.MemberAccess && withoutUnary is MemberExpression memberExpression)
            {
                return memberExpression.Member;
            }

            throw new InvalidOperationException($"Could not infer property name for expression.");
        }

        /// <summary>
        /// Get ColumnAttribute
        /// </summary>
        /// <typeparam name="T">Type of class</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="expression">Expression to evaluate</param>
        /// <returns>ColumnAttribute</returns>
        /// <exception cref="InvalidOperationException"></exception>
        internal static ClassOptionsTupla<ColumnAttribute> GetColumnAttribute<T, TProperties>(Expression<Func<T, TProperties>> expression)
        {
            MemberInfo memberInfo = GetMember(expression);
            ClassOptions options = ClassOptionsFactory.GetClassOptions(memberInfo.DeclaringType);
            PropertyOptions propertyOptions = GeneralExtension.ValidateMemberInfo(memberInfo, options);
            return new ClassOptionsTupla<ColumnAttribute>(options, propertyOptions.ColumnAttribute);
        }
    }
}