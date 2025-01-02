using GSqlQuery.Cache;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GSqlQuery
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDatabaseManagement<TDbConnection>
    {
        DatabaseManagementEvents Events { get; set; }

        string ConnectionString { get; }

        TDbConnection GetConnection();

        IEnumerable<T> ExecuteReader<T>(IQuery<T> query, PropertyOptionsCollection propertyOptions)
            where T : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="propertyOptions"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IEnumerable<T> ExecuteReader<T>(TDbConnection connection, IQuery<T> query, PropertyOptionsCollection propertyOptions)
            where T : class;

        int ExecuteNonQuery(IQuery query);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="propertyOptions"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int ExecuteNonQuery(TDbConnection connection, IQuery query);

        T ExecuteScalar<T>(IQuery query);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="propertyOptions"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        T ExecuteScalar<T>(TDbConnection connection, IQuery query);

        Task<TDbConnection> GetConnectionAsync(CancellationToken cancellationToken = default);

        Task<IEnumerable<T>> ExecuteReaderAsync<T>(IQuery<T> query, PropertyOptionsCollection propertyOptions,
            CancellationToken cancellationToken = default)
            where T : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="propertyOptions"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> ExecuteReaderAsync<T>(TDbConnection connection, IQuery<T> query, PropertyOptionsCollection propertyOptions, CancellationToken cancellationToken = default)
            where T : class;

        Task<int> ExecuteNonQueryAsync(IQuery query, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="propertyOptions"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<int> ExecuteNonQueryAsync(TDbConnection connection, IQuery query, CancellationToken cancellationToken = default);

        Task<T> ExecuteScalarAsync<T>(IQuery query, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="propertyOptions"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<T> ExecuteScalarAsync<T>(TDbConnection connection, IQuery query, CancellationToken cancellationToken = default);
    }
}