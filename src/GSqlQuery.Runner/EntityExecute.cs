using GSqlQuery.Extensions;
using GSqlQuery.Runner.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace GSqlQuery
{
    public abstract class EntityExecute<T> : Entity<T>, Runner.IRead<T>, Runner.ICreate<T>, Runner.IUpdate<T>, Runner.IDelete<T>
        where T : class, new()
    {
        public static IJoinQueryBuilder<T, SelectQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection>
           Select<TProperties, TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions, Expression<Func<T, TProperties>> expression)
        {
            connectionOptions.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(connectionOptions));
            ClassOptionsTupla<IEnumerable<MemberInfo>> options = expression.GetOptionsAndMembers();
            options.MemberInfo.ValidateMemberInfos($"Could not infer property name for expression. Please explicitly specify a property name by calling {options.ClassOptions.Type.Name}.Select(x => x.{options.ClassOptions.PropertyOptions.First().PropertyInfo.Name}) or {options.ClassOptions.Type.Name}.Select(x => new {{ {string.Join(",", options.ClassOptions.PropertyOptions.Select(x => $"x.{x.PropertyInfo.Name}"))} }})");
            return new SelectQueryBuilder<T, TDbConnection>(options.MemberInfo.Select(x => x.Name), connectionOptions);
        }

        public static IJoinQueryBuilder<T, SelectQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection>
            Select<TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions)
        {
            connectionOptions.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(connectionOptions));
            return new SelectQueryBuilder<T, TDbConnection>(ClassOptionsFactory.GetClassOptions(typeof(T)).PropertyOptions.Select(x => x.PropertyInfo.Name), connectionOptions);
        }

        public IQueryBuilder<InsertQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>> Insert<TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions)
        {
            connectionOptions.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(connectionOptions));
            return new InsertQueryBuilderExecute<T, TDbConnection>(connectionOptions, this);
        }

        public static IQueryBuilder<InsertQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>> Insert<TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions, T entity)
        {
            connectionOptions.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(connectionOptions));
            entity.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(entity));
            return new InsertQueryBuilderExecute<T, TDbConnection>(connectionOptions, entity);
        }

        public static ISet<T, UpdateQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>
            Update<TProperties, TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions,
            Expression<Func<T, TProperties>> expression, TProperties value)
        {
            connectionOptions.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(connectionOptions));
            ClassOptionsTupla<MemberInfo> options = expression.GetOptionsAndMember();
            options.MemberInfo.ValidateMemberInfo(options.ClassOptions);
            return new UpdateQueryBuilder<T, TDbConnection>(connectionOptions, new string[] { options.MemberInfo.Name }, value);
        }

        public ISet<T, UpdateQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>
            Update<TProperties, TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions, Expression<Func<T, TProperties>> expression)
        {
            connectionOptions.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(connectionOptions));
            ClassOptionsTupla<IEnumerable<MemberInfo>> options = expression.GetOptionsAndMembers();
            options.MemberInfo.ValidateMemberInfos($"Could not infer property name for expression. Please explicitly specify a property name by calling {options.ClassOptions.Type.Name}.Update(x => x.{options.ClassOptions.PropertyOptions.First().PropertyInfo.Name}) or {options.ClassOptions.Type.Name}.Update(x => new {{ {string.Join(",", options.ClassOptions.PropertyOptions.Select(x => $"x.{x.PropertyInfo.Name}"))} }})");
            return new UpdateQueryBuilder<T, TDbConnection>(connectionOptions, this, options.MemberInfo.Select(x => x.Name));
        }

        public static IQueryBuilderWithWhere<T, DeleteQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>
            Delete<TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions)
        {
            connectionOptions.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(connectionOptions));
            return new DeleteQueryBuilder<T, TDbConnection>(connectionOptions);
        }
    }
}