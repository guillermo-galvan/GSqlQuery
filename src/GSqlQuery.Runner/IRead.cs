using GSqlQuery.Extensions;
using GSqlQuery.Runner.Queries;
using System.Linq.Expressions;
using System.Reflection;

namespace GSqlQuery.Runner
{
    public interface IRead<T> : GSqlQuery.IRead<T> where T : class, new()
    {
        public static IQueryBuilderWithWhere<T, SelectQuery<T, TDbConnection>, TDbConnection>
            Select<TProperties, TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions,
            Expression<Func<T, TProperties>> expression)
        {
            connectionOptions.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(connectionOptions));
            ClassOptionsTupla<IEnumerable<MemberInfo>> options = expression.GetOptionsAndMembers();
            options.MemberInfo.ValidateMemberInfos($"Could not infer property name for expression. Please explicitly specify a property name by calling {options.ClassOptions.Type.Name}.Select(x => x.{options.ClassOptions.PropertyOptions.First().PropertyInfo.Name}) or {options.ClassOptions.Type.Name}.Select(x => new {{ {string.Join(",", options.ClassOptions.PropertyOptions.Select(x => $"x.{x.PropertyInfo.Name}"))} }})");
            return new SelectQueryBuilder<T, TDbConnection>(options.MemberInfo.Select(x => x.Name), connectionOptions);
        }

        public static IQueryBuilderWithWhere<T, SelectQuery<T, TDbConnection>, TDbConnection>
            Select<TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions)
        {
            connectionOptions.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(connectionOptions));
            return new SelectQueryBuilder<T, TDbConnection>(ClassOptionsFactory.GetClassOptions(typeof(T)).PropertyOptions.Select(x => x.PropertyInfo.Name), connectionOptions);
        }
    }
}
