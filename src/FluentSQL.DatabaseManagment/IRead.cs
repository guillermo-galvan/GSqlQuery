using FluentSQL.DatabaseManagement.Models;
using FluentSQL.Extensions;
using FluentSQL.Helpers;
using System.Linq.Expressions;

namespace FluentSQL.DatabaseManagement
{
    public interface IRead<T> : FluentSQL.IRead<T> where T : class, new()
    {
        public static IQueryBuilderWithWhere<T, Default.SelectQuery<T, TDbConnection>, TDbConnection>
            Select<TProperties, TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions,
            Expression<Func<T, TProperties>> expression)
        {
            connectionOptions.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(connectionOptions));
            var (options, memberInfos) = expression.GetOptionsAndMembers();
            memberInfos.ValidateMemberInfos($"Could not infer property name for expression. Please explicitly specify a property name by calling {options.Type.Name}.Select(x => x.{options.PropertyOptions.First().PropertyInfo.Name}) or {options.Type.Name}.Select(x => new {{ {string.Join(",", options.PropertyOptions.Select(x => $"x.{x.PropertyInfo.Name}"))} }})");
            return new Default.SelectQueryBuilder<T, TDbConnection>(memberInfos.Select(x => x.Name), connectionOptions);
        }

        public static IQueryBuilderWithWhere<T, Default.SelectQuery<T, TDbConnection>, TDbConnection>
            Select<TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions)
        {
            connectionOptions.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(connectionOptions));
            return new Default.SelectQueryBuilder<T, TDbConnection>(ClassOptionsFactory.GetClassOptions(typeof(T)).PropertyOptions.Select(x => x.PropertyInfo.Name), connectionOptions);
        }
    }
}
