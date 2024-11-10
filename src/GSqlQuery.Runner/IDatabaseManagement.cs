using System.Collections.Generic;
using System.Data;
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

        IEnumerable<T> ExecuteReader<T>(IQuery<T> query, IEnumerable<PropertyOptions> propertyOptions, IEnumerable<IDataParameter> parameters)
            where T : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="propertyOptions"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IEnumerable<T> ExecuteReader<T>(TDbConnection connection, IQuery<T> query, IEnumerable<PropertyOptions> propertyOptions, IEnumerable<IDataParameter> parameters)
            where T : class;

        int ExecuteNonQuery(IQuery query, IEnumerable<IDataParameter> parameters);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="propertyOptions"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int ExecuteNonQuery(TDbConnection connection, IQuery query, IEnumerable<IDataParameter> parameters);

        T ExecuteScalar<T>(IQuery query, IEnumerable<IDataParameter> parameters);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="propertyOptions"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        T ExecuteScalar<T>(TDbConnection connection, IQuery query, IEnumerable<IDataParameter> parameters);

        Task<TDbConnection> GetConnectionAsync(CancellationToken cancellationToken = default);

        Task<IEnumerable<T>> ExecuteReaderAsync<T>(IQuery<T> query, IEnumerable<PropertyOptions> propertyOptions, IEnumerable<IDataParameter> parameters,
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
        Task<IEnumerable<T>> ExecuteReaderAsync<T>(TDbConnection connection, IQuery<T> query, IEnumerable<PropertyOptions> propertyOptions,
            IEnumerable<IDataParameter> parameters, CancellationToken cancellationToken = default)
            where T : class;

        Task<int> ExecuteNonQueryAsync(IQuery query, IEnumerable<IDataParameter> parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="propertyOptions"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<int> ExecuteNonQueryAsync(TDbConnection connection, IQuery query, IEnumerable<IDataParameter> parameters, CancellationToken cancellationToken = default);

        Task<T> ExecuteScalarAsync<T>(IQuery query, IEnumerable<IDataParameter> parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="propertyOptions"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<T> ExecuteScalarAsync<T>(TDbConnection connection, IQuery query, IEnumerable<IDataParameter> parameters, CancellationToken cancellationToken = default);
    }
}