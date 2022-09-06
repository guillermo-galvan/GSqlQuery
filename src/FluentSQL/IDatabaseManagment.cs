using FluentSQL.Models;
using System.Data;

namespace FluentSQL
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDatabaseManagement<TDbConnection> 
    {
        DatabaseManagmentEvents Events { get; set; }

        string ConnectionString { get; }

        new TDbConnection GetConnection();

        new IEnumerable<T> ExecuteReader<T>(IQuery<T> query, IEnumerable<PropertyOptions> propertyOptions, IEnumerable<IDataParameter> parameters) where T : class, new();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="propertyOptions"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IEnumerable<T> ExecuteReader<T>(TDbConnection connection,IQuery<T> query, IEnumerable<PropertyOptions> propertyOptions, IEnumerable<IDataParameter> parameters) where T : class, new();

        new int ExecuteNonQuery(IQuery query, IEnumerable<IDataParameter> parameters);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="propertyOptions"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int ExecuteNonQuery(TDbConnection connection, IQuery query, IEnumerable<IDataParameter> parameters);

        new object ExecuteScalar(IQuery query, IEnumerable<IDataParameter> parameters, Type resultType);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="propertyOptions"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        object ExecuteScalar(TDbConnection connection,IQuery query, IEnumerable<IDataParameter> parameters, Type resultType);
    }
}
