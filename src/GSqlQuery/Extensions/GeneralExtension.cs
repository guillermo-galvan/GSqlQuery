using GSqlQuery.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace GSqlQuery.Extensions
{
    internal static class GeneralExtension
    {
        internal static IEnumerable<PropertyOptions> GetPropertyQuery(this ClassOptions options, IEnumerable<string> selectMember)
        {
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            return (from prop in options.PropertyOptions
                    join sel in selectMember on prop.PropertyInfo.Name equals sel
                    select prop).ToArray();
        }

        internal static IEnumerable<PropertyOptions> GetPropertyQuery(this ClassOptionsTupla<IEnumerable<MemberInfo>> optionsTupla)
        {
            optionsTupla.NullValidate(ErrorMessages.ParameterNotNull, nameof(optionsTupla));

            List<PropertyOptions> properties = new List<PropertyOptions>();
            var listName = optionsTupla.MemberInfo.Select(x => x.Name);

            if (optionsTupla.MemberInfo.Any(x => x.DeclaringType.IsGenericType))
            {
                foreach (var item in optionsTupla.ClassOptions.PropertyOptions.Where(x => x.PropertyInfo.PropertyType.IsClass))
                {
                    var classOptions = ClassOptionsFactory.GetClassOptions(item.PropertyInfo.PropertyType);
                    var a = classOptions.PropertyOptions.Where(x => listName.Contains(x.PropertyInfo.Name));

                    properties.AddRange(a);
                }
            }
            else
            {
                foreach (var item in optionsTupla.MemberInfo)
                {
                    var classOptions = ClassOptionsFactory.GetClassOptions(item.DeclaringType);
                    var a = classOptions.PropertyOptions.Where(x => listName.Contains(x.PropertyInfo.Name));

                    properties.AddRange(a);
                }
            }

            return properties;
        }

        internal static IEnumerable<ColumnAttribute> GetColumnsQuery(this ClassOptions options, IEnumerable<string> selectMember)
        {
            return (from prop in options.PropertyOptions
                    join sel in selectMember on prop.PropertyInfo.Name equals sel
                    select prop.ColumnAttribute).ToArray();
        }

        internal static ClassOptionsTupla<IEnumerable<MemberInfo>> GetOptionsAndMembers<T, TProperties>(this Expression<Func<T, TProperties>> expression)
        {
            expression.NullValidate(ErrorMessages.ParameterNotNull, nameof(expression));

            IEnumerable<MemberInfo> memberInfos = expression.GetMembers();
            ClassOptions options = ClassOptionsFactory.GetClassOptions(typeof(T));

            return new ClassOptionsTupla<IEnumerable<MemberInfo>>(options, memberInfos);
        }

        internal static ClassOptionsTupla<MemberInfo> GetOptionsAndMember<T, TProperties>(this Expression<Func<T, TProperties>> expression)
        {
            expression.NullValidate(ErrorMessages.ParameterNotNull, nameof(expression));

            MemberInfo memberInfos = expression.GetMember();
            ClassOptions options = ClassOptionsFactory.GetClassOptions(typeof(T));

            return new ClassOptionsTupla<MemberInfo>(options, memberInfos);
        }

        internal static void ValidateMemberInfos(this IEnumerable<MemberInfo> memberInfos, string message)
        {
            if (!memberInfos.Any())
            {
                throw new InvalidOperationException(message);
            }
        }

        internal static PropertyOptions ValidateMemberInfo(this MemberInfo memberInfo, ClassOptions options)
        {
            PropertyOptions result = options.PropertyOptions.FirstOrDefault(x => x.PropertyInfo.Name == memberInfo.Name);
            return result ?? throw new InvalidOperationException($"Could not find property {memberInfo.Name} on type {options.Type.Name}");
        }

        internal static object GetValue(this PropertyOptions options, object entity)
        {
            return options.PropertyInfo.GetValue(entity, null) ?? DBNull.Value;
        }

        internal static JoinCriteriaPart GetJoinColumn<T1, T2, TProperties>(this Expression<Func<Join<T1, T2>, TProperties>> expression)
            where T1 : class
            where T2 : class
        {
            expression.NullValidate(ErrorMessages.ParameterNotNull, nameof(expression));
            MemberInfo memberInfos = expression.GetMember();
            ClassOptions options = ClassOptionsFactory.GetClassOptions(memberInfos.ReflectedType);
            ColumnAttribute columnAttribute = options.PropertyOptions.First(x => x.PropertyInfo.Name == memberInfos.Name).ColumnAttribute;

            return new JoinCriteriaPart()
            {
                Column = columnAttribute,
                Table = options.Table,
                MemberInfo = memberInfos,
            };
        }

        internal static JoinCriteriaPart GetJoinColumn<T1, T2, T3, TProperties>(this Expression<Func<Join<T1, T2, T3>, TProperties>> expression)
            where T1 : class
            where T2 : class
            where T3 : class
        {
            expression.NullValidate(ErrorMessages.ParameterNotNull, nameof(expression));
            MemberInfo memberInfos = expression.GetMember();
            ClassOptions options = ClassOptionsFactory.GetClassOptions(memberInfos.ReflectedType);
            ColumnAttribute columnAttribute = options.PropertyOptions.First(x => x.PropertyInfo.Name == memberInfos.Name).ColumnAttribute;

            return new JoinCriteriaPart()
            {
                Column = columnAttribute,
                Table = options.Table,
                MemberInfo = memberInfos,
            };
        }
    }
}