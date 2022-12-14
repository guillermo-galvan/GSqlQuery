using GSqlQuery.Extensions;
using GSqlQuery.Runner.Default;
using GSqlQuery.Runner.Models;

namespace GSqlQuery.Runner
{
    public interface ICreate<T> : GSqlQuery.ICreate<T> where T : class, new()
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
