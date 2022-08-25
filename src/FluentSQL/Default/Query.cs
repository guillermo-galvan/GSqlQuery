using FluentSQL.Extensions;
using FluentSQL.Helpers;
using FluentSQL.Models;
using System.Data.Common;

namespace FluentSQL.Default
{
    /// <summary>
    /// Query
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public abstract class Query<T,TResult> : IQuery<T>, IExecute<TResult> where T : class, new()
    {
        private string _text;
        private readonly IEnumerable<ColumnAttribute> _columns;
        private readonly IEnumerable<CriteriaDetail>? _criteria;
        private readonly ConnectionOptions _connectionOptions;

        /// <summary>
        /// Columns of the query
        /// </summary>
        public IEnumerable<ColumnAttribute> Columns => _columns;

        /// <summary>
        /// Query criteria
        /// </summary>
        public IEnumerable<CriteriaDetail>? Criteria => _criteria;

        /// <summary>
        /// Options to use in the query
        /// </summary>
        public ConnectionOptions ConnectionOptions => _connectionOptions;

        /// <summary>
        /// The Query
        /// </summary>
        public string Text { get => _text; set  => _text = value;}

        internal ClassOptions GetClassOptions()
        {
#pragma warning disable CS8604 // Possible null reference argument.
            ConnectionOptions.DatabaseManagment.ValidateDatabaseManagment();
#pragma warning restore CS8604 // Possible null reference argument.

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
        public Query(string text, IEnumerable<ColumnAttribute> columns, IEnumerable<CriteriaDetail>? criteria, ConnectionOptions connectionOptions)
        {
            _columns = columns ?? throw new ArgumentNullException(nameof(columns));            
            _connectionOptions = connectionOptions ?? throw new ArgumentNullException(nameof(connectionOptions));
            _text = text ?? throw new ArgumentNullException(nameof(text));
            _criteria = criteria;
        }

        public abstract TResult Exec();

        object? IExecute.Exec() => Exec();

        public abstract TResult Exec(DbConnection connection);        

        object? IExecute.Exec(DbConnection connection) => Exec(connection);
    }
}
