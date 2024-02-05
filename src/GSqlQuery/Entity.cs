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
        /// <exception cref="ArgumentNullException"></exception>
        public static IJoinQueryBuilder<T, SelectQuery<T>, IFormats> Select<TProperties>(IFormats formats, Expression<Func<T, TProperties>> expression)
        {
            if (formats == null)
            {
                throw new ArgumentNullException(nameof(formats), ErrorMessages.ParameterNotNull);
            }

            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression), ErrorMessages.ParameterNotNull);
            }

            ClassOptionsTupla<IEnumerable<MemberInfo>> options = GeneralExtension.GetOptionsAndMembers(expression);
            GeneralExtension.ValidateMemberInfos(QueryType.Read, options);
            IEnumerable<string> selectMembers = options.MemberInfo.Select(x => x.Name);
            return new SelectQueryBuilder<T>(selectMembers, formats);
        }

        /// <summary>
        /// Select query
        /// </summary>
        /// <param name="formats">Formats</param>
        /// <returns>Bulder</returns>
        public static IJoinQueryBuilder<T, SelectQuery<T>, IFormats> Select(IFormats formats)
        {
            if (formats == null)
            {
                throw new ArgumentNullException(nameof(formats), ErrorMessages.ParameterNotNull);
            }
            IEnumerable<PropertyOptions> propertyOptions = ClassOptionsFactory.GetClassOptions(typeof(T)).PropertyOptions;
            return new SelectQueryBuilder<T>(propertyOptions, formats);
        }

        /// <summary>
        /// Insert query
        /// </summary>
        /// <param name="formats">Formats</param>
        /// <returns>Bulder</returns>
        public IQueryBuilder<InsertQuery<T>, IFormats> Insert(IFormats formats)
        {
            if (formats == null)
            {
                throw new ArgumentNullException(nameof(formats), ErrorMessages.ParameterNotNull);
            }

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
            if (formats == null)
            {
                throw new ArgumentNullException(nameof(formats), ErrorMessages.ParameterNotNull);
            }

            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), ErrorMessages.ParameterNotNullEmpty);
            }
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
            if (formats == null)
            {
                throw new ArgumentNullException(nameof(formats), ErrorMessages.ParameterNotNull);
            }

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
            if (formats == null)
            {
                throw new ArgumentNullException(nameof(formats), ErrorMessages.ParameterNotNull);
            }
            ClassOptionsTupla<IEnumerable<MemberInfo>> options = GeneralExtension.GetOptionsAndMembers(expression);
            GeneralExtension.ValidateMemberInfos(QueryType.Update, options);
            return new UpdateQueryBuilder<T>(formats, this, options.MemberInfo.Select(x => x.Name));
        }

        /// <summary>
        /// Delete query
        /// </summary>
        /// <param name="formats">formats</param>
        /// <returns>Bulder</returns>
        public static IQueryBuilderWithWhere<T, DeleteQuery<T>, IFormats> Delete(IFormats formats)
        {
            if (formats == null)
            {
                throw new ArgumentNullException(nameof(formats), ErrorMessages.ParameterNotNull);
            }
            return new DeleteQueryBuilder<T>(formats);
        }
    }
}