using GSqlQuery.Extensions;
using GSqlQuery.Runner.Queries;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace GSqlQuery
{
    public abstract class EntityExecute<T> : Entity<T>
        where T : class
    {
        public static Runner.IJoinQueryBuilder<T, SelectQuery<T, TDbConnection>, TDbConnection>
           Select<TProperties, TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions, Expression<Func<T, TProperties>> expression)
        {
            if (connectionOptions == null)
            {
                throw new ArgumentNullException(nameof(connectionOptions), ErrorMessages.ParameterNotNull);
            }

            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression), ErrorMessages.ParameterNotNull);
            }

            ClassOptionsTupla<IEnumerable<MemberInfo>> options = ExpressionExtension.GetOptionsAndMembers(expression);
            ExpressionExtension.ValidateMemberInfos(QueryType.Read, options);
            return new SelectQueryBuilder<T, TDbConnection>(options, connectionOptions);
        }

        public static Runner.IJoinQueryBuilder<T, SelectQuery<T, TDbConnection>, TDbConnection> Select<TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions)
        {
            if (connectionOptions == null)
            {
                throw new ArgumentNullException(nameof(connectionOptions), ErrorMessages.ParameterNotNull);
            }

            return new SelectQueryBuilder<T, TDbConnection>(connectionOptions);
        }

        public IQueryBuilder<InsertQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>> Insert<TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions)
        {
            if (connectionOptions == null)
            {
                throw new ArgumentNullException(nameof(connectionOptions), ErrorMessages.ParameterNotNull);
            }
            return new InsertQueryBuilderExecute<T, TDbConnection>(connectionOptions, this);
        }

        public static IQueryBuilder<InsertQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>> Insert<TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions, T entity)
        {
            if (connectionOptions == null)
            {
                throw new ArgumentNullException(nameof(connectionOptions), ErrorMessages.ParameterNotNull);
            }

            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), ErrorMessages.ParameterNotNull);
            }
            return new InsertQueryBuilderExecute<T, TDbConnection>(connectionOptions, entity);
        }

        public static ISet<T, UpdateQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>> Update<TProperties, TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions, Expression<Func<T, TProperties>> expression, TProperties value)
        {
            if (connectionOptions == null)
            {
                throw new ArgumentNullException(nameof(connectionOptions), ErrorMessages.ParameterNotNull);
            }
            ClassOptionsTupla<MemberInfo> options = ExpressionExtension.GetOptionsAndMember(expression);
            return new UpdateQueryBuilder<T, TDbConnection>(connectionOptions, options, value);
        }

        public ISet<T, UpdateQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>> Update<TProperties, TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions, Expression<Func<T, TProperties>> expression)
        {
            if (connectionOptions == null)
            {
                throw new ArgumentNullException(nameof(connectionOptions), ErrorMessages.ParameterNotNull);
            }

            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression), ErrorMessages.ParameterNotNull);
            }

            ClassOptionsTupla<IEnumerable<MemberInfo>> options = ExpressionExtension.GetOptionsAndMembers(expression);
            ExpressionExtension.ValidateMemberInfos(QueryType.Update, options);
            return new UpdateQueryBuilder<T, TDbConnection>(connectionOptions, this, options);
        }

        public static IQueryBuilderWithWhere<T, DeleteQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>> Delete<TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions)
        {
            if (connectionOptions == null)
            {
                throw new ArgumentNullException(nameof(connectionOptions), ErrorMessages.ParameterNotNull);
            }
            return new DeleteQueryBuilder<T, TDbConnection>(connectionOptions);
        }

        public static IQueryBuilderWithWhere<T, DeleteQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>> Delete<TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions, T entity)
        {
            if (connectionOptions == null)
            {
                throw new ArgumentNullException(nameof(connectionOptions), ErrorMessages.ParameterNotNull);
            }
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), ErrorMessages.ParameterNotNull);
            }

            return new DeleteQueryBuilder<T, TDbConnection>(entity, connectionOptions);
        }
    }
}