using FluentSQL.Extensions;
using FluentSQL.Helpers;
using FluentSQL.Models;
using System.Data.Common;

namespace FluentSQL.Default
{
    public abstract class Query : QueryBase, IExecute
    {
        public Query(string text, IEnumerable<ColumnAttribute> columns, IEnumerable<CriteriaDetail>? criteria, ConnectionOptions connectionOptions) : 
            base(text, columns, criteria, connectionOptions)
        {}

        public abstract object? Exec();

        public abstract object? Exec(DbConnection connection);
    }

    /// <summary>
    /// Query
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public abstract class Query<T, TResult> : QueryBase, IQuery<T>, IExecute<TResult> where T : class, new()
    {
        internal void ValidateDbManagment()
        {
#pragma warning disable CS8604 // Possible null reference argument.
            ConnectionOptions.DatabaseManagment.ValidateDatabaseManagment();
#pragma warning restore CS8604 // Possible null reference argument.
        }

        internal ClassOptions GetClassOptions()
        {
            ValidateDbManagment();
            return ClassOptionsFactory.GetClassOptions(typeof(T));
        }

        /// <summary>
        /// Create Query object 
        /// </summary>
        /// <param name="columns">Columns of the query</param>
        /// <param name="criteria">Query criteria</param>
        /// <param name="statements">Statements to use in the query</param>
        /// <param name="text">The Query</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Query(string text, IEnumerable<ColumnAttribute> columns, IEnumerable<CriteriaDetail>? criteria, ConnectionOptions connectionOptions):
            base(text,columns, criteria, connectionOptions)
        {
            
        }

        public abstract TResult Exec();

        object? IExecute.Exec() => Exec();

        public abstract TResult Exec(DbConnection connection);

        object? IExecute.Exec(DbConnection connection) => Exec(connection);
    }
}
