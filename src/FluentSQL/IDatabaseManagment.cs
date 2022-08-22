using FluentSQL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQL
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDatabaseManagment
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="propertyOptions"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IEnumerable<T> ExecuteReader<T>(IQuery<T> query, IEnumerable<PropertyOptions> propertyOptions, IEnumerable<IDataParameter> parameters = null) where T : class, new();

        /// <summary>
        /// 
        /// </summary>
        DatabaseManagmentEvents? Events { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ConnectionString { get; }

    }
}
