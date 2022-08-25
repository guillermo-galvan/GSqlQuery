﻿using FluentSQL.Helpers;
using FluentSQL.Models;
using FluentSQL.Extensions;
using System.Data.Common;

namespace FluentSQL.Default
{
    /// <summary>
    /// Select query
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public class SelectQuery<T> : Query<T, IEnumerable<T>>, IExecute<IEnumerable<T>> where T : class, new()
    {
        /// <summary>
        /// Initializes a new instance of the SelectQuery class.
        /// </summary>
        /// <param name="text">The Query</param>
        /// <param name="columns">Columns of the query</param>
        /// <param name="criteria">Query criteria</param>
        /// <param name="statements">Statements to use in the query</param>        
        /// <exception cref="ArgumentNullException"></exception>
        public SelectQuery(string text, IEnumerable<ColumnAttribute> columns, IEnumerable<CriteriaDetail>? criteria, ConnectionOptions connectionOptions) :
            base(text, columns, criteria, connectionOptions)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<T> Exec()
        {
            return ConnectionOptions.DatabaseManagment.ExecuteReader(this, GetClassOptions().PropertyOptions, this.GetParameters());
        }

        public override IEnumerable<T> Exec(DbConnection connection)
        {
            return ConnectionOptions.DatabaseManagment.ExecuteReader(connection,this, GetClassOptions().PropertyOptions, this.GetParameters());
        }
    }
}
