using FluentSQL.Models;
using System.Runtime.CompilerServices;
using FluentSQL.Extensions;

[assembly: InternalsVisibleTo("FluentSQLTest")]

namespace FluentSQL.Default
{
    /// <summary>
    /// Select Query Builder
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    internal class SelectQueryBuilder<T> : QueryBuilderWithCriteria<T, SelectQuery<T>>, IQueryBuilderWithWhere<T, SelectQuery<T>> where T : class, new()
    {
        /// <summary>
        /// Initializes a new instance of the SelectQueryBuilder class.
        /// </summary>
        /// <param name="options">Detail of the class to transform</param>
        /// <param name="selectMember">Selected Member Set</param>
        /// <param name="statements">Statements to build the query</param>
        /// <exception cref="ArgumentNullException"></exception>
        public SelectQueryBuilder(ClassOptions options, IEnumerable<string> selectMember, ConnectionOptions connectionOptions)
            : base(options, selectMember, connectionOptions, QueryType.Select)
        {   
        }

        protected override string GenerateQuery()
        {
            string result = string.Empty;

            if (_queryType == QueryType.Select)
            {
                result = string.Format(_connectionOptions.Statements.Select, 
                    string.Join(",", _columns.Select(x => x.GetColumnName(_tableName, _connectionOptions.Statements))), 
                    _tableName);
            }
            else if (_queryType == QueryType.SelectWhere)
            {
                result = string.Format(_connectionOptions.Statements.SelectWhere, 
                    string.Join(",", _columns.Select(x => x.GetColumnName(_tableName, _connectionOptions.Statements))), 
                    _tableName, GetCriteria());
            }

            return result;
        }

        /// <summary>
        /// Build select query
        /// </summary>
        /// <returns>SelectQuery</returns>
        public virtual SelectQuery<T> Build()
        {
            return new SelectQuery<T>(GenerateQuery(), _columns, _criteria, _connectionOptions);
        }

        /// <summary>
        /// Add where query
        /// </summary>
        /// <returns>IWhere</returns>
        public virtual IWhere<T, SelectQuery<T>> Where()
        {
            ChangeQueryType();
            SelectWhere<T> selectWhere = new(this);
            _andOr = selectWhere;
            return (IWhere<T, SelectQuery<T>>)_andOr;
        }
    }
}