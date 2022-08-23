﻿using FluentSQL.Default;
using FluentSQL.Extensions;
using FluentSQL.Helpers;
using FluentSQL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQL
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class DatabaseManagment : IDatabaseManagment
    {
        protected readonly string _connectionString;

        public DatabaseManagmentEvents? Events { get; set; }

        public string ConnectionString => _connectionString;

        public abstract string ValueAutoIncrementingQuery { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public DatabaseManagment(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
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
                FluentSQLManagement._options.Logger?.LogWarning<ClassOptions>("{0} constructor with properties {1} not found",
                                                              classOptions.Type.Name,
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
        protected virtual object? SwitchTypeValue(Type type, object value)
        {
            return (type, value) switch
            {
                (Type, object o) when o == DBNull.Value => null,
                (Type, object o) when o == null => null,
                (Type t, object) when t == typeof(byte) || t == typeof(byte?) => Convert.ToByte(value),
                (Type t, object) when t == typeof(short) || t == typeof(short?) => Convert.ToInt16(value),
                (Type t, object) when t == typeof(int) || t == typeof(int?) => Convert.ToInt32(value),
                (Type t, object) when t == typeof(long) || t == typeof(long?) => Convert.ToInt64(value),
                (Type t, object) when t == typeof(float) || t == typeof(float?) => Convert.ToSingle(value),
                (Type t, object) when t == typeof(decimal) || t == typeof(decimal?) => Convert.ToDecimal(value),
                (Type t, object) when t == typeof(double) || t == typeof(double?) => Convert.ToDouble(value),
                (Type t, object) when t == typeof(bool) || t == typeof(bool?) => Convert.ToBoolean(value),
                (Type t, object) when t == typeof(DateTime) || t == typeof(DateTime?) => Convert.ToDateTime(value),
                (Type t, object) when t == typeof(string) => value.ToString(),
                (Type t, object) when t == typeof(char) || t == typeof(char?) => Convert.ToChar(value),
                (Type t, object) when t == typeof(Guid) || t == typeof(Guid?) => Guid.Parse(value.ToString()),
                (Type t, object) when t == typeof(uint) || t == typeof(uint?) => Convert.ToUInt32(value),
                (Type t, object) when t == typeof(ushort) || t == typeof(ushort?) => Convert.ToUInt16(value),
                (Type t, object) when t == typeof(ulong) || t == typeof(ulong?) => Convert.ToUInt64(value),
                (Type t, object) when t == typeof(sbyte) || t == typeof(sbyte?) => Convert.ToSByte(value),
                (Type t, object) when t == typeof(TimeSpan) || t == typeof(TimeSpan?) => TimeSpan.Parse(value.ToString()),
                _ => null,
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="propertyOptions"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public abstract IEnumerable<T> ExecuteReader<T>(IQuery<T> query, IEnumerable<PropertyOptions> propertyOptions, 
            IEnumerable<IDataParameter> parameters) where T : class, new();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="propertyOptions"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public abstract int ExecuteNonQuery<T>(IQuery<T> query, IEnumerable<PropertyOptions> propertyOptions,
            IEnumerable<IDataParameter> parameters) where T : class, new();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="propertyOptions"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public abstract object ExecuteScalar<T>(IQuery<T> query, IEnumerable<PropertyOptions> propertyOptions, 
            IEnumerable<IDataParameter> parameters, Type result) where T : class, new();
    }
}
