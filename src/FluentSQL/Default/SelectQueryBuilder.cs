using FluentSQL.Models;
using System.Runtime.CompilerServices;
using FluentSQL.Extensions;
using FluentSQL.Helpers;

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
        public SelectQueryBuilder(IEnumerable<string> selectMember, IStatements statements)
            : base(statements, QueryType.Select)
        {
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            Columns = ClassOptionsFactory.GetClassOptions(typeof(T)).GetPropertyQuery(selectMember);
        }

        protected override string GenerateQuery()
        {
            string result = string.Empty;

            if (_queryType == QueryType.Select)
            {
                result = string.Format(Statements.Select, 
                    string.Join(",", Columns.Select(x => x.ColumnAttribute.GetColumnName(_tableName, Statements))), 
                    _tableName);
            }
            else if (_queryType == QueryType.SelectWhere)
            {
                result = string.Format(Statements.SelectWhere, 
                    string.Join(",", Columns.Select(x => x.ColumnAttribute.GetColumnName(_tableName, Statements))), 
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
            return new SelectQuery<T>(GenerateQuery(), Columns.Select(x => x.ColumnAttribute), _criteria, Statements);
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