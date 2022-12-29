﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace GSqlQuery.Extensions
{
    public class ClassOptionsTupla<T>
    {
        public ClassOptions ClassOptions { get; set; }

        public T MemberInfo { get; set; }

        public ClassOptionsTupla(ClassOptions classOptions, T memberInfo)
        {
            ClassOptions = classOptions;
            MemberInfo = memberInfo;
        }
    }

    public static class GeneralExtension
    {
        public static IEnumerable<PropertyOptions> GetPropertyQuery(this ClassOptions options, IEnumerable<string> selectMember)
        {
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            return (from prop in options.PropertyOptions
                    join sel in selectMember on prop.PropertyInfo.Name equals sel
                    select prop).ToArray();
        }

        public static IEnumerable<ColumnAttribute> GetColumnsQuery(this ClassOptions options, IEnumerable<string> selectMember)
        {
            return (from prop in options.PropertyOptions
                    join sel in selectMember on prop.PropertyInfo.Name equals sel
                    select prop.ColumnAttribute).ToArray();
        }

        public static ClassOptionsTupla<IEnumerable<MemberInfo>> GetOptionsAndMembers<T, TProperties>(this Expression<Func<T, TProperties>> expression)
        {
            expression.NullValidate(ErrorMessages.ParameterNotNull, nameof(expression));

            IEnumerable<MemberInfo> memberInfos = expression.GetMembers();
            ClassOptions options = ClassOptionsFactory.GetClassOptions(typeof(T));

            return new ClassOptionsTupla<IEnumerable<MemberInfo>>(options, memberInfos);
        }

        public static ClassOptionsTupla<MemberInfo> GetOptionsAndMember<T, TProperties>(this Expression<Func<T, TProperties>> expression)
        {
            expression.NullValidate(ErrorMessages.ParameterNotNull, nameof(expression));

            MemberInfo memberInfos = expression.GetMember();
            ClassOptions options = ClassOptionsFactory.GetClassOptions(typeof(T));

            return new ClassOptionsTupla<MemberInfo>(options, memberInfos);
        }

        public static void ValidateMemberInfos(this IEnumerable<MemberInfo> memberInfos, string message)
        {
            if (!memberInfos.Any())
            {
                throw new InvalidOperationException(message);
            }
        }

        public static PropertyOptions ValidateMemberInfo(this MemberInfo memberInfo, ClassOptions options)
        {
            PropertyOptions result = options.PropertyOptions.FirstOrDefault(x => x.PropertyInfo.Name == memberInfo.Name);

            if (result == null)
            {
                throw new InvalidOperationException($"Could not find property {memberInfo.Name} on type {options.Type.Name}");
            }

            return result;
        }

        public static object GetValue(this PropertyOptions options, object entity)
        {
            return options.PropertyInfo.GetValue(entity, null) ?? DBNull.Value;
        }
    }
}