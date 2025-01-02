using GSqlQuery.Queries;
using System;
using System.Linq.Expressions;

namespace GSqlQuery
{
    /// <summary>
    /// Entity 
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public abstract class Entity<T>
        where T : class
    {
        /// <summary>
        /// Select query
        /// </summary>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="queryOptions">QueryOptions</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <returns>Bulder</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IJoinQueryBuilder<T, SelectQuery<T>, QueryOptions> Select<TProperties>(QueryOptions queryOptions, Func<T, TProperties> func)
        {
            if (queryOptions == null)
            {
                throw new ArgumentNullException(nameof(queryOptions), ErrorMessages.ParameterNotNull);
            }

            if (func == null)
            {
                throw new ArgumentNullException(nameof(func), ErrorMessages.ParameterNotNull);
            }

            return new SelectQueryBuilder<T>(new DynamicQuery(typeof(T), typeof(TProperties)), queryOptions);
        }

        /// <summary>
        /// Select query
        /// </summary>
        /// <param name="queryOptions">QueryOptions</param>
        /// <returns>Bulder</returns>
        public static IJoinQueryBuilder<T, SelectQuery<T>, QueryOptions> Select(QueryOptions queryOptions)
        {
            if (queryOptions == null)
            {
                throw new ArgumentNullException(nameof(queryOptions), ErrorMessages.ParameterNotNull);
            }
            return new SelectQueryBuilder<T>(queryOptions);
        }

        /// <summary>
        /// Insert query
        /// </summary>
        /// <param name="queryOptions">QueryOptions</param>
        /// <returns>Bulder</returns>
        public IQueryBuilder<InsertQuery<T>, QueryOptions> Insert(QueryOptions queryOptions)
        {
            if (queryOptions == null)
            {
                throw new ArgumentNullException(nameof(queryOptions), ErrorMessages.ParameterNotNull);
            }

            return new InsertQueryBuilder<T>(queryOptions, this);
        }

        /// <summary>
        /// Insert query
        /// </summary>
        /// <param name="queryOptions">QueryOptions</param>
        /// <param name="entity">Entity</param>
        /// <returns>Bulder</returns>
        public static IQueryBuilder<InsertQuery<T>, QueryOptions> Insert(QueryOptions queryOptions, T entity)
        {
            if (queryOptions == null)
            {
                throw new ArgumentNullException(nameof(queryOptions), ErrorMessages.ParameterNotNull);
            }

            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), ErrorMessages.ParameterNotNullEmpty);
            }
            return new InsertQueryBuilder<T>(queryOptions, entity);
        }

        /// <summary>
        /// Update query
        /// </summary>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="queryOptions">QueryOptions</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="value">Value</param>
        /// <returns>Instance of ISet</returns>
        public static ISet<T, UpdateQuery<T>, QueryOptions> Update<TProperties>(QueryOptions queryOptions, Expression<Func<T, TProperties>> expression, TProperties value)
        {
            if (queryOptions == null)
            {
                throw new ArgumentNullException(nameof(queryOptions), ErrorMessages.ParameterNotNull);
            }

            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression), ErrorMessages.ParameterNotNull);
            }

            return new UpdateQueryBuilder<T>(queryOptions, expression, value);
        }

        /// <summary>
        /// Update query
        /// </summary>
        /// <typeparam name="TProperties">The property or properties for the query</typeparam>
        /// <param name="queryOptions">QueryOptions</param>
        /// <param name="expression">The expression representing the property or properties</param>
        /// <returns>Instance of ISet</returns>
        public ISetByEntity<T, UpdateQuery<T>, QueryOptions> Update<TProperties>(QueryOptions queryOptions, Expression<Func<T, TProperties>> expression)
        {
            if (queryOptions == null)
            {
                throw new ArgumentNullException(nameof(queryOptions), ErrorMessages.ParameterNotNull);
            }

            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression), ErrorMessages.ParameterNotNull);
            }

            return new UpdateQueryBuilder<T>(queryOptions, this, expression);
        }

        /// <summary>
        /// Delete query
        /// </summary>
        /// <param name="queryOptions">QueryOptions</param>
        /// <returns>Bulder</returns>
        public static IQueryBuilderWithWhere<T, DeleteQuery<T>, QueryOptions> Delete(QueryOptions queryOptions)
        {
            if (queryOptions == null)
            {
                throw new ArgumentNullException(nameof(queryOptions), ErrorMessages.ParameterNotNull);
            }
            return new DeleteQueryBuilder<T>(queryOptions);
        }

        /// <summary>
        /// Delete query
        /// </summary>
        /// <param name="queryOptions">QueryOptions</param>
        /// <param name="entity">Entity</param>
        /// <returns>Bulder</returns>
        public static IQueryBuilder<DeleteQuery<T>, QueryOptions> Delete(QueryOptions queryOptions, T entity)
        {
            if (queryOptions == null)
            {
                throw new ArgumentNullException(nameof(queryOptions), ErrorMessages.ParameterNotNull);
            }
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), ErrorMessages.ParameterNotNull);
            }

            return new DeleteQueryBuilder<T>(entity, queryOptions);
        }
    }
}