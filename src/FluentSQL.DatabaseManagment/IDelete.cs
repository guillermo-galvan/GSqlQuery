using FluentSQL.DatabaseManagement.Models;
using FluentSQL.Extensions;

namespace FluentSQL.DatabaseManagement
{
    public interface IDelete<T> : FluentSQL.IDelete<T> where T : class, new()
    {
        public static IQueryBuilderWithWhere<T, Default.DeleteQuery<T, TDbConnection>, TDbConnection> Delete<TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions)
        {
            connectionOptions.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(connectionOptions));
            return new Default.DeleteQueryBuilder<T, TDbConnection>(connectionOptions);

        }
    }
}
