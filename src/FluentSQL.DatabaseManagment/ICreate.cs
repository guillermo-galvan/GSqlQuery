using FluentSQL.DatabaseManagement.Models;

namespace FluentSQL.DatabaseManagement
{
    public interface ICreate<T> : FluentSQL.ICreate<T> where T : class, new()
    {
        IQueryBuilder<T, Default.InsertQuery<T, TDbConnection>, TDbConnection> Insert<TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions);
    }
}
