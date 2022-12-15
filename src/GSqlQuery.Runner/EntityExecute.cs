using GSqlQuery.Extensions;
using GSqlQuery.Runner;
using GSqlQuery.Runner.Queries;
using System.Linq.Expressions;
using System.Reflection;

namespace GSqlQuery
{
    public abstract class EntityExecute<T> : Entity<T>, ICreate<T>, IRead<T>, IUpdate<T>, IDelete<T>
        where T : class, new()
    {
        public static IQueryBuilderWithWhere<T, SelectQuery<T, TDbConnection>, TDbConnection>
           Select<TProperties, TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions, Expression<Func<T, TProperties>> expression)
        {
            return Runner.IRead<T>.Select(connectionOptions, expression);
        }

        public static IQueryBuilderWithWhere<T, SelectQuery<T, TDbConnection>, TDbConnection>
            Select<TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions)
        {
            return Runner.IRead<T>.Select(connectionOptions);
        }

        public IQueryBuilder<T, InsertQuery<T, TDbConnection>, TDbConnection> Insert<TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions)
        {
            connectionOptions.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(connectionOptions));
            return new InsertQueryBuilder<T, TDbConnection>(connectionOptions, this);
        }

        public static IQueryBuilder<T, InsertQuery<T, TDbConnection>, TDbConnection> Insert<TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions, T entity)
        {
            return Runner.ICreate<T>.Insert(connectionOptions, entity);
        }

        public static ISet<T, UpdateQuery<T, TDbConnection>>
            Update<TProperties, TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions,
            Expression<Func<T, TProperties>> expression, TProperties value)
        {
            return Runner.IUpdate<T>.Update(connectionOptions, expression, value)!;
        }

        public ISet<T, UpdateQuery<T, TDbConnection>> Update<TProperties, TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions, Expression<Func<T, TProperties>> expression)
        {
            connectionOptions.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(connectionOptions));
            ClassOptionsTupla<IEnumerable<MemberInfo>> options = expression.GetOptionsAndMembers();
            options.MemberInfo.ValidateMemberInfos($"Could not infer property name for expression. Please explicitly specify a property name by calling {options.ClassOptions.Type.Name}.Update(x => x.{options.ClassOptions.PropertyOptions.First().PropertyInfo.Name}) or {options.ClassOptions.Type.Name}.Update(x => new {{ {string.Join(",", options.ClassOptions.PropertyOptions.Select(x => $"x.{x.PropertyInfo.Name}"))} }})");
            return new UpdateQueryBuilder<T, TDbConnection>(connectionOptions, this, options.MemberInfo.Select(x => x.Name));
        }

        public static IQueryBuilderWithWhere<T, DeleteQuery<T, TDbConnection>, TDbConnection> Delete<TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions)
        {
            return Runner.IDelete<T>.Delete(connectionOptions)!;

        }
    }
}
