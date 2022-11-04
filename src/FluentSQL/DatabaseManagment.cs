using FluentSQL.Default;
using FluentSQL.Extensions;
using FluentSQL.Helpers;
using FluentSQL.Models;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.Data;
using System.Data.Common;

namespace FluentSQL
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class DatabaseManagment<TDbConnection> : IDatabaseManagement<TDbConnection> where TDbConnection : DbConnection
    {
        protected readonly string _connectionString;
        protected ILogger? _logger;

        public DatabaseManagmentEvents Events { get; set; }

        public string ConnectionString => _connectionString;

        public DatabaseManagment(string connectionString) : this(connectionString, null, null)
        {

        }

        public DatabaseManagment(string connectionString, DatabaseManagmentEvents? events)
                : this(connectionString, events, null)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public DatabaseManagment(string connectionString, DatabaseManagmentEvents events, ILogger? logger)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            Events = events ?? throw new ArgumentNullException(nameof(events));
            _logger = logger;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected virtual ITransformTo<T> GetTransformTo<T>() where T : class, new()
        {
            var classOptions = ClassOptionsFactory.GetClassOptions(typeof(T));

            if (classOptions.ConstructorInfo.GetParameters().Length == 0)
            {
                _logger?.LogWarning("{0} constructor with properties {1} not found", classOptions.Type.Name,
                    string.Join(", ", classOptions.PropertyOptions.Select(x => $"{x.PropertyInfo.Name}")));
                return new TransformToByField<T>(classOptions.PropertyOptions.Count());
            }
            else
            {
                return new TransformToByConstructor<T>(classOptions.PropertyOptions.Count());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected virtual object? SwitchTypeValue(Type type, object? value)
        {
            try
            {
                if (value == DBNull.Value || value == null)
                {
                    return null;
                }
                else 
                {
                    var newType = Nullable.GetUnderlyingType(type);
                    return newType == null ? Convert.ChangeType(value, type) : Convert.ChangeType(value, newType);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public abstract TDbConnection GetConnection();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="propertyOptions"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public abstract IEnumerable<T> ExecuteReader<T>(IQuery query, IEnumerable<PropertyOptions> propertyOptions, 
            IEnumerable<IDataParameter> parameters) where T : class, new();

        public abstract IEnumerable<T> ExecuteReader<T>(TDbConnection connection, IQuery query, IEnumerable<PropertyOptions> propertyOptions,
           IEnumerable<IDataParameter> parameters) where T : class, new();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="propertyOptions"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public abstract int ExecuteNonQuery(IQuery query, IEnumerable<IDataParameter> parameters);

        public abstract int ExecuteNonQuery(TDbConnection connection, IQuery query, IEnumerable<IDataParameter> parameters);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="propertyOptions"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public abstract T ExecuteScalar<T>(IQuery query, IEnumerable<IDataParameter> parameters);

        public abstract T ExecuteScalar<T>(TDbConnection connection, IQuery query, IEnumerable<IDataParameter> parameters);

        public abstract Task<IEnumerable<T>> ExecuteReaderAsync<T>(IQuery query, IEnumerable<PropertyOptions> propertyOptions, IEnumerable<IDataParameter> parameters,
            CancellationToken cancellationToken = default) where T : class, new();

        public abstract Task<IEnumerable<T>> ExecuteReaderAsync<T>(TDbConnection connection, IQuery query, IEnumerable<PropertyOptions> propertyOptions, 
            IEnumerable<IDataParameter> parameters, CancellationToken cancellationToken = default) where T : class, new();

        public abstract Task<int> ExecuteNonQueryAsync(IQuery query, IEnumerable<IDataParameter> parameters, CancellationToken cancellationToken = default);

        public abstract Task<int> ExecuteNonQueryAsync(TDbConnection connection, IQuery query, IEnumerable<IDataParameter> parameters, CancellationToken cancellationToken = default);

        public abstract Task<T> ExecuteScalarAsync<T>(IQuery query, IEnumerable<IDataParameter> parameters, CancellationToken cancellationToken = default);

        public abstract Task<T> ExecuteScalarAsync<T>(TDbConnection connection, IQuery query, IEnumerable<IDataParameter> parameters, CancellationToken cancellationToken = default);
    }
}
