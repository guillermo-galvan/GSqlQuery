using GSqlQuery.Extensions;
using GSqlQuery.Runner.Models;

namespace GSqlQuery.Runner
{
    public interface IDelete<T> : GSqlQuery.IDelete<T> where T : class, new()
    {
        public static IQueryBuilderWithWhere<T, Default.DeleteQuery<T, TDbConnection>, TDbConnection> Delete<TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions)
        {
            connectionOptions.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(connectionOptions));
            return new Default.DeleteQueryBuilder<T, TDbConnection>(connectionOptions);

        }
    }
}
