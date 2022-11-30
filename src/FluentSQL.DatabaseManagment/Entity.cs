using FluentSQL.DatabaseManagement.Default;
using FluentSQL.DatabaseManagement.Models;
using FluentSQL.Extensions;
using System.Linq.Expressions;

namespace FluentSQL.DatabaseManagement
{
    public abstract class Entity<T> : FluentSQL.Entity<T>, ICreate<T>, IRead<T>, IUpdate<T>, IDelete<T>
        where T : class, new()
    {
        public static IQueryBuilderWithWhere<T, SelectQuery<T, TDbConnection>, TDbConnection>
           Select<TProperties, TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions, Expression<Func<T, TProperties>> expression)
        {
            return IRead<T>.Select(connectionOptions, expression);
        }

        public static IQueryBuilderWithWhere<T, SelectQuery<T, TDbConnection>, TDbConnection>
            Select<TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions)
        {
            return IRead<T>.Select(connectionOptions);
        }

        public IQueryBuilder<T, InsertQuery<T, TDbConnection>, TDbConnection> Insert<TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions)
        {
            connectionOptions.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(connectionOptions));
            return new InsertQueryBuilder<T, TDbConnection>(connectionOptions, this);
        }

        public static ISet<T, UpdateQuery<T, TDbConnection>>
            Update<TProperties, TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions,
            Expression<Func<T, TProperties>> expression, TProperties value)
        {
            return IUpdate<T>.Update(connectionOptions, expression, value)!;
        }

        public ISet<T, UpdateQuery<T, TDbConnection>> Update<TProperties, TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions, Expression<Func<T, TProperties>> expression)
        {
            connectionOptions.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(connectionOptions));
            var (options, memberInfos) = expression.GetOptionsAndMembers();
            memberInfos.ValidateMemberInfos($"Could not infer property name for expression. Please explicitly specify a property name by calling {options.Type.Name}.Update(x => x.{options.PropertyOptions.First().PropertyInfo.Name}) or {options.Type.Name}.Update(x => new {{ {string.Join(",", options.PropertyOptions.Select(x => $"x.{x.PropertyInfo.Name}"))} }})");
            return new UpdateQueryBuilder<T, TDbConnection>(connectionOptions, this, memberInfos.Select(x => x.Name));
        }

        public static IQueryBuilderWithWhere<T, DeleteQuery<T, TDbConnection>, TDbConnection> Delete<TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions)
        {
            return IDelete<T>.Delete(connectionOptions)!;

        }
    }
}
