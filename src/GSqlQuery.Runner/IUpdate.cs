using GSqlQuery.Extensions;
using GSqlQuery.Runner.Models;
using System.Linq.Expressions;

namespace GSqlQuery.Runner
{
    public interface IUpdate<T> : GSqlQuery.IUpdate<T> where T : class, new()
    {
        ISet<T, Default.UpdateQuery<T, TDbConnection>> Update<TProperties, TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions,
            Expression<Func<T, TProperties>> expression);

        public static ISet<T, Default.UpdateQuery<T, TDbConnection>> Update<TProperties, TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions,
            Expression<Func<T, TProperties>> expression, TProperties value)
        {
            connectionOptions.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(connectionOptions));
            var (options, memberInfos) = expression.GetOptionsAndMember();
            memberInfos.ValidateMemberInfo(options);
            return new Default.UpdateQueryBuilder<T, TDbConnection>(connectionOptions, new string[] { memberInfos.Name }, value);
        }
    }
}
