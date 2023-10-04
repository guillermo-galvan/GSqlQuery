using GSqlQuery.Extensions;
using GSqlQuery.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace GSqlQuery
{
    /// <summary>
    /// Entity 
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public abstract class Entity<T> : ICreate<T>, IRead<T>, IUpdate<T>, IDelete<T> where T : class
    {
        /// <summary>
        /// Select query
        /// </summary>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="formats">Formats</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <returns>Bulder</returns>
        public static IJoinQueryBuilder<T, SelectQuery<T>, IFormats> Select<TProperties>(IFormats formats, Expression<Func<T, TProperties>> expression)
        {
            formats.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(formats));
            ClassOptionsTupla<IEnumerable<MemberInfo>> options = expression.GetOptionsAndMembers();
            options.MemberInfo.ValidateMemberInfos($"Could not infer property name for expression. Please explicitly specify a property name by calling {options.ClassOptions.Type.Name}.Select(x => x.{options.ClassOptions.PropertyOptions.First().PropertyInfo.Name}) or {options.ClassOptions.Type.Name}.Select(x => new {{ {string.Join(",", options.ClassOptions.PropertyOptions.Select(x => $"x.{x.PropertyInfo.Name}"))} }})");
            return new SelectQueryBuilder<T>(options.MemberInfo.Select(x => x.Name), formats);
        }

        /// <summary>
        /// Select query
        /// </summary>
        /// <param name="formats">Formats</param>
        /// <returns>Bulder</returns>
        public static IJoinQueryBuilder<T, SelectQuery<T>, IFormats> Select(IFormats formats)
        {
            formats.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(formats));
            return new SelectQueryBuilder<T>(ClassOptionsFactory.GetClassOptions(typeof(T)).PropertyOptions.Select(x => x.PropertyInfo.Name), formats);
        }

        /// <summary>
        /// Insert query
        /// </summary>
        /// <param name="formats">Formats</param>
        /// <returns>Bulder</returns>
        public IQueryBuilder<InsertQuery<T>, IFormats> Insert(IFormats formats)
        {
            formats.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(formats));
            return new InsertQueryBuilder<T>(formats, this);
        }

        /// <summary>
        /// Insert query
        /// </summary>
        /// <param name="formats">Formats</param>
        /// <param name="entity">Entity</param>
        /// <returns>Bulder</returns>
        public static IQueryBuilder<InsertQuery<T>, IFormats> Insert(IFormats formats, T entity)
        {
            formats.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(formats));
            entity.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(entity));
            return new InsertQueryBuilder<T>(formats, entity);
        }

        /// <summary>
        /// Update query
        /// </summary>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="formats">Formats</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="value">Value</param>
        /// <returns>Instance of ISet</returns>
        public static ISet<T, UpdateQuery<T>, IFormats> Update<TProperties>(IFormats formats, Expression<Func<T, TProperties>> expression, TProperties value)
        {
            formats.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(formats));
            ClassOptionsTupla<MemberInfo> options = expression.GetOptionsAndMember();
            options.MemberInfo.ValidateMemberInfo(options.ClassOptions);
            return new UpdateQueryBuilder<T>(formats, new string[] { options.MemberInfo.Name }, value);
        }

        /// <summary>
        /// Update query
        /// </summary>
        /// <typeparam name="TProperties">The property or properties for the query</typeparam>
        /// <param name="formats">Formats</param>
        /// <param name="expression">The expression representing the property or properties</param>
        /// <returns>Instance of ISet</returns>
        public ISet<T, UpdateQuery<T>, IFormats> Update<TProperties>(IFormats formats, Expression<Func<T, TProperties>> expression)
        {
            formats.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(formats));
            ClassOptionsTupla<IEnumerable<MemberInfo>> options = expression.GetOptionsAndMembers();
            options.MemberInfo.ValidateMemberInfos($"Could not infer property name for expression. Please explicitly specify a property name by calling {options.ClassOptions.Type.Name}.Update(x => x.{options.ClassOptions.PropertyOptions.First().PropertyInfo.Name}) or {options.ClassOptions.Type.Name}.Update(x => new {{ {string.Join(",", options.ClassOptions.PropertyOptions.Select(x => $"x.{x.PropertyInfo.Name}"))} }})");
            return new UpdateQueryBuilder<T>(formats, this, options.MemberInfo.Select(x => x.Name));
        }

        /// <summary>
        /// Delete query
        /// </summary>
        /// <param name="formats">formats</param>
        /// <returns>Bulder</returns>
        public static IQueryBuilderWithWhere<T, DeleteQuery<T>, IFormats> Delete(IFormats formats)
        {
            formats.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(formats));
            return new DeleteQueryBuilder<T>(formats);
        }
    }
}