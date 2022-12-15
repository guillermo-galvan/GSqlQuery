using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GSqlQuery.Extensions;

[assembly: InternalsVisibleTo("GSqlQuery.Test, PublicKey=0024000004800000940000000602000000240000525341310004000001000100913cebd9950f6fcb7fb913297422ef8f3cbdec249d3bbba88346b2045500eeda9546b5fd977bc95be5efb2ca6a8f15a2907dc1bab80d177d2e43b77db77befe6ce26b647e89871a9fede8174dc504ac3322cf5952141cf5fbbdf789fc074bcced5cdc939120d2f67ac483495a97d4df9d3a5fe13f76e40840ee0d70b2dda4b9c")]

namespace GSqlQuery.Queries
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
        public override SelectQuery<T> Build()
        {
            return new SelectQuery<T>(GenerateQuery(), Columns.Select(x => x.ColumnAttribute), _criteria, Statements);
        }

        /// <summary>
        /// Add where query
        /// </summary>
        /// <returns>IWhere</returns>
        public override IWhere<T, SelectQuery<T>> Where()
        {
            ChangeQueryType();
            SelectWhere<T> selectWhere = new SelectWhere<T>(this);
            _andOr = selectWhere;
            return (IWhere<T, SelectQuery<T>>)_andOr;
        }
    }
}