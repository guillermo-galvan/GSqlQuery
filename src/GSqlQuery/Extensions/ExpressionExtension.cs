using GSqlQuery.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace GSqlQuery.Extensions
{
    internal static class ExpressionExtension
    {
        /// <summary>
        /// Gets the ClassOptionsTupla
        /// </summary>
        /// <typeparam name="T">Type of class</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="expression">Expression to evaluate</param>
        /// <returns>ClassOptionsTupla that match expression</returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static ClassOptionsTupla<IEnumerable<MemberInfo>> GeTQueryOptionsAndMembers<T, TProperties>(Expression<Func<T, TProperties>> expression)
        {
            IEnumerable<MemberInfo> memberInfos = GetMembers(expression);
            ClassOptions options = ClassOptionsFactory.GetClassOptions(typeof(T));
            return new ClassOptionsTupla<IEnumerable<MemberInfo>>(options, memberInfos);
        }

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
        /// Valid that the memberInfos are not empty
        /// </summary>
        /// <param name="memberInfos">MemberInfo list</param>
        /// <param name="message">Message in case of error</param>
        /// <exception cref="InvalidOperationException"></exception>
        internal static void ValidateMemberInfos(QueryType queryType, ClassOptionsTupla<IEnumerable<MemberInfo>> options)
        {
            if (!options.MemberInfo.Any())
            {
                string message = $"Could not infer property name for expression.";

                switch (queryType)
                {
                    case QueryType.Read:
                        message = $"Could not infer property name for expression. Please explicitly specify a property name by calling {options.ClassOptions.Type.Name}.Select(x => x.{options.ClassOptions.PropertyOptions.First().PropertyInfo.Name}) or {options.ClassOptions.Type.Name}.Select(x => new {{ {string.Join(",", options.ClassOptions.PropertyOptions.Select(x => $"x.{x.PropertyInfo.Name}"))} }})";
                        break;
                    case QueryType.Update:
                        message = $"Could not infer property name for expression. Please explicitly specify a property name by calling {options.ClassOptions.Type.Name}.Update(x => x.{options.ClassOptions.PropertyOptions.First().PropertyInfo.Name}) or {options.ClassOptions.Type.Name}.Update(x => new {{ {string.Join(",", options.ClassOptions.PropertyOptions.Select(x => $"x.{x.PropertyInfo.Name}"))} }})";
                        break;
                    case QueryType.Join:
                        message = $"Could not infer property name for expression.";
                        break;
                }

                throw new InvalidOperationException(message);
            }
        }

        /// <summary>
        /// Gets the property options
        /// </summary>
        /// <param name="options">Contains the class information</param>
        /// <param name="selectMember">Name of properties to search</param>
        /// <returns>Properties that match selectMember</returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static IEnumerable<PropertyOptions> GetPropertyQuery(ClassOptions options, IEnumerable<string> selectMember)
        {
            return (from prop in options.PropertyOptions
                    join sel in selectMember on prop.PropertyInfo.Name equals sel
                    select prop).ToArray();
        }

        /// <summary>
        /// Gets the property options
        /// </summary>
        /// <param name="options">Contains the class information</param>
        /// <param name="selectMember">Name of properties to search</param>
        /// <returns>Properties that match selectMember</returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static IEnumerable<PropertyOptions> GetPropertyQuery(ClassOptionsTupla<IEnumerable<MemberInfo>> classOptionsTupla)
        {
            return (from prop in classOptionsTupla.ClassOptions.PropertyOptions
                    join sel in classOptionsTupla.MemberInfo on prop.PropertyInfo.Name equals sel.Name
                    select prop).ToArray();
        }

        /// <summary>
        /// Gets the value of the entity
        /// </summary>
        /// <param name="options">PropertyOptions</param>
        /// <param name="entity">Entity</param>
        /// <returns>property value</returns>
        internal static object GetValue(PropertyOptions options, object entity)
        {
            return options.PropertyInfo.GetValue(entity, null) ?? DBNull.Value;
        }

        /// <summary>
        /// Gets the ClassOptionsTupla
        /// </summary>
        /// <typeparam name="T">Type of class</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="expression">Expression to evaluate</param>
        /// <returns>ClassOptionsTupla that match expression</returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static ClassOptionsTupla<MemberInfo> GetOptionsAndMember<T, TProperties>(Expression<Func<T, TProperties>> expression)
        {
            MemberInfo memberInfos = GetMember(expression);
            ClassOptions options = ClassOptionsFactory.GetClassOptions(typeof(T));

            return new ClassOptionsTupla<MemberInfo>(options, memberInfos);
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
        /// Gets the ColumnAttribute
        /// </summary>
        /// <param name="options">Contains the class information</param>
        /// <param name="selectMember">Name of properties to search</param>
        /// <returns>ColumnAttribute that match selectMember</returns>
        internal static IEnumerable<ColumnAttribute> GetColumnsQuery(ClassOptions options, IEnumerable<string> selectMember)
        {
            return from prop in options.PropertyOptions
                   join sel in selectMember on prop.PropertyInfo.Name equals sel
                   select prop.ColumnAttribute;
        }

        /// <summary>
        /// Gets the ColumnAttribute
        /// </summary>
        /// <param name="options">Contains the class information</param>
        /// <param name="selectMember">Name of properties to search</param>
        /// <returns>ColumnAttribute that match selectMember</returns>
        internal static ColumnAttribute GetColumnQuery(ClassOptionsTupla<MemberInfo> classOptionsTupla)
        {
            return classOptionsTupla.ClassOptions.PropertyOptions.First(x => x.PropertyInfo.Name == classOptionsTupla.MemberInfo.Name).ColumnAttribute;
        }

        /// <summary>
        /// Gets the property option
        /// </summary>
        /// <param name="memberInfo">MemberInfo</param>
        /// <param name="options">Contains the class information</param>
        /// <returns>PropertyOptions</returns>
        /// <exception cref="InvalidOperationException"></exception>
        internal static PropertyOptions ValidateMemberInfo(MemberInfo memberInfo, ClassOptions options)
        {
            PropertyOptions result = options.PropertyOptions.FirstOrDefault(x => x.PropertyInfo.Name == memberInfo.Name);
            return result ?? throw new InvalidOperationException($"Could not find property {memberInfo.Name} on type {options.Type.Name}");
        }

        /// <summary>
        /// Gets the ClassOptionsTupla
        /// </summary>
        /// <typeparam name="T">Type of class</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="expression">Expression to evaluate</param>
        /// <returns>ClassOptionsTupla that match expression</returns>
        internal static ClassOptionsTupla<IEnumerable<MemberInfo>> GetOptionsAndMembers<T, TProperties>(Expression<Func<T, TProperties>> expression, ClassOptions options)
        {
            IEnumerable<MemberInfo> memberInfos = GetMembers(expression);
            return new ClassOptionsTupla<IEnumerable<MemberInfo>>(options, memberInfos);
        }

        /// <summary>
        /// Gets the ClassOptionsTupla
        /// </summary>
        /// <typeparam name="T">Type of class</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="expression">Expression to evaluate</param>
        /// <returns>ClassOptionsTupla that match expression</returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static ClassOptionsTupla<IEnumerable<MemberInfo>> GetOptionsAndMembers<T, TProperties>(Expression<Func<T, TProperties>> expression)
        {
            IEnumerable<MemberInfo> memberInfos = GetMembers(expression);
            ClassOptions options = ClassOptionsFactory.GetClassOptions(typeof(T));
            return new ClassOptionsTupla<IEnumerable<MemberInfo>>(options, memberInfos);
        }

        /// <summary>
        /// Gets the JoinCriteriaPart
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="expression">Expression to evaluate</param>
        /// <returns>JoinCriteriaPart</returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static JoinCriteriaPart GetJoinColumn<T1, T2, TProperties>(Expression<Func<Join<T1, T2>, TProperties>> expression)
            where T1 : class
            where T2 : class
        {
            MemberInfo memberInfos = ExpressionExtension.GetMember(expression);
            ClassOptions options = ClassOptionsFactory.GetClassOptions(memberInfos.ReflectedType);
            ColumnAttribute columnAttribute = options.PropertyOptions.First(x => x.PropertyInfo.Name == memberInfos.Name).ColumnAttribute;

            return new JoinCriteriaPart(columnAttribute, options.Table, memberInfos); ;
        }

        /// <summary>
        /// Gets the JoinCriteriaPart
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="T3">Table type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="expression">Expression to evaluate</param>
        /// <returns>JoinCriteriaPart</returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static JoinCriteriaPart GetJoinColumn<T1, T2, T3, TProperties>(Expression<Func<Join<T1, T2, T3>, TProperties>> expression)
            where T1 : class
            where T2 : class
            where T3 : class
        {
            MemberInfo memberInfos = ExpressionExtension.GetMember(expression);
            ClassOptions options = ClassOptionsFactory.GetClassOptions(memberInfos.ReflectedType);
            ColumnAttribute columnAttribute = options.PropertyOptions.First(x => x.PropertyInfo.Name == memberInfos.Name).ColumnAttribute;

            return new JoinCriteriaPart(columnAttribute, options.Table, memberInfos);
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
            PropertyOptions propertyOptions = ValidateMemberInfo(memberInfo, options);
            return new ClassOptionsTupla<ColumnAttribute>(options, propertyOptions.ColumnAttribute);
        }
    }
}
