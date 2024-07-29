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
        internal static ClassOptionsTupla<PropertyOptionsCollection> GeTQueryOptionsAndMembers<T, TProperties>(Expression<Func<T, TProperties>> expression)
        {
            PropertyOptionsCollection valuePairs = GetPropertyOptionsCollections(expression);
            ClassOptions options = ClassOptionsFactory.GetClassOptions(typeof(T));
            return new ClassOptionsTupla<PropertyOptionsCollection>(options, valuePairs);
        }

        /// <summary>
        /// Gets the ClassOptionsTupla
        /// </summary>
        /// <typeparam name="T">Type of class</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="dynamicQuery">Expression to evaluate</param>
        /// <returns>ClassOptionsTupla that match expression</returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static ClassOptionsTupla<PropertyOptionsCollection> GeTQueryOptionsAndMembersByFunc(DynamicQuery dynamicQuery)
        {
            PropertyOptionsCollection valuePairs = GetPropertyOptionsCollectionsByFunc(dynamicQuery);
            ClassOptions options = ClassOptionsFactory.GetClassOptions(dynamicQuery.Entity);
            return new ClassOptionsTupla<PropertyOptionsCollection>(options, valuePairs);
        }

        /// <summary>
        /// Get members information
        /// </summary>
        /// <typeparam name="T">Type of class</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="func">Expression to evaluate</param>
        /// <returns>IEnumerable of PropertyOptionsCollection</returns>
		internal static PropertyOptionsCollection GetPropertyOptionsCollectionsByFunc(DynamicQuery dynamicQuery)
        {
            PropertyInfo[] properties = dynamicQuery.Properties.GetProperties();
            

            if (properties.Length == 0 || dynamicQuery.Properties == dynamicQuery.Entity || dynamicQuery.Properties.IsPrimitive)
            {
                throw new InvalidOperationException();
            }

            ClassOptions classOptions = ClassOptionsFactory.GetClassOptions(dynamicQuery.Entity);
            List<KeyValuePair<string, PropertyOptions>> result = [];

            foreach (PropertyInfo item in properties)
            {
                result.Add(new KeyValuePair<string, PropertyOptions>(item.Name, classOptions.PropertyOptions[item.Name]));
            }

            return new PropertyOptionsCollection(result);
        }

        /// <summary>
        /// Get members information
        /// </summary>
        /// <typeparam name="T">Type of class</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="expression">Expression to evaluate</param>
        /// <returns>IEnumerable of PropertyOptionsCollection</returns>
		internal static PropertyOptionsCollection GetPropertyOptionsCollections<T, TProperties>(Expression<Func<T, TProperties>> expression)
        {
            Expression withoutUnary = expression.Body is UnaryExpression unaryExpression ? unaryExpression.Operand : expression.Body;

            if (withoutUnary.NodeType == ExpressionType.MemberAccess && withoutUnary is MemberExpression memberExpression)
            {
                return new PropertyOptionsCollection([new KeyValuePair<string, PropertyOptions>(memberExpression.Member.Name, ClassOptionsFactory.GetClassOptions(memberExpression.Expression.Type).PropertyOptions[memberExpression.Member.Name])]);
            }
            else if (withoutUnary.NodeType == ExpressionType.New && withoutUnary is NewExpression newExpression && newExpression.Members != null)
            {
                List<KeyValuePair<string, PropertyOptions>> result = [];

                foreach (Expression item in newExpression.Arguments)
                {
                    if (item is MemberExpression member  && (member.Expression.NodeType == ExpressionType.Parameter || member.Expression.NodeType == ExpressionType.MemberAccess))
                    {
                        result.Add(new KeyValuePair<string, PropertyOptions>(member.Member.Name, ClassOptionsFactory.GetClassOptions(member.Expression.Type).PropertyOptions[member.Member.Name]));
                    }
                    else
                    {
                        throw new InvalidOperationException();
                    }
                }

                return new PropertyOptionsCollection(result);
            }

            return null;
        }

        /// <summary>
        /// Valid that the ClassOptionsTupla are not empty
        /// </summary>
        /// <param name="queryType">Query Type</param>
        /// <param name="options">ClassOptionsTupla</param>
        /// <exception cref="InvalidOperationException"></exception>
        internal static void ValidateClassOptionsTupla(QueryType queryType, ClassOptionsTupla<PropertyOptionsCollection> options)
        {
            if (options.Columns == null || options.Columns.Count == 0)
            {
                string message = $"Could not infer property name for expression.";

                switch (queryType)
                {
                    case QueryType.Read:
                        message = $"Could not infer property name for expression. Please explicitly specify a property name by calling {options.ClassOptions.Type.Name}.Select(x => x.{options.ClassOptions.PropertyOptions.First().PropertyInfo.Name}) or {options.ClassOptions.Type.Name}.Select(x => new {{ {string.Join(",", options.ClassOptions.PropertyOptions.Keys.Select(x => $"x.{x}"))} }})";
                        break;
                    case QueryType.Update:
                        message = $"Could not infer property name for expression. Please explicitly specify a property name by calling {options.ClassOptions.Type.Name}.Update(x => x.{options.ClassOptions.PropertyOptions.First().PropertyInfo.Name}) or {options.ClassOptions.Type.Name}.Update(x => new {{ {string.Join(",", options.ClassOptions.PropertyOptions.Keys.Select(x => $"x.{x}"))} }})";
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
        internal static PropertyOptionsCollection GetPropertyQuery(ClassOptions options, IEnumerable<string> selectMember)
        {
            var columns = (from prop in options.PropertyOptions
                           join sel in selectMember on prop.Key equals sel
                           select new KeyValuePair<string, PropertyOptions>(prop.Key, prop.Value)).ToArray();

            return new PropertyOptionsCollection(columns);
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
        internal static ClassOptionsTupla<KeyValuePair<string, PropertyOptions>> GetOptionsAndMember<T, TProperties>(Expression<Func<T, TProperties>> expression)
        {
            KeyValuePair<string, PropertyOptions> keyValue = GetKeyValue(expression);
            ClassOptions options = ClassOptionsFactory.GetClassOptions(typeof(T));

            return new ClassOptionsTupla<KeyValuePair<string, PropertyOptions>>(options, keyValue);
        }

        /// <summary>
        /// Get Column information
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProperties"></typeparam>
        /// <param name="expression"></param>
        /// <returns>KeyValuePair<string, PropertyOptions></returns>
        /// <exception cref="InvalidOperationException"></exception>
        internal static KeyValuePair<string, PropertyOptions> GetKeyValue<T, TProperties>(Expression<Func<T, TProperties>> expression)
        {
            Expression withoutUnary = expression.Body is UnaryExpression unaryExpression ? unaryExpression.Operand : expression.Body;

            if (withoutUnary.NodeType == ExpressionType.MemberAccess && withoutUnary is MemberExpression memberExpression)
            {
                return new KeyValuePair<string, PropertyOptions> (memberExpression.Member.Name, ClassOptionsFactory.GetClassOptions(memberExpression.Expression.Type).PropertyOptions[memberExpression.Member.Name]);
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
                   join sel in selectMember on prop.Key equals sel
                   select prop.Value.ColumnAttribute;
        }

        /// <summary>
        /// Gets the ClassOptionsTupla
        /// </summary>
        /// <typeparam name="T">Type of class</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="expression">Expression to evaluate</param>
        /// <returns>ClassOptionsTupla that match expression</returns>
        internal static ClassOptionsTupla<PropertyOptionsCollection> GetClassOptionsTuplaColumns<T, TProperties>(Expression<Func<T, TProperties>> expression, ClassOptions options)
        {
            PropertyOptionsCollection valuePairs = GetPropertyOptionsCollections(expression);
            return new ClassOptionsTupla<PropertyOptionsCollection>(options, valuePairs);
        }

        /// <summary>
        /// Gets the ClassOptionsTupla
        /// </summary>
        /// <typeparam name="T">Type of class</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="expression">Expression to evaluate</param>
        /// <returns>ClassOptionsTupla that match expression</returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static ClassOptionsTupla<PropertyOptionsCollection> GetOptionsAndMembers<T, TProperties>(Expression<Func<T, TProperties>> expression)
        {
            PropertyOptionsCollection valuePairs = GetPropertyOptionsCollections(expression);
            ClassOptions options = ClassOptionsFactory.GetClassOptions(typeof(T));
            return new ClassOptionsTupla<PropertyOptionsCollection>(options, valuePairs);
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
            KeyValuePair<string, PropertyOptions> keyValue = GetKeyValue(expression);
            ClassOptions options = ClassOptionsFactory.GetClassOptions(keyValue.Value.PropertyInfo.ReflectedType);
            return new JoinCriteriaPart(options, keyValue); ;
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
            KeyValuePair<string, PropertyOptions> keyValue = GetKeyValue(expression);
            ClassOptions options = ClassOptionsFactory.GetClassOptions(keyValue.Value.PropertyInfo.ReflectedType);

            return new JoinCriteriaPart(options, keyValue);
        }

        /// <summary>
        /// Get ColumnAttribute
        /// </summary>
        /// <typeparam name="T">Type of class</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="expression">Expression to evaluate</param>
        /// <returns>ColumnAttribute</returns>
        /// <exception cref="InvalidOperationException"></exception>
        internal static ClassOptionsTupla<PropertyOptions> GetColumnAttribute<T, TProperties>(Expression<Func<T, TProperties>> expression)
        {
            KeyValuePair<string, PropertyOptions> keyValue = GetKeyValue(expression);
            ClassOptions options = ClassOptionsFactory.GetClassOptions(keyValue.Value.PropertyInfo.ReflectedType);
            return new ClassOptionsTupla<PropertyOptions>(options, keyValue.Value);
        }
    }
}
