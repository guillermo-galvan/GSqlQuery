using GSqlQuery.Extensions;
using GSqlQuery.Runner.Queries;

namespace GSqlQuery.Runner
{
    public interface IDelete<T> : GSqlQuery.IDelete<T> where T : class, new()
    {
        public static IQueryBuilderWithWhere<T, DeleteQuery<T, TDbConnection>, TDbConnection> Delete<TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions)
        {
            connectionOptions.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(connectionOptions));
            return new DeleteQueryBuilder<T, TDbConnection>(connectionOptions);

        }
    }
}
