using FluentSQL.DatabaseManagement.Default;
using FluentSQL.DatabaseManagement.Models;
using FluentSQL.Extensions;

namespace FluentSQL.DatabaseManagement
{
    public interface ICreate<T> : FluentSQL.ICreate<T> where T : class, new()
    {
        IQueryBuilder<T, InsertQuery<T, TDbConnection>, TDbConnection> Insert<TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions);

        public static IQueryBuilder<T, InsertQuery<T, TDbConnection>, TDbConnection> Insert<TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions, T entity)
        {
            connectionOptions.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(connectionOptions));
            entity.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(entity));
            return new InsertQueryBuilder<T, TDbConnection>(connectionOptions, entity);
        }
    }
}
