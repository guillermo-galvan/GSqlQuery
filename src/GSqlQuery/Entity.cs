using GSqlQuery.Extensions;
using GSqlQuery.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace GSqlQuery
{
    public abstract class Entity<T> : ICreate<T>, IRead<T>, IUpdate<T>, IDelete<T> where T : class
    {
        public static IJoinQueryBuilder<T, SelectQuery<T>, IFormats> Select<TProperties>(IFormats statements, Expression<Func<T, TProperties>> expression)
        {
            statements.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(statements));
            ClassOptionsTupla<IEnumerable<MemberInfo>> options = expression.GetOptionsAndMembers();
            options.MemberInfo.ValidateMemberInfos($"Could not infer property name for expression. Please explicitly specify a property name by calling {options.ClassOptions.Type.Name}.Select(x => x.{options.ClassOptions.PropertyOptions.First().PropertyInfo.Name}) or {options.ClassOptions.Type.Name}.Select(x => new {{ {string.Join(",", options.ClassOptions.PropertyOptions.Select(x => $"x.{x.PropertyInfo.Name}"))} }})");
            return new SelectQueryBuilder<T>(options.MemberInfo.Select(x => x.Name), statements);
        }

        public static IJoinQueryBuilder<T, SelectQuery<T>, IFormats> Select(IFormats statements)
        {
            statements.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(statements));
            return new SelectQueryBuilder<T>(ClassOptionsFactory.GetClassOptions(typeof(T)).PropertyOptions.Select(x => x.PropertyInfo.Name), statements);
        }


        public IQueryBuilder<InsertQuery<T>, IFormats> Insert(IFormats statements)
        {
            statements.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(statements));
            return new InsertQueryBuilder<T>(statements, this);
        }

        public static IQueryBuilder<InsertQuery<T>, IFormats> Insert(IFormats statements, T entity)
        {
            statements.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(statements));
            entity.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(entity));
            return new InsertQueryBuilder<T>(statements, entity);
        }

        public static ISet<T, UpdateQuery<T>, IFormats> Update<TProperties>(IFormats statements, Expression<Func<T, TProperties>> expression, TProperties value)
        {
            statements.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(statements));
            ClassOptionsTupla<MemberInfo> options = expression.GetOptionsAndMember();
            options.MemberInfo.ValidateMemberInfo(options.ClassOptions);
            return new UpdateQueryBuilder<T>(statements, new string[] { options.MemberInfo.Name }, value);
        }

        /// <summary>
        /// Generate the update query
        /// </summary>
        /// <typeparam name="TProperties">The property or properties for the query</typeparam>
        /// <param name="key">The name of the statement collection</param>
        /// <param name="expression">The expression representing the property or properties</param>
        /// <returns>Instance of ISet</returns>
        public ISet<T, UpdateQuery<T>, IFormats> Update<TProperties>(IFormats statements, Expression<Func<T, TProperties>> expression)
        {
            statements.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(statements));
            ClassOptionsTupla<IEnumerable<MemberInfo>> options = expression.GetOptionsAndMembers();
            options.MemberInfo.ValidateMemberInfos($"Could not infer property name for expression. Please explicitly specify a property name by calling {options.ClassOptions.Type.Name}.Update(x => x.{options.ClassOptions.PropertyOptions.First().PropertyInfo.Name}) or {options.ClassOptions.Type.Name}.Update(x => new {{ {string.Join(",", options.ClassOptions.PropertyOptions.Select(x => $"x.{x.PropertyInfo.Name}"))} }})");
            return new UpdateQueryBuilder<T>(statements, this, options.MemberInfo.Select(x => x.Name));
        }

        public static IQueryBuilderWithWhere<T, DeleteQuery<T>, IFormats> Delete(IFormats statements)
        {
            statements.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(statements));
            return new DeleteQueryBuilder<T>(statements);
        }
    }
}